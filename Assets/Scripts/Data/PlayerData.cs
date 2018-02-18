using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    List<Color> UsableColours = new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };

    void Awake()
    {
        Instance = this;
        
    }

    public void UpdateData(ObjectTraits traits)
    {
        Debug.Log("Player Data updated color: " + traits.Colour + ". ");

        MongoClient client = new MongoClient("mongodb://kmcl:12081995@ds119988.mlab.com:19988/templeofthothstats");
        MongoServer server = client.GetServer();
        MongoDatabase db = server.GetDatabase("templeofthothstats");
        MongoCollection<BsonDocument> users = db.GetCollection<BsonDocument>("users");
        //Debug.Log(db.CollectionExists("users").ToString());

        /* var user1 = new User
         {
             Name = "Conor Mc Loughlin",
             Age = 23
         };*/

        //insert a user
        //users.Insert(user1);

        //print all users
        MongoCursor<BsonDocument> dbusers = users.FindAll();
        foreach (var i in dbusers)
        {
            Debug.Log(i);
        }
    }
}
