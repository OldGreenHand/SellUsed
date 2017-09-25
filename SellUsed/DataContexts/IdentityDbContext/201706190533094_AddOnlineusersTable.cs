namespace SellUsed.DataContexts.IdentityDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOnlineusersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlineUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        OnlineTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OnlineUsers");
        }
    }
}
