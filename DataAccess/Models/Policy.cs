using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Policy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Roll { get; set; }




        //public string NameOfInsuredObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public DateTime ValidFrom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public DateTime ValidTill { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public decimal Premium { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public IList<Risk> InsuredRisks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
