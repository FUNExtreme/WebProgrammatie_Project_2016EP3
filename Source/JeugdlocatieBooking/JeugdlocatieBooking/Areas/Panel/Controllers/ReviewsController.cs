using System.Web.Mvc;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}