using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Assets.Scripts.Data
{
    public class BeginningRoomDetails
    {
        public string TrialName { get; set; }
        public BsonValue UserId { get; set; }
        public BsonDocument DoorChoice { get; set; }

        public BeginningRoomDetails(BsonValue userId, ObjectTraits doorTraits)
        {
            UserId = userId;
            TrialName = "Beginning Room";
            DoorChoice = new BsonDocument
            {
                {"Color", doorTraits.Colour.ToString() },
                {"Direction", doorTraits.Direction.ToString() },
                {"Lighting", doorTraits.Lighting.ToString() }
            };
        }
    }
}
