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

        //convert details to bson document in order to retrieve its appointed id
        var detailsDocument = details.ToBsonDocument();

        //insert a user
        users.Insert(detailsDocument);
        CurrentUserID = detailsDocument["_id"];
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

    public void QuerySplitDecisionTrialData()
    {
        /*MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> usersCollection = db.GetCollection<BsonDocument>("users");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");
        MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");
        */


    }
}
