namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagProjectCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagProjectCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AddedDateTime = c.DateTime(nullable: false),
                        TagProjectId = c.Long(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Permission = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagProjects", t => t.TagProjectId, cascadeDelete: false)
                .Index(t => t.TagProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagProjectCategories", "TagProjectId", "dbo.TagProjects");
            DropIndex("dbo.TagProjectCategories", new[] { "TagProjectId" });
            DropTable("dbo.TagProjectCategories");
        }
    }
}
