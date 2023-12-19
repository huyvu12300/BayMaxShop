namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixcontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "ReplyMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "ReplyMessage");
        }
    }
}
