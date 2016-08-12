using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationFilterViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
        [Range(1, int.MaxValue)]
        public int? MinCapacity { get; set; }
        public string CityOrPostcode { get; set; }
        public string Province { get; set; }

        // Used for pagination
        public int Page { get; set; }
    }
}
