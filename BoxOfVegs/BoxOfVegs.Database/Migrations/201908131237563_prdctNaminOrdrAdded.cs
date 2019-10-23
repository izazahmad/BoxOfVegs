namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prdctNaminOrdrAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ProductName");
        }
    }
}
