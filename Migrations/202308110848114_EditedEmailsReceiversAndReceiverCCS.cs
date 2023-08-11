namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedEmailsReceiversAndReceiverCCS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttachmentDatas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attachments", "AttachmentDataId", "dbo.AttachmentDatas");
            DropIndex("dbo.AttachmentDatas", new[] { "UserId" });
            DropIndex("dbo.Attachments", new[] { "AttachmentDataId" });
            AddColumn("dbo.Attachments", "Lp", c => c.Int(nullable: false));
            AddColumn("dbo.Attachments", "FileName", c => c.String());
            AddColumn("dbo.Attachments", "FilePath", c => c.String());
            AddColumn("dbo.Attachments", "FileData", c => c.Binary());
            AddColumn("dbo.Attachments", "ContentType", c => c.String());
            AddColumn("dbo.Attachments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Attachments", "ApplicationUser_Id");
            AddForeignKey("dbo.Attachments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Attachments", "AttachmentDataId");
            DropColumn("dbo.Receivers", "EmailId");
            DropColumn("dbo.ReceiverCCs", "EmailId");
            DropTable("dbo.AttachmentDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AttachmentDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lp = c.Int(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        FileData = c.Binary(),
                        ContentType = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ReceiverCCs", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Receivers", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Attachments", "AttachmentDataId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Attachments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attachments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Attachments", "ApplicationUser_Id");
            DropColumn("dbo.Attachments", "ContentType");
            DropColumn("dbo.Attachments", "FileData");
            DropColumn("dbo.Attachments", "FilePath");
            DropColumn("dbo.Attachments", "FileName");
            DropColumn("dbo.Attachments", "Lp");
            CreateIndex("dbo.Attachments", "AttachmentDataId");
            CreateIndex("dbo.AttachmentDatas", "UserId");
            AddForeignKey("dbo.Attachments", "AttachmentDataId", "dbo.AttachmentDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AttachmentDatas", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
