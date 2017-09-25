namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("identity.Products", "Status", c => c.Boolean(nullable: false));
            AlterColumn("identity.Products", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("identity.Products", "UserId", c => c.String(maxLength: 128));
            DropColumn("identity.Products", "Status");
        }
    }
}
