namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "SeoDescription");
            DropColumn("dbo.Product", "SeoKeywords");
            DropColumn("dbo.ProductCategory", "SeoTitle");
            DropColumn("dbo.ProductCategory", "SeoDescription");
            DropColumn("dbo.ProductCategory", "SeoKeywords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategory", "SeoKeywords", c => c.String(maxLength: 250));
            AddColumn("dbo.ProductCategory", "SeoDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.ProductCategory", "SeoTitle", c => c.String(maxLength: 250));
            AddColumn("dbo.Product", "SeoKeywords", c => c.String());
            AddColumn("dbo.Product", "SeoDescription", c => c.String());
        }
    }
}
