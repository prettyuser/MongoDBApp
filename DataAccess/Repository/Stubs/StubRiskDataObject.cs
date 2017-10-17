using DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using MongoDB.Driver;
using System.Threading.Tasks;


namespace DataAccess.Repository.Stubs
{
    public class StubRiskDataObject : IRiskRepository
    {
        public Task Add(Risk Risk)
        {
            throw new NotImplementedException();
        }

        //implemented
        public async Task<IEnumerable<Risk>> Get()
        {
            IEnumerable<Risk> _list = new List<Risk>
            {
                new Risk{Name = "Risk A", YearlyPrice = 99},
                new Risk{Name = "Risk B", YearlyPrice = 101},
                new Risk{Name = "Risk C", YearlyPrice = 299},
                new Risk{Name = "Risk D", YearlyPrice = 300},
                new Risk{Name = "Risk E", YearlyPrice = 459},
                new Risk{Name = "Risk F", YearlyPrice = 599},
                new Risk{Name = "Risk G", YearlyPrice = 699},
                new Risk{Name = "Risk H", YearlyPrice = 770},
                new Risk{Name = "Risk I", YearlyPrice = 4},
                new Risk{Name = "Risk J", YearlyPrice = 100},
                new Risk{Name = "Risk K", YearlyPrice = 1000},
                new Risk{Name = "Risk L", YearlyPrice = 991},
                new Risk{Name = "Risk M", YearlyPrice = 3499}
            };

            return await Task.Run(() => _list);
        }

        //implemented
        public Task<Risk> Get(string id)
        {
            return Task.Run(() => new Risk { Name = "Risk XYZ", YearlyPrice = 1111 });
        }

        public Task<DeleteResult> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(string id, Risk Risk)
        {
            throw new NotImplementedException();
        }
    }
}
