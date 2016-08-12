using System.Web.Mvc;
using YouthLocationBooking.Web.Code;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
    public class SummaryController : UnitOfWorkControllerBase
    {
        public ActionResult Index()
        {
            var bookingsRepository = _unitOfWork.BookingsRepository;
            ViewBag.Bookings = bookingsRepository.GetAllByUserId(((AuthenticatedUser)User).Id);

            return View();
        }
    }
}