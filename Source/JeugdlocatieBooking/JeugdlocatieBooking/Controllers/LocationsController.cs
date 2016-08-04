using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class LocationsController : Controller
    {
        private LocationsRepository _locationsRepository;

        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
        }

        public ActionResult Index(LocationFilterFormValidationModel model = null)
        {
            if (model == null)
                ViewBag.Locations = _locationsRepository.GetAll();
            else
                ViewBag.Locations = _locationsRepository.GetAllWithFilter(model);

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(LocationFilterFormValidationModel model)
        {
            // This is a hack used to keep a clean URL.
            // If the form is of FormMethod.Get it will send all fields as query string, this is default behavior
            // However by using POST and redirecting to the GET action you push the values through MVC routing, which filters out null values
            return RedirectToAction("Index", "Locations", model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}