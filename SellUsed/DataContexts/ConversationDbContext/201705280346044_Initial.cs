namespace SellUsed.DataContexts.ConversationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationDetails",
                c => new
                    {
                        ConversationID = c.Int(nullable: false, identity: true),
                        UserID1 = c.String(maxLength: 128),
                        UserID2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ConversationID);
            
            CreateTable(
                "dbo.MessageDetails",
                c => new
                    {
                        MessageId = c.String(nullable: false, maxLength: 128),
                        FromUserID = c.String(maxLength: 128),
                        FromUserName = c.String(),
                        ToUserID = c.String(maxLength: 128),
                        ToUserName = c.String(),
                        Message = c.String(),
                        ConversationID = c.String(),
                        Conversation_ConversationID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.ConversationDetails", t => t.Conversation_ConversationID)
                .Index(t => t.Conversation_ConversationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageDetails", "Conversation_ConversationID", "dbo.ConversationDetails");
            DropIndex("dbo.MessageDetails", new[] { "Conversation_ConversationID" });
            DropTable("dbo.MessageDetails");
            DropTable("dbo.ConversationDetails");
        }
    }
}
