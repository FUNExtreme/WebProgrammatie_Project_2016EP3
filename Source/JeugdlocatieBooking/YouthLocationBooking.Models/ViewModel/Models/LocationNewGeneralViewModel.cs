using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationNewGeneralViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        [Required]
        public double PricePerDay { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
