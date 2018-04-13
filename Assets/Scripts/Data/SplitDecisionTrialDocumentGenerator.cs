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
    public class SplitDecisionTrialDocumentGenerator : ITrialEventDocumentGenerator
    {
        public BsonDocument GenerateDocument(BsonValue userId, List<TrackableEventObject> eventData)
        {
            //if the data passed in isn't 9 objects we know its wrong because the event data for this
            //trial should be 6 of the decision corridors and the 3 corridors the player chose
            if (eventData.Count != 9)
            {
                throw new Exception("Wrong data provided. Unable to generate document.");
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

        //create the document section that describes the chosen corridor traits and ignored corridor traits for a decision number
        private BsonDocument ConvertDecisionDetailsToDocument(int decisionNumber, ObjectTraits leftChoiceTraits, ObjectTraits rightChoiceTraits, Direction choice)
        {
            string decisionTitle = "Decision" + decisionNumber;
            ObjectTraits decisionTraits;
            ObjectTraits ignoredTraits;
            //assign the traits of the chosen and ignored corridor based on the direction choose
            if (leftChoiceTraits.Direction == choice)
            {
                decisionTraits = leftChoiceTraits;
                ignoredTraits = rightChoiceTraits;
            }
            else
            {
                decisionTraits = rightChoiceTraits;
                ignoredTraits = leftChoiceTraits;
            }

                return new BsonDocument {
                {
                    decisionTitle, new BsonDocument {
                        { "Decision Choice", new BsonDocument
                            {
                                { "Color", ColorToEnumConverter.ColorToColourEnum(decisionTraits.Colour).ToString() },
                                { "Direction", decisionTraits.Direction.ToString() },
                                { "Lighting", decisionTraits.Lighting.ToString() }
                            }
                        },
                        { "Ignored Choice", new BsonDocument
                            {
                                { "Color", ColorToEnumConverter.ColorToColourEnum(ignoredTraits.Colour).ToString() },
                                { "Direction", ignoredTraits.Direction.ToString() },
                                { "Lighting", ignoredTraits.Lighting.ToString() }
                            }
                        }
                    }
                }
            };
        }
    }
}
