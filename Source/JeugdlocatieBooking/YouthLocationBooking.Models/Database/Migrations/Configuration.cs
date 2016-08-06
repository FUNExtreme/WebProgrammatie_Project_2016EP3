namespace YouthLocationBooking.Data.Database.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<YouthLocationBooking.Data.Database.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "YouthLocationBooking.Data.Database.DatabaseContext";
            MigrationsDirectory = "Database\\Migrations";
            MigrationsNamespace = "YouthLocationBooking.Data.Database.Migrations";
        }

        protected override void Seed(YouthLocationBooking.Data.Database.DatabaseContext context)
        {
            context.BookingStatuses.AddOrUpdate(
                new Entities.DbBookingStatus { Id = 1, Name = "Pending", Description = "Awaiting approval" },
                new Entities.DbBookingStatus { Id = 2, Name = "Denied", Description = "Rent Request was denied by owner of property" },
                new Entities.DbBookingStatus { Id = 2, Name = "Cancelled", Description = "Rentee has cancelled the request" },
                new Entities.DbBookingStatus { Id = 2, Name = "Accepted", Description = "Rent request accepted" }
            );
            context.SaveChanges();
        }
    }
}
