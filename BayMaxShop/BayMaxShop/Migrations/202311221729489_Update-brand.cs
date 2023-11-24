namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatebrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brand", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brand", "CreatedDate");
        }
    }
}
