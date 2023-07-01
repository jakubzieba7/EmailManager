﻿namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedAddressProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropColumn("dbo.Addresses", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
