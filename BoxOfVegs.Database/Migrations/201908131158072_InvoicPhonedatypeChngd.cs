namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoicPhonedatypeChngd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "PhoneNumber", c => c.String());
            DropColumn("dbo.Invoices", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "PhoneNumber");
        }
    }
}
