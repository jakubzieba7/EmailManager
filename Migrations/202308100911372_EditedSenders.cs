namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedSenders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.ReceiverSenders", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverSenders", "Sender_Id", "dbo.Senders");
            DropIndex("dbo.Attachments", new[] { "Sender_Id" });
            DropIndex("dbo.ReceiverSenders", new[] { "Receiver_Id" });
            DropIndex("dbo.ReceiverSenders", new[] { "Sender_Id" });
            AddColumn("dbo.Senders", "Receiver_Id", c => c.Int());
            CreateIndex("dbo.Senders", "Receiver_Id");
            AddForeignKey("dbo.Senders", "Receiver_Id", "dbo.Receivers", "Id");
            DropColumn("dbo.Attachments", "Sender_Id");
            DropColumn("dbo.Senders", "EmailId");
            DropColumn("dbo.Senders", "ReceiverId");
            DropColumn("dbo.Senders", "FooterId");
            DropColumn("dbo.Senders", "AttachmentId");
            DropTable("dbo.ReceiverSenders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReceiverSenders",
                c => new
                    {
                        Receiver_Id = c.Int(nullable: false),
                        Sender_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receiver_Id, t.Sender_Id });
            
            AddColumn("dbo.Senders", "AttachmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Senders", "FooterId", c => c.Int(nullable: false));
            AddColumn("dbo.Senders", "ReceiverId", c => c.Int(nullable: false));
            AddColumn("dbo.Senders", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Attachments", "Sender_Id", c => c.Int());
            DropForeignKey("dbo.Senders", "Receiver_Id", "dbo.Receivers");
            DropIndex("dbo.Senders", new[] { "Receiver_Id" });
            DropColumn("dbo.Senders", "Receiver_Id");
            CreateIndex("dbo.ReceiverSenders", "Sender_Id");
            CreateIndex("dbo.ReceiverSenders", "Receiver_Id");
            CreateIndex("dbo.Attachments", "Sender_Id");
            AddForeignKey("dbo.ReceiverSenders", "Sender_Id", "dbo.Senders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverSenders", "Receiver_Id", "dbo.Receivers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attachments", "Sender_Id", "dbo.Senders", "Id");
        }
    }
}
