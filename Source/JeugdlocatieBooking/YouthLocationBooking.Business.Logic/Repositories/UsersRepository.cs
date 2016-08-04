using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class UsersRepository : RepositoryBase, IRepository<DbUser>
    {
        public UsersRepository()
            : base()
        {
        }

        public IList<DbUser> GetAll()
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

        public void Update(DbUser entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(DbUser entity)
        {
            _dbContext.Users.Remove(entity);
        }
    }
}
