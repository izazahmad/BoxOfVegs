namespace BoxOfVegs.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
            AddColumn("dbo.Users", "UserRoleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserRoleID");
            DropTable("dbo.UserRoles");
        }
    }
}
