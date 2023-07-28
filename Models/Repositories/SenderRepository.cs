using EmailManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace EmailManager.Models.Repositories
{
    public class SenderRepository
    {
        public List<Sender> GetSenders(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Senders.Where(x => x.UserId == userId).ToList();
            }
        }
    }
}