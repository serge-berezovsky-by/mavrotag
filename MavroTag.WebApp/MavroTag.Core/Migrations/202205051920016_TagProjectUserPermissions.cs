namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagProjectUserPermissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagProjectUserPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AddedDateTime = c.DateTime(nullable: false),
                        TagProjectId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        Permission = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagProjects", t => t.TagProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TagProjectId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagProjectUserPermissions", "UserId", "dbo.Users");
            DropForeignKey("dbo.TagProjectUserPermissions", "TagProjectId", "dbo.TagProjects");
            DropIndex("dbo.TagProjectUserPermissions", new[] { "UserId" });
            DropIndex("dbo.TagProjectUserPermissions", new[] { "TagProjectId" });
            DropTable("dbo.TagProjectUserPermissions");
        }
    }
}
