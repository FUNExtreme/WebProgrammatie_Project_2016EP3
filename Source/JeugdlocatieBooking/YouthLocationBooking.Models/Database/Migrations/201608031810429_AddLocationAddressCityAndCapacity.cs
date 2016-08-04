namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationAddressCityAndCapacity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbLocations", "AddressCity", c => c.String());
            AddColumn("dbo.DbLocations", "Capacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbLocations", "Capacity");
            DropColumn("dbo.DbLocations", "AddressCity");
        }
    }
}
