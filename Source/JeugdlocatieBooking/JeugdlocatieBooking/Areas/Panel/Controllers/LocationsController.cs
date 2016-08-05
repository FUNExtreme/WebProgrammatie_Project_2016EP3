using System.Collections.Generic;
using System.Web.Mvc;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Validation.Mappings;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        #region Variables
        private LocationsRepository _locationsRepository;
        private UsersRepository _usersRepository;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
            _usersRepository = new UsersRepository();
        }
        #endregion

        #region New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(LocationNewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DbLocation dbLocation = model.ToDbEntity();
            dbLocation.CreatedByUserId = _usersRepository.GetByEmail(User.Identity.Name).Id;
            using (var repository = new LocationsRepository())
            {
                repository.Add(dbLocation);
            }

            return RedirectToAction("Details", "Locations", new { id = dbLocation.Id, Area = string.Empty });
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            DbLocation location = _locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Summary");

            LocationEditModel locationModel = location.ToLocationEditValidationModel();

            ViewBag.LocationId = location.Id;
            return View(locationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DbLocation location = _locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Summary");

            location.Name = model.Name;
            location.Description = model.Description;
            location.Organisation = model.Organisation;
            location.PricePerDay = model.PricePerDay;
            location.Capacity = model.Capacity;
            location.AddressCity = model.AddressCity;
            location.AddressNumber = model.AddressNumber;
            location.AddressPostalCode = model.AddressPostalCode;
            location.AddressProvince = model.AddressProvince;
            location.AddressStreet = model.AddressStreet;
            _locationsRepository.Update(location);

            ViewBag.LocationId = location.Id;
            return View(model);
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
                if(_usersRepository != null)
                {
                    _usersRepository.Dispose();
                    _usersRepository = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}