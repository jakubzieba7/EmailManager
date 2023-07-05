namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSenderEmailParams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SenderEmailParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HostSmtp = c.String(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        Port = c.Int(nullable: false),
                        SenderEmail = c.String(nullable: false),
                        SenderEmailPassword = c.String(nullable: false),
                        SenderName = c.String(nullable: false),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .Index(t => t.Sender_Id);
            
            AddColumn("dbo.Senders", "SenderEmailParamsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SenderEmailParams", "Sender_Id", "dbo.Senders");
            DropIndex("dbo.SenderEmailParams", new[] { "Sender_Id" });
            DropColumn("dbo.Senders", "SenderEmailParamsId");
            DropTable("dbo.SenderEmailParams");
        }
    }
}
