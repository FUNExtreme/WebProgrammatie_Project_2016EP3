using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Enumerations;
using YouthLocationBooking.Data.Database.Repositories;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        #region Variables
        private UnitOfWork _unitOfWork { get; set; }
        #endregion

        #region Constructor
        public RequestsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        public ActionResult Index()
        {
            var usersRepository = _unitOfWork.UsersRepository;
            var bookingsRepository = _unitOfWork.BookingsRepository;

            IList<DbBooking> bookings = bookingsRepository.GetAllByLocationUserId(usersRepository.GetByEmail(User.Identity.Name).Id);
            ViewBag.PendingBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Pending);
            ViewBag.AcceptedBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Accepted);
            ViewBag.DeniedOrCancelledBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Denied || x.StatusId == (int)EBookingStatus.Cancelled);
            return View();
        }
    }
}