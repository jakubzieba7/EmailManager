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
            DropForeignKey("dbo.Emails", "FooterData_Id", "dbo.FooterDatas");
            DropIndex("dbo.Emails", new[] { "FooterData_Id" });
            DropIndex("dbo.Receivers", new[] { "UserId" });
            DropPrimaryKey("dbo.ReceiverSenders");
            CreateTable(
                "dbo.ReceiverDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Emails", "ReceiverId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "ReceiverDataId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Receiver_Id", "Sender_Id" });
            CreateIndex("dbo.Receivers", "ReceiverDataId");
            AddForeignKey("dbo.Receivers", "ReceiverDataId", "dbo.ReceiverDatas", "Id");
            DropColumn("dbo.Emails", "Receiver");
            DropColumn("dbo.Emails", "ReceiverDW");
            DropColumn("dbo.Emails", "FooterData_Id");
            DropColumn("dbo.Receivers", "Name");
            DropColumn("dbo.Receivers", "Surname");
            DropColumn("dbo.Receivers", "EmailAddress");
            DropColumn("dbo.Receivers", "EmailReceivedId");
            DropColumn("dbo.Receivers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receivers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Receivers", "EmailReceivedId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "EmailAddress", c => c.String());
            AddColumn("dbo.Receivers", "Surname", c => c.String());
            AddColumn("dbo.Receivers", "Name", c => c.String());
            AddColumn("dbo.Emails", "FooterData_Id", c => c.Int());
            AddColumn("dbo.Emails", "ReceiverDW", c => c.String());
            AddColumn("dbo.Emails", "Receiver", c => c.String(nullable: false));
            DropForeignKey("dbo.Receivers", "ReceiverDataId", "dbo.ReceiverDatas");
            DropIndex("dbo.ReceiverDatas", new[] { "UserId" });
            DropIndex("dbo.Receivers", new[] { "ReceiverDataId" });
            DropPrimaryKey("dbo.ReceiverSenders");
            DropColumn("dbo.Receivers", "ReceiverDataId");
            DropColumn("dbo.Receivers", "EmailId");
            DropColumn("dbo.Emails", "ReceiverId");
            DropTable("dbo.ReceiverDatas");
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Sender_Id", "Receiver_Id" });
            CreateIndex("dbo.Receivers", "UserId");
            CreateIndex("dbo.Emails", "FooterData_Id");
            AddForeignKey("dbo.Emails", "FooterData_Id", "dbo.FooterDatas", "Id");
            RenameTable(name: "dbo.ReceiverSenders", newName: "SenderReceivers");
        }
    }
}
