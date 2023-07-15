namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttachment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        FileData = c.Binary(),
                        ContentType = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Email_Id = c.Int(),
                        Sender_Id = c.Int(),
                        Receiver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Emails", t => t.Email_Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .ForeignKey("dbo.Receivers", t => t.Receiver_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Email_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.Receiver_Id);
            
            AddColumn("dbo.Senders", "AttachmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "AttachmentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.Attachments", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.Attachments", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Attachments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attachments", new[] { "Receiver_Id" });
            DropIndex("dbo.Attachments", new[] { "Sender_Id" });
            DropIndex("dbo.Attachments", new[] { "Email_Id" });
            DropIndex("dbo.Attachments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Receivers", "AttachmentId");
            DropColumn("dbo.Senders", "AttachmentId");
            DropTable("dbo.Attachments");
        }
    }
}
