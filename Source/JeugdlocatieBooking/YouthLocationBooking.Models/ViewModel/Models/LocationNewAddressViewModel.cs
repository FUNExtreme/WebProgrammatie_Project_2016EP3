using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationNewAddressViewModel
    {
        [Display(Name = "Straat")]
        [Required(ErrorMessage = "Straat is verplicht")]
        [StringLength(255, ErrorMessage = "Straat mag maximaal 255 karakters lang zijn")]
        public string AddressStreet { get; set; }

        [Display(Name = "Nummer")]
        [Required(ErrorMessage = "Nummer is verplicht")]
        [StringLength(20, ErrorMessage = "Nummer mag maximaal 20 karakters lang zijn")]
        public string AddressNumber { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Postcode is verplicht")]
        public int AddressPostalCode { get; set; }

        [Display(Name = "Stad")]
        [Required(ErrorMessage = "Stad is verplicht")]
        [StringLength(255, ErrorMessage = "Stad mag maximaal 255 karakters lang zijn")]
        public string AddressCity { get; set; }

        [Display(Name = "Provincie")]
        [Required(ErrorMessage = "Provincie is verplicht")]
        [StringLength(255, ErrorMessage = "Provincie mag maximaal 255 karakters lang zijn")]
        public string AddressProvince { get; set; }
    }
}
