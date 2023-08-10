namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReceiverToEmailsAndClearReceivers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverEmails", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Senders", "Receiver_Id", "dbo.Receivers");
            DropIndex("dbo.Attachments", new[] { "Receiver_Id" });
            DropIndex("dbo.Senders", new[] { "Receiver_Id" });
            DropIndex("dbo.ReceiverEmails", new[] { "Receiver_Id" });
            DropIndex("dbo.ReceiverEmails", new[] { "Email_Id" });
            AddColumn("dbo.Emails", "Receiver_Id", c => c.Int());
            AddColumn("dbo.Receivers", "Email_Id", c => c.Int());
            CreateIndex("dbo.Emails", "ReceiverId");
            CreateIndex("dbo.Emails", "Receiver_Id");
            CreateIndex("dbo.Receivers", "Email_Id");
            AddForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers", "Id");
            AddForeignKey("dbo.Emails", "ReceiverId", "dbo.Receivers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Receivers", "Email_Id", "dbo.Emails", "Id");
            DropColumn("dbo.Attachments", "Receiver_Id");
            DropColumn("dbo.Receivers", "SenderId");
            DropColumn("dbo.Receivers", "AttachmentId");
            DropColumn("dbo.Senders", "Receiver_Id");
            DropTable("dbo.ReceiverEmails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReceiverEmails",
                c => new
                    {
                        Receiver_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receiver_Id, t.Email_Id });
            
            AddColumn("dbo.Senders", "Receiver_Id", c => c.Int());
            AddColumn("dbo.Receivers", "AttachmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Attachments", "Receiver_Id", c => c.Int());
            DropForeignKey("dbo.Receivers", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Emails", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers");
            DropIndex("dbo.Receivers", new[] { "Email_Id" });
            DropIndex("dbo.Emails", new[] { "Receiver_Id" });
            DropIndex("dbo.Emails", new[] { "ReceiverId" });
            DropColumn("dbo.Receivers", "Email_Id");
            DropColumn("dbo.Emails", "Receiver_Id");
            CreateIndex("dbo.ReceiverEmails", "Email_Id");
            CreateIndex("dbo.ReceiverEmails", "Receiver_Id");
            CreateIndex("dbo.Senders", "Receiver_Id");
            CreateIndex("dbo.Attachments", "Receiver_Id");
            AddForeignKey("dbo.Senders", "Receiver_Id", "dbo.Receivers", "Id");
            AddForeignKey("dbo.ReceiverEmails", "Email_Id", "dbo.Emails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverEmails", "Receiver_Id", "dbo.Receivers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attachments", "Receiver_Id", "dbo.Receivers", "Id");
        }
    }
}
