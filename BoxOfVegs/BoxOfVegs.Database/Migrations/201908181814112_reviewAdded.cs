namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        UserReviewID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Review = c.String(),
                        Rating = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserReviewID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserReviews");
        }
    }
}
