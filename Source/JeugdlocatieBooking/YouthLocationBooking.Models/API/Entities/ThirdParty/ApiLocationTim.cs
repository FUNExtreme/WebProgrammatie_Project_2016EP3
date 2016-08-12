using Newtonsoft.Json;

namespace YouthLocationBooking.Data.API.Entities.ThirdParty
{
    public class ApiLocationTim : ApiLocationThirdPartyBase
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("StreetAndNr")]
        public string StreetAndNr { get; set; }
        [JsonProperty("Zip")]
        public string Zip { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("Website")]
        public string Website { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("DetailUrl")]
        public string DetailUrl { get; set; }
        [JsonProperty("HeadPictureUrl")]
        public string HeadPictureUrl { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        public ApiLocationTim()
        {
            OriginSiteUrl = "http://jeugdlocatie.timvettori.ikdoeict.net";
        }
    }
}
