using System.Web.Mvc;

namespace YouthLocationBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
