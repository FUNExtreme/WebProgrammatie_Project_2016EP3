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

        public IList<DbLocationReview> GetByLocationId(int locationId)
        {
            return _dbSet.Where(x => x.LocationId == locationId).ToList();
        }
    }
}
