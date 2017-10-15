using DataAccess.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.Base
{
    public interface IRiskRepository
    {
        Task<IEnumerable<Risk>> Get();

        Task<Risk> Get(string id);

        Task Add(Risk Risk);

        Task<string> Update(string id, Risk Risk);

        Task<DeleteResult> Remove(string id);

        Task<DeleteResult> RemoveAll();
    }
}
