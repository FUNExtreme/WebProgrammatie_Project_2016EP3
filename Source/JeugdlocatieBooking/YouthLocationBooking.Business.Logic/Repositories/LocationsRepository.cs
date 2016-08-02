using System;
using System.Collections.Generic;
using System.Linq;
using YouthLocationBooking.Data.Database.Models;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class LocationsRepository : RepositoryBase, IRepository<DbLocation>
    {
        public LocationsRepository()
            : base()
        {
        }

        public IEnumerable<DbLocation> GetAll()
        {
            return _dbContext.Locations.AsEnumerable();
        }

        public DbLocation Get(int id)
        {
            return _dbContext.Locations.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(DbLocation entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(DbLocation entity)
        {
            throw new NotImplementedException();
        }
    }
}
