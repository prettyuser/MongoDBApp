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
    [Route("api/[controller]")]
    public class RisksController
    {
        private IRiskService _riskService;

        public RisksController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        //get all risks
        [HttpGet]
        public Task<string> Get()
        {
            return this.GetRisk();
        }

        private async Task<string> GetRisk()
        {
            var risks = await _riskService.Get();
            return JsonConvert.SerializeObject(risks);
        }

        //get one risk by its id
        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return this.GetRiskById(id);
        }

        private async Task<string> GetRiskById(string id)
        {
            var risk = await _riskService.Get(id) ?? new Risk();
            return JsonConvert.SerializeObject(risk);
        }

        //create risk
        [HttpPost]
        public async Task<string> Post([FromBody] Risk risk)
        {
            await _riskService.Add(risk);
            return "";
        }

        //modify risk
        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] Risk risk)
        {
            if (string.IsNullOrEmpty(id)) return "Invalid id";
            return await _riskService.Update(id, risk);
        }

        //delete risk
        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";

            await _riskService.Remove(id);
            return "";
        }
    }
}
