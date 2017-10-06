using Services.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;


namespace Services.BusinessLogic
{
    class PolicyService
        : Base.IPolicyService
    {
        private IPolicyService _policyRepository = null;

        public PolicyService(IPolicyService policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task Add(Policy policy)
        {
            await _policyRepository.Add(policy).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Policy>> Get()
        {
            return await _policyRepository.Get().ConfigureAwait(false);
        }

        public async Task<Policy> Get(string id)
        {
            return await _policyRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await _policyRepository.Remove(id).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _policyRepository.RemoveAll().ConfigureAwait(false);
        }

        public async Task<string> Update(string id, Policy policy)
        {
            return await _policyRepository.Update(id, policy).ConfigureAwait(false);
        }
    }
}
