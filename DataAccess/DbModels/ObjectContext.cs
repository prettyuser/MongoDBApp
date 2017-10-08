using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DataAccess.Models;

namespace DataAccess.DbModels
{
    /// <summary>
    /// Class to create a db context for MongoDB
    /// </summary>
    public class ObjectContext
    {
        public IConfigurationRoot Configuration { get; }

        private IMongoDatabase _database = null;

        public ObjectContext(IOptions<Settings> settings)
        {
            Configuration = settings.Value.iConfigurationRoot;
            settings.Value.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            settings.Value.Database = Configuration.GetSection("MongoConnection:Database").Value;

            var client = new MongoClient(settings.Value.ConnectionString);
            if(client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Policy> Policies
        {
            get
            {
                return _database.GetCollection<Policy>("Policy");
            }
        }

        public IMongoCollection<Risk> AvailableRisks
        {
            get
            {
                return _database.GetCollection<Risk>("AvailableRisk");
            }
        }
    }
}
