using DataAccess.Models;
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
    public class IfCompanyController
    {
        private IInsuranceCompany _insuranceCompany;

        public IfCompanyController(IInsuranceCompany insuranceCompany)
        {
            _insuranceCompany = insuranceCompany;
        }

        [HttpGet]
        public Task<string> GetAll()
        {
            return this.GetAllRisks();
        }

        private async Task<string> GetAllRisks()
        {
            var risks = _insuranceCompany.AvailableRisks;
            return await Task.Run(() => JsonConvert.SerializeObject(risks));
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Policy policy)
        {
            await Task.Run(() => _insuranceCompany.SellPolicy(policy.NameOfInsuredObject, policy.ValidFrom, policy.ValidMonths, policy.InsuredRisks));
            return "";
        }

    }
}
