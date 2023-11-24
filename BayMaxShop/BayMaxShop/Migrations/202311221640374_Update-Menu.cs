namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMenu : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Menu", newName: "tb_Category");
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Link = c.String(),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(maxLength: 250),
                        SeoKeywords = c.String(maxLength: 150),
                        Position = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Category", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_New", "Menu_Id", c => c.Int());
            AddColumn("dbo.tb_Posts", "Menu_Id", c => c.Int());
            CreateIndex("dbo.tb_New", "Menu_Id");
            CreateIndex("dbo.tb_Posts", "Menu_Id");
            AddForeignKey("dbo.tb_New", "Menu_Id", "dbo.Menu", "Id");
            AddForeignKey("dbo.tb_Posts", "Menu_Id", "dbo.Menu", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Posts", "Menu_Id", "dbo.Menu");
            DropForeignKey("dbo.tb_New", "Menu_Id", "dbo.Menu");
            DropIndex("dbo.tb_Posts", new[] { "Menu_Id" });
            DropIndex("dbo.tb_New", new[] { "Menu_Id" });
            DropColumn("dbo.tb_Posts", "Menu_Id");
            DropColumn("dbo.tb_New", "Menu_Id");
            DropColumn("dbo.tb_Category", "IsActive");
            DropTable("dbo.Menu");
            RenameTable(name: "dbo.tb_Category", newName: "Menu");
        }
    }
}
