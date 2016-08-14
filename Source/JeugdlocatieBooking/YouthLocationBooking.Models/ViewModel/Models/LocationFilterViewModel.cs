using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationFilterViewModel
    {
        [Display(Name = "Naam")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 karakters lang zijn")]
        public string Name { get; set; }

        [Display(Name = "Van")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "Tot")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        [Display(Name = "Minimum Capaciteit")]
        [Range(1, int.MaxValue, ErrorMessage = "Capaciteit mag niet lager dan 1 zijn")]
        public int? MinCapacity { get; set; }

        [Display(Name = "Stad of Postcode")]
        [StringLength(255, ErrorMessage = "Stad of postcode mag maximaal 255 karakters lang zijn")]
        public string CityOrPostcode { get; set; }

        [Display(Name = "Provincie")]
        [StringLength(255, ErrorMessage = "Provincie mag maximaal 255 karakters lang zijn")]
        public string Province { get; set; }

        // Used for pagination
        public int Page { get; set; }
    }
}
