namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("identity.Products", "Createtime", c => c.DateTime(nullable: false));
            AddColumn("identity.Products", "StreetNo", c => c.String(maxLength: 5));
            AddColumn("identity.Products", "StreetRoute", c => c.String());
            AddColumn("identity.Products", "Suburb", c => c.String(nullable: false, maxLength: 26));
            AddColumn("identity.Products", "State", c => c.String(nullable: false, maxLength: 3));
            AddColumn("identity.Products", "Postcode", c => c.String(nullable: false, maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("identity.Products", "Postcode");
            DropColumn("identity.Products", "State");
            DropColumn("identity.Products", "Suburb");
            DropColumn("identity.Products", "StreetRoute");
            DropColumn("identity.Products", "StreetNo");
            DropColumn("identity.Products", "Createtime");
        }
    }
}
