using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class NewLocationValidationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressStreet { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        [Required]
        public string AddressProvince { get; set; }
        [Required]
        public int AddressPostalCode { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        [Required]
        public double PricePerDay { get; set; }
    }
}
