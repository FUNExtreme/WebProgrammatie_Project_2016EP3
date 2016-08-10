using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Enumerations;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.ViewModel.Models;
using static YouthLocationBooking.Data.Database.Repositories.LocationsRepository;

namespace YouthLocationBooking.Web.Controllers
{
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

        public ActionResult Index(LocationFilterViewModel model = null, int page = 1)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            if (page < 1)
                page = 1;

            int itemsPerPage = 10;

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

        public ActionResult Details(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var locationReviewsRepository = _unitOfWork.LocationReviewsRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");
            ViewBag.Location = location;

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
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id, LocationBookingViewModel model)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;
            var bookingsRepository = _unitOfWork.BookingsRepository;
            var locationReviewsRepository = _unitOfWork.LocationReviewsRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");
            ViewBag.Location = location;

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

            if (model.From > model.To)
            {
                ModelState.AddModelError("From", "Van datum moet voor Tot datum liggen");
                return View(model);
            }

            DbBooking booking = new DbBooking();
            booking.LocationId = id;
            booking.Organisation = model.Organisation;
            booking.StartDateTime = model.From;
            booking.EndDateTime = model.To;
            booking.StatusId = (int)EBookingStatus.Pending;
            booking.UserId = usersRepository.GetByEmail(User.Identity.Name).Id;
            bookingsRepository.Insert(booking);

            // TODO Success message
            return View(model);
        }
    }
}