using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocationFacility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<DbLocation> Locations { get; set; }
    }
}
