namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategory", "ProductCategoryName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.ProductCategory", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategory", "Title", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.ProductCategory", "ProductCategoryName");
        }
    }
}
