using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Enumerations;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
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

        #region Index
        public ActionResult Index()
        {
            try
            {
                var bookingsRepository = _unitOfWork.BookingsRepository;
                IList<DbBooking> bookings = bookingsRepository.GetAllByLocationUserId(((AuthenticatedUser)User).Id);
                ViewBag.PendingBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Pending);
                ViewBag.AcceptedBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Accepted);
                ViewBag.DeniedOrCancelledBookings = bookings.Where(x => x.StatusId == (int)EBookingStatus.Denied || x.StatusId == (int)EBookingStatus.Cancelled);
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het ophalen van de verzoeken!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        #endregion

        #region Deny
        public ActionResult Deny(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ActionName("Deny")]
        public ActionResult DenyPost(int? id)
        {
            try
            {
                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "Het verzoek werd afgewezen!";

                return SetStatusIfExistsAndUserIsOwner(id, EBookingStatus.Denied);
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van het verzoek!";

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Accept
        public ActionResult Accept(int? id)
        {
            try
            {
                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "Het verzoek werd geaccepteerd!";

                return SetStatusIfExistsAndUserIsOwner(id, EBookingStatus.Accepted);
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van het verzoek!";

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Helper
        private ActionResult SetStatusIfExistsAndUserIsOwner(int? bookingId, EBookingStatus status)
        {
            if (bookingId == null)
                return RedirectToAction("Index");

            var bookingsRepository = _unitOfWork.BookingsRepository;
            DbBooking booking = bookingsRepository.Get((int)bookingId);
            if (booking == null)
                return RedirectToAction("Index");

            AuthenticatedUser user = (AuthenticatedUser)User;
            if (user.Id != booking.Location.CreatedByUserId)
                return RedirectToAction("Index");

            booking.StatusId = (int)status;
            bookingsRepository.Update(booking);

            return RedirectToAction("Index");
        }
        #endregion
    }
}