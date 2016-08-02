using System.Collections.Generic;
using System.Linq;
using YouthLocationBooking.Data.Database;
using YouthLocationBooking.Data.Database.Models;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class LocationsRepository
    {
        private DatabaseContext _dbContext;

        public LocationsRepository()
        {
            _dbContext = new DatabaseContext();
        }

        public IEnumerable<DbLocation> GetAll()
        {
            return _dbContext.Locations.AsEnumerable();
        }
    }
}
