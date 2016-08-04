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
        private UsersRepository _usersRepository;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
            _usersRepository = new UsersRepository();
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
        public ActionResult New(LocationNewFormValidationModel model)
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