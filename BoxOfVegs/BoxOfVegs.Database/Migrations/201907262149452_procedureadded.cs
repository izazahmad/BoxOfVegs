namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class procedureadded : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        ProductName = p.String(maxLength: 50),
                        Price = p.Decimal(precision: 18, scale: 2),
                        CategoryID = p.Int(),
                        ImageUrl = p.String(),
                        isFeatured = p.Boolean(),
                        Quantity = p.Int(),
                        Description = p.String(maxLength: 500),
                    },
                body:
                    @"INSERT [dbo].[Products]([ProductName], [Price], [CategoryID], [ImageUrl], [isFeatured], [Quantity], [Description])
                      VALUES (@ProductName, @Price, @CategoryID, @ImageUrl, @isFeatured, @Quantity, @Description)
                      
                      DECLARE @ProductID int
                      SELECT @ProductID = [ProductID]
                      FROM [dbo].[Products]
                      WHERE @@ROWCOUNT > 0 AND [ProductID] = scope_identity()
                      
                      SELECT t0.[ProductID]
                      FROM [dbo].[Products] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ProductID] = @ProductID"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        ProductID = p.Int(),
                        ProductName = p.String(maxLength: 50),
                        Price = p.Decimal(precision: 18, scale: 2),
                        CategoryID = p.Int(),
                        ImageUrl = p.String(),
                        isFeatured = p.Boolean(),
                        Quantity = p.Int(),
                        Description = p.String(maxLength: 500),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [ProductName] = @ProductName, [Price] = @Price, [CategoryID] = @CategoryID, [ImageUrl] = @ImageUrl, [isFeatured] = @isFeatured, [Quantity] = @Quantity, [Description] = @Description
                      WHERE ([ProductID] = @ProductID)"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        ProductID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Products]
                      WHERE ([ProductID] = @ProductID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Product_Delete");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Insert");
        }
    }
}
