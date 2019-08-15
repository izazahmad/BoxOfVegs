namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InvoiceID);
            
            AddColumn("dbo.Orders", "InvoiceID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "InvoiceID");
            DropTable("dbo.Invoices");
        }
    }
}
