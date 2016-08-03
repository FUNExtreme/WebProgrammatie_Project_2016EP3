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
        #region Variables
        private LocationsRepository _locationsRepository;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            ViewBag.Locations = _locationsRepository.GetAll();

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
        public ActionResult New(NewLocationFormValidationModel model)
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

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_locationsRepository != null)
                {
                    _locationsRepository.Dispose();
                    _locationsRepository = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}