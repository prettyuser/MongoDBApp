using DataAccess.DbModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.Base
{
    public abstract class AbstractRepository
    {
        protected ObjectContext _context = null;

        protected AbstractRepository(IOptions<Settings> settings)
        {
            _context = new ObjectContext(settings);
        }
    }
}
