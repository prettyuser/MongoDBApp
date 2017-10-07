using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.BusinessLogic.Base;

namespace MongoDBApp.Controllers
{
    [Route("api/[controller]")]
    public class PoliciesController
    {
        
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public Task<string> Get()
        {
            return this.GetPolicy();
        }

        private async Task<string> GetPolicy()
        {
            var policies = await _policyService.Get();
            return JsonConvert.SerializeObject(policies);
        }



        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return this.GetPolicyById(id);
        }

        private async Task<string> GetPolicyById(string id)
        {
            var policy = await _policyService.Get(id) ?? new Policy();
            return JsonConvert.SerializeObject(policy);
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Policy policy)
        {
            await _policyService.Add(policy);
            return "";
        }

        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] Policy policy)
        {
            if (string.IsNullOrEmpty(id)) return "Invalid id";
            return await _policyService.Update(id, policy);            
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            await _policyService.Remove(id);
            return "";
        }
    }
}
