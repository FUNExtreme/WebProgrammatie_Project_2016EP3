using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Mappings;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
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

        #region Details
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(NewLocationValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DbLocation dbLocation = model.ToDbEntity();
            // TODO set id to logged in user
            dbLocation.CreatedByUserId = null;

            using (var repository = new LocationsRepository())
            {
                repository.Add(dbLocation);
            }

            return RedirectToAction("Details", "Locations", new { id = dbLocation.Id, Area = string.Empty });
        }
        #endregion
    }
}