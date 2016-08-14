using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocation
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string AddressStreet { get; set; }
        [MaxLength(20)]
        public string AddressNumber { get; set; }
        [MaxLength(255)]
        public string AddressProvince { get; set; }
        public int AddressPostalCode { get; set; }
        [MaxLength(255)]
        public string AddressCity { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Organisation { get; set; }
        public double PricePerDay { get; set; }
        public int Capacity { get; set; }
        [MaxLength(255)]
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
