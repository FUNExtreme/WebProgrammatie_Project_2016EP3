using System;
using System.Collections.Generic;
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

        public void Add(DbUser user)
        {
            _dbContext.Users.Add(user);
        }

        public void Remove(DbUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
