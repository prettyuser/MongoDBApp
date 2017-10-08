using DataAccess.Repository.Base;
using System.Collections.Generic;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using DataAccess.DbModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace DataAccess.Repository
{
    /// <summary>
    /// Class for risk's repository with async methods. Basic CRUD operations
    /// </summary>
    public class RiskRepository 
        : AbstractRepository, IRiskRepository
    {
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
