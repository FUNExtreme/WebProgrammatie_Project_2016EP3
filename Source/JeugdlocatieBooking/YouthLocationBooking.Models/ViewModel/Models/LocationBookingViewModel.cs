using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationBookingViewModel
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        public string Organisation { get; set; }
    }
}
