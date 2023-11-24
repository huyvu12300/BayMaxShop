namespace BayMaxShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedatabaes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brand", "Image", c => c.String(maxLength: 250));
            DropColumn("dbo.Brand", "Description");
            DropTable("dbo.Contact");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Website = c.String(),
                        Messsage = c.String(maxLength: 4000),
                        IsRead = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Brand", "Description", c => c.String(maxLength: 150));
            DropColumn("dbo.Brand", "Image");
        }
    }
}
