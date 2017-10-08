using DataAccess.Models;
using DataAccess.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class IfCompanyController
    {
        private IInsuranceCompany _insuranceCompany;

        public IfCompanyController(IInsuranceCompany insuranceCompany)
        {
            _insuranceCompany = insuranceCompany;
        }

        [HttpGet]
        [ActionName("AvailableRisks")]
        public string GetAll()
        {
            var risks = _insuranceCompany.AvailableRisks;
            return JsonConvert.SerializeObject(risks);

            //return this.GetAllRisks();
        }

        //private string GetAllRisks()
        //{
        //    var risks = _insuranceCompany.AvailableRisks;
        //    return JsonConvert.SerializeObject(risks);
        //}

        [HttpPost]
        [ActionName("SellPolicy")]
        public IPolicy Post([FromBody] Policy policy)
        {
            return _insuranceCompany.SellPolicy(policy.NameOfInsuredObject, policy.ValidFrom, policy.ValidMonths, policy.InsuredRisks);
        }

        [HttpPost]
        [ActionName("AddRiskPolicy")]
        public string PostRiskPolicy([FromBody] Policy policy)
        {
            _insuranceCompany.AddRisk(policy.NameOfInsuredObject, _insuranceCompany.AvailableRisks.First(x => x.YearlyPrice == 3355), DateTime.Now);
            return "";
        }

        [HttpPost]
        [ActionName("DeleteRiskPolicy")]
        public string Delete([FromBody] Policy policy)
        {
            _insuranceCompany.RemoveRisk(policy.NameOfInsuredObject, _insuranceCompany.AvailableRisks.First(x => x.YearlyPrice == 3355), DateTime.Now);
            return "";
        }

    }
}
