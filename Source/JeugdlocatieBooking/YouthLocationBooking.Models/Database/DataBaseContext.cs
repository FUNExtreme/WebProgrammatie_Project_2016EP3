using System;
using System.Data.Entity;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DbUser> Users { get; set; }

        public DbSet<DbBooking> Bookings { get; set; }
        public DbSet<DbBookingMessage> BookingMessages { get; set; }
        public DbSet<DbBookingStatus> BookingStatuses { get; set; }

        public DbSet<DbLocation> Locations { get; set; }
        public DbSet<DbLocationAvailability> LocationAvailabilities { get; set; }
        public DbSet<DbLocationFacility> LocationFacilities { get; set; }

        public DbSet<DbLocationReview> LocationReviews { get; set; }
        public DbSet<DbLocationFacilityRating> LocationFacilityRatings { get; set; }

        public DatabaseContext()
            : base("DefaultConnection")
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
        }
    }
}
