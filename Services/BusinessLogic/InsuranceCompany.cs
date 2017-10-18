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
            if (nameOfInsuredObject == null)
            {
                throw new ArgumentNullException("Argument nameOfInsuredObject not defined!");
            }

            if (_policyRepository.Get(nameOfInsuredObject).Result == null)
            {
                throw new NullReferenceException($"There is no object with name \"{nameOfInsuredObject}\" in policy");
            }

            var policy = _policyRepository.Get(nameOfInsuredObject).Result;

            if (policy.InsuredRisks.Contains(risk))
            {
                throw new DuplicateRiskException($"Duplicate Risk \"{risk}\"");
            }

            policy.InsuredRisks.Add(risk);

            if(validFrom >= policy.ValidTill)
            {
                throw new BadDateException("validFrom must be l.t. validTill");
            }

            policy.AttachedRisks.Add(risk.Name, new ActiveState
            {
                IsActive = DateTime.Now >= validFrom && DateTime.Now <= policy.ValidTill,
                RiskFrom = validFrom,
                RiskTill = policy.ValidTill,
                YearlyPrice = risk.YearlyPrice
            });

            policy.Premium = CalculationPolicies.CalcPolicyPremium(policy.AttachedRisks);

            _policyRepository.Update(policy.NameOfInsuredObject, policy);
        }

        /// <summary>
        /// Get the state of policy for the date
        /// </summary>
        /// <param name="nameOfInsuredObject"></param>
        /// <param name="effectiveDate"></param>
        /// <returns>Get policy's information at a given time</returns>
        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            if (nameOfInsuredObject == null)
            {
                throw new ArgumentNullException("Argument nameOfInsuredObject not defined!");
            }

            if (_policyRepository.Get(nameOfInsuredObject).Result == null)
            {
                throw new NullReferenceException($"There is no object with name \"{nameOfInsuredObject}\" in policy");
            }

            var policy = _policyRepository.Get(nameOfInsuredObject).Result;

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
            if (nameOfInsuredObject == null)
            {
                throw new ArgumentNullException("Argument nameOfInsuredObject not defined!");
            }

            if (_policyRepository.Get(nameOfInsuredObject).Result == null)
            {
                throw new NullReferenceException($"There is no object with name \"{nameOfInsuredObject}\" in policy");
            }

            var policy = _policyRepository.Get(nameOfInsuredObject).Result;

            if (validTill <= policy.ValidFrom)
            {
                throw new BadDateException("validTill must be g.t. validFrom");
            }

            policy.AttachedRisks[risk.Name].RiskTill = validTill;
                
            policy.Premium = CalculationPolicies.CalcPolicyPremium(policy.AttachedRisks);

            policy.AttachedRisks[risk.Name].IsActive = 
                DateTime.Now >= policy.AttachedRisks[risk.Name].RiskFrom && DateTime.Now <= validTill;

            _policyRepository.Update(policy.NameOfInsuredObject, policy);
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
            if (selectedRisks == null)
            {
                throw new ArgumentNullException("Argument selectedRisks must contain at least one risk");
            }
            if (nameOfInsuredObject == null)
            {
                throw new ArgumentNullException("Argument nameOfInsuredObject not defined!");
            }

            if (_policyRepository.Get(nameOfInsuredObject).Result == null)
            {
                throw new NullReferenceException($"There is no object with name \"{nameOfInsuredObject}\" in policy");
            }

            if(validMonths <= 0)
            {
                throw new BadDateException("validMonths must be g.t. 0(zero)");
            }

            var validTill = validFrom.AddMonths(validMonths);
            decimal totalPrice = 0;

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
