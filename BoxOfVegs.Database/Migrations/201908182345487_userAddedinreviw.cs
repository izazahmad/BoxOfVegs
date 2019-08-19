namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAddedinreviw : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserReviews", "UserID");
            AddForeignKey("dbo.UserReviews", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserReviews", "UserID", "dbo.Users");
            DropIndex("dbo.UserReviews", new[] { "UserID" });
        }
    }
}
