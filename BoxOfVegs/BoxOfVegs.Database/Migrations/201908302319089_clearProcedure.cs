namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clearProcedure : DbMigration
    {
        public override void Up()
        {
            DropStoredProcedure("dbo.Product_Insert");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Delete");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
