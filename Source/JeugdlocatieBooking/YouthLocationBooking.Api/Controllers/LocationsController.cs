using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.API.Entities;
using YouthLocationBooking.Data.Database.Mappings;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        private static string DetailsPageUrlTemplate = "";
        private static string BookingPageUrlTemplate = "";

        private LocationsRepository _locationsRepository;

        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
        }

        [Route()]
        public IEnumerable<ApiLocation> Get()
        {
            return _locationsRepository.GetAll().Select(x =>
            {
                // Todo generate details and booking URLs
                return x.ToApiEntity();
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_locationsRepository != null)
                {
                    _locationsRepository.Dispose();
                    _locationsRepository = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
