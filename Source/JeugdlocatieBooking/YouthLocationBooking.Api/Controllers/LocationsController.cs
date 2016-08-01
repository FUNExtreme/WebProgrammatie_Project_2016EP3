using System.Collections.Generic;
using System.Web.Http;
using YouthLocationBooking.Models.API;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        [Route()]
        public IEnumerable<ApiLocation> Get()
        {
            return new ApiLocation[] 
            {
                new ApiLocation()
            };
        }
    }
}
