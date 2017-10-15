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
    [Route("api/[controller]/[action]")]
    public class PoliciesController
    {
        
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        /// <summary>
        /// Get all policies
        /// </summary>
        /// <returns>All policies</returns>
        [HttpGet]
        [ActionName("GetPolicies")]
        public Task<string> Get()
        {
            return this.GetPolicy();
        }
        
        private async Task<string> GetPolicy()
        {
            var policies = await _policyService
                                .Get();

            return JsonConvert
                .SerializeObject(policies);
        }

        /// <summary>
        /// Get one policy by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One policy by id</returns>
        [HttpGet("{id}")]
        [ActionName("GetPolicyByName")]
        public Task<string> Get(string id)
        {
            return this.GetPolicyById(id);
        }

        private async Task<string> GetPolicyById(string id)
        {
            var policy = await _policyService
                .Get(id) ?? new Policy();

            return JsonConvert
                .SerializeObject(policy);
        }

        /// <summary>
        /// Create policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>Null</returns>
        [HttpPost]
        [ActionName("CreatePolicy")]
        public async Task<string> Post([FromBody] Policy policy)
        {
            await _policyService
                .Add(policy);

            return "";
        }

        /// <summary>
        /// Modify policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ActionName("ModifyPolicy")]
        public async Task<string> Put(string id, [FromBody] Policy policy)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            return await _policyService
                .Update(id, policy);            
        }

        /// <summary>
        /// Delete policy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ActionName("DeletePolicyByName")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            await _policyService.Remove(id);

            return "";
        }
    }
}
