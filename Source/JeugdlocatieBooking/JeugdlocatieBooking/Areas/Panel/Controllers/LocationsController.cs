using System.Web.Mvc;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    public class LocationsController : Controller
    {
        // GET: Panel/Locations
        public ActionResult Index()
        {
            return View();
        }

        #region New
        public ActionResult New()
        {
            return View();
        }
        #endregion
    }
}