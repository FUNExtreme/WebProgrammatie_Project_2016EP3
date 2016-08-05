using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class LocationEditModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        [Required]
        public double PricePerDay { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string AddressStreet { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        [Required]
        public int AddressPostalCode { get; set; }
        [Required]
        public string AddressCity { get; set; }
        [Required]
        public string AddressProvince { get; set; }
    }
}
