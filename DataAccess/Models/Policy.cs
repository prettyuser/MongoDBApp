using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Policy : IPolicy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [Required (ErrorMessage = "You have to input name of insured object")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Amount of chars must be from 3 to 50")]
        [Display(Name = "Name of insured object")]
        public string NameOfInsuredObject { get; set; }

        [Display(Name = "Valid From")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Valid Till")]
        public DateTime ValidTill { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Premium { get; set; }

        [Display(Name = "Insured Risks")]
        public IList<Risk> InsuredRisks { get; set; }

        [Range(1, 120, ErrorMessage = "Must be between 1 and 120 months")]
        [Display(Name = "Valid Months")]
        public short ValidMonths { get; set; }

        public Dictionary<string, ActiveState> AttachedRisks { get; set; }
    }
}
