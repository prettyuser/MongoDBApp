using DataAccess.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Risk : IRisk
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       
        public string Name { get; set; }
       
        public decimal YearlyPrice { get; set; }

        public DateTime RiskFrom { get; set; }

        public DateTime RiskTill { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
