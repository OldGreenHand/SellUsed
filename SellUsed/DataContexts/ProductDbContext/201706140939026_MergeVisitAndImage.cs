namespace SellUsed.DataContexts.ProductDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeVisitAndImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.ProductVisits",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        VisitTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductVisits");
            DropTable("dbo.ProductImages");
        }
    }
}
