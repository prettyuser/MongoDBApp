using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using DataAccess.Repository.Base;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using DataAccess.Models.Base;

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

        #region Risks Service
        public async Task AddRisk(Risk risk)
        {
            await _riskRepository.Add(risk).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveRisk(string id)
        {
            return await _riskRepository.Remove(id).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAllRisks()
        {
            return await _riskRepository.RemoveAll().ConfigureAwait(false);
        }

        public async Task<string> UpdateRisks(string id, Risk risk)
        {
            return await _riskRepository.Update(id, risk).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Risk>> GetRisks()
        {
            return await _riskRepository.Get().ConfigureAwait(false);
        }
        #endregion

        #region Policy Service
        public async Task AddPolicy(Policy policy)
        {
            await _policyRepository.Add(policy).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Policy>> GetPolicy()
        {
            return await _policyRepository.Get().ConfigureAwait(false);
        }

        public async Task<Policy> GetPolicyById(string id)
        {
            return await _policyRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemovePolicy(string id)
        {
            return await _policyRepository.Remove(id).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAllPolicies()
        {
            return await _policyRepository.RemoveAll().ConfigureAwait(false);
        }

        public async Task<string> UpdatePolicy(string id, Policy policy)
        {
            return await _policyRepository.Update(id, policy).ConfigureAwait(false);
        }
        #endregion

        #region interface implementation
        public IList<Risk> AvailableRisks
        {
            get
            {
                return GetRisks().Result.ToList();
            }
            set
            {
                listrisks = value;
            }
        }

        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            throw new NotImplementedException();
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            throw new NotImplementedException();
        }

        public void RemoveRisk(string nameOfInsuredObject, Risk risk, DateTime validTill)
        {
            throw new NotImplementedException();
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

                var days_in_risk = (int) (item.RiskTill - item.RiskFrom).TotalDays; // all days in policy
                var daily_price = item.YearlyPrice / 365; // price per day
                var realprice = days_in_risk * daily_price;
                total_price += realprice;
            }

            Policy _policy = new Policy { NameOfInsuredObject = nameOfInsuredObject, InsuredRisks = selectedRisks, ValidFrom = validFrom, ValidTill = validFrom.AddMonths(validMonths), Premium = total_price, ValidMonths = validMonths };

            AddPolicy(_policy);

            return _policy;
        }
        #endregion
    }
}
