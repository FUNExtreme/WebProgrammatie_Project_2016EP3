using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressProvince { get; set; }
        public int AddressPostalCode { get; set; }
        public string AddressCity { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        public double PricePerDay { get; set; }
        public int Capacity { get; set; }
        public string BannerImageFileName { get; set; }
        public int? CreatedByUserId { get; set; }

        public virtual DbUser CreatedByUser { get; set; }

        public virtual ICollection<DbBooking> Bookings { get; set; }
        public virtual ICollection<DbLocationAvailability> Availabilities { get; set; }
        public virtual ICollection<DbLocationFacility> Facilities { get; set; }

        public DbLocation()
        {
            Bookings = new HashSet<DbBooking>();
            Availabilities = new HashSet<DbLocationAvailability>();
            Facilities = new HashSet<DbLocationFacility>();
        }
    }
}
