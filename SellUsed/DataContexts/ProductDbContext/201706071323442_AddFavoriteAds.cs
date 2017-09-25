namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoriteAds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "identity.UserFavoriteAds",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        productId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("identity.UserFavoriteAds");
        }
    }
}
