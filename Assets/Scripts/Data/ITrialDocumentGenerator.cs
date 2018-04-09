using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Assets.Scripts.Trackers;

namespace Assets.Scripts.Data
{
    public interface ITrialEventDocumentGenerator
    {
        BsonDocument GenerateDocument(BsonValue userId, List<TrackableEventObject> eventData);
    }
}
