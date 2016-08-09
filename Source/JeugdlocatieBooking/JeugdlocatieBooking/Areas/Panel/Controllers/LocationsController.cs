using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;
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
            return RedirectToAction("NewStep1", "Locations");
        }

        /// <summary>
        /// General info
        /// </summary>
        /// <returns></returns>
        public ActionResult NewStep1()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep1(LocationNewGeneralViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            TempData["NewLocationStepOneData"] = model;

            return RedirectToAction("NewStep2", "Locations");
        }

        /// <summary>
        /// Address
        /// </summary>
        /// <returns></returns>
        public ActionResult NewStep2()
        {
            if (TempData.Peek("NewLocationStepOneData") == null)
                return RedirectToAction("New", "Locations");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep2(LocationNewAddressViewModel model)
        {
            if (TempData.Peek("NewLocationStepOneData") == null)
                return RedirectToAction("New", "Locations");

            if (!ModelState.IsValid)
                return View(model);

            TempData["NewLocationStepTwoData"] = model;

            return RedirectToAction("NewStep3", "Locations");
        }

        /// <summary>
        /// Capabilities
        /// </summary>
        /// <returns></returns>
        public ActionResult NewStep3()
        {
            if (TempData.Peek("NewLocationStepOneData") == null)
                return RedirectToAction("New", "Locations");

            if (TempData.Peek("NewLocationStepTwoData") == null)
                return RedirectToAction("New", "Locations");

            var locationFacilitiesRepository = _unitOfWork.LocationFacilitiesRepository;
            var facilities = locationFacilitiesRepository.GetAll();

            ViewBag.Facilities = facilities;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep3(LocationNewFacilitiesViewModel model)
        {
            if (TempData.Peek("NewLocationStepOneData") == null)
                return RedirectToAction("New", "Locations");

            if (TempData.Peek("NewLocationStepTwoData") == null)
                return RedirectToAction("New", "Locations");

            if (!ModelState.IsValid)
                return View(model);

            TempData["NewLocationStepThreeData"] = model;

            return RedirectToAction("NewStep4", "Locations");
        }

        /// <summary>
        /// Pictures
        /// </summary>
        /// <returns></returns>
        public ActionResult NewStep4()
        {
            if (TempData.Peek("NewLocationStepOneData") == null)
                return RedirectToAction("New", "Locations");

            if (TempData.Peek("NewLocationStepTwoData") == null)
                return RedirectToAction("New", "Locations");

            if (TempData.Peek("NewLocationStepThreeData") == null)
                return RedirectToAction("New", "Locations");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep4(LocationNewImagesViewModel model)
        {
            LocationNewGeneralViewModel generalData = (LocationNewGeneralViewModel)TempData["NewLocationStepOneData"];
            if (generalData == null)
                return RedirectToAction("New", "Locations");

            LocationNewAddressViewModel addressData = (LocationNewAddressViewModel)TempData["NewLocationStepTwoData"];
            if (addressData == null)
                return RedirectToAction("New", "Locations");

            LocationNewFacilitiesViewModel facilitiesData = (LocationNewFacilitiesViewModel)TempData["NewLocationStepThreeData"];
            if (TempData.Peek("NewLocationStepThreeData") == null)
                return RedirectToAction("New", "Locations");

            if (!ModelState.IsValid)
                return View(model);

            var locationsRepository = _unitOfWork.LocationsRepository;
            var locationsFacilitiesRepository = _unitOfWork.LocationFacilitiesRepository;
            var usersRepository = _unitOfWork.UsersRepository;

            DbLocation dbLocation = new DbLocation();
            dbLocation.Name = generalData.Name;
            dbLocation.Description = generalData.Description;
            dbLocation.Capacity = generalData.Capacity;
            dbLocation.Organisation = generalData.Organisation;
            dbLocation.PricePerDay = generalData.PricePerDay;
            dbLocation.AddressCity = addressData.AddressCity;
            dbLocation.AddressNumber = addressData.AddressNumber;
            dbLocation.AddressPostalCode = addressData.AddressPostalCode;
            dbLocation.AddressProvince = addressData.AddressProvince;
            dbLocation.AddressStreet = addressData.AddressStreet;
            dbLocation.CreatedByUserId = usersRepository.GetByEmail(User.Identity.Name).Id;

            dbLocation.Facilities = facilitiesData.SelectedDatabaseIds.Select(x =>
            {
                DbLocationFacility facility = new DbLocationFacility { Id = x };
                locationsFacilitiesRepository.Attach(facility);
                return facility;
            }).ToList();

            // Save the location first because we want the location Id to store our images
            locationsRepository.Insert(dbLocation);

            string locationImagesBasePath = HostingEnvironment.MapPath("~/Resources/Location/" + dbLocation.Id + "/Images/");
            Directory.CreateDirectory(locationImagesBasePath);

            foreach (var file in model.Images)
            {
                if (file == null || file.ContentLength <= 0)
                    continue;

                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                    continue;

                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(locationImagesBasePath, fileName);
                file.SaveAs(filePath);
            }

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