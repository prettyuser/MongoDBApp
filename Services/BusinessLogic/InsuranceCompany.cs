using System;
using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Repository.Base;
using MongoDB.Driver;
using System.Linq;
using DataAccess.Models.Base;
using AutoMapper;

namespace Services.BusinessLogic
{
    /// <summary>
    /// Main service-level-class implemented task's interface for IInsurance company
    /// </summary>
    public class InsuranceCompany : 
        Base.IInsuranceCompany
    {
        private IPolicyRepository _policyRepository = null;
        private IRiskRepository _riskRepository = null;
        IList<Risk> listrisks;

        /// <summary>
        /// Constructor that using 2 repositories: policies and risks
        /// </summary>
        /// <param name="policyRepository"></param>
        /// <param name="riskRepository"></param>
        public InsuranceCompany(IPolicyRepository policyRepository, IRiskRepository riskRepository)
        {
            _policyRepository = policyRepository;
            _riskRepository = riskRepository;
        }

        #region interface implementation

        /// <summary>
        /// Private field to change company's name through the function
        /// </summary>
        private string _companyName = "IF";

        /// <summary>
        /// Getter's property of company's Name
        /// </summary>
        public string Name
        {
            get
            {
                return this._companyName;
            }
        }

        /// <summary>
        /// Changes company's name
        /// </summary>
        /// <param name="_newCompanyName"></param>
        public void ChangeCompanyName(string _newCompanyName)
        {
            this._companyName = _newCompanyName;
        }

        /// <summary>
        /// Get all available risks
        /// </summary>
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

        /// <summary>
        /// Add risks for insured objects
        /// </summary>
        /// <param name="nameOfInsuredObject"></param>
        /// <param name="risk"></param>
        /// <param name="validFrom"></param>
        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            var _policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            _policy.InsuredRisks.Add(risk);

            _policy.AttachedRisks.Add(risk.Name, new ActiveState
            {
                IsActive = DateTime.Now >= validFrom && DateTime.Now <= _policy.ValidTill,
                RiskFrom = validFrom,
                RiskTill = _policy.ValidTill,
                YearlyPrice = risk.YearlyPrice
            });

            _policy.Premium = CalculationPolicies.CalcPolicyPremium(_policy.AttachedRisks);

            _policyRepository.Update(_policy.NameOfInsuredObject, _policy);
        }

        /// <summary>
        /// Get the state of policy for the date
        /// </summary>
        /// <param name="nameOfInsuredObject"></param>
        /// <param name="effectiveDate"></param>
        /// <returns>Get policy's information at a given time</returns>
        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            var policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            var policy_to_get = new Policy();

            policy_to_get = Mapper.Map<Policy>(policy);

            //setting flags 'IsActive'
            foreach (var item in policy_to_get.AttachedRisks)
            {
                if (effectiveDate >= item.Value.RiskFrom && effectiveDate <= item.Value.RiskTill)
                    item.Value.IsActive = true;
                else
                    item.Value.IsActive = false;
            }

            return policy_to_get;
        }

        /// <summary>
        /// Remove risks for insured objects means put a date of risk's end
        /// </summary>
        /// <param name="nameOfInsuredObject"></param>
        /// <param name="risk"></param>
        /// <param name="validTill"></param>
        public void RemoveRisk(string nameOfInsuredObject, Risk risk, DateTime validTill)
        {
            var _policy = _policyRepository.Get().Result.Where(x => x.NameOfInsuredObject == nameOfInsuredObject).First();

            _policy.AttachedRisks[risk.Name].RiskTill = validTill;
                
            _policy.Premium = CalculationPolicies.CalcPolicyPremium(_policy.AttachedRisks);

            _policy.AttachedRisks[risk.Name].IsActive = 
                DateTime.Now >= _policy.AttachedRisks[risk.Name].RiskFrom && DateTime.Now <= validTill;

            _policyRepository.Update(_policy.NameOfInsuredObject, _policy);
        }

        /// <summary>
        /// Return and create policy in DB with initial (examples) list of risks
        /// </summary>
        /// <param name="nameOfInsuredObject"></param>
        /// <param name="validFrom"></param>
        /// <param name="validMonths"></param>
        /// <param name="selectedRisks"></param>
        /// <returns>Policy's information</returns>
        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            var validTill = validFrom.AddMonths(validMonths);
            decimal totalPrice = 0;

            //Mocks
            //if (selectedRisks == null)
            //{
            //    selectedRisks = new List<Risk>
            //    {
            //        _riskRepository.Get("Stolen Cat").Result,
            //        _riskRepository.Get("Headache").Result
            //    };
            //}

            Policy policy = new Policy { NameOfInsuredObject = nameOfInsuredObject,
                                          AttachedRisks = null,
                                          ValidFrom = validFrom,
                                          ValidTill = validTill,
                                          Premium = 0,
                                          ValidMonths = validMonths,
                                          InsuredRisks = null
                                        };

            policy.AttachedRisks = new Dictionary<string, ActiveState>(selectedRisks.Count);

            foreach (var item in selectedRisks)
            {
                policy.AttachedRisks.Add(item.Name, new ActiveState()
                {
                    IsActive = DateTime.Now >= validFrom && DateTime.Now <= validTill,
                    RiskFrom = validFrom,
                    RiskTill = validTill,
                    YearlyPrice = item.YearlyPrice
                });
            }

            totalPrice = CalculationPolicies.CalcPolicyPremium(policy.AttachedRisks);

            policy.Premium = totalPrice;
            policy.InsuredRisks = selectedRisks;

            _policyRepository.Add(policy);

            return policy;
        }
        #endregion
    }
}
