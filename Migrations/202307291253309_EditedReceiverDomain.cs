namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedReceiverDomain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Receivers", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Receivers", "EmailAddress", c => c.String(nullable: false));
        }
    }
}
