using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using Assets.Scripts.Data;
using Assets.Scripts.Trackers;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    public BsonValue CurrentUserID { get { return CurrentUserDocument["_id"]; } }
    public BsonDocument CurrentUserDocument { get { return currentUserDocumentValue.ToBsonDocument(); } }
    private BsonValue currentUserDocumentValue;
    public ITrialEventDocumentGenerator DocumentGeneratorForCurrentTrial;
    VisualTrackerDocumentGenerator visualTrackerDocumentGenerator = new VisualTrackerDocumentGenerator();
    List<BsonDocument> currentUserTrialEventDetails = new List<BsonDocument>();
    List<BsonDocument> currentUserVisualDetails = new List<BsonDocument>();
    
    void Awake()
    {
        //dont destroy as the user id needs to persist
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    public void InsertNewUserToDatabase(UserDetails details)
    {
        //establish connection to mongodb database and retrieve the users collection
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> users = db.GetCollection<BsonDocument>("users");

        //convert details to bson document in order to retrieve its appointed id
        var currentUserDocument = details.ToBsonDocument();

        //insert a user
        users.Insert(currentUserDocument);

        //store the details of the inserted document
        currentUserDocumentValue = currentUserDocument.DeepClone();
        Debug.Log("doc: " + CurrentUserDocument);
    }
    
    public void InsertTrialData(List<TrackableEventObject> eventData, List<VisualTrackerObjectDetails> visualData)
    {
        //establish connection to mongodb database and retrieve the trials and visual data collections
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");
        MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");
        
        //generate the document for the trial with the current trial document generator
        var trialEventDocument = DocumentGeneratorForCurrentTrial.GenerateDocument(CurrentUserID, eventData);
        //get the trial name for the visual data document generator
        var trialName = trialEventDocument["TrialName"].ToString();
        //insert trial data into the database
        trialCollection.Insert(trialEventDocument);

        //generate the document for the visual data
        var trialVisualDocument = visualTrackerDocumentGenerator.GenerateDocument(trialName, CurrentUserID, visualData);
        //inser the visual data into the database
        visualDataCollection.Insert(trialVisualDocument);

        //add these documents to current user details so they can be queried later
        currentUserTrialEventDetails.Add(trialEventDocument);
        currentUserVisualDetails.Add(trialVisualDocument);

        Debug.Log("Inserted Data into database for trial '" + trialName + "'");
    }

    public SplitDecisionTrialQueryResults QuerySplitDecisionTrialData(int decisionNumber)
    {
        //establish connection to mongodb database and retrieve the user and trial collections
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> usersCollection = db.GetCollection<BsonDocument>("users");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");

        //get a cursor for all the users and sub group that have similar details to the current player
        var allUsers = usersCollection.FindAll();
        var sameGenderUsers = usersCollection.Find(new QueryDocument("Gender", CurrentUserDocument["Gender"]));
        var sameAgeRangeUsers = usersCollection.Find(new QueryDocument("AgeRange", CurrentUserDocument["AgeRange"]));
        var sameNationalityUsers = usersCollection.Find(new QueryDocument("Nationality", CurrentUserDocument["Nationality"]));
        
        //generate the query results for each of these user groups
        var queryResults = new SplitDecisionTrialQueryResults
        {
            AllUsers = GetDecisionDetailsForUserGroup(trialCollection, allUsers, decisionNumber),
            SameGenderUsers = GetDecisionDetailsForUserGroup(trialCollection, sameGenderUsers, decisionNumber),
            SameAgeGroupUsers = GetDecisionDetailsForUserGroup(trialCollection, sameAgeRangeUsers, decisionNumber),
            SameNationalityUsers = GetDecisionDetailsForUserGroup(trialCollection, sameNationalityUsers, decisionNumber)
        };

        return queryResults;
    }

    //go through each user in the user group and find the traits most commonly picked
    DecisionDetails GetDecisionDetailsForUserGroup(MongoCollection<BsonDocument> trialCollection, MongoCursor<BsonDocument> users, int decisionNumber)
    {
        var details = new DecisionDetails();
        details.TotalChoices = 0;
        details.RightChoiceCount = 0;
        
        //find total number of choices for the user base for this trial and how many chose right or left
        foreach(var user in users)
        {
            details.TotalChoices += trialCollection.Find(new QueryDocument {
                { "TrialName", "SplitDecisionTrial" },
                { "UserID", user["_id"] }
            }).Count<BsonDocument>();
            details.RightChoiceCount += trialCollection.Find(new QueryDocument {
            { "TrialName", "SplitDecisionTrial" },
            { "UserID", user["_id"] },
            { "Decisions.Decision0.Decision Choice.Direction", "Right" }
            }).Count<BsonDocument>();
        }
        details.LeftChoiceCount = details.TotalChoices - details.RightChoiceCount;

        //get the most common lighting chosen with the number of times it was picked
        GetMostAndLeastChosenForUsers<Lighting>(trialCollection, users, decisionNumber, "Lighting",
                              out details.MostPickedLighting, out details.LeastPickedLighting,
                              out details.CountOfMostPickedLighting, out details.CountOfLeastPickedLighting);

        //get the most common colour chosen with the number of times it was picked
        GetMostAndLeastChosenForUsers<Colour>(trialCollection, users, decisionNumber, "Color",
                              out details.MostPickedColour, out details.LeastPickedColour,
                              out details.CountOfMostPickedColour, out details.CountOfLeastPickedColour);

        return details;
    }

    void GetMostAndLeastChosenForUsers<TEnum>(MongoCollection<BsonDocument> trialCollection, MongoCursor<BsonDocument> users, int decisionNumber, string trait,
                                  out TEnum mostChosenType, out TEnum leastChosenType, out int mostCount, out int leastCount)
    {
        mostCount = 0;
        leastCount = 0;
        TEnum[] types = (TEnum[])System.Enum.GetValues(typeof(TEnum));
        mostChosenType = types.First();
        leastChosenType = types.First();
        
        //loop through each type of the enum and find the most and least chosen type
        foreach (var type in types)
        {
            var decisionCount = 0;
            var ignoredCount = 0;
            foreach (var user in users)
            {
                decisionCount += trialCollection.Find(new QueryDocument {
                    { "TrialName", "SplitDecisionTrial" },
                    { "UserID", user["_id"] },
                    { "Decisions.Decision" + decisionNumber + ".Decision Choice." + trait, type.ToString() }
                }).Count<BsonDocument>();

                ignoredCount += trialCollection.Find(new QueryDocument {
                    { "TrialName", "SplitDecisionTrial" },
                    { "UserID", user["_id"] },
                    { "Decisions.Decision" + decisionNumber + ".Ignored Choice." + trait, type.ToString() }
                }).Count<BsonDocument>();
            }
            if (decisionCount >= mostCount)
            {
                mostCount = decisionCount;
                mostChosenType = type;
            }
            if (ignoredCount >= leastCount)
            {
                leastCount = ignoredCount;
                leastChosenType = type;
            }
        }
        Debug.Log("Most chosen " + trait + ": " + mostChosenType.ToString() + ", chosen times: " + mostCount);
        Debug.Log("Most ignored " + trait + ": " + leastChosenType.ToString() + ", ignored times: " + leastCount);
    }
}

public class SplitDecisionTrialQueryResults
{
    public DecisionDetails AllUsers { get; set; }
    public DecisionDetails SameGenderUsers { get; set; }
    public DecisionDetails SameAgeGroupUsers { get; set; }
    public DecisionDetails SameNationalityUsers { get; set; }
    public DecisionDetails SameFirstChoiceUsers { get; set; }
}

public class DecisionDetails
{
    public int TotalChoices;
    public int RightChoiceCount;
    public int LeftChoiceCount;
    public Lighting MostPickedLighting;
    public int CountOfMostPickedLighting;
    public Lighting LeastPickedLighting;
    public int CountOfLeastPickedLighting;
    public Colour MostPickedColour;
    public int CountOfMostPickedColour;
    public Colour LeastPickedColour;
    public int CountOfLeastPickedColour;
}
