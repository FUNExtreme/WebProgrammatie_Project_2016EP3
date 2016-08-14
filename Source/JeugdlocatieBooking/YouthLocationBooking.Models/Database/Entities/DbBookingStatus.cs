using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbBookingStatus
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
