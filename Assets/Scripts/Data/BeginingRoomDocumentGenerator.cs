using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Assets.Scripts.Data
{
    public class BeginingRoomDocumentGenerator : ITrialDocumentGenerator
    {
        public BsonDocument GenerateDocument(ObjectTraits traits, BsonValue userId)
        {
            var document = new BsonDocument
            {
                {"TrialName", "BeginningRoom" },
                {"UserID", userId },
                {"DoorChoice", new BsonDocument {
                    {"Color", traits.Colour.ToString() },
                    {"Direction", traits.Direction.ToString() },
                    {"Lighting", traits.Lighting.ToString() }
                }}
            };

            return document;
        }
    }
}
