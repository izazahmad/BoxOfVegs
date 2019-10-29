namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdatabasechange : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Invoices", "UserID");
            CreateIndex("dbo.Orders", "ProductID");
            CreateIndex("dbo.Orders", "InvoiceID");
            AddForeignKey("dbo.Invoices", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "UserID", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "InvoiceID" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Invoices", new[] { "UserID" });
        }
    }
}
