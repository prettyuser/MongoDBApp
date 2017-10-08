using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using DataAccess.Repository.Base;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using DataAccess.Models.Base;
using AutoMapper;

namespace Services.BusinessLogic
{
    public class InsuranceCompany : 
        Base.IInsuranceCompany
    {
        private IPolicyRepository _policyRepository = null;
        private IRiskRepository _riskRepository = null;
        IList<Risk> listrisks;

        public InsuranceCompany(IPolicyRepository policyRepository, IRiskRepository riskRepository)
        {
            _policyRepository = policyRepository;
            _riskRepository = riskRepository;
        }

        public string Name => "IF...";

        #region interface implementation
        public IList<Risk> AvailableRisks
        {
            get
            {
                return _riskRepository.Get().Result.ToList();
            }
            set
            {
                listrisks = value;
            }
        }

        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            var policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            risk.RiskFrom = validFrom;
            risk.RiskTill = policy.ValidTill;

            if(validFrom > DateTime.Now)
            {
                risk.IsActive = false;
            }

            policy.InsuredRisks.Add(risk);

            policy.Premium = CalcPolicyPremium(policy.InsuredRisks);

            _policyRepository.Update(policy.Id, policy);
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            //we believe we have unique names of insured objects
            var policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            var policy_to_get = new Policy();

            //clone the instance for working with its copy
            Mapper.Initialize(cfg => cfg.CreateMap<Policy, Policy>());
            policy_to_get = Mapper.Map<Policy>(policy);

            //setting flags 'IsActive'
            foreach(var item in policy_to_get.InsuredRisks) 
            {
                if (effectiveDate >= item.RiskFrom && effectiveDate <= item.RiskTill)
                    item.IsActive = true;
                else
                    item.IsActive = false;
            }
            
            //calc the premium at the moment
            policy_to_get.Premium = CalcPolicyPremium(policy_to_get.InsuredRisks);


            return policy_to_get;
        }

        public void RemoveRisk(string nameOfInsuredObject, Risk risk, DateTime validTill)
        {
            var policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            List<Risk> _list_to_remove = new List<Risk>();
            _list_to_remove = (List<Risk>) policy.InsuredRisks;
            _list_to_remove.RemoveAll(x => x.Id == risk.Id);
            policy.InsuredRisks = _list_to_remove;
                
            policy.Premium = CalcPolicyPremium(policy.InsuredRisks);

            _policyRepository.Update(policy.Id, policy);
        }

        public decimal CalcPolicyPremium(IList<Risk> listrisk)
        {
            decimal total_price = 0;

            foreach (var item in listrisk)
            {
                if (item.IsActive)
                {
                    var days_in_risk = (int) (item.RiskTill - item.RiskFrom).TotalDays; // all days in policy
                    var daily_price = item.YearlyPrice / 365; // price per day
                    var realprice = days_in_risk * daily_price;
                    total_price += realprice;
                }
            }

            return total_price;
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            var validTill = validFrom.AddMonths(validMonths);
            decimal total_price = 0;

            if (selectedRisks == null)
            {
                selectedRisks = new List<Risk>();
                selectedRisks.Add(_riskRepository.Get("59d7862f40860722a8cd9698").Result);
                selectedRisks.Add(_riskRepository.Get("59d8a6a3b830e6266cf1738b").Result);
            }

            foreach (var item in selectedRisks)
            {
                item.RiskFrom = validFrom;
                item.RiskTill = validTill;
            }

            total_price = CalcPolicyPremium(selectedRisks);

            Policy _policy = new Policy { NameOfInsuredObject = nameOfInsuredObject, InsuredRisks = selectedRisks, ValidFrom = validFrom, ValidTill = validFrom.AddMonths(validMonths), Premium = total_price, ValidMonths = validMonths };

            _policyRepository.Add(_policy);

            return _policy;
        }
        #endregion
    }
}
