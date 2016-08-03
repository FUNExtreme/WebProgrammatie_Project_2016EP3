using System;
using YouthLocationBooking.Data.Database;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class RepositoryBase : IDisposable
    {
        #region Variables
        protected DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public RepositoryBase()
        {
            _dbContext = new DatabaseContext();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
        #endregion
    }
}
