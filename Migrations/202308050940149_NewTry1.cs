namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTry1 : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.Emails drop constraint [FK_dbo.Emails_dbo.Senders_SenderId];");
            
        }
        
        public override void Down()
        {
        }
    }
}
