namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tb_AddressBook", newName: "AddressBook");
            RenameTable(name: "dbo.tb_Contact", newName: "Contact");
            RenameTable(name: "dbo.tb_New", newName: "New");
            RenameTable(name: "dbo.tb_Posts", newName: "Posts");
            RenameTable(name: "dbo.tb_OrderDetail", newName: "OrderDetail");
            RenameTable(name: "dbo.tb_Order", newName: "Order");
            RenameTable(name: "dbo.tb_Product", newName: "Product");
            RenameTable(name: "dbo.tb_ProductCategory", newName: "ProductCategory");
            RenameTable(name: "dbo.tb_ProductImage", newName: "ProductImage");
            RenameTable(name: "dbo.tb_Review", newName: "Review");
            RenameTable(name: "dbo.tb_Wishlist", newName: "Wishlist");
            RenameTable(name: "dbo.tb_Subscribe", newName: "Subscribe");
            RenameTable(name: "dbo.tb_SystemSetting", newName: "SystemSetting");
            AddColumn("dbo.New", "NewsName", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Posts", "PostName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.New", "Title");
            DropColumn("dbo.Posts", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.New", "Title", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Posts", "PostName");
            DropColumn("dbo.New", "NewsName");
            RenameTable(name: "dbo.SystemSetting", newName: "tb_SystemSetting");
            RenameTable(name: "dbo.Subscribe", newName: "tb_Subscribe");
            RenameTable(name: "dbo.Wishlist", newName: "tb_Wishlist");
            RenameTable(name: "dbo.Review", newName: "tb_Review");
            RenameTable(name: "dbo.ProductImage", newName: "tb_ProductImage");
            RenameTable(name: "dbo.ProductCategory", newName: "tb_ProductCategory");
            RenameTable(name: "dbo.Product", newName: "tb_Product");
            RenameTable(name: "dbo.Order", newName: "tb_Order");
            RenameTable(name: "dbo.OrderDetail", newName: "tb_OrderDetail");
            RenameTable(name: "dbo.Posts", newName: "tb_Posts");
            RenameTable(name: "dbo.New", newName: "tb_New");
            RenameTable(name: "dbo.Contact", newName: "tb_Contact");
            RenameTable(name: "dbo.AddressBook", newName: "tb_AddressBook");
        }
    }
}
