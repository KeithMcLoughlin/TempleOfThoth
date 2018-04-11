using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using UnityEngine;
using Assets.Scripts.Trackers;
using Assets.Scripts.Data;

namespace Assets.Scripts.Data
{
    public class VisualTrackerDocumentGenerator
    {
        public BsonDocument GenerateDocument(string trialName, BsonValue userId, List<VisualTrackerObjectDetails> objectsLookedAt)
        {
            BsonArray objectsLookedAtWithTraitsAndTime = new BsonArray();
            foreach(var viewedObject in objectsLookedAt)
            {
                var trackableObject = viewedObject.ObjectLookedAt;
                var timeSpentLookingAtIt = viewedObject.TimeSpentLookingAtObject;
                BsonDocument data = new BsonDocument
                {
                    {"Object Type", trackableObject.name },
                    {"Color", ColorToEnumConverter.ColorToColourEnum(trackableObject.Traits.Colour).ToString() },
                    {"Direction", trackableObject.Traits.Direction.ToString() },
                    {"Lighting", trackableObject.Traits.Lighting.ToString() },
                    {"Time Spent Viewing", timeSpentLookingAtIt.ToString() }
                };
                objectsLookedAtWithTraitsAndTime.Add(data);
            }

            var document = new BsonDocument
            {
                {"TrialName", trialName },
                {"UserID", userId },
                {"ObjectsLookedAt", objectsLookedAtWithTraitsAndTime }
            };

            return document;
        }
    }
}
