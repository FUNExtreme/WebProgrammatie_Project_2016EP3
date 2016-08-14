using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationNewGeneralViewModel
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
    }
}
