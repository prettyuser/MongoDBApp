using DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using DataAccess.DbModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace DataAccess.Repository
{
    public class RiskRepository 
        : AbstractRepository, IRiskRepository
    {
        //private readonly ObjectContext _context = null;

        //public RiskRepository(IOptions<Settings> settings)
        //{
        //    _context = new ObjectContext(settings);
        //}

        public RiskRepository(IOptions<Settings> settings) 
            : base(settings)
        {
        }

        public async Task Add(Risk risk)
        {
            await base._context.AvailableRisks.InsertOneAsync(risk).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Risk>> Get()
        {
            return await base._context.AvailableRisks.Find(x => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Risk> Get(string id)
        {
            var risk = Builders<Risk>.Filter.Eq("Id", id);
            return await base._context.AvailableRisks.Find(risk).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await base._context.AvailableRisks.DeleteOneAsync(Builders<Risk>.Filter.Eq("Id", id)).ConfigureAwait(false);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await base._context.AvailableRisks.DeleteManyAsync(new BsonDocument()).ConfigureAwait(false);
        }

        public async Task<string> Update(string id, Risk risk)
        {
            await base._context.AvailableRisks.ReplaceOneAsync(x => x.Id == id, risk).ConfigureAwait(false);
            return "";
        }
    }
}
