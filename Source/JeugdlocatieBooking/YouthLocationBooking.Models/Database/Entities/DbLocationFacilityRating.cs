namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbLocationFacilityRating
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int FacilityId { get; set; }
        public float Rating { get; set; }

        public virtual DbLocationReview Review { get; set; }
        public virtual DbLocationFacility Facility { get; set; }
    }
}
