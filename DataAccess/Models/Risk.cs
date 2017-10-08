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
        /// <summary>
        /// Unique name of the risk
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Risk yearly price
        /// </summary>
        public decimal YearlyPrice { get; set; }

        public DateTime RiskFrom { get; set; }

        public DateTime RiskTill { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
