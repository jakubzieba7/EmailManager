namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdTransferedFromSenderToSenderPersonalData : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.Senders drop constraint [FK_dbo.Senders_dbo.AspNetUsers_UserId];");

            DropIndex("dbo.Senders", new[] { "UserId" });
            AddColumn("dbo.SenderPersonalDatas", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.SenderPersonalDatas", "UserId");
            DropColumn("dbo.Senders", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Senders", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.SenderPersonalDatas", new[] { "UserId" });
            DropColumn("dbo.SenderPersonalDatas", "UserId");
            CreateIndex("dbo.Senders", "UserId");
        }
    }
}
