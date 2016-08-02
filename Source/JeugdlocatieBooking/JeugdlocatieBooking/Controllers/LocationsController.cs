using System.Web.Mvc;

namespace YouthLocationBooking.Web.Controllers
{
    public class LocationsController : Controller
    {
        [Route()]
        public ActionResult Index()
        {
            return View();
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}