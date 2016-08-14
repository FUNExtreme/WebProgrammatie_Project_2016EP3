using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationReviewViewModel
    {
        [Display(Name = "Titel")]
        [Required(ErrorMessage = "Titel is verplicht")]
        [StringLength(100, ErrorMessage = "Titel mag maximaal 100 karakters lang zijn")]
        public string Title { get; set; }

        [Display(Name = "Review")]
        [StringLength(255, ErrorMessage = "Review mag maximaal 255 karakters lang zijn")]
        public string Review { get; set; }

        public IList<LocationFacilityReviewViewModel> FacilityRatings { get; set; }
    }

    public class LocationFacilityReviewViewModel
    {
        public int Id { get; set; }
        public float Rating { get; set; }
    }
}
