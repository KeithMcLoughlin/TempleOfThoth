using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Assets.Scripts.Trackers;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class BeginingRoomDocumentGenerator : ITrialEventDocumentGenerator
    {
        public BsonDocument GenerateDocument(BsonValue userId, List<TrackableEventObject> eventData)
        {
            //if the data passed in isn't only one object we know its wrong because the only event data for this
            //trial should be the door chosen
            if (eventData.Count != 1)
            {
                throw new Exception("Wrong data provided. Unable to generate document.");
            }

            var traits = eventData[0].Traits;

            var document = new BsonDocument
            {
                {"TrialName", "BeginningRoom" },
                {"UserID", userId },
                {"DoorChoice", new BsonDocument {
                    {"Color", ColorToEnumConverter.ColorToColourEnum(traits.Colour).ToString() },
                    {"Direction", traits.Direction.ToString() },
                    {"Lighting", traits.Lighting.ToString() }
                }}
            };

            return document;
        }
    }
}
