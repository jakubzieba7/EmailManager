﻿// <auto-generated />
namespace EmailManager.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class AddedReceiverToEmailsAndClearReceivers : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddedReceiverToEmailsAndClearReceivers));
        
        string IMigrationMetadata.Id
        {
            get { return "202308101157352_AddedReceiverToEmailsAndClearReceivers"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}