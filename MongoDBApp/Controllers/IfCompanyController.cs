using DataAccess.Models;
using DataAccess.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.BusinessLogic.Base;
using System;
using System.Linq;

namespace MongoDBApp.Controllers
{
    /// <summary>
    /// Main controller for this API application
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class IfCompanyController
    {
        private IInsuranceCompany _insuranceCompany;

        public IfCompanyController(IInsuranceCompany insuranceCompany)
        {
            _insuranceCompany = insuranceCompany;
        }

        //get all available risks in the company
        [HttpGet]
        [ActionName("AvailableRisks")]
        public string GetAvailableRisks()
        {
            var risks = _insuranceCompany.AvailableRisks;
            return JsonConvert.SerializeObject(risks);
        }

        //action to get policy state at the concrete moment of time
        [HttpPost]
        [ActionName("PolicyState")]
        public string GetPolicyState([FromBody] Policy policy)
        {
            var riskState = _insuranceCompany.GetPolicy(policy.NameOfInsuredObject, DateTime.Now);
            return JsonConvert.SerializeObject(riskState);
        }

        //action to create and return policy
        [HttpPost]
        [ActionName("SellPolicy")]
        public IPolicy PostSellPolicy([FromBody] Policy policy)
        {
            return _insuranceCompany.SellPolicy(policy.NameOfInsuredObject, policy.ValidFrom, policy.ValidMonths, policy.InsuredRisks);
        }

        //action to add risk inside policy
        [HttpPost]
        [ActionName("AddRiskPolicy")]
        public string PostAddRiskPolicy([FromBody] Policy policy)
        {
            _insuranceCompany.AddRisk(policy.NameOfInsuredObject, _insuranceCompany.AvailableRisks.First(x => x.YearlyPrice == 3355), DateTime.Now);
            return "";
        }

        //action to delete risk inside policy
        [HttpPost]
        [ActionName("DeleteRiskPolicy")]
        public string PostDeleteRiskPolicy([FromBody] Policy policy)
        {
            _insuranceCompany.RemoveRisk(policy.NameOfInsuredObject, _insuranceCompany.AvailableRisks.First(x => x.YearlyPrice == 3355), DateTime.Now);
            return "";
        }

    }
}
