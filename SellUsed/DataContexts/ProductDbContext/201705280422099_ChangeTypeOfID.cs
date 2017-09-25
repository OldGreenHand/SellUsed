namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("identity.Products");

            AddColumn("identity.Products", "VisitTimes", c => c.Int(nullable: false));

            DropColumn("identity.Products", "ProductId");
            AddColumn("identity.Products", "ProductId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("identity.Products", "ProductId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("identity.Products");

            DropColumn("identity.Products", "ProductId");
            AddColumn("identity.Products", "ProductId", c => c.Guid(nullable: false, identity: true));
            DropColumn("identity.Products", "VisitTimes");
            AddPrimaryKey("identity.Products", "ProductId");
        }
    }
}
