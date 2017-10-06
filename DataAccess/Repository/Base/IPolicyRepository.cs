using MongoDB.Driver;
using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.Base
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> Get();
        Task<Policy> Get(string id);
        Task Add(Policy Policy);
        Task<string> Update(string id, Policy Policy);
        Task<DeleteResult> Remove(string id);
        Task<DeleteResult> RemoveAll();
    }
}
