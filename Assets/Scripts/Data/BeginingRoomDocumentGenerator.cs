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
            Debug.Log("Userid: " + userId);
            if (eventData.Count != 1)
            {
                //throw error
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
