namespace YouthLocationBooking.Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbLocationReviews", "UserId", c => c.Int());
            CreateIndex("dbo.DbLocationReviews", "UserId");
            AddForeignKey("dbo.DbLocationReviews", "UserId", "dbo.DbUsers", "Id");
            DropColumn("dbo.DbLocationReviews", "ReviewerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbLocationReviews", "ReviewerName", c => c.String());
            DropForeignKey("dbo.DbLocationReviews", "UserId", "dbo.DbUsers");
            DropIndex("dbo.DbLocationReviews", new[] { "UserId" });
            DropColumn("dbo.DbLocationReviews", "UserId");
        }
    }
}
