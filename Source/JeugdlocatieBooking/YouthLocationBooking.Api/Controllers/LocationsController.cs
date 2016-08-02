using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.API.Entities;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
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
                return new ApiLocation
                {
                    Name = x.Name,
                    AddressNumber = x.AddressNumber,
                    AddressPostalCode = x.AddressPostalCode,
                    PricePerDay = x.PricePerDay,
                    AddressStreet = x.AddressStreet,
                    AddressProvince = x.AddressProvince,
                    Description = x.Description
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
