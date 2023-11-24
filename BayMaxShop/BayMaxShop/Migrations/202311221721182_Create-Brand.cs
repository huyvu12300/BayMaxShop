namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "BrandId");
            AddForeignKey("dbo.Product", "BrandId", "dbo.Brand", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "BrandId", "dbo.Brand");
            DropIndex("dbo.Product", new[] { "BrandId" });
            DropColumn("dbo.Product", "BrandId");
            DropTable("dbo.Brand");
        }
    }
}
