namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToSenderDomain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Senders", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Senders", "Name");
        }
    }
}
