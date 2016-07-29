using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouthLocationBooking.Areas.Panel.Controllers
{
    public class LocationsController : Controller
    {
        // GET: Panel/Locations
        public ActionResult Index()
        {
            return View();
        }
    }
}