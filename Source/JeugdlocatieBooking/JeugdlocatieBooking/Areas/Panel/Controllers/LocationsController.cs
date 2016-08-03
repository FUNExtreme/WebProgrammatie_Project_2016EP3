using System.Web.Mvc;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(NewLocationValidationModel model)
        {
            return View(model);
        }
        #endregion
    }
}