namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SendersListAddedToEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "SenderId", "dbo.Senders");
            AddColumn("dbo.Emails", "Sender_Id", c => c.Int());
            AddColumn("dbo.Senders", "Email_Id", c => c.Int());
            CreateIndex("dbo.Emails", "Sender_Id");
            CreateIndex("dbo.Senders", "Email_Id");
            AddForeignKey("dbo.Senders", "Email_Id", "dbo.Emails", "Id");
            AddForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.Senders", "Email_Id", "dbo.Emails");
            DropIndex("dbo.Senders", new[] { "Email_Id" });
            DropIndex("dbo.Emails", new[] { "Sender_Id" });
            DropColumn("dbo.Senders", "Email_Id");
            DropColumn("dbo.Emails", "Sender_Id");
            AddForeignKey("dbo.Emails", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
        }
    }
}
