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
    public BsonValue CurrentUserID;
    public UserDetails CurrentUserDetails;
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
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> users = db.GetCollection<BsonDocument>("users");

        CurrentUserDetails = details;

        //convert details to bson document in order to retrieve its appointed id
        var currentUserDocument = details.ToBsonDocument();
        CurrentUserID = currentUserDocument["_id"];

        //insert a user
        users.Insert(currentUserDocument);
        Debug.Log("New users id is: " + CurrentUserID);
    }
    
    public void InsertTrialData(List<TrackableEventObject> eventData, List<VisualTrackerObjectDetails> visualData)
    {
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");
        MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");

        var trialEventDocument = DocumentGeneratorForCurrentTrial.GenerateDocument(CurrentUserID, eventData);
        var trialName = trialEventDocument["TrialName"].ToString();
        trialCollection.Insert(trialEventDocument);
        var trialVisualDocument = visualTrackerDocumentGenerator.GenerateDocument(trialName, CurrentUserID, visualData);
        visualDataCollection.Insert(trialVisualDocument);

        //add these document to current user details so they can be queried later
        currentUserTrialEventDetails.Add(trialEventDocument);
        currentUserVisualDetails.Add(trialVisualDocument);

        Debug.Log("Inserted Data into database for trial '" + trialName + "'");
    }

    public void QuerySplitDecisionTrialData(int decisionNumber)
    {
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        //MongoCollection<BsonDocument> usersCollection = db.GetCollection<BsonDocument>("users");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");
        //MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");



        /*var genderComparisonString = "Gender: '" + CurrentUserDetails.Gender + "'";
        var ageComparisonString = "AgeRange: '" + CurrentUserDetails.AgeRange + "'";
        var nationalityComparisonString = "Nationality: '" + CurrentUserDetails.Nationality + "'";

        var sameGenderFilter = "{ " + genderComparisonString + " }";
        var sameAgeFilter = "{ " + ageComparisonString + " }";
        var sameNationalityFilter = "{ " + nationalityComparisonString +" }";
        var sameGenderNationalityFilter = 
        */

        var details = new DecisionDetails();
        Debug.Log("Querying DB");
        details.TotalChoices = trialCollection.Find(new QueryDocument("TrialName", "SplitDecisionTrial")).Count<BsonDocument>();
        details.RightChoiceCount = trialCollection.Find(new QueryDocument {
            { "TrialName", "SplitDecisionTrial" },
            { "Decisions.Decision0.Decision Choice.Direction", "Right" }
        }).Count<BsonDocument>();
        details.LeftChoiceCount = details.TotalChoices - details.RightChoiceCount;
        
        GetMostAndLeastChosen<Lighting>(trialCollection, decisionNumber, "Lighting", 
                              out details.MostPickedLighting, out details.LeastPickedLighting,
                              out details.CountOfMostPickedLighting, out details.CountOfLeastPickedLighting);

        GetMostAndLeastChosen<Colour>(trialCollection, decisionNumber, "Color",
                              out details.MostPickedColour, out details.LeastPickedColour,
                              out details.CountOfMostPickedColour, out details.CountOfLeastPickedColour);
    }

    void GetMostAndLeastChosen<TEnum>(MongoCollection<BsonDocument> trialCollection, int decisionNumber, string trait, 
                                  out TEnum mostChosenType, out TEnum leastChosenType, out int mostCount, out int leastCount)
    {
        mostCount = 0;
        leastCount = 0;
        TEnum[] types = (TEnum[])System.Enum.GetValues(typeof(TEnum));
        mostChosenType = types.First();
        leastChosenType = types.First();

        foreach (var type in types)
        {
            var decisionCount = trialCollection.Find(new QueryDocument {
                { "TrialName", "SplitDecisionTrial" },
                { "Decisions.Decision" + decisionNumber + ".Decision Choice." + trait, type.ToString() }
            }).Count<BsonDocument>();

            if (decisionCount >= mostCount)
            {
                mostCount = decisionCount;
                mostChosenType = type;
            }

            var ignoredCount = trialCollection.Find(new QueryDocument {
                { "TrialName", "SplitDecisionTrial" },
                { "Decisions.Decision" + decisionNumber + ".Ignored Choice." + trait, type.ToString() }
            }).Count<BsonDocument>();

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
