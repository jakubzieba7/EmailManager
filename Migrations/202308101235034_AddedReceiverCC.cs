namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReceiverCC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReceiverCCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailId = c.Int(nullable: false),
                        ReceiverDataId = c.Int(nullable: false),
                        Email_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReceiverDatas", t => t.ReceiverDataId, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.Email_Id)
                .Index(t => t.ReceiverDataId)
                .Index(t => t.Email_Id);
            
            AddColumn("dbo.Emails", "ReceiverCCId", c => c.Int(nullable: false));
            AddColumn("dbo.Emails", "ReceiverCC_Id", c => c.Int());
            CreateIndex("dbo.Emails", "ReceiverCCId");
            CreateIndex("dbo.Emails", "ReceiverCC_Id");
            AddForeignKey("dbo.Emails", "ReceiverCC_Id", "dbo.ReceiverCCs", "Id");
            AddForeignKey("dbo.Emails", "ReceiverCCId", "dbo.ReceiverCCs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiverCCs", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Emails", "ReceiverCCId", "dbo.ReceiverCCs");
            DropForeignKey("dbo.ReceiverCCs", "ReceiverDataId", "dbo.ReceiverDatas");
            DropForeignKey("dbo.Emails", "ReceiverCC_Id", "dbo.ReceiverCCs");
            DropIndex("dbo.ReceiverCCs", new[] { "Email_Id" });
            DropIndex("dbo.ReceiverCCs", new[] { "ReceiverDataId" });
            DropIndex("dbo.Emails", new[] { "ReceiverCC_Id" });
            DropIndex("dbo.Emails", new[] { "ReceiverCCId" });
            DropColumn("dbo.Emails", "ReceiverCC_Id");
            DropColumn("dbo.Emails", "ReceiverCCId");
            DropTable("dbo.ReceiverCCs");
        }
    }
}
