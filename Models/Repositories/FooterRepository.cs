using EmailManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace EmailManager.Models.Repositories
{
    public class FooterRepository
    {
        public List<FooterData> GetFooters(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Footers.Where(x => x.UserId == userId).ToList();
            }
        }
    }
}