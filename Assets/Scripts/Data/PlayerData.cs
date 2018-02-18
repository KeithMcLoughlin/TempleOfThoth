using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using Assets.Scripts.Data;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    public BsonValue CurrentUserID;
    List<Color> UsableColours = new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };

    void Awake()
    {
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

    public void UpdateData(ObjectTraits traits)
    {
        Debug.Log("Player Data updated color: " + traits.Colour + ". ");
    }
}
