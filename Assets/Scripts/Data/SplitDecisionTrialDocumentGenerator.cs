using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Assets.Scripts.Trackers;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class SplitDecisionTrialDocumentGenerator : ITrialEventDocumentGenerator
    {
        public BsonDocument GenerateDocument(BsonValue userId, List<TrackableEventObject> eventData)
        {
            if (eventData.Count != 9)
            {
                //throw error
            }

            BsonArray decisions = new BsonArray();
            var eventDataIndex = 0;
            for (var i = 0; i < SplitDecisionTrialRoom.totalNumberOfDecisions; i++)
            {
                var leftTraits = eventData[eventDataIndex++].Traits;
                var rightTraits = eventData[eventDataIndex++].Traits;
                var choiceDirection = eventData[eventDataIndex++].Traits.Direction;
                decisions.Add(ConvertDecisionDetailsToDocument(i, leftTraits, rightTraits, choiceDirection));
            }

            var document = new BsonDocument
            {
                {"TrialName", "SplitDecisionTrial" },
                {"UserID", userId },
                {"Decisions", decisions }
            };

            return document;
        }

        private BsonDocument ConvertDecisionDetailsToDocument(int decisionNumber, ObjectTraits leftChoiceTraits, ObjectTraits rightChoiceTraits, Direction choice)
        {
            string decisionTitle = "Decision" + decisionNumber;
            return new BsonDocument {
                {
                    decisionTitle, new BsonDocument {
                        { "Left Choice", new BsonDocument
                            {
                                { "Color", leftChoiceTraits.Colour.ToString() },
                                { "Direction", leftChoiceTraits.Direction.ToString() },
                                { "Lighting", leftChoiceTraits.Lighting.ToString() }
                            }
                        },
                        { "Right Choice", new BsonDocument
                            {
                                { "Color", rightChoiceTraits.Colour.ToString() },
                                { "Direction", rightChoiceTraits.Direction.ToString() },
                                { "Lighting", rightChoiceTraits.Lighting.ToString() }
                            }
                        },
                        { "Decision", choice.ToString() }
                    }
                }
            };
        }
    }
}
