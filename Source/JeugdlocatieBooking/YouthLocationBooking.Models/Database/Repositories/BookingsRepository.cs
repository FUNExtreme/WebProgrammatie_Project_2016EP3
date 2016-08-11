using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class BookingsRepository : GenericRepository<DbBooking>
    {
        public BookingsRepository(DbContext context) 
            : base(context)
        {
        }

        public override DbBooking Get(int id)
        {
            return _dbSet
                .Include("Location")
                .FirstOrDefault(x => x.Id == id);
        }

        public IList<DbBooking> GetAllByUserId(int userId)
        {
            return _dbSet.Where(x => x.UserId == userId).ToList();
        }

        public IList<DbBooking> GetAllByUserIdAndLocationId(int userId, int locationId)
        {
            return _dbSet.Where(x => x.UserId == userId && x.LocationId == locationId).ToList();
        }

        public IList<DbBooking> GetAfterEndDateByUserId(int userId)
        {
            var currentDate = DateTime.Now;
            return _dbSet
                .Include("Location")
                .Where(x => x.UserId == userId && x.EndDateTime <= currentDate)
                .ToList();
        }

        public IList<DbBooking> GetAllByLocationUserId(int userId)
        {
            // We eager load the Location and User information
            return _dbSet
                .Include("Location")
                .Include("User")
                .Join(_dbContext.Set<DbLocation>(), b => b.LocationId, l => l.Id, (b, l) => new { b, l })
                .Where(x => x.l.CreatedByUserId == userId)
                .Select(x => x.b)
                .ToList();
        }
    }
}
