using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.ViewModel.Models;
using YouthLocationBooking.Web.Code;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
    public class LocationsController : UnitOfWorkControllerBase
    {
        #region Constructor
        public LocationsController()
            : base()
        {
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            ViewBag.Locations = locationsRepository.GetAllByUserId(((AuthenticatedUser)User).Id);

            return View();
        }
        #endregion

        #region New
        public ActionResult New()
        {
            return RedirectToActionPermanent("NewStep1");
        }

        #region New Step 1 - General Info
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
        #endregion

        #region New Step 2 - Address
        public ActionResult NewStep2()
        {
            if (!VerifyDataExists(step: 2))
                return RedirectToAction("New");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep2(LocationNewAddressViewModel model)
        {
            if (!VerifyDataExists(step: 2))
                return RedirectToAction("New");

            if (!ModelState.IsValid)
                return View(model);

            TempData["NewLocationStepTwoData"] = model;

            return RedirectToAction("NewStep3", "Locations");
        }
        #endregion

        #region New Step 3 - Facilities
        [NonAction]
        public ActionResult NewStep3(LocationNewFacilitiesViewModel model = null)
        {
            var locationFacilitiesRepository = _unitOfWork.LocationFacilitiesRepository;
            ViewBag.Facilities = locationFacilitiesRepository.GetAll();

            return View();
        }

        public ActionResult NewStep3()
        {
            if (!VerifyDataExists(step: 3))
                return RedirectToAction("New");

            return NewStep3();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("NewStep3")]
        public ActionResult PostNewStep3(LocationNewFacilitiesViewModel model)
        {
            if (!VerifyDataExists(step: 3))
                return RedirectToAction("New");

            if (!ModelState.IsValid)
                return NewStep3(model);

            TempData["NewLocationStepThreeData"] = model;

            return RedirectToAction("NewStep4", "Locations");
        }
        #endregion

        #region Step 4 - Pictures
        public ActionResult NewStep4()
        {
            if (!VerifyDataExists(step: 4))
                return RedirectToAction("New");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStep4(LocationNewImagesViewModel model)
        {
            if (!VerifyDataExists(step: 4))
                return RedirectToAction("New");

            LocationNewGeneralViewModel generalData = (LocationNewGeneralViewModel)TempData["NewLocationStepOneData"];
            LocationNewAddressViewModel addressData = (LocationNewAddressViewModel)TempData["NewLocationStepTwoData"];
            LocationNewFacilitiesViewModel facilitiesData = (LocationNewFacilitiesViewModel)TempData["NewLocationStepThreeData"];

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var locationsRepository = _unitOfWork.LocationsRepository;
                var locationsFacilitiesRepository = _unitOfWork.LocationFacilitiesRepository;

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
                dbLocation.CreatedByUserId = ((AuthenticatedUser)User).Id;

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

                string bannerImageFileName = null;
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

                    if (bannerImageFileName == null)
                        bannerImageFileName = fileName;
                }

                dbLocation.BannerImageFileName = bannerImageFileName;
                locationsRepository.Update(dbLocation);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De locatie is succesvol aangemaakt";

                return RedirectToAction("Details", "Locations", new { id = dbLocation.Id, Area = string.Empty });
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van de nieuwe locatie!";

                // We've previously accessed our data, indicate that we want to keep it
                TempData["NewLocationStepOneData"] = generalData;
                TempData["NewLocationStepTwoData"] = addressData;
                TempData["NewLocationStepThreeData"] = facilitiesData;

                return View(model);
            }
        }
        #endregion
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            DbLocation location = GetLocationIfExistsAndUserHasAccess(id);
            if (location == null)
                return RedirectToAction("Index");

            ViewBag.Location = location;
            ViewBag.LocationId = location.Id;

            LocationEditViewModel locationModel = location.ToLocationEditValidationModel();
            return View(locationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult EditPost(int id, LocationEditViewModel model)
        {
            try
            {
                DbLocation location = GetLocationIfExistsAndUserHasAccess(id);
                if (location == null)
                    return RedirectToAction("Index");

                if (!ModelState.IsValid)
                    return View(model);

                if (location == null)
                    return RedirectToAction("Index", "Summary");

                LocationsRepository locationsRepository = _unitOfWork.LocationsRepository;
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

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De aanpassingen zijn correct opgeslagen.";

                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van de aanpassingen!";

                return View(model);
            }
        }
        #endregion

        #region Remove
        public ActionResult Remove(int? id)
        {
            DbLocation location = GetLocationIfExistsAndUserHasAccess(id);
            if (location == null)
                return RedirectToAction("Index");

            ViewBag.Location = location;

            return View();
        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult RemovePost(int id)
        {
            try
            {
                DbLocation location = GetLocationIfExistsAndUserHasAccess(id);
                if (location == null)
                    return RedirectToAction("Index");

                LocationsRepository locationsRepository = _unitOfWork.LocationsRepository;
                locationsRepository.Remove(location);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De locatie is verwijderd!";
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwijderen van de locatie!";

                return View();
            }
            
            return RedirectToAction("Index", "Locations");
        }
        #endregion

        #region Reviews
        public ActionResult Review(int id)
        {
            DbLocation location = GetLocationIfExists(id);
            if (location == null)
                return RedirectToAction("Index", "Reviews");

            BookingsRepository bookingsRepository = _unitOfWork.BookingsRepository;
            IList<DbBooking> booking = bookingsRepository.GetAllByUserIdAndLocationId(((AuthenticatedUser)User).Id, id);
            if (booking == null || !booking.Any())
                return RedirectToAction("Index", "Reviews");

            ViewBag.Location = location;
             
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(int id, LocationReviewViewModel model)
        {
            try
            {
                DbLocation location = GetLocationIfExists(id);
                if (location == null)
                    return RedirectToAction("Index", "Reviews");

                AuthenticatedUser user = (AuthenticatedUser)User;

                BookingsRepository bookingsRepository = _unitOfWork.BookingsRepository;
                IList<DbBooking> booking = bookingsRepository.GetAllByUserIdAndLocationId(user.Id, id);
                if (booking == null || !booking.Any())
                    return RedirectToAction("Index", "Reviews");

                if (!ModelState.IsValid)
                    return View(model);

                LocationReviewsRepository locationReviewsRepository = _unitOfWork.LocationReviewsRepository;
                DbLocationReview review = new DbLocationReview();
                review.DateTime = DateTime.Now;
                review.LocationId = id;
                review.Review = model.Review;
                review.ReviewerName = user.FirstName + " " + user.LastName[0];
                review.Title = model.Title;
                // We insert our review before storing the facility ratings so we have a review id
                locationReviewsRepository.Insert(review);

                review.FacilityRatings = model.FacilityRatings.Select(x =>
                {
                    return new DbLocationFacilityRating
                    {
                        FacilityId = x.Id,
                        Rating = x.Rating,
                        ReviewId = review.Id
                    };
                }).ToList();
                locationReviewsRepository.Update(review);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De review is succesvol toegevoegd aan de locatie.";

                return RedirectToAction("Index", "Reviews");
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van de review!";

                return View(model);
            }
        }
        #endregion

        private DbLocation GetLocationIfExists(int? id)
        {
            if (id == null)
                return null;

            var locationsRepository = _unitOfWork.LocationsRepository;
            DbLocation location = locationsRepository.Get((int)id);
            if (location == null)
                return null;

            return location;
        }

        private DbLocation GetLocationIfExistsAndUserHasAccess(int? id)
        {
            DbLocation location = GetLocationIfExists(id);
            if (location == null)
                return null;

            if (location.CreatedByUserId == ((AuthenticatedUser)User).Id)
                return null;

            return location;
        }

        private bool VerifyDataExists(int step = 1)
        {
            if (step < 2 || step > 4)
                return true;

            if(step >= 2)
            {
                if (TempData.Peek("NewLocationStepOneData") == null)
                    return false;
            }

            if (step >= 3)
            {
                if (TempData.Peek("NewLocationStepTwoData") == null)
                    return false;
            }

            if (step >= 4)
            {
                if (TempData.Peek("NewLocationStepThreeData") == null)
                    return false;
            }

            return true;
        }
    }
}