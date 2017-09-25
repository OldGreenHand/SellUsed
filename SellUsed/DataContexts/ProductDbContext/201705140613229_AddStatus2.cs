namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("identity.Products", "UserId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("identity.Products", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
