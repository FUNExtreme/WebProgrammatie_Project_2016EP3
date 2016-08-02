using System;
using System.Collections.Generic;
using System.Linq;
using YouthLocationBooking.Data.Database.Models;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class UsersRepository : RepositoryBase, IRepository<DbUser>
    {
        public UsersRepository()
            : base()
        {
        }

        public IEnumerable<DbUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public DbUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public DbUser GetByEmail(string email)
        {
            return _dbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public void Add(DbUser user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Remove(DbUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
