namespace SellUsed.DataContexts.ConversationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seperateoneconversationintotwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageDetails", "ConversationId", "dbo.ConversationDetails");
            DropIndex("dbo.MessageDetails", new[] { "ConversationId" });
            RenameColumn(table: "dbo.MessageDetails", name: "ConversationId", newName: "Conversation_ConversationId");
            AddColumn("dbo.ConversationDetails", "FromUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ConversationDetails", "ToUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ConversationDetails", "ToUserName", c => c.String(maxLength: 128));
            AddColumn("dbo.ConversationDetails", "UnreadMessageNo", c => c.Int(nullable: false));
            AddColumn("dbo.MessageDetails", "ConversationId1", c => c.Guid(nullable: false));
            AddColumn("dbo.MessageDetails", "ConversationId2", c => c.Guid(nullable: false));
            AlterColumn("dbo.MessageDetails", "Conversation_ConversationId", c => c.Guid());
            CreateIndex("dbo.MessageDetails", "Conversation_ConversationId");
            AddForeignKey("dbo.MessageDetails", "Conversation_ConversationId", "dbo.ConversationDetails", "ConversationId");
            DropColumn("dbo.ConversationDetails", "UserId1");
            DropColumn("dbo.ConversationDetails", "UserId2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConversationDetails", "UserId2", c => c.String(maxLength: 128));
            AddColumn("dbo.ConversationDetails", "UserId1", c => c.String(maxLength: 128));
            DropForeignKey("dbo.MessageDetails", "Conversation_ConversationId", "dbo.ConversationDetails");
            DropIndex("dbo.MessageDetails", new[] { "Conversation_ConversationId" });
            AlterColumn("dbo.MessageDetails", "Conversation_ConversationId", c => c.Guid(nullable: false));
            DropColumn("dbo.MessageDetails", "ConversationId2");
            DropColumn("dbo.MessageDetails", "ConversationId1");
            DropColumn("dbo.ConversationDetails", "UnreadMessageNo");
            DropColumn("dbo.ConversationDetails", "ToUserName");
            DropColumn("dbo.ConversationDetails", "ToUserId");
            DropColumn("dbo.ConversationDetails", "FromUserId");
            RenameColumn(table: "dbo.MessageDetails", name: "Conversation_ConversationId", newName: "ConversationId");
            CreateIndex("dbo.MessageDetails", "ConversationId");
            AddForeignKey("dbo.MessageDetails", "ConversationId", "dbo.ConversationDetails", "ConversationId", cascadeDelete: true);
        }
    }
}
