namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagProjects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        AddedDateTime = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagProjects", "UserId", "dbo.Users");
            DropIndex("dbo.TagProjects", new[] { "UserId" });
            DropTable("dbo.TagProjects");
        }
    }
}
