namespace YouthLocationBooking.Data.Database.Models
{
    public class DbBookingMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }

        public virtual DbUser User { get; set; }
        public virtual DbBooking Booking { get; set; }
    }
}
