using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Policy : IPolicy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string NameOfInsuredObject { get; set; }
        
        public DateTime ValidFrom { get; set; }

        public DateTime ValidTill { get; set; }

        public decimal Premium { get; set; }

        public IList<Risk> InsuredRisks { get; set; }

        public short ValidMonths { get; set; }

    }
}
