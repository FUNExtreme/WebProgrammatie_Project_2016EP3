using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Enumerations;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticateAttribute]
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

        #region Deny
        public ActionResult Deny(int id)
        {
            return View();
        }

        [HttpPost]
        [ActionName("Deny")]
        public ActionResult DenyPost(int id)
        {
            return VerifyOwnershipAndSetStatus(id, EBookingStatus.Denied);
        }
        #endregion

        #region Accept
        public ActionResult Accept(int id)
        {
            return VerifyOwnershipAndSetStatus(id, EBookingStatus.Accepted);
        }
        #endregion

        #region Helper
        private ActionResult VerifyOwnershipAndSetStatus(int bookingId, EBookingStatus status)
        {
            var usersRepository = _unitOfWork.UsersRepository;
            var bookingsRepository = _unitOfWork.BookingsRepository;

            DbBooking booking = bookingsRepository.Get(bookingId);
            if (booking == null)
                return RedirectToAction("Index", "Requests");

            DbUser user = usersRepository.GetByEmail(User.Identity.Name);
            if (user.Id != booking.Location.CreatedByUserId)
                return RedirectToAction("Index", "Requests");

            booking.StatusId = (int)status;
            bookingsRepository.Update(booking);
            // TODO message
            return RedirectToAction("Index", "Requests");
        }
        #endregion
    }
}