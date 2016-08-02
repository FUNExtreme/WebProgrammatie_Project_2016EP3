namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStateToProvince : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbLocations", "AddressProvince", c => c.String());
            DropColumn("dbo.DbLocations", "AddressState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbLocations", "AddressState", c => c.String());
            DropColumn("dbo.DbLocations", "AddressProvince");
        }
    }
}
