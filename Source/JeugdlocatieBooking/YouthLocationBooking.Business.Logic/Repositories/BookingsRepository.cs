using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    class BookingsRepository : RepositoryBase, IRepository<DbBooking>
    {
        public void Add(DbBooking entity)
        {
            _dbContext.Bookings.Add(entity);
            _dbContext.SaveChanges();
        }

        public DbBooking Get(int id)
        {
            return _dbContext.Bookings.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<DbBooking> GetAllByUserId(int userId)
        {
            return _dbContext.Bookings.Where(x => x.UserId == userId).ToList();
        }

        public IList<DbBooking> GetAll()
        {
            return _dbContext.Bookings.ToList();
        }

        public void Remove(DbBooking entity)
        {
            _dbContext.Bookings.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(DbBooking entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
