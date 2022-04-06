namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Passphrase = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        Permission_Id = c.Long(nullable: false),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.User_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PermissionUsers", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionUsers", new[] { "User_Id" });
            DropIndex("dbo.PermissionUsers", new[] { "Permission_Id" });
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.Permissions");
            DropTable("dbo.Users");
        }
    }
}
