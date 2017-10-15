using DataAccess.Models;
using DataAccess.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.BusinessLogic;
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

        /// <summary>
        /// Get all available risks in the company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("AvailableRisks")]
        public string GetAvailableRisks()
        {
            var risks = _insuranceCompany
                .AvailableRisks;

            return JsonConvert
                .SerializeObject(risks);
        }
        
        /// <summary>
        /// Get the state of policy at the moment 
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("PolicyState")]
        public string GetPolicyState([FromBody] Policy policy)
        {
            var riskState = _insuranceCompany
                .GetPolicy(policy.NameOfInsuredObject, 
                            DateTime.Now.AddMonths(5));

            return JsonConvert
                .SerializeObject(riskState);
        }

        /// <summary>
        /// Action to create and return policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SellPolicy")]
        public IPolicy PostSellPolicy([FromBody] Policy policy)
        {
            return _insuranceCompany
                .SellPolicy(policy.NameOfInsuredObject, 
                            policy.ValidFrom, 
                            policy.ValidMonths, 
                            policy.InsuredRisks);
        }

        /// <summary>
        /// Action to add risk inside policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        /// /// !!! Не забыть убрать хрень с Моком (3355)
        [HttpPost]
        [ActionName("AddRiskPolicy")]
        public string PostAddRiskPolicy([FromBody] Policy policy)
        {
            _insuranceCompany
                .AddRisk(policy.NameOfInsuredObject, 
                        new Risk {Name = "Stolen Wheels", YearlyPrice = 700 }, 
                        DateTime.Now.AddMonths(4));

            return "";
        }

        /// <summary>
        /// Action to delete risk inside policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        /// !!! Не забыть убрать хрень с Моком (3355)
        [HttpPost]
        [ActionName("DeleteRiskPolicy")]
        public string PostDeleteRiskPolicy([FromBody] Policy policy)
        {
            _insuranceCompany
                .RemoveRisk(policy.NameOfInsuredObject, 
                            new Risk {Name = "Stolen Cat", YearlyPrice = 3355 }, 
                            DateTime.Now.AddMonths(3));

            return "";
        }

    }
}
