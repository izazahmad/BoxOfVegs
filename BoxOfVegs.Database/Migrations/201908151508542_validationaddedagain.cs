namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationaddedagain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Categories", "isFeatured");
            DropColumn("dbo.Products", "isFeatured");
            AlterStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        ProductName = p.String(maxLength: 50),
                        Price = p.Decimal(precision: 18, scale: 2),
                        CategoryID = p.Int(),
                        ImageUrl = p.String(),
                        Quantity = p.Int(),
                        Description = p.String(maxLength: 500),
                    },
                body:
                    @"INSERT [dbo].[Products]([ProductName], [Price], [CategoryID], [ImageUrl], [Quantity], [Description])
                      VALUES (@ProductName, @Price, @CategoryID, @ImageUrl, @Quantity, @Description)
                      
                      DECLARE @ProductID int
                      SELECT @ProductID = [ProductID]
                      FROM [dbo].[Products]
                      WHERE @@ROWCOUNT > 0 AND [ProductID] = scope_identity()
                      
                      SELECT t0.[ProductID]
                      FROM [dbo].[Products] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ProductID] = @ProductID"
            );
            
            AlterStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        ProductID = p.Int(),
                        ProductName = p.String(maxLength: 50),
                        Price = p.Decimal(precision: 18, scale: 2),
                        CategoryID = p.Int(),
                        ImageUrl = p.String(),
                        Quantity = p.Int(),
                        Description = p.String(maxLength: 500),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [ProductName] = @ProductName, [Price] = @Price, [CategoryID] = @CategoryID, [ImageUrl] = @ImageUrl, [Quantity] = @Quantity, [Description] = @Description
                      WHERE ([ProductID] = @ProductID)"
            );
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "isFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "isFeatured", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
