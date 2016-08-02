using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Data.API.Models;

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
                    Number = x.AddressNumber,
                    PostCode = x.AddressPostalCode,
                    PricePerDay = x.PricePerDay,
                    Street = x.AddressStreet
                };
            });
        }
    }
}
