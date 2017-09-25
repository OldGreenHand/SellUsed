namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    using SellUsed.DataContexts.IdentityDbContext;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "identity.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
        }
        
        public override void Down()
        {
            DropTable("identity.Products");
        }
    }
}
