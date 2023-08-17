namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSenderProperties : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Senders", "SenderCompanyDataId");
            CreateIndex("dbo.Senders", "SenderEmailParamsId");
            AddForeignKey("dbo.Senders", "SenderCompanyDataId", "dbo.SenderCompanyDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Senders", "SenderEmailParamsId", "dbo.SenderEmailParams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Senders", "SenderEmailParamsId", "dbo.SenderEmailParams");
            DropForeignKey("dbo.Senders", "SenderCompanyDataId", "dbo.SenderCompanyDatas");
            DropIndex("dbo.Senders", new[] { "SenderEmailParamsId" });
            DropIndex("dbo.Senders", new[] { "SenderCompanyDataId" });
        }
    }
}
