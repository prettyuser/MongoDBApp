using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.BusinessLogic.Base;
using System.Threading.Tasks;

namespace MongoDBApp.Controllers
{
    /// <summary>
    /// Controller-helper to expand functionality of access to risks separately 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class RisksController
    {
        private IRiskService _riskService;

        public RisksController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        /// <summary>
        /// Get all risks
        /// </summary>
        /// <returns>Whole list </returns>
        [HttpGet]
        [ActionName("GetRisks")]
        public Task<string> Get()
        {
            return this.GetRisk();
        }

        private async Task<string> GetRisk()
        {
            var risks = await _riskService.Get();

            return JsonConvert
                .SerializeObject(risks);
        }

        /// <summary>
        /// Get one risk by its name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Risk</returns>
        [HttpGet("{id}")]
        [ActionName("GetRiskByName")]
        public Task<string> Get(string id)
        {
            return this.GetRiskById(id);
        }

        private async Task<string> GetRiskById(string id)
        {
            var risk = await _riskService.Get(id);// ?? new Risk();

            return JsonConvert
                .SerializeObject(risk);
        }

        /// <summary>
        /// Create risk and add to company's available list or risks
        /// </summary>
        /// <param name="risk"></param>
        /// <returns>Null</returns>
        [HttpPost]
        [ActionName("CreateRisk")]
        public async Task<string> Post([FromBody] Risk risk)
        {
            await _riskService
                .Add(risk);

            return "";
        }

        /// <summary>
        /// Modify risk in available list or risks
        /// </summary>
        /// <param name="id"></param>
        /// <param name="risk"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ActionName("ModifyRisk")]
        public async Task<string> Put(string id, [FromBody] Risk risk)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            return await _riskService
                .Update(id, risk);
        }

        /// <summary>
        /// Delete risk from entire available list or risks
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null</returns>
        [HttpDelete("{id}")]
        [ActionName("DeleteRisk")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            await _riskService
                .Remove(id);

            return "";
        }
    }
}
