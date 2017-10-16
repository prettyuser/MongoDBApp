using DataAccess.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public struct Risk
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(1, 1000000, ErrorMessage = "Value must be between 1 and 1 000 000 USD")]
        public decimal YearlyPrice { get; set; }
    }
}
