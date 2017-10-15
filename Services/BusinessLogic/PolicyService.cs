using System.Collections.Generic;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using DataAccess.Repository.Base;

namespace Services.BusinessLogic
{
    public class PolicyService
        : Base.IPolicyService
    {
        /// <summary>
        /// Service level for access the policies in DB through repositories
        /// </summary>
        private IPolicyRepository _policyRepository = null;

        public PolicyService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task Add(Policy policy)
        {
            await _policyRepository
                .Add(policy)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Policy>> Get()
        {
            return await _policyRepository
                        .Get()
                        .ConfigureAwait(false);
        }

        public async Task<Policy> Get(string id)
        {
            return await _policyRepository
                        .Get(id)
                        .ConfigureAwait(false);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await _policyRepository
                        .Remove(id)
                        .ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _policyRepository
                        .RemoveAll()
                        .ConfigureAwait(false);
        }

        public async Task<string> Update(string id, Policy policy)
        {
            return await _policyRepository
                        .Update(id, policy)
                        .ConfigureAwait(false);
        }
    }
}
