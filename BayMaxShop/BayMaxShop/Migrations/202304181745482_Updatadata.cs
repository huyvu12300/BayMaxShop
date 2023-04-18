namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatadata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Images", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Images");
        }
    }
}
