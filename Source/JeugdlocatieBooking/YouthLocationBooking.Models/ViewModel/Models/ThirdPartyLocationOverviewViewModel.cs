namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ThirdPartyLocationOverviewViewModel
    {
        public string BannerImageUrl { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public string AddressCity { get; set; }
        public float? PricePerDay { get; set; }
        public string DetailsPageUrl { get; set; } 

        public string OriginSiteUrl { get; set; }
    }
}
