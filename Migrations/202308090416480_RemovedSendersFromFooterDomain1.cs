namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSendersFromFooterDomain1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SenderReceivers", newName: "ReceiverSenders");
            DropPrimaryKey("dbo.ReceiverSenders");
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Receiver_Id", "Sender_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ReceiverSenders");
            AddPrimaryKey("dbo.ReceiverSenders", new[] { "Sender_Id", "Receiver_Id" });
            RenameTable(name: "dbo.ReceiverSenders", newName: "SenderReceivers");
        }
    }
}
