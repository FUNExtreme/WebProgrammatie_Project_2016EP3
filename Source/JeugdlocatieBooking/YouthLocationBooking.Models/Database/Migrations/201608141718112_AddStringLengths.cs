namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DbBookings", "Organisation", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocations", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocations", "AddressStreet", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocations", "AddressNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.DbLocations", "AddressProvince", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocations", "AddressCity", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocations", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocations", "Organisation", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocations", "BannerImageFileName", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocationAvailabilities", "Remark", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbUsers", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbUsers", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbUsers", "Email", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbUsers", "PhoneNumber", c => c.String(maxLength: 30));
            AlterColumn("dbo.DbUsers", "Password", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocationFacilities", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocationFacilities", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbBookingStatus", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbBookingStatus", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.DbLocationReviews", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.DbLocationReviews", "Review", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DbLocationReviews", "Review", c => c.String());
            AlterColumn("dbo.DbLocationReviews", "Title", c => c.String());
            AlterColumn("dbo.DbBookingStatus", "Description", c => c.String());
            AlterColumn("dbo.DbBookingStatus", "Name", c => c.String());
            AlterColumn("dbo.DbLocationFacilities", "Description", c => c.String());
            AlterColumn("dbo.DbLocationFacilities", "Name", c => c.String());
            AlterColumn("dbo.DbUsers", "Password", c => c.String());
            AlterColumn("dbo.DbUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.DbUsers", "Email", c => c.String());
            AlterColumn("dbo.DbUsers", "LastName", c => c.String());
            AlterColumn("dbo.DbUsers", "FirstName", c => c.String());
            AlterColumn("dbo.DbLocationAvailabilities", "Remark", c => c.String());
            AlterColumn("dbo.DbLocations", "BannerImageFileName", c => c.String());
            AlterColumn("dbo.DbLocations", "Organisation", c => c.String());
            AlterColumn("dbo.DbLocations", "Description", c => c.String());
            AlterColumn("dbo.DbLocations", "AddressCity", c => c.String());
            AlterColumn("dbo.DbLocations", "AddressProvince", c => c.String());
            AlterColumn("dbo.DbLocations", "AddressNumber", c => c.String());
            AlterColumn("dbo.DbLocations", "AddressStreet", c => c.String());
            AlterColumn("dbo.DbLocations", "Name", c => c.String());
            AlterColumn("dbo.DbBookings", "Organisation", c => c.String());
        }
    }
}
