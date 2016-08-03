﻿using System;
using System.Collections.Generic;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class LocationsRepository : RepositoryBase, IRepository<DbLocation>
    {
        public LocationsRepository()
            : base()
        {
        }

        public IList<DbLocation> GetAll()
        {
            return _dbContext.Locations.ToList();
        }

        public DbLocation Get(int id)
        {
            return _dbContext.Locations.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(DbLocation entity)
        {
            _dbContext.Locations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Remove(DbLocation entity)
        {
            throw new NotImplementedException();
        }
    }
}
