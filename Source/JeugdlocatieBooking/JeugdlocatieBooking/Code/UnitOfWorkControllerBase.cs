using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Repositories;

namespace YouthLocationBooking.Web.Code
{
    public class UnitOfWorkControllerBase : Controller
    {
        #region Variables
        protected UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UnitOfWorkControllerBase()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region IDispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}