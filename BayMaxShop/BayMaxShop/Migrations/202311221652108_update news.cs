namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_New", "CategoryId", "dbo.tb_Category");
            DropForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category");
            DropForeignKey("dbo.tb_New", "Menu_Id", "dbo.Menu");
            DropForeignKey("dbo.tb_Posts", "Menu_Id", "dbo.Menu");
            DropIndex("dbo.tb_New", new[] { "CategoryId" });
            DropIndex("dbo.tb_New", new[] { "Menu_Id" });
            DropIndex("dbo.tb_Posts", new[] { "CategoryId" });
            DropIndex("dbo.tb_Posts", new[] { "Menu_Id" });
            RenameColumn(table: "dbo.tb_New", name: "Menu_Id", newName: "MenuId");
            RenameColumn(table: "dbo.tb_Posts", name: "Menu_Id", newName: "MenuId");
            AlterColumn("dbo.tb_New", "MenuId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Posts", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_New", "MenuId");
            CreateIndex("dbo.tb_Posts", "MenuId");
            AddForeignKey("dbo.tb_New", "MenuId", "dbo.Menu", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_Posts", "MenuId", "dbo.Menu", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_New", "CategoryId");
            DropColumn("dbo.tb_Posts", "CategoryId");
            DropTable("dbo.tb_Adv");
            DropTable("dbo.tb_Category");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Link = c.String(),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(maxLength: 250),
                        SeoKeywords = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Adv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Link = c.String(maxLength: 500),
                        Type = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Posts", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.tb_New", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_Posts", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.tb_New", "MenuId", "dbo.Menu");
            DropIndex("dbo.tb_Posts", new[] { "MenuId" });
            DropIndex("dbo.tb_New", new[] { "MenuId" });
            AlterColumn("dbo.tb_Posts", "MenuId", c => c.Int());
            AlterColumn("dbo.tb_New", "MenuId", c => c.Int());
            RenameColumn(table: "dbo.tb_Posts", name: "MenuId", newName: "Menu_Id");
            RenameColumn(table: "dbo.tb_New", name: "MenuId", newName: "Menu_Id");
            CreateIndex("dbo.tb_Posts", "Menu_Id");
            CreateIndex("dbo.tb_Posts", "CategoryId");
            CreateIndex("dbo.tb_New", "Menu_Id");
            CreateIndex("dbo.tb_New", "CategoryId");
            AddForeignKey("dbo.tb_Posts", "Menu_Id", "dbo.Menu", "Id");
            AddForeignKey("dbo.tb_New", "Menu_Id", "dbo.Menu", "Id");
            AddForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_New", "CategoryId", "dbo.tb_Category", "Id", cascadeDelete: true);
        }
    }
}
