using System;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.Validation.Mappings;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        #region Variables
        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        public ActionResult Index()
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            ViewBag.Locations = locationsRepository.GetAllByUserId(usersRepository.GetByEmail(User.Identity.Name).Id);
            return View();
        }

        #region New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(LocationNewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbLocation dbLocation = model.ToDbEntity();
            dbLocation.CreatedByUserId = usersRepository.GetByEmail(User.Identity.Name).Id;
            locationsRepository.Insert(dbLocation);

            return RedirectToAction("Details", "Locations", new { id = dbLocation.Id, Area = string.Empty });
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Summary");

            LocationEditViewModel locationModel = location.ToLocationEditValidationModel();

            ViewBag.LocationId = location.Id;
            return View(locationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEditViewModel model)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            if (!ModelState.IsValid)
                return View(model);

            DbLocation location = locationsRepository.Get(id);
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
            locationsRepository.Update(location);

            ViewBag.LocationId = location.Id;
            return View(model);
        }
        #endregion

        #region Remove
        public ActionResult Remove(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");

            DbUser user = usersRepository.GetByEmail(User.Identity.Name);
            if(user.Id != location.CreatedByUserId)
                return RedirectToAction("Index", "Locations");

            ViewBag.Location = location;
            return View();
        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult RemovePost(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");

            DbUser user = usersRepository.GetByEmail(User.Identity.Name);
            if (user.Id != location.CreatedByUserId)
                return RedirectToAction("Index", "Locations");

            locationsRepository.Remove(location);
            // TODO success message
            return RedirectToAction("Index", "Locations");
        }
        #endregion

        #region Reviews
        public ActionResult Review(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbLocation location = locationsRepository.Get(id);
            ViewBag.Location = location;
            if (location == null)
                return RedirectToAction("Index", "Reviews");

            // TODO Verify that the current user has a booking there
             
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(int id, LocationReviewViewModel model)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;
            var locationReviewsRepository = _unitOfWork.LocationReviewsRepository;

            DbLocation location = locationsRepository.Get(id);
            ViewBag.Location = location;
            if (location == null)
                return RedirectToAction("Index", "Reviews");

            DbUser user = usersRepository.GetByEmail(User.Identity.Name);
            // TODO Verify that the current user has a booking there

            if (!ModelState.IsValid)
                return View(model);

            DbLocationReview review = new DbLocationReview();
            review.DateTime = DateTime.Now;
            review.LocationId = id;
            review.Review = model.Review;
            review.ReviewerName = user.FirstName + " " + user.LastName[0];
            review.Title = model.Title;
            // TODO facility ratings
            locationReviewsRepository.Insert(review);

            // TODO success message
            return RedirectToAction("Index", "Reviews");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}