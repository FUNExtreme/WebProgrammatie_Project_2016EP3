using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Enumerations;
using YouthLocationBooking.Data.ViewModel.Models;
using YouthLocationBooking.Web.Code;
using YouthLocationBooking.Web.Code.Auth;
using static YouthLocationBooking.Data.Database.Repositories.LocationsRepository;

namespace YouthLocationBooking.Web.Controllers
{
    public class LocationsController : UnitOfWorkControllerBase
    {
        #region Constructor
        public LocationsController()
            : base()
        {
        }
        #endregion

        #region Index
        public ActionResult Index(LocationFilterViewModel model = null, int page = 1)
        {
            int itemsPerPage = 10;

            if (page < 1)
                page = 1;

            var locationsRepository = _unitOfWork.LocationsRepository;

            if (model == null)
                ViewBag.PagedLocations = locationsRepository.GetAllPaged(page, itemsPerPage);
            else
            {
                LocationFilter filter = new LocationFilter();
                filter.Name = model.Name;
                filter.CityOrPostcode = model.CityOrPostcode;
                filter.From = model.From;
                filter.MinCapacity = model.MinCapacity;
                filter.Province = model.Province;
                filter.To = model.To;
                ViewBag.PagedLocations = locationsRepository.GetAllPagedWithFilter(page, itemsPerPage, filter);
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(LocationFilterViewModel model)
        {
            // This is a hack used to keep a clean URL.
            // If the form is of FormMethod.Get it will send all fields as query string, this is default behavior
            // However by using POST and redirecting to the GET action you push the values through MVC routing, which filters out null values
            return RedirectToAction("Index", "Locations", model);
        }
        #endregion

        #region Details
        [NonAction]
        private ActionResult Details(int id, LocationBookingViewModel model)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            DbLocation location = locationsRepository.Get(id);
            ViewBag.Location = location;

            var locationReviewsRepository = _unitOfWork.LocationReviewsRepository;
            IList<DbLocationReview> locationReviews = locationReviewsRepository.GetByLocationId(id);
            ViewBag.LocationReviews = locationReviews;

            string locationImagesDirectoryPath = HostingEnvironment.MapPath("~/Resources/Location/" + location.Id + "/Images/");
            string[] locationImagesPaths = new string[0];
            if (Directory.Exists(locationImagesDirectoryPath))
            {
                locationImagesPaths = Directory.GetFiles(locationImagesDirectoryPath);
                for (int x = 0; x < locationImagesPaths.Length; x++)
                    locationImagesPaths[x] = locationImagesPaths[x].Replace(HostingEnvironment.ApplicationPhysicalPath, "/");
            }
            ViewBag.LocationImagesPaths = locationImagesPaths;

            string calenderEventArray = "[";
            foreach (DbBooking booking in location.Bookings)
            {
                if (booking.StatusId != (int)EBookingStatus.Cancelled && booking.StatusId != (int)EBookingStatus.Denied)
                    calenderEventArray += "{ startDate: '" + booking.StartDateTime.Date.ToString("yyyy-MM-dd") + "', endDate: '" + booking.EndDateTime.Date.ToString("yyyy-MM-dd") + "'},";
            }
            calenderEventArray += "]";
            ViewBag.CalenderEventArray = calenderEventArray;

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (!DoesLocationExist(id))
                return RedirectToAction("Index");

            return Details((int)id, null);
        }

        [YLBAuthenticate]
        [HttpPost]
        [ActionName("Details")]
        public ActionResult PostDetails(int? id, LocationBookingViewModel model)
        {
            if (!DoesLocationExist(id))
                return RedirectToAction("Index");

            if (model.From > model.To)
                ModelState.AddModelError("From", "Van datum moet voor Tot datum liggen");

            if (!ModelState.IsValid)
                return Details((int)id, model);

            try
            {
                var bookingsRepository = _unitOfWork.BookingsRepository;
                DbBooking booking = new DbBooking();
                booking.LocationId = (int)id;
                booking.Organisation = model.Organisation;
                booking.StartDateTime = model.From;
                booking.EndDateTime = model.To;
                booking.StatusId = (int)EBookingStatus.Pending;
                booking.UserId = ((AuthenticatedUser)User).Id;
                bookingsRepository.Insert(booking);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De aanvraag om deze locatie te boeken van " + model.From.Date + " tot " + model.To.Date + " is successvol ingediend.";
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van de boeking!";
                return Details(id, model);
            }

            return RedirectToAction("Details", new { id = id });
        }
        #endregion

        private bool DoesLocationExist(int? id)
        {
            if (id == null)
                return false;

            var locationsRepository = _unitOfWork.LocationsRepository;
            DbLocation location = locationsRepository.Get((int)id);
            if (location == null)
                return false;

            return true;
        }
    }
}