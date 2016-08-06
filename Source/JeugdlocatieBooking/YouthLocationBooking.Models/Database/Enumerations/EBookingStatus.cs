namespace YouthLocationBooking.Data.Database.Enumerations
{
    /// <summary>
    /// Represents DbBookingStatus IDs and their Name
    /// This way no lookups are required to get the correct ID
    /// 
    /// IMPORTANT NOTE: If these values somehow change in the database, this will no longer be correct
    /// </summary>
    public enum EBookingStatus
    {
        Pending = 1,
        Denied = 2,
        Cancelled = 3,
        Accepted = 4
    }
}
