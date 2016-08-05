using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Data.API.Entities;
using YouthLocationBooking.Data.Database;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        #region Variables
        private static string DetailsPageUrlTemplate = "";
        private static string BookingPageUrlTemplate = "";

        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        [Route()]
        public IEnumerable<ApiLocation> Get()
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            return locationsRepository.GetAll().Select(x =>
            {
                // Todo generate details and booking URLs
                return x.ToApiEntity();
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
