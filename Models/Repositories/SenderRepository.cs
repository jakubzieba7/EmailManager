using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailManager.Models.Repositories
{
    public class SenderRepository
    {
        public List<SenderPersonalData> GetSenders(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.SendersPersonalData.Where(x => x.UserId == userId).ToList();
            }
        }

        public void DeleteSenderPersonalData(int senderId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var senderToDelete = context.SendersPersonalData.Single(x => x.Id == senderId && x.UserId == userId);

                context.SendersPersonalData.Remove(senderToDelete);
                context.SaveChanges();
            }
        }
    }
}