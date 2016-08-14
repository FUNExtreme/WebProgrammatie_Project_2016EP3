using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocationAvailability
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [MaxLength(255)]
        public string Remark { get; set; }
        public int LocationId { get; set; }

        public virtual DbLocation Location { get; set; }
    }
}
