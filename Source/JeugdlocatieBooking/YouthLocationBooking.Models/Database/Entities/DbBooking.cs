using System;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbBooking
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int StatusId { get; set; }
        public string Organisation { get; set; }
        public int? UserId { get; set; }
        public int LocationId { get; set; }

        public virtual DbBookingStatus Status { get; set; }
        public virtual DbUser User { get; set; }
        public virtual DbLocation Location { get; set; }
    }
}
