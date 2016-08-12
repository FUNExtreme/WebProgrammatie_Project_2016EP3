using YouthLocationBooking.Data.API.Entities.ThirdParty;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Data.API.Mappings
{
    public static class ToViewModelExtensions
    {
        public static ThirdPartyLocationOverviewViewModel ToViewModel(this ApiLocationDiede entity)
        {
            ThirdPartyLocationOverviewViewModel mappedEntity = new ThirdPartyLocationOverviewViewModel();
            mappedEntity.AddressCity = entity.AddressCity;
            mappedEntity.BannerImageUrl = null;
            mappedEntity.Capacity = entity.Capacity;
            mappedEntity.DetailsPageUrl = entity.DetailsPageUrl;
            mappedEntity.Name = entity.Name;
            mappedEntity.OriginSiteUrl = entity.OriginSiteUrl;
            mappedEntity.PricePerDay = entity.PricePerDay;
            return mappedEntity;
        }

        public static ThirdPartyLocationOverviewViewModel ToViewModel(this ApiLocationTim entity)
        {
            ThirdPartyLocationOverviewViewModel mappedEntity = new ThirdPartyLocationOverviewViewModel();
            mappedEntity.AddressCity = entity.City;
            mappedEntity.BannerImageUrl = entity.HeadPictureUrl;
            mappedEntity.Capacity = null;
            mappedEntity.DetailsPageUrl = entity.DetailUrl;
            mappedEntity.Name = entity.Title;
            mappedEntity.OriginSiteUrl = entity.OriginSiteUrl;
            mappedEntity.PricePerDay = null;
            return mappedEntity;
        }
    }
}
