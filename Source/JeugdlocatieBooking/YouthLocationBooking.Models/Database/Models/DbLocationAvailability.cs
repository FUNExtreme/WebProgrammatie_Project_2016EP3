using System;

namespace YouthLocationBooking.Data.Database.Models
{
    public class DbLocationAvailability
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Remark { get; set; }
        public int LocationId { get; set; }

        public virtual DbLocation Location { get; set; }
    }
}
