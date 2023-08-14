using System.Data.Entity;
using EmailManager.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmailManager.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CompanyData> CompaniesData { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FooterData> FooterDatas { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<ReceiverData> ReceiverDatas { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<ReceiverCC> ReceiverCCs { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<SenderCompanyData> SendersCompanyData { get; set; }
        public DbSet<SenderPersonalData> SendersPersonalData { get; set; }
        public DbSet<SenderEmailParams> SendersEmailParams { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receiver>()
                .HasRequired(x => x.ReceiverData)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReceiverCC>()
               .HasRequired(x => x.ReceiverData)
               .WithMany()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Emails)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.FooterDatas)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ReceiverDatas)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.SenderPersonalDatas)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}