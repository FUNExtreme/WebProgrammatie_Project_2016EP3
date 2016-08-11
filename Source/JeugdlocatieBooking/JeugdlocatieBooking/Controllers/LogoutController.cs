using System.Web.Mvc;
using System.Web.Security;

namespace YouthLocationBooking.Web.Controllers
{
    public class LogoutController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}