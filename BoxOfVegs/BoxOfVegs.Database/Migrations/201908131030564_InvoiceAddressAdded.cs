namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceAddressAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Address", c => c.String());
            AddColumn("dbo.Invoices", "PostCode", c => c.String());
            AddColumn("dbo.Invoices", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "City");
            DropColumn("dbo.Invoices", "PostCode");
            DropColumn("dbo.Invoices", "Address");
        }
    }
}
