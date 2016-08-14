using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationEditViewModel
    {
        [Display(Name = "Naam")]
        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 karakters lang zijn")]
        public string Name { get; set; }

        [Display(Name = "Beschrijving")]
        [StringLength(255, ErrorMessage = "Beschrijving mag maximaal 255 karakters lang zijn")]
        public string Description { get; set; }

        [Display(Name = "Organisatie")]
        [StringLength(100, ErrorMessage = "Organisatie mag maximaal 100 karakters lang zijn")]
        public string Organisation { get; set; }

        [Display(Name = "Prijs Per Dag")]
        [Required(ErrorMessage = "Prijs is verplicht")]
        public double PricePerDay { get; set; }

        [Display(Name = "Capaciteit")]
        [Required(ErrorMessage = "Capaciteit is verplicht")]
        public int Capacity { get; set; }

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
