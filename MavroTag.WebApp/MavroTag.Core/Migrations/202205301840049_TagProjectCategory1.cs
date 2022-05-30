namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagProjectCategory1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TagProjectCategories", "Permission");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagProjectCategories", "Permission", c => c.String());
        }
    }
}
