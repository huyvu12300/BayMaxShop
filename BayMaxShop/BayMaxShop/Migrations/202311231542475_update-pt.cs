namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatept : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "SeoTitle");
            DropColumn("dbo.Posts", "SeoDescription");
            DropColumn("dbo.Posts", "SeoKeywords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "SeoKeywords", c => c.String());
            AddColumn("dbo.Posts", "SeoDescription", c => c.String());
            AddColumn("dbo.Posts", "SeoTitle", c => c.String());
        }
    }
}
