namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEmailProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers");
            DropIndex("dbo.Emails", new[] { "Receiver_Id" });
            RenameColumn(table: "dbo.Emails", name: "Receiver_Id", newName: "ReceiverId");
            AddColumn("dbo.Emails", "ReceiverCCId", c => c.Int(nullable: false));
            AlterColumn("dbo.Emails", "ReceiverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emails", "ReceiverId");
            AddForeignKey("dbo.Emails", "ReceiverId", "dbo.Receivers", "Id", cascadeDelete: true);
            DropColumn("dbo.Emails", "Receiver");
            DropColumn("dbo.Emails", "ReceiverDW");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "ReceiverDW", c => c.String());
            AddColumn("dbo.Emails", "Receiver", c => c.String(nullable: false));
            DropForeignKey("dbo.Emails", "ReceiverId", "dbo.Receivers");
            DropIndex("dbo.Emails", new[] { "ReceiverId" });
            AlterColumn("dbo.Emails", "ReceiverId", c => c.Int());
            DropColumn("dbo.Emails", "ReceiverCCId");
            RenameColumn(table: "dbo.Emails", name: "ReceiverId", newName: "Receiver_Id");
            CreateIndex("dbo.Emails", "Receiver_Id");
            AddForeignKey("dbo.Emails", "Receiver_Id", "dbo.Receivers", "Id");
        }
    }
}
