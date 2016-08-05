using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class LocationEditAddressModel
    {
        [Required]
        public string AddressStreet { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        [Required]
        public int AddressPostalCode { get; set; }
        [Required]
        public string AddressCity { get; set; }
        [Required]
        public string AddressProvince { get; set; }
    }
}
