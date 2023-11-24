namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tb_Category", newName: "Menu");
            DropColumn("dbo.Menu", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu", "IsActive", c => c.Boolean(nullable: false));
            RenameTable(name: "dbo.Menu", newName: "tb_Category");
        }
    }
}
