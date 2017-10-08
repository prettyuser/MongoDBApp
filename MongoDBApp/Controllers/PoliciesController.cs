using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Services.BusinessLogic.Base;

namespace MongoDBApp.Controllers
{
    /// <summary>
    /// Controller-helper to expand functionality of access to policies separately 
    /// </summary>
    [Route("api/[controller]")]
    public class PoliciesController
    {
        
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        //get all policies
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

        //get one policy by its id
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

        //create policy
        [HttpPost]
        public async Task<string> Post([FromBody] Policy policy)
        {
            await _policyService.Add(policy);
            return "";
        }

        //modify policy
        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] Policy policy)
        {
            if (string.IsNullOrEmpty(id)) return "Invalid id";
            return await _policyService.Update(id, policy);            
        }

        //delete policy
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
