using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;
using MongoDB.Driver;
using DataAccess.Repository.Base;
using System;

namespace Services.BusinessLogic
{
    /// <summary>
    /// Service level for access the risks in DB through repositories
    /// </summary>
    public class RiskService
        :Base.IRiskService
    {
        private IRiskRepository _riskRepository = null;

        public RiskService(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        public async Task Add(Risk risk)
        {
            await _riskRepository
                .Add(risk)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Risk>> Get()
        {
            return await _riskRepository
                .Get()
                .ConfigureAwait(false);
        }

        public async Task<Risk> Get(string id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("ID equals NULL");
            }

            return await _riskRepository
                .Get(id)
                .ConfigureAwait(false);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("ID equals NULL");
            }

            return await _riskRepository
                .Remove(id)
                .ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _riskRepository
                .RemoveAll()
                .ConfigureAwait(false);
        }

        public async Task<string> Update(string id, Risk risk)
        {
            if (id == null)
            {
                throw new ArgumentNullException("ID equals NULL");
            }

            return await _riskRepository
                .Update(id, risk)
                .ConfigureAwait(false);
        }
    }
}
