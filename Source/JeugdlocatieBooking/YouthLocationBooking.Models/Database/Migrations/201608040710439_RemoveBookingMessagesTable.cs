namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBookingMessagesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbBookingMessages", "BookingId", "dbo.DbBookings");
            DropForeignKey("dbo.DbBookingMessages", "UserId", "dbo.DbUsers");
            DropIndex("dbo.DbBookingMessages", new[] { "UserId" });
            DropIndex("dbo.DbBookingMessages", new[] { "BookingId" });
            DropTable("dbo.DbBookingMessages");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.DbBookingMessages", "BookingId");
            CreateIndex("dbo.DbBookingMessages", "UserId");
            AddForeignKey("dbo.DbBookingMessages", "UserId", "dbo.DbUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbBookingMessages", "BookingId", "dbo.DbBookings", "Id", cascadeDelete: true);
        }
    }
}
