namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Status");
        }
    }
}
