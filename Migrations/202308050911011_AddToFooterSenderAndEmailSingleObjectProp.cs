namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToFooterSenderAndEmailSingleObjectProp : DbMigration
    {
        public override void Up()
        {
            //Sql("alter table dbo.Emails drop constraint [FK_dbo.Emails_dbo.Footers_FooterId];");

            DropForeignKey("dbo.FooterEmails", "Footer_Id", "dbo.Footers");
            DropForeignKey("dbo.FooterEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.SenderEmails", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.SenderEmails", "Email_Id", "dbo.Emails");
            DropIndex("dbo.FooterEmails", new[] { "Footer_Id" });
            DropIndex("dbo.FooterEmails", new[] { "Email_Id" });
            DropIndex("dbo.SenderEmails", new[] { "Sender_Id" });
            DropIndex("dbo.SenderEmails", new[] { "Email_Id" });
            AddColumn("dbo.Emails", "Footer_Id", c => c.Int());
            AddColumn("dbo.Emails", "Sender_Id", c => c.Int());
            AddColumn("dbo.Footers", "Email_Id", c => c.Int());
            AddColumn("dbo.FooterDatas", "Footer_Id", c => c.Int());
            AddColumn("dbo.Senders", "Email_Id", c => c.Int());
            CreateIndex("dbo.Emails", "SenderId");
            CreateIndex("dbo.Emails", "FooterId");
            CreateIndex("dbo.Emails", "Footer_Id");
            CreateIndex("dbo.Emails", "Sender_Id");
            CreateIndex("dbo.Footers", "Email_Id");
            CreateIndex("dbo.FooterDatas", "Footer_Id");
            CreateIndex("dbo.Senders", "SenderPersonalDataId");
            CreateIndex("dbo.Senders", "Email_Id");
            AddForeignKey("dbo.Emails", "Footer_Id", "dbo.Footers", "Id");
            AddForeignKey("dbo.FooterDatas", "Footer_Id", "dbo.Footers", "Id");
            AddForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders", "Id");
            AddForeignKey("dbo.Senders", "SenderPersonalDataId", "dbo.SenderPersonalDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Emails", "FooterId", "dbo.Footers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Footers", "Email_Id", "dbo.Emails", "Id");
            AddForeignKey("dbo.Emails", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Senders", "Email_Id", "dbo.Emails", "Id");
            DropTable("dbo.FooterEmails");
            DropTable("dbo.SenderEmails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SenderEmails",
                c => new
                    {
                        Sender_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sender_Id, t.Email_Id });
            
            CreateTable(
                "dbo.FooterEmails",
                c => new
                    {
                        Footer_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Footer_Id, t.Email_Id });
            
            DropForeignKey("dbo.Senders", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Emails", "SenderId", "dbo.Senders");
            DropForeignKey("dbo.Footers", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Emails", "FooterId", "dbo.Footers");
            DropForeignKey("dbo.Senders", "SenderPersonalDataId", "dbo.SenderPersonalDatas");
            DropForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.FooterDatas", "Footer_Id", "dbo.Footers");
            DropForeignKey("dbo.Emails", "Footer_Id", "dbo.Footers");
            DropIndex("dbo.Senders", new[] { "Email_Id" });
            DropIndex("dbo.Senders", new[] { "SenderPersonalDataId" });
            DropIndex("dbo.FooterDatas", new[] { "Footer_Id" });
            DropIndex("dbo.Footers", new[] { "Email_Id" });
            DropIndex("dbo.Emails", new[] { "Sender_Id" });
            DropIndex("dbo.Emails", new[] { "Footer_Id" });
            DropIndex("dbo.Emails", new[] { "FooterId" });
            DropIndex("dbo.Emails", new[] { "SenderId" });
            DropColumn("dbo.Senders", "Email_Id");
            DropColumn("dbo.FooterDatas", "Footer_Id");
            DropColumn("dbo.Footers", "Email_Id");
            DropColumn("dbo.Emails", "Sender_Id");
            DropColumn("dbo.Emails", "Footer_Id");
            CreateIndex("dbo.SenderEmails", "Email_Id");
            CreateIndex("dbo.SenderEmails", "Sender_Id");
            CreateIndex("dbo.FooterEmails", "Email_Id");
            CreateIndex("dbo.FooterEmails", "Footer_Id");
            AddForeignKey("dbo.SenderEmails", "Email_Id", "dbo.Emails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SenderEmails", "Sender_Id", "dbo.Senders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FooterEmails", "Email_Id", "dbo.Emails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FooterEmails", "Footer_Id", "dbo.Footers", "Id", cascadeDelete: true);
        }
    }
}
