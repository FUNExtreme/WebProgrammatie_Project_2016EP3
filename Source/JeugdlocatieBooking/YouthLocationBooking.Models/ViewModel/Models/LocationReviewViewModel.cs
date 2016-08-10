using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationReviewViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Review { get; set; }

        public IList<LocationFacilityReviewViewModel> FacilityRatings { get; set; }
    }

    public class LocationFacilityReviewViewModel
    {
        public int Id { get; set; }
        public float Rating { get; set; }
    }
}
