using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.Base;

namespace MongoDBApp.Controllers
{
    [Route("api/[controller]")]
    public class PoliciesController
    {
        
        private readonly IPolicyRepository _policyRepository;

        public PoliciesController(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        [HttpGet]
        public Task<string> Get()
        {
            return this.GetPolicy();
        }

        private async Task<string> GetPolicy()
        {
            var policies = await _policyRepository.Get();
            return JsonConvert.SerializeObject(policies);
        }



        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return this.GetPolicyById(id);
        }

        private async Task<string> GetPolicyById(string id)
        {
            var policy = await _policyRepository.Get(id) ?? new Policy();
            return JsonConvert.SerializeObject(policy);
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Policy policy)
        {
            await _policyRepository.Add(policy);
            return "";
        }

        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] Policy policy)
        {
            if (string.IsNullOrEmpty(id)) return "Invalid id";
            return await _policyRepository.Update(id, policy);            
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            await _policyRepository.Remove(id);
            return "";
        }
    }
}
