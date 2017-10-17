using DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DataAccess.Repository.Stubs
{
    public class StubPolicyDataObject:IPolicyRepository
    {
        public Task Add(Policy Policy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Policy>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Policy> Get(string id)
        {
            var dict = new Dictionary<string, ActiveState>(1);
            dict.Add( "Risk ABC", new ActiveState {
                IsActive = true,
                RiskFrom = DateTime.Now,
                RiskTill = DateTime.Now.AddMonths(2),
                YearlyPrice = 300
            });

            var risks = new List<Risk>();
            risks.Add(new Risk { Name = "Risk ABC", YearlyPrice = 300 });

            return Task.Run(() => new Policy {
                AttachedRisks = dict,
                InsuredRisks = risks,
                NameOfInsuredObject = "Policy ABC",
                Premium = 51,
                ValidFrom = DateTime.Now,
                ValidTill = DateTime.Now.AddMonths(2),
                ValidMonths = 2
            });
        }

        public Task<DeleteResult> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(string id, Policy Policy)
        {
            throw new NotImplementedException();
        }
    }
}
