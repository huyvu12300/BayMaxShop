namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenews1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.New", "SeoTitle");
            DropColumn("dbo.New", "SeoDescription");
            DropColumn("dbo.New", "SeoKeywords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.New", "SeoKeywords", c => c.String());
            AddColumn("dbo.New", "SeoDescription", c => c.String());
            AddColumn("dbo.New", "SeoTitle", c => c.String());
        }
    }
}
