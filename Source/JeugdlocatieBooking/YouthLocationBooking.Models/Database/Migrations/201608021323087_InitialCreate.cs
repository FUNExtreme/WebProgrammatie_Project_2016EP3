namespace YouthLocationBooking.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbBookingMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        UserId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbBookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.DbUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.DbBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Organisation = c.String(),
                        UserId = c.Int(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbLocations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.DbBookingStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.DbUsers", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.DbLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressStreet = c.String(),
                        AddressNumber = c.String(),
                        AddressState = c.String(),
                        AddressPostalCode = c.Int(nullable: false),
                        Description = c.String(),
                        Organisation = c.String(),
                        PricePerDay = c.Double(nullable: false),
                        CreatedByUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbUsers", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.DbUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        RegisterDateTime = c.DateTime(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbLocationAvailabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbLocations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.DbLocationFacilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DbLocation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbLocations", t => t.DbLocation_Id)
                .Index(t => t.DbLocation_Id);
            
            CreateTable(
                "dbo.DbBookingStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbLocationFacilityRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbLocationFacilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.DbLocationReviews", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ReviewId)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.DbLocationReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Review = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        ReviewerName = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbLocations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbLocationReviews", "LocationId", "dbo.DbLocations");
            DropForeignKey("dbo.DbLocationFacilityRatings", "ReviewId", "dbo.DbLocationReviews");
            DropForeignKey("dbo.DbLocationFacilityRatings", "FacilityId", "dbo.DbLocationFacilities");
            DropForeignKey("dbo.DbBookingMessages", "UserId", "dbo.DbUsers");
            DropForeignKey("dbo.DbBookingMessages", "BookingId", "dbo.DbBookings");
            DropForeignKey("dbo.DbBookings", "UserId", "dbo.DbUsers");
            DropForeignKey("dbo.DbBookings", "StatusId", "dbo.DbBookingStatus");
            DropForeignKey("dbo.DbBookings", "LocationId", "dbo.DbLocations");
            DropForeignKey("dbo.DbLocationFacilities", "DbLocation_Id", "dbo.DbLocations");
            DropForeignKey("dbo.DbLocationAvailabilities", "LocationId", "dbo.DbLocations");
            DropForeignKey("dbo.DbLocations", "CreatedByUserId", "dbo.DbUsers");
            DropIndex("dbo.DbLocationReviews", new[] { "LocationId" });
            DropIndex("dbo.DbLocationFacilityRatings", new[] { "FacilityId" });
            DropIndex("dbo.DbLocationFacilityRatings", new[] { "ReviewId" });
            DropIndex("dbo.DbLocationFacilities", new[] { "DbLocation_Id" });
            DropIndex("dbo.DbLocationAvailabilities", new[] { "LocationId" });
            DropIndex("dbo.DbLocations", new[] { "CreatedByUserId" });
            DropIndex("dbo.DbBookings", new[] { "LocationId" });
            DropIndex("dbo.DbBookings", new[] { "UserId" });
            DropIndex("dbo.DbBookings", new[] { "StatusId" });
            DropIndex("dbo.DbBookingMessages", new[] { "BookingId" });
            DropIndex("dbo.DbBookingMessages", new[] { "UserId" });
            DropTable("dbo.DbLocationReviews");
            DropTable("dbo.DbLocationFacilityRatings");
            DropTable("dbo.DbBookingStatus");
            DropTable("dbo.DbLocationFacilities");
            DropTable("dbo.DbLocationAvailabilities");
            DropTable("dbo.DbUsers");
            DropTable("dbo.DbLocations");
            DropTable("dbo.DbBookings");
            DropTable("dbo.DbBookingMessages");
        }
    }
}
