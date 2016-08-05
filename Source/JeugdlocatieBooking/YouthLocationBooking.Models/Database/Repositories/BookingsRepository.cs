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

        public IList<DbBooking> GetAllByUserId(int userId)
        {
            return _dbSet.Where(x => x.UserId == userId).ToList();
        }
    }
}
