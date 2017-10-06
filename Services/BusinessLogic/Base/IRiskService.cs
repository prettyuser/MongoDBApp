using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic.Base
{
    public interface IRiskService
    {
        Task<IEnumerable<Risk>> Get();
        Task<Risk> Get(string id);
        Task Add(Risk Risk);
        Task<string> Update(string id, Risk Risk);
        Task<DeleteResult> Remove(string id);
        Task<DeleteResult> RemoveAll();
    }    
}
