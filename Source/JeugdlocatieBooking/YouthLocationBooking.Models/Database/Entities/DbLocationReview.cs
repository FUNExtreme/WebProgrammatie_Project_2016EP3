using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocationReview
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Review { get; set; }
        public DateTime DateTime { get; set; }
        public int? UserId { get; set; }
        public int LocationId { get; set; }

        public virtual DbUser User { get; set; }
        public virtual DbLocation Location { get; set; }

        public ICollection<DbLocationFacilityRating> FacilityRatings { get; set; }
    }
}
