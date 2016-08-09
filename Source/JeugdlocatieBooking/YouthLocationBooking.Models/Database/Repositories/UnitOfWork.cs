using System;
using System.Data.Entity;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class UnitOfWork : IDisposable
    {
        #region Variables
        private DbContext _dbContext = new DatabaseContext();

        private UsersRepository _usersRepository;
        private LocationsRepository _locationsRepository;
        private LocationReviewsRepository _locationReviewsRepository;
        private LocationFacilitiesRepository _locationFacilitiesRepository;
        private BookingsRepository _bookingsRepository;
        #endregion

        #region Property
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
        public LocationFacilitiesRepository LocationFacilitiesRepository
        {
            get
            {
                if (_locationFacilitiesRepository == null)
                    _locationFacilitiesRepository = new LocationFacilitiesRepository(_dbContext);

                return _locationFacilitiesRepository;
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
        #endregion

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
