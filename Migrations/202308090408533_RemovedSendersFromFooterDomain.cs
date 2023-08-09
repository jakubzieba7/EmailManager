namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSendersFromFooterDomain : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReceiverSenders", newName: "SenderReceivers");
            DropForeignKey("dbo.Footers", "Senders_Id", "dbo.Senders");
            DropForeignKey("dbo.Footers", "Sender_Id", "dbo.Senders");
            DropIndex("dbo.Footers", new[] { "Sender_Id" });
            DropIndex("dbo.Footers", new[] { "Senders_Id" });
            DropColumn("dbo.Footers", "SenderId");
            RenameColumn(table: "dbo.Footers", name: "Sender_Id", newName: "SenderId");
            DropPrimaryKey("dbo.SenderReceivers");
            AlterColumn("dbo.Footers", "SenderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SenderReceivers", new[] { "Sender_Id", "Receiver_Id" });
            CreateIndex("dbo.Footers", "SenderId");
            AddForeignKey("dbo.Footers", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
            DropColumn("dbo.Footers", "Senders_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Footers", "Senders_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Footers", "SenderId", "dbo.Senders");
            DropIndex("dbo.Footers", new[] { "SenderId" });
            DropPrimaryKey("dbo.SenderReceivers");
            AlterColumn("dbo.Footers", "SenderId", c => c.Int());
            AddPrimaryKey("dbo.SenderReceivers", new[] { "Receiver_Id", "Sender_Id" });
            RenameColumn(table: "dbo.Footers", name: "SenderId", newName: "Sender_Id");
            AddColumn("dbo.Footers", "SenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Footers", "Senders_Id");
            CreateIndex("dbo.Footers", "Sender_Id");
            AddForeignKey("dbo.Footers", "Sender_Id", "dbo.Senders", "Id");
            AddForeignKey("dbo.Footers", "Senders_Id", "dbo.Senders", "Id");
            RenameTable(name: "dbo.SenderReceivers", newName: "ReceiverSenders");
        }
    }
}
