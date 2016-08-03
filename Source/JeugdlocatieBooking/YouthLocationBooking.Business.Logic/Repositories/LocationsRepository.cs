using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class LocationsRepository : RepositoryBase, IRepository<DbLocation>
    {
        #region Constructor
        public LocationsRepository()
            : base()
        {
        }
        #endregion

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

        public void Update(DbLocation entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(DbLocation entity)
        {
            _dbContext.Locations.Remove(entity);
        }
    }
}
