﻿namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenderPropertiesNamesEdited : DbMigration
    {
        public override void Up()
        {
            //Sql("alter table dbo.Emails drop constraint [FK_dbo.Emails_dbo.Footers_SenderId];");

        }

        public override void Down()
        {
        }
    }
}
