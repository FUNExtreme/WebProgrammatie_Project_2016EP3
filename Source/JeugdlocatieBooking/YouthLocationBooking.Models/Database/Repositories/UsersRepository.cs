using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class UsersRepository : GenericRepository<DbUser>
    {
        public UsersRepository(DbContext context) 
            : base(context)
        {
        }

        public DbUser GetByEmail(string email)
        {
            return _dbSet.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }
    }
}
