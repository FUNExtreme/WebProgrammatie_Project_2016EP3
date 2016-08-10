using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticateAttribute]
    public class ReviewsController : Controller
    {
        #region Variables
        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ReviewsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        public ActionResult Index()
        {
            var bookingsRepository = _unitOfWork.BookingsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbUser user = usersRepository.GetByEmail(User.Identity.Name);

            IList<DbBooking> endedBookings = bookingsRepository.GetAfterEndDateByUserId(user.Id);
            ViewBag.EndedBookings = endedBookings;
            return View();
        }

        #region Dispose
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