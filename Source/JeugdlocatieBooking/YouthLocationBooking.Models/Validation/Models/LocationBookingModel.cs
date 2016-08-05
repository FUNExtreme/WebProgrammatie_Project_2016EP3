using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class LocationBookingModel
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        public string Organisation { get; set; }
    }
}
