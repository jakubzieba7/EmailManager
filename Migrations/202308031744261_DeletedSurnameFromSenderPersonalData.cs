namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedSurnameFromSenderPersonalData : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SenderPersonalDatas", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SenderPersonalDatas", "Surname", c => c.String(nullable: false));
        }
    }
}
