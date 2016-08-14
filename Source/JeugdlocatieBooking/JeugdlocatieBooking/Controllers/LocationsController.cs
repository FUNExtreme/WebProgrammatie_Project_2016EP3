using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using X.PagedList;
using YouthLocationBooking.Data.API;
using YouthLocationBooking.Data.API.Entities.ThirdParty;
using YouthLocationBooking.Data.API.Mappings;
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
        public async Task<ActionResult> Index(LocationFilterViewModel model = null, int page = 1)
        {
            int itemsPerPage = 10;

            if (page < 1)
                page = 1;

            var locationsRepository = _unitOfWork.LocationsRepository;

            IPagedList<DbLocation> pagedLocations = null;
            LocationFilter filter = new LocationFilter();
            if (model == null)
                pagedLocations = locationsRepository.GetAllPaged(page, itemsPerPage);
            else
            {
                filter.Name = model.Name;
                filter.CityOrPostcode = model.CityOrPostcode;
                filter.From = model.From;
                filter.MinCapacity = model.MinCapacity;
                filter.Province = model.Province;
                filter.To = model.To;
                pagedLocations = locationsRepository.GetAllPagedWithFilter(page, itemsPerPage, filter);
            }
            ViewBag.PagedLocations = pagedLocations;

            // If there are no results, we try to get some from our partners
            ViewBag.PagedThirdPartyLocations = new List<ThirdPartyLocationOverviewViewModel>();
            if (pagedLocations.TotalItemCount == 0)
            {
                List<ThirdPartyLocationOverviewViewModel> thirdPartyLocations = new List<ThirdPartyLocationOverviewViewModel>();

                ApiClient apiClient = new ApiClient();
                // Tim
                string urlTim = GenerateUrlWithFilter("http://jeugdlocatie.timvettori.ikdoeict.net/Api/SharedLocations/", filter, cityOrPostCodeParamName: "location", fromParamName: "startDateTime", toParamName: "endDateTime");
                IEnumerable<ApiLocationTim> locationsTim = await apiClient.Request<IEnumerable<ApiLocationTim>>(urlTim);
                IEnumerable<ThirdPartyLocationOverviewViewModel> mappedLocationsTim = locationsTim.Select(x =>
                {
                    return x.ToViewModel();
                });
                thirdPartyLocations.AddRange(mappedLocationsTim);

                // Diede
                string urlDiede = GenerateUrlWithFilter("http://diedeseldeslachts.ikdoeict.net/api/LocationSearchAPI/Get", filter, cityOrPostCodeParamName: "cityOrPostcode", fromParamName: "startDateTime", toParamName: "endDateTime");
                IEnumerable <ApiLocationDiede> locationsDiede = await apiClient.Request<IEnumerable<ApiLocationDiede>>(urlDiede);
                IEnumerable<ThirdPartyLocationOverviewViewModel> mappedLocationsDiede = locationsDiede.Select(x =>
                {
                    return x.ToViewModel();
                });
                thirdPartyLocations.AddRange(mappedLocationsDiede);

                ViewBag.PagedThirdPartyLocations = thirdPartyLocations.ToPagedList(page, itemsPerPage);
            }

            model.Page = page;
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
        private ActionResult Details(int id, LocationBookingViewModel model = null)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            DbLocation location = locationsRepository.Get(id);
            ViewBag.Location = location;

            var locationReviewsRepository = _unitOfWork.LocationReviewsRepository;
            IList<DbLocationReview> locationReviews = locationReviewsRepository.GetAllByLocationId(id);
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

            return Details((int)id);
        }

        [YLBAuthenticate]
        [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDetails(int? id, LocationBookingViewModel model)
        {
            if (!DoesLocationExist(id))
                return RedirectToAction("Index");

            if (model.From > model.To)
                ModelState.AddModelError("From", "Van datum moet voor Tot datum liggen");

            if (!ModelState.IsValid)
                return Details((int)id, model);

            // Only allow booking if the location is not yet booked during this period
            var bookingsRepository = _unitOfWork.BookingsRepository;
            bool isAlreadyBooked = bookingsRepository.IsLocationBookedDuringPeriod((int)id, model.From, model.To);
            if(isAlreadyBooked)
            {
                ModelState.AddModelError(string.Empty, "Locatie is al geboekt in deze periode");
                return Details((int)id, model);
            }

            try
            {
                
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
                return Details((int)id, model);
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

        private string GenerateUrlWithFilter(string baseUrl, LocationFilter filter, string cityOrPostCodeParamName, string fromParamName, string toParamName)
        {
            string fullUrl = baseUrl + "?";

            if(filter.CityOrPostcode != null)
                fullUrl += cityOrPostCodeParamName + "=" + filter.CityOrPostcode + "&";
            if(filter.From != null)
                fullUrl += fromParamName + "=" + filter.From + "&";
            if(filter.To != null)
                fullUrl += toParamName + "=" + filter.To + "&";

            fullUrl = fullUrl.Remove(fullUrl.Length - 1, 1);
            return fullUrl;
        }
    }
}