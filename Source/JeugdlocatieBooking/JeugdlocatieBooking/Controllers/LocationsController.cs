using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.Validation.Models;

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

        public ActionResult Index(LocationFilterModel model = null, int page = 1)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            if (page < 1)
                page = 1;

            int itemsPerPage = 10;

            if (model == null)
                ViewBag.PagedLocations = locationsRepository.GetAllPaged(page, itemsPerPage);
            else
                ViewBag.PagedLocations = locationsRepository.GetAllPagedWithFilter(page, itemsPerPage, model);

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(LocationFilterModel model)
        {
            // This is a hack used to keep a clean URL.
            // If the form is of FormMethod.Get it will send all fields as query string, this is default behavior
            // However by using POST and redirecting to the GET action you push the values through MVC routing, which filters out null values
            return RedirectToAction("Index", "Locations", model);
        }

        public ActionResult Details(int id)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");

            ViewBag.Location = location;
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id, LocationBookingModel model)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;
            var usersRepository = _unitOfWork.UsersRepository;
            var bookingsRepository = _unitOfWork.BookingsRepository;

            DbLocation location = locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");

            ViewBag.Location = location;

            if (model.From > model.To)
            {
                ModelState.AddModelError("From", "Van datum moet voor Tot datum liggen");
                return View(model);
            }

            // TODO create enum with Status values
            // TODO Prepopulate BookingStatuses table
            DbBooking booking = new DbBooking();
            booking.LocationId = id;
            booking.Organisation = model.Organisation;
            booking.StartDateTime = model.From;
            booking.EndDateTime = model.To;
            booking.StatusId = 0;
            booking.UserId = usersRepository.GetByEmail(User.Identity.Name).Id;
            bookingsRepository.Insert(booking);

            return View(model);
        }
    }
}