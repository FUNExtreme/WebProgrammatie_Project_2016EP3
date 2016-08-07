using System;
using System.Data.Entity;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DbContext _dbContext = new DatabaseContext();

        private UsersRepository _usersRepository;
        private LocationsRepository _locationsRepository;
        private LocationReviewsRepository _locationReviewsRepository;
        private BookingsRepository _bookingsRepository;

        public UsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_dbContext);

                return _usersRepository;
            }
        }

        public LocationsRepository LocationsRepository
        {
            get
            {
                if (_locationsRepository == null)
                    _locationsRepository = new LocationsRepository(_dbContext);

                return _locationsRepository;
            }
        }

        public LocationReviewsRepository LocationReviewsRepository
        {
            get
            {
                if (_locationReviewsRepository == null)
                    _locationReviewsRepository = new LocationReviewsRepository(_dbContext);

                return _locationReviewsRepository;
            }
        }

        public BookingsRepository BookingsRepository
        {
            get
            {
                if (_bookingsRepository == null)
                    _bookingsRepository = new BookingsRepository(_dbContext);

                return _bookingsRepository;
            }
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
