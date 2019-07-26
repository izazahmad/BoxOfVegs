namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bitchang : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.Int(nullable: false));
        }
    }
}
