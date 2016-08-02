using System;
using YouthLocationBooking.Data.Database;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class RepositoryBase : IDisposable
    {
        protected DatabaseContext _dbContext;

        public RepositoryBase()
        {
            _dbContext = new DatabaseContext();
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
