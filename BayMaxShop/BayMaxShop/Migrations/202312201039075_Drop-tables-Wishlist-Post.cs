namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DroptablesWishlistPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wishlist", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Wishlist", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "MenuId", "dbo.Menu");
            DropIndex("dbo.Wishlist", new[] { "ProductId" });
            DropIndex("dbo.Wishlist", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "MenuId" });
            AddColumn("dbo.New", "Author", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AddressBook", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Review", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Review", "FullName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Review", "Content", c => c.String(maxLength: 70));
            DropTable("dbo.Wishlist");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        PostName = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        MenuId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wishlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        UserName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Review", "Content", c => c.String());
            AlterColumn("dbo.Review", "FullName", c => c.String());
            AlterColumn("dbo.Review", "Email", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AlterColumn("dbo.AddressBook", "Phone", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.New", "Author");
            CreateIndex("dbo.Posts", "MenuId");
            CreateIndex("dbo.Wishlist", "UserID");
            CreateIndex("dbo.Wishlist", "ProductId");
            AddForeignKey("dbo.Posts", "MenuId", "dbo.Menu", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Wishlist", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Wishlist", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
    }
}
