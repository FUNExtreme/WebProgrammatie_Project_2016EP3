using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class LocationReviewsRepository : GenericRepository<DbLocationReview>
    {
        public LocationReviewsRepository(DbContext context)
            : base(context)
        {
        }

        public IList<DbLocationReview> GetAllByLocationId(int locationId)
        {
            return _dbSet
                .Include("FacilityRatings")
                .Include("FacilityRatings.Facility")
                .Where(x => x.LocationId == locationId)
                .ToList();
        }

        public IList<DbLocationReview> GetAllByUserId(int userId)
        {
            return _dbSet
                .Include("FacilityRatings")
                .Include("FacilityRatings.Facility")
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public DbLocationReview GetByUserIdAndLocationId(int userId, int locationId)
        {
            return _dbSet
                .Include("FacilityRatings")
                .Include("FacilityRatings.Facility")
                .Where(x => x.UserId == userId && x.LocationId == locationId)
                .FirstOrDefault();
        }
    }
}
