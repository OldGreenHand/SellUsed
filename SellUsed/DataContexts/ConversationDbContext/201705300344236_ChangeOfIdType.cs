namespace SellUsed.DataContexts.ConversationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOfIdType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageDetails", "Conversation_ConversationID", "dbo.ConversationDetails");
            DropIndex("dbo.MessageDetails", new[] { "Conversation_ConversationID" });
            DropColumn("dbo.MessageDetails", "ConversationID");
            RenameColumn(table: "dbo.MessageDetails", name: "Conversation_ConversationID", newName: "ConversationId");
            DropPrimaryKey("dbo.ConversationDetails");
            DropPrimaryKey("dbo.MessageDetails");
            AddColumn("dbo.ConversationDetails", "Updatetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageDetails", "Createtime", c => c.DateTime(nullable: false));

            DropColumn("dbo.ConversationDetails", "ConversationId");
            AddColumn("dbo.ConversationDetails", "ConversationId", c => c.Guid(nullable: false));
            DropColumn("dbo.MessageDetails", "MessageId");
            AddColumn("dbo.MessageDetails", "MessageId", c => c.Guid(nullable: false));
            DropColumn("dbo.MessageDetails", "ConversationId");
            AddColumn("dbo.MessageDetails", "ConversationId", c => c.Guid(nullable: false));
            DropColumn("dbo.MessageDetails", "ConversationId");
            AddColumn("dbo.MessageDetails", "ConversationId", c => c.Guid(nullable: false));

            AddPrimaryKey("dbo.ConversationDetails", "ConversationId");
            AddPrimaryKey("dbo.MessageDetails", "MessageId");
            CreateIndex("dbo.MessageDetails", "ConversationId");
            AddForeignKey("dbo.MessageDetails", "ConversationId", "dbo.ConversationDetails", "ConversationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageDetails", "ConversationId", "dbo.ConversationDetails");
            DropIndex("dbo.MessageDetails", new[] { "ConversationId" });
            DropPrimaryKey("dbo.MessageDetails");
            DropPrimaryKey("dbo.ConversationDetails");
            
            DropColumn("dbo.MessageDetails", "ConversationId");
            AddColumn("dbo.MessageDetails", "ConversationId", c => c.Int());
            DropColumn("dbo.MessageDetails", "ConversationId");
            AddColumn("dbo.MessageDetails", "ConversationId", c => c.String());
            DropColumn("dbo.MessageDetails", "MessageId");
            AddColumn("dbo.MessageDetails", "MessageId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ConversationDetails", "ConversationId");
            AddColumn("dbo.ConversationDetails", "ConversationId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.MessageDetails", "Createtime");
            DropColumn("dbo.ConversationDetails", "Updatetime");
            AddPrimaryKey("dbo.MessageDetails", "MessageId");
            AddPrimaryKey("dbo.ConversationDetails", "ConversationID");
            RenameColumn(table: "dbo.MessageDetails", name: "ConversationId", newName: "Conversation_ConversationID");
            AddColumn("dbo.MessageDetails", "ConversationID", c => c.String());
            CreateIndex("dbo.MessageDetails", "Conversation_ConversationID");
            AddForeignKey("dbo.MessageDetails", "Conversation_ConversationID", "dbo.ConversationDetails", "ConversationID");
        }
    }
}
