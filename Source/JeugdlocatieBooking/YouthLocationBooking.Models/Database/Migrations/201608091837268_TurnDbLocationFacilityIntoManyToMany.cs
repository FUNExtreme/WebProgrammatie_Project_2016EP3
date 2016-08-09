namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurnDbLocationFacilityIntoManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbLocationFacilities", "DbLocation_Id", "dbo.DbLocations");
            DropIndex("dbo.DbLocationFacilities", new[] { "DbLocation_Id" });
            CreateTable(
                "dbo.DbLocationFacilityDbLocations",
                c => new
                    {
                        DbLocationFacility_Id = c.Int(nullable: false),
                        DbLocation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbLocationFacility_Id, t.DbLocation_Id })
                .ForeignKey("dbo.DbLocationFacilities", t => t.DbLocationFacility_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbLocations", t => t.DbLocation_Id, cascadeDelete: true)
                .Index(t => t.DbLocationFacility_Id)
                .Index(t => t.DbLocation_Id);
            
            DropColumn("dbo.DbLocationFacilities", "DbLocation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbLocationFacilities", "DbLocation_Id", c => c.Int());
            DropForeignKey("dbo.DbLocationFacilityDbLocations", "DbLocation_Id", "dbo.DbLocations");
            DropForeignKey("dbo.DbLocationFacilityDbLocations", "DbLocationFacility_Id", "dbo.DbLocationFacilities");
            DropIndex("dbo.DbLocationFacilityDbLocations", new[] { "DbLocation_Id" });
            DropIndex("dbo.DbLocationFacilityDbLocations", new[] { "DbLocationFacility_Id" });
            DropTable("dbo.DbLocationFacilityDbLocations");
            CreateIndex("dbo.DbLocationFacilities", "DbLocation_Id");
            AddForeignKey("dbo.DbLocationFacilities", "DbLocation_Id", "dbo.DbLocations", "Id");
        }
    }
}
