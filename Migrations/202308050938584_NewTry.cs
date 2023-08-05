namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTry : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.Emails drop constraint [FK_dbo.Emails_dbo.Footers_FooterId];");
        }
        
        public override void Down()
        {
        }
    }
}
