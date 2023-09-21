using EmailManager.Models.Domains;
using System;
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
                return context.FooterDatas.Where(x => x.UserId == userId).ToList();
            }
        }

        public FooterData GetFooterData(int footerId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.FooterDatas.Single(x => x.UserId == userId && x.Id == footerId);
            }
        }
    }
}