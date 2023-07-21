namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttachmentDomainEdited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "Email_Id", "dbo.Emails");
            DropIndex("dbo.Attachments", new[] { "Email_Id" });
            RenameColumn(table: "dbo.Attachments", name: "Email_Id", newName: "EmailId");
            AddColumn("dbo.Attachments", "Lp", c => c.Int(nullable: false));
            AlterColumn("dbo.Attachments", "EmailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attachments", "EmailId");
            AddForeignKey("dbo.Attachments", "EmailId", "dbo.Emails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "EmailId", "dbo.Emails");
            DropIndex("dbo.Attachments", new[] { "EmailId" });
            AlterColumn("dbo.Attachments", "EmailId", c => c.Int());
            DropColumn("dbo.Attachments", "Lp");
            RenameColumn(table: "dbo.Attachments", name: "EmailId", newName: "Email_Id");
            CreateIndex("dbo.Attachments", "Email_Id");
            AddForeignKey("dbo.Attachments", "Email_Id", "dbo.Emails", "Id");
        }
    }
}
