namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Texts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagProjectTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TagProjectCategoryId = c.Long(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        AddedDateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TagProjectCategories", t => t.TagProjectCategoryId, cascadeDelete: true)
                .Index(t => t.TagProjectCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagProjectTexts", "TagProjectCategoryId", "dbo.TagProjectCategories");
            DropIndex("dbo.TagProjectTexts", new[] { "TagProjectCategoryId" });
            DropTable("dbo.TagProjectTexts");
        }
    }
}
