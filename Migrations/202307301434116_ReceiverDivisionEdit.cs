namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiverDivision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receivers", "ReceiverDataId", c => c.Int(nullable: false));
            CreateIndex("dbo.Receivers", "ReceiverDataId");
            AddForeignKey("dbo.Receivers", "ReceiverDataId", "dbo.ReceiverDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receivers", "ReceiverDataId", "dbo.ReceiverDatas");
            DropIndex("dbo.Receivers", new[] { "ReceiverDataId" });
            DropColumn("dbo.Receivers", "ReceiverDataId");
        }
    }
}
