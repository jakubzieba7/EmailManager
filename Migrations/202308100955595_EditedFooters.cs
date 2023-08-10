namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedFooters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Footers", "SenderId", "dbo.Senders");
            DropIndex("dbo.Footers", new[] { "SenderId" });
            DropColumn("dbo.Footers", "SenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Footers", "SenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Footers", "SenderId");
            AddForeignKey("dbo.Footers", "SenderId", "dbo.Senders", "Id", cascadeDelete: true);
        }
    }
}
