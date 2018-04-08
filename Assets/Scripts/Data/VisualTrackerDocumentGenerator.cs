using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Assets.Scripts.Trackers;

namespace Assets.Scripts.Data
{
    public class VisualTrackerDocumentGenerator
    {
        public BsonDocument GenerateDocument(string trialName, Dictionary<TrackableEventObject, float> objectsLookedAt)
        {
            BsonArray objectsLookedAtWithTraitsAndTime = new BsonArray();
            foreach(TrackableEventObject trackableObject in objectsLookedAt.Keys)
            {
                BsonDocument data = new BsonDocument
                {
                    {"Object Type", trackableObject.name },
                    {"Color", trackableObject.Traits.Colour.ToString() },
                    {"Direction", trackableObject.Traits.Direction.ToString() },
                    {"Lighting", trackableObject.Traits.Lighting.ToString() },
                    {"Time Spent Viewing", objectsLookedAt[trackableObject] }
                };
                objectsLookedAtWithTraitsAndTime.Add(data);
            }

            var document = new BsonDocument
            {
                {"TrialName", trialName },
                {"ObjectsLookedAt", objectsLookedAtWithTraitsAndTime }
            };

            return document;
        }
    }
}
