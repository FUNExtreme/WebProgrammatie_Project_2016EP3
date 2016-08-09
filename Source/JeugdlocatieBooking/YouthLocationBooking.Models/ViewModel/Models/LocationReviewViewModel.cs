using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationReviewViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Review { get; set; }
    }
}
