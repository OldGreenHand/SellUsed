namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LimitTilteLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("identity.Products", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("identity.Products", "Name", c => c.String(nullable: false));
        }
    }
}
