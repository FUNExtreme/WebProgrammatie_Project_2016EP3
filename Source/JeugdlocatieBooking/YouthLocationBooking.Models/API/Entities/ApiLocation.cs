namespace YouthLocationBooking.Data.API.Entities
{
    public class ApiLocation
    {
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressProvince { get; set; }
        public int AddressPostalCode { get; set; }
        public string AddressCity { get; set; }
        public string Description { get; set; }
        public double PricePerDay { get; set; }
        public int Capacity { get; set; }

        public string DetailsPageUrl { get; set; }
        public string BookingPageUrl { get; set; }
        public string BannerImageUrl { get; set; }
    }
}
