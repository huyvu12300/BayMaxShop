namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategory", "SeoTitle", c => c.String());
            AddColumn("dbo.ProductCategory", "SeoDescription", c => c.String());
            AddColumn("dbo.ProductCategory", "SeoKeywords", c => c.String());
            AlterColumn("dbo.ProductCategory", "Description", c => c.String());
            AlterColumn("dbo.ProductCategory", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategory", "Icon", c => c.String(maxLength: 250));
            AlterColumn("dbo.ProductCategory", "Description", c => c.String(maxLength: 150));
            DropColumn("dbo.ProductCategory", "SeoKeywords");
            DropColumn("dbo.ProductCategory", "SeoDescription");
            DropColumn("dbo.ProductCategory", "SeoTitle");
        }
    }
}
