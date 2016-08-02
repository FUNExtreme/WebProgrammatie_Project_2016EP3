using System;
using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocationReview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public DateTime DateTime { get; set; }
        public string ReviewerName { get; set; }
        public int LocationId { get; set; }

        public virtual DbLocation Location { get; set; }

        public ICollection<DbLocationFacilityRating> FacilityRatings { get; set; }
    }
}
