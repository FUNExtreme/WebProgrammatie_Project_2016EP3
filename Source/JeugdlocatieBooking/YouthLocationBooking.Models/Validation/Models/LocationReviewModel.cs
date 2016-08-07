using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class LocationReviewModel
    {
        [Required]
        public string Title { get; set; }
        public string Review { get; set; }
    }
}
