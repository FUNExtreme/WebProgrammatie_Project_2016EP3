using System.Web.Mvc;
using System.Web.Security;

namespace YouthLocationBooking.Web.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}