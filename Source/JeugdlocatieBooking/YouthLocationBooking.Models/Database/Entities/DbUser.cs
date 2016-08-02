using System;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}
