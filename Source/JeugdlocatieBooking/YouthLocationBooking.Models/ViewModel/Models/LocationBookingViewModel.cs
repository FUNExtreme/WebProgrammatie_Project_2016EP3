using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationBookingViewModel
    {
        [Display(Name = "Van")]
        [Required(ErrorMessage = "Van is verplicht")]
        public DateTime From { get; set; }

        [Display(Name = "Tot")]
        [Required(ErrorMessage = "Tot is verplicht")]
        public DateTime To { get; set; }

        [Display(Name = "Organisatie")]
        [Required(ErrorMessage = "Organisatie is verplicht")]
        [StringLength(100, ErrorMessage = "Organisatie mag maximaal 100 karakters lang zijn")]
        public string Organisation { get; set; }
    }
}
