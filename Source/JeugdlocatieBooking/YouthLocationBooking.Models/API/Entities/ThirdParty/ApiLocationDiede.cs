using Newtonsoft.Json;

namespace YouthLocationBooking.Data.API.Entities.ThirdParty
{
    public class ApiLocationDiede : ApiLocationThirdPartyBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("AddressProvince")]
        public string AddressProvince { get; set; }
        [JsonProperty("AddressCity")]
        public string AddressCity { get; set; }
        [JsonProperty("AddressStreet")]
        public string AddressStreet { get; set; }
        [JsonProperty("AddressNumber")]
        public string AddressNumber { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("PricePerDay")]
        public float PricePerDay { get; set; }
        [JsonProperty("Capacity")]
        public int Capacity { get; set; }
        [JsonProperty("DetailsPageUrl")]
        public string DetailsPageUrl { get; set; }
        [JsonProperty("BookingPageUrl")]
        public string BookingPageUrl { get; set; }

        public ApiLocationDiede()
        {
            OriginSiteUrl = "http://diedeseldeslachts.ikdoeict.net";
        }
    }
}
