using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        // GET: Panel/Ratings
        public ActionResult Index()
        {
            return View();
        }
    }
}