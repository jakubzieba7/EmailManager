namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotOfDomainChanges : DbMigration
    {
        public override void Up()
        {

            DropForeignKey("dbo.Emails", "FooterId", "dbo.Footers");
            DropForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.Emails", "SenderId", "dbo.Senders");
            DropForeignKey("dbo.Senders", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Footers", "SenderId", "dbo.Senders");
            DropIndex("dbo.Emails", new[] { "SenderId" });
            DropIndex("dbo.Emails", new[] { "FooterId" });
            DropIndex("dbo.Emails", new[] { "Sender_Id" });
            DropIndex("dbo.Footers", new[] { "SenderId" });
            DropIndex("dbo.Senders", new[] { "Email_Id" });
            CreateTable(
                "dbo.FooterEmails",
                c => new
                    {
                        Footer_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Footer_Id, t.Email_Id })
                .ForeignKey("dbo.Footers", t => t.Footer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.Email_Id, cascadeDelete: true)
                .Index(t => t.Footer_Id)
                .Index(t => t.Email_Id);
            
            CreateTable(
                "dbo.SenderEmails",
                c => new
                    {
                        Sender_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sender_Id, t.Email_Id })
                .ForeignKey("dbo.Senders", t => t.Sender_Id, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.Email_Id, cascadeDelete: true)
                .Index(t => t.Sender_Id)
                .Index(t => t.Email_Id);
            
            AddColumn("dbo.Footers", "Senders_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Footers", "Senders_Id");
            AddForeignKey("dbo.Footers", "Senders_Id", "dbo.Senders", "Id");
            DropColumn("dbo.Emails", "Sender_Id");
            DropColumn("dbo.Senders", "Email_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Senders", "Email_Id", c => c.Int());
            AddColumn("dbo.Emails", "Sender_Id", c => c.Int());
            DropForeignKey("dbo.Footers", "Senders_Id", "dbo.Senders");
            DropForeignKey("dbo.SenderEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.SenderEmails", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.FooterEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.FooterEmails", "Footer_Id", "dbo.Footers");
            DropIndex("dbo.SenderEmails", new[] { "Email_Id" });
            DropIndex("dbo.SenderEmails", new[] { "Sender_Id" });
            DropIndex("dbo.FooterEmails", new[] { "Email_Id" });
            DropIndex("dbo.FooterEmails", new[] { "Footer_Id" });
            DropIndex("dbo.Footers", new[] { "Senders_Id" });
            DropColumn("dbo.Footers", "Senders_Id");
            DropTable("dbo.SenderEmails");
            DropTable("dbo.FooterEmails");
            CreateIndex("dbo.Senders", "Email_Id");
            CreateIndex("dbo.Footers", "SenderId");
            CreateIndex("dbo.Emails", "Sender_Id");
            CreateIndex("dbo.Emails", "FooterId");
            CreateIndex("dbo.Emails", "SenderId");
            AddForeignKey("dbo.Footers", "SenderId", "dbo.Senders", "Id");
            AddForeignKey("dbo.Senders", "Email_Id", "dbo.Emails", "Id");
            AddForeignKey("dbo.Emails", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Emails", "Sender_Id", "dbo.Senders", "Id");
            AddForeignKey("dbo.Emails", "FooterId", "dbo.Footers", "Id", cascadeDelete: true);
        }
    }
}
