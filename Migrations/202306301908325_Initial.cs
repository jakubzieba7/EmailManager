namespace EmailManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageSubject = c.String(nullable: false),
                        MessageBody = c.String(),
                        Receiver = c.String(nullable: false),
                        ReceiverDW = c.String(),
                        SenderId = c.Int(nullable: false),
                        FooterId = c.Int(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                        EmailSendDate = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Senders", t => t.SenderId, cascadeDelete: true)
                .ForeignKey("dbo.Footers", t => t.FooterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.SenderId)
                .Index(t => t.FooterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Footers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplimentaryClose = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        SenderId = c.Int(nullable: false),
                        EmailId = c.Int(nullable: false),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .ForeignKey("dbo.Senders", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SenderId)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Senders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderPersonalDataId = c.Int(nullable: false),
                        SenderCompanyDataId = c.Int(nullable: false),
                        EmailId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        FooterId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Receivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        EmailAddress = c.String(nullable: false),
                        EmailReceivedId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SenderCompanyDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyAddressId = c.Int(nullable: false),
                        CompanyDataId = c.Int(nullable: false),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.CompanyAddressId, cascadeDelete: true)
                .ForeignKey("dbo.CompanyDatas", t => t.CompanyDataId, cascadeDelete: true)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .Index(t => t.CompanyAddressId)
                .Index(t => t.CompanyDataId)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.CompanyDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        NIP = c.String(nullable: false),
                        REGON = c.String(),
                        Comments = c.String(),
                        Logo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SenderPersonalDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        CompanyPositionPl = c.String(nullable: false),
                        CompanyPositionEn = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ReceiverEmails",
                c => new
                    {
                        Receiver_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receiver_Id, t.Email_Id })
                .ForeignKey("dbo.Receivers", t => t.Receiver_Id, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.Email_Id, cascadeDelete: true)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Email_Id);
            
            CreateTable(
                "dbo.ReceiverSenders",
                c => new
                    {
                        Receiver_Id = c.Int(nullable: false),
                        Sender_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receiver_Id, t.Sender_Id })
                .ForeignKey("dbo.Receivers", t => t.Receiver_Id, cascadeDelete: true)
                .ForeignKey("dbo.Senders", t => t.Sender_Id, cascadeDelete: true)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Senders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Receivers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Footers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Emails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Emails", "FooterId", "dbo.Footers");
            DropForeignKey("dbo.Footers", "SenderId", "dbo.Senders");
            DropForeignKey("dbo.Emails", "SenderId", "dbo.Senders");
            DropForeignKey("dbo.SenderPersonalDatas", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.SenderCompanyDatas", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.SenderCompanyDatas", "CompanyDataId", "dbo.CompanyDatas");
            DropForeignKey("dbo.SenderCompanyDatas", "CompanyAddressId", "dbo.Addresses");
            DropForeignKey("dbo.ReceiverSenders", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.ReceiverSenders", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.ReceiverEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.ReceiverEmails", "Receiver_Id", "dbo.Receivers");
            DropForeignKey("dbo.Footers", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.ReceiverSenders", new[] { "Sender_Id" });
            DropIndex("dbo.ReceiverSenders", new[] { "Receiver_Id" });
            DropIndex("dbo.ReceiverEmails", new[] { "Email_Id" });
            DropIndex("dbo.ReceiverEmails", new[] { "Receiver_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.SenderPersonalDatas", new[] { "Sender_Id" });
            DropIndex("dbo.SenderCompanyDatas", new[] { "Sender_Id" });
            DropIndex("dbo.SenderCompanyDatas", new[] { "CompanyDataId" });
            DropIndex("dbo.SenderCompanyDatas", new[] { "CompanyAddressId" });
            DropIndex("dbo.Receivers", new[] { "UserId" });
            DropIndex("dbo.Senders", new[] { "UserId" });
            DropIndex("dbo.Footers", new[] { "Sender_Id" });
            DropIndex("dbo.Footers", new[] { "SenderId" });
            DropIndex("dbo.Footers", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "FooterId" });
            DropIndex("dbo.Emails", new[] { "SenderId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.ReceiverSenders");
            DropTable("dbo.ReceiverEmails");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.SenderPersonalDatas");
            DropTable("dbo.CompanyDatas");
            DropTable("dbo.SenderCompanyDatas");
            DropTable("dbo.Receivers");
            DropTable("dbo.Senders");
            DropTable("dbo.Footers");
            DropTable("dbo.Emails");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
