namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiverDivisionEdit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receivers", "EmailId", c => c.Int(nullable: false));
            DropColumn("dbo.Receivers", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receivers", "EmailAddress", c => c.String());
            DropColumn("dbo.Receivers", "EmailId");
        }
    }
}
