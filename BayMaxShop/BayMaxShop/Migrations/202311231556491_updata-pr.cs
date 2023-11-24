namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatapr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Product", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Title", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Product", "ProductName");
        }
    }
}
