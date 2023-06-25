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
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<SenderCompanyData> SendersCompanyData { get; set; }
        public DbSet<SenderPersonalData> SendersPersonalData { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}