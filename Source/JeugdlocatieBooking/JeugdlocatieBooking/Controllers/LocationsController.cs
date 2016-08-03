using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Web.Controllers
{
    public class LocationsController : Controller
    {
        [Route()]
        public ActionResult Index()
        {
            IList<DbLocation> dbLocations = null;
            using (var repository = new LocationsRepository())
            {
                dbLocations = repository.GetAll();
            }
            ViewBag.Locations = dbLocations;

            return View();
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}