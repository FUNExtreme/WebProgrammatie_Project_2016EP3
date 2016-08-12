using System.Web.Mvc;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}