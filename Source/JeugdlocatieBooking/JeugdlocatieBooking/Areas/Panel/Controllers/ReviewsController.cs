using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Web.Code;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
    public class ReviewsController : UnitOfWorkControllerBase
    {
        #region Constructor
        public ReviewsController()
            : base()
        {
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            var bookingsRepository = _unitOfWork.BookingsRepository;

            AuthenticatedUser user = (AuthenticatedUser)User;
            IList<DbBooking> endedBookings = bookingsRepository.GetAfterEndDateByUserId(user.Id);
            ViewBag.EndedBookings = endedBookings;

            return View();
        }
        #endregion
    }
}