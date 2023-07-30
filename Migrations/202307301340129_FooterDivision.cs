namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FooterDivision : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.Footers drop constraint [FK_dbo.Footers_dbo.AspNetUsers_UserId];");
            RenameTable(name: "dbo.ReceiverSenders", newName: "SenderReceivers");
            DropIndex("dbo.Footers", new[] { "UserId" });
            DropPrimaryKey("dbo.SenderReceivers");
            CreateTable(
                "dbo.FooterDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplimentaryClose = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Emails", "FooterData_Id", c => c.Int());
            AddColumn("dbo.Footers", "FooterDataId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SenderReceivers", new[] { "Sender_Id", "Receiver_Id" });
            CreateIndex("dbo.Emails", "FooterData_Id");
            CreateIndex("dbo.Footers", "FooterDataId");
            AddForeignKey("dbo.Emails", "FooterData_Id", "dbo.FooterDatas", "Id");
            AddForeignKey("dbo.Footers", "FooterDataId", "dbo.FooterDatas", "Id", cascadeDelete: true);
            DropColumn("dbo.Footers", "ComplimentaryClose");
            DropColumn("dbo.Footers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Footers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Footers", "ComplimentaryClose", c => c.String(nullable: false));
            DropForeignKey("dbo.Footers", "FooterDataId", "dbo.FooterDatas");
            DropForeignKey("dbo.Emails", "FooterData_Id", "dbo.FooterDatas");
            DropIndex("dbo.Footers", new[] { "FooterDataId" });
            DropIndex("dbo.FooterDatas", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "FooterData_Id" });
            DropPrimaryKey("dbo.SenderReceivers");
            DropColumn("dbo.Footers", "FooterDataId");
            DropColumn("dbo.Emails", "FooterData_Id");
            DropTable("dbo.FooterDatas");
            AddPrimaryKey("dbo.SenderReceivers", new[] { "Receiver_Id", "Sender_Id" });
            CreateIndex("dbo.Footers", "UserId");
            RenameTable(name: "dbo.SenderReceivers", newName: "ReceiverSenders");
        }
    }
}
