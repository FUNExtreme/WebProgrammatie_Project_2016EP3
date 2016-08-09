using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationNewAddressViewModel
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
