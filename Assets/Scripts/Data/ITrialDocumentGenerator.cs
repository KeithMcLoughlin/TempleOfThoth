using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Assets.Scripts.Data
{
    public interface ITrialDocumentGenerator
    {
        BsonDocument GenerateDocument(ObjectTraits traits, BsonValue userId);
    }
}
