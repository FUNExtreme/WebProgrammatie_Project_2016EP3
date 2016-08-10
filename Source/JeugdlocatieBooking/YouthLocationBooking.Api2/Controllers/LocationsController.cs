using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouthLocationBooking.Data.API.Entities;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;
using static YouthLocationBooking.Data.Database.Repositories.LocationsRepository;

namespace YouthLocationBooking.Api.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        #region Variables
        private static string DetailsPageUrlTemplate = @"http://jlb.robinmaenhaut.ikdoeict.net/Locations/{0}/Details";
        private static string BookingPageUrlTemplate = @"http://jlb.robinmaenhaut.ikdoeict.net/Locations/{0}/Details";
        private static string BannerImageBaseUrl = @"http://jlb.robinmaenhaut.ikdoeict.net/Resources/Location/{0}/Images/{1}";

        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LocationsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        [Route()]
        public IEnumerable<ApiLocation> Get(string name = null, string cityOrPostcode = null, string province = null, DateTime? from = null, DateTime? to = null, int? minCapacity = null)
        {
            var locationsRepository = _unitOfWork.LocationsRepository;

            LocationFilter filter = new LocationFilter();
            filter.Name = name;
            filter.CityOrPostcode = cityOrPostcode;
            filter.Province = province;
            filter.From = from;
            filter.To = to;
            filter.MinCapacity = minCapacity;
            IList<DbLocation> locations = locationsRepository.GetAllWithFilter(filter);

            return locations.Select(x =>
            {
                ApiLocation location = x.ToApiEntity();
                location.DetailsPageUrl = string.Format(DetailsPageUrlTemplate, x.Id);
                location.BookingPageUrl = string.Format(BookingPageUrlTemplate, x.Id);
                location.BannerImageUrl = string.Format(BannerImageBaseUrl, x.Id, x.BannerImageFileName);
                return location;
            });
        }

        #region IDispose
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
        #endregion
    }
}
