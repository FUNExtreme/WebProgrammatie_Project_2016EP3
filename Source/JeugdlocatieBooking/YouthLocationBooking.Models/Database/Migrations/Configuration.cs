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

            context.LocationFacilities.AddOrUpdate(
                new Entities.DbLocationFacility { Id = 1, Name = "Keuken", Description = "Er is een keuken aanwezig" },
                new Entities.DbLocationFacility { Id = 2, Name = "Keuken Personeel", Description = "Keuken personeel zorgt voor de maaltijden" },
                new Entities.DbLocationFacility { Id = 3, Name = "Slaapkamers", Description = "Er zijn slaapkamers aanwezig" },
                new Entities.DbLocationFacility { Id = 4, Name = "Douches", Description = "Er zijn douches aanwezig" },
                new Entities.DbLocationFacility { Id = 5, Name = "Kuispersoneel", Description = "De locatie wordt proper gehouden door kuis personeel" }
            );
            context.SaveChanges();
        }
    }
}
