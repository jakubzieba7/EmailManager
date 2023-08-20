namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Footers", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Senders", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Emails", "Footer_Id", "dbo.Footers");
            DropForeignKey("dbo.ReceiverCCs", "ReceiverDataId", "dbo.ReceiverDatas");
            DropForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders");
            DropIndex("dbo.Emails", new[] { "SenderId" });
            DropIndex("dbo.Emails", new[] { "FooterId" });
            DropIndex("dbo.Emails", new[] { "Footer_Id" });
            DropIndex("dbo.Emails", new[] { "Sender_Id" });
            DropIndex("dbo.Footers", new[] { "Email_Id" });
            DropIndex("dbo.Senders", new[] { "Email_Id" });
            DropColumn("dbo.Emails", "FooterId");
            DropColumn("dbo.Emails", "SenderId");
            RenameColumn(table: "dbo.Emails", name: "Footer_Id", newName: "FooterId");
            RenameColumn(table: "dbo.Emails", name: "Sender_Id", newName: "SenderId");
            AlterColumn("dbo.ReceiverCCs", "ReceiverDataId", c => c.Int(nullable: true));
            AlterColumn("dbo.Emails", "FooterId", c => c.Int(nullable: false));
            AlterColumn("dbo.Emails", "SenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emails", "SenderId");
            CreateIndex("dbo.Emails", "FooterId");
            AddForeignKey("dbo.Emails", "FooterId", "dbo.Footers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceiverCCs", "ReceiverDataId", "dbo.ReceiverDatas", "Id");
            AddForeignKey("dbo.Emails", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
            DropColumn("dbo.Footers", "Email_Id");
            DropColumn("dbo.Receivers", "EmailId");
            DropColumn("dbo.ReceiverCCs", "EmailId");
            DropColumn("dbo.Senders", "Email_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Senders", "Email_Id", c => c.Int());
            AddColumn("dbo.ReceiverCCs", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Footers", "Email_Id", c => c.Int());
            DropForeignKey("dbo.Emails", "SenderId", "dbo.Senders");
            DropForeignKey("dbo.ReceiverCCs", "ReceiverDataId", "dbo.ReceiverDatas");
            DropForeignKey("dbo.Emails", "FooterId", "dbo.Footers");
            DropIndex("dbo.Emails", new[] { "FooterId" });
            DropIndex("dbo.Emails", new[] { "SenderId" });
            AlterColumn("dbo.Emails", "SenderId", c => c.Int());
            AlterColumn("dbo.Emails", "FooterId", c => c.Int());
            RenameColumn(table: "dbo.Emails", name: "SenderId", newName: "Sender_Id");
            RenameColumn(table: "dbo.Emails", name: "FooterId", newName: "Footer_Id");
            AddColumn("dbo.Emails", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Emails", "FooterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Senders", "Email_Id");
            CreateIndex("dbo.Footers", "Email_Id");
            CreateIndex("dbo.Emails", "Sender_Id");
            CreateIndex("dbo.Emails", "Footer_Id");
            CreateIndex("dbo.Emails", "FooterId");
            CreateIndex("dbo.Emails", "SenderId");
            AddForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders", "Id");
            AddForeignKey("dbo.ReceiverCCs", "ReceiverDataId", "dbo.ReceiverDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Emails", "Footer_Id", "dbo.Footers", "Id");
            AddForeignKey("dbo.Senders", "Email_Id", "dbo.Emails", "Id");
            AddForeignKey("dbo.Footers", "Email_Id", "dbo.Emails", "Id");
        }
    }
}
