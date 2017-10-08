using DataAccess.Models;
using DataAccess.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBApp.Controllers
{
    [Route("api/[controller]")]
    public class RisksController
    {
        private IRiskService _riskService;

        public RisksController(IRiskService riskService)
        {
            _riskService = riskService;
        }

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

        [HttpPost]
        public async Task<string> Post([FromBody] Risk risk)
        {
            await _riskService.Add(risk);
            return "";
        }

        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] Risk risk)
        {
            if (string.IsNullOrEmpty(id)) return "Invalid id";
            return await _riskService.Update(id, risk);
        }

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
