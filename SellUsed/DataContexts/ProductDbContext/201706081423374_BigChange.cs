namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BigChange : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "identity.Products", newSchema: "dbo");
            MoveTable(name: "identity.UserFavoriteAds", newSchema: "dbo");
            DropColumn("dbo.Products", "VisitTimes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "VisitTimes", c => c.Int(nullable: false));
            MoveTable(name: "dbo.UserFavoriteAds", newSchema: "identity");
            MoveTable(name: "dbo.Products", newSchema: "identity");
        }
    }
}
