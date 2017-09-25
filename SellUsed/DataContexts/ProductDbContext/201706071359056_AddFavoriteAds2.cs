namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoriteAds2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("identity.UserFavoriteAds");
            AddColumn("identity.UserFavoriteAds", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("identity.UserFavoriteAds", "UserId", c => c.String());
            AddPrimaryKey("identity.UserFavoriteAds", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("identity.UserFavoriteAds");
            AlterColumn("identity.UserFavoriteAds", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("identity.UserFavoriteAds", "Id");
            AddPrimaryKey("identity.UserFavoriteAds", "UserId");
        }
    }
}
