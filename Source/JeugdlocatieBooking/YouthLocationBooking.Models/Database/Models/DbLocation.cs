using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Models
{
    public class DbLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressState { get; set; }
        public int AddressPostalCode { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        public double PricePerDay { get; set; }
        public int? CreatedByUserId { get; set; }

        public virtual DbUser CreatedByUser { get; set; }

        public virtual ICollection<DbLocationAvailability> LocationAvailabilities { get; set; }
        public virtual ICollection<DbLocationFacility> LocationFacilities { get; set; }

        public DbLocation()
        {
            LocationAvailabilities = new HashSet<DbLocationAvailability>();
            LocationFacilities = new HashSet<DbLocationFacility>();
        }
    }
}
