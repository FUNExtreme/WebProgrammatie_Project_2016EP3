using System.Data.Entity;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class LocationFacilitiesRepository : GenericRepository<DbLocationFacility>
    {
        public LocationFacilitiesRepository(DbContext context)
            : base(context)
        {
        }
    }
}
