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
    List<Color> UsableColours = new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };

    void Awake()
    {
        //dont destroy as the user id needs to always be stored
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
    /*
    public void UpdateData(ObjectTraits traits)
    {
        Debug.Log("Player Data updated color: " + traits.Colour + ". ");
        //temp insert of data to db. todo: logic to know when sufficient data is recieved + trial has ended
        InsertUserTrialData(traits);
    }
    
    public void InsertUserTrialData(ObjectTraits traitsToInsert)
    {
        var document = DocumentGeneratorForCurrentTrial.GenerateDocument(traitsToInsert, CurrentUserID);

        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");

        trialCollection.Insert(document);
    }

    public void InsertUserTrialVisualData(string trial, Dictionary<TrackableEventObject, float> objectsLookedAt)
    {
        var documentGenerator = new VisualTrackerDocumentGenerator();
        var document = documentGenerator.GenerateDocument(trial, objectsLookedAt);
        
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");

        visualDataCollection.Insert(document);
        Debug.Log("Insert Visual Data to DB");
    }
    */
    public void InsertTrialData(List<TrackableEventObject> eventData, Dictionary<TrackableEventObject, float> visualData)
    {
        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> trialCollection = db.GetCollection<BsonDocument>("Trials");
        MongoCollection<BsonDocument> visualDataCollection = db.GetCollection<BsonDocument>("VisualData");

        var trialEventDocument = DocumentGeneratorForCurrentTrial.GenerateDocument(CurrentUserID, eventData);
        var trialName = trialEventDocument["TrialName"].ToString();
        trialCollection.Insert(trialEventDocument);
        visualDataCollection.Insert(visualTrackerDocumentGenerator.GenerateDocument(trialName, CurrentUserID, visualData));

        Debug.Log("Inserted Data into database for trial '" + trialName + "'");
    }
}
