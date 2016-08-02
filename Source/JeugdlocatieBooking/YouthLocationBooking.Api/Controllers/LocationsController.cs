using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.API.Models;
using YouthLocationBooking.Data.Database.Models;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        private IRepository<DbLocation> _locationsRepository;

        public LocationsController()
        {
            _locationsRepository = new LocationsRepository();
        }

        [Route()]
        public IEnumerable<ApiLocation> Get()
        {
            return _locationsRepository.GetAll().Select(x =>
            {
                return new ApiLocation
                {
                    Name = x.Name,
                    Number = x.AddressNumber,
                    PostCode = x.AddressPostalCode,
                    PricePerDay = x.PricePerDay,
                    Street = x.AddressStreet
                };
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
