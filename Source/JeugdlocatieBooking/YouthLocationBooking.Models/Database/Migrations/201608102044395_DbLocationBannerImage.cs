namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbLocationBannerImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbLocations", "BannerImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbLocations", "BannerImageFileName");
        }
    }
}
