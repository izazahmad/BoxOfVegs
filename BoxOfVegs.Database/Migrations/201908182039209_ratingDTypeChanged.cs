namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingDTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserReviews", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserReviews", "Rating", c => c.Int(nullable: false));
        }
    }
}
