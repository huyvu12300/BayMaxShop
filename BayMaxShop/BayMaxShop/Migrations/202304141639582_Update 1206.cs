namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1206 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_ProductImage", "IsDefault", c => c.Boolean(nullable: false));
            CreateIndex("dbo.tb_ProductImage", "ProductId");
            AddForeignKey("dbo.tb_ProductImage", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_ProductImage", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_ProductImage", new[] { "ProductId" });
            AlterColumn("dbo.tb_ProductImage", "IsDefault", c => c.String());
        }
    }
}
