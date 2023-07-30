namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiverDivision : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.Receivers drop constraint [FK_dbo.Receivers_dbo.AspNetUsers_UserId];");
            RenameTable(name: "dbo.SenderReceivers", newName: "ReceiverSenders");
            DropForeignKey("dbo.ReceiverEmails", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverEmails", "Email_Id", "dbo.Emails");
            DropIndex("dbo.Receivers", new[] { "UserId" });
            DropIndex("dbo.ReceiverEmails", new[] { "Receiver_Id" });
            DropIndex("dbo.ReceiverEmails", new[] { "Email_Id" });
            DropPrimaryKey("dbo.ReceiverSenders");
            CreateTable(
                "dbo.ReceiverDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Emails", t => t.Email_Id)
                .Index(t => t.UserId)
                .Index(t => t.Email_Id);
            
            AddColumn("dbo.Emails", "Receiver_Id", c => c.Int());
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Receiver_Id", "Sender_Id" });
            CreateIndex("dbo.Emails", "Receiver_Id");
            AddForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers", "Id");
            DropColumn("dbo.Receivers", "Name");
            DropColumn("dbo.Receivers", "Surname");
            DropColumn("dbo.Receivers", "EmailReceivedId");
            DropColumn("dbo.Receivers", "UserId");
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
            
            AddColumn("dbo.Receivers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Receivers", "EmailReceivedId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "Surname", c => c.String());
            AddColumn("dbo.Receivers", "Name", c => c.String());
            DropForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverDatas", "Email_Id", "dbo.Emails");
            DropIndex("dbo.ReceiverDatas", new[] { "Email_Id" });
            DropIndex("dbo.ReceiverDatas", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "Receiver_Id" });
            DropPrimaryKey("dbo.ReceiverSenders");
            DropColumn("dbo.Emails", "Receiver_Id");
            DropTable("dbo.ReceiverDatas");
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Sender_Id", "Receiver_Id" });
            CreateIndex("dbo.ReceiverEmails", "Email_Id");
            CreateIndex("dbo.ReceiverEmails", "Receiver_Id");
            CreateIndex("dbo.Receivers", "UserId");
            AddForeignKey("dbo.ReceiverEmails", "Email_Id", "dbo.Emails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverEmails", "Receiver_Id", "dbo.Receivers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ReceiverSenders", newName: "SenderReceivers");
        }
    }
}
