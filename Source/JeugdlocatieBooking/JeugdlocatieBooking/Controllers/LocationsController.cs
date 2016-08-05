using System.Web.Mvc;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class LocationsController : Controller
    {
        private LocationsRepository _locationsRepository;
        private UsersRepository _usersRepository;
        //private BookingsRepository _bookingRepository;

        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
        }

        public ActionResult Index(LocationFilterModel model = null, int page = 1)
        {
            if (page < 1)
                page = 1;

            int itemsPerPage = 10;

            if (model == null)
                ViewBag.PagedLocations = _locationsRepository.GetAllPaged(page, itemsPerPage);
            else
                ViewBag.PagedLocations = _locationsRepository.GetAllPagedWithFilter(page, itemsPerPage, model);

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
            DbLocation location = _locationsRepository.Get(id);
            if (location == null)
                return RedirectToAction("Index", "Locations");

            ViewBag.Location = location;
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id, LocationBookingModel model)
        {
            DbLocation location = _locationsRepository.Get(id);
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
            // TODO Refactor repositories, create Generic implementation
            // TODO Move Repositories to Data?
            // TODO Store booking in database
            DbBooking booking = new DbBooking();
            booking.LocationId = id;
            booking.Organisation = model.Organisation;
            booking.StartDateTime = model.From;
            booking.EndDateTime = model.To;
            booking.StatusId = 0;
            booking.UserId = _usersRepository.GetByEmail(User.Identity.Name).Id;

            return View(model);
        }
    }
}