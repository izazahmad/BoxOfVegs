namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoicPhoneAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Phone");
        }
    }
}
