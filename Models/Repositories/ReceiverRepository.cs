using EmailManager.Models;
using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailManager.Controllers
{
    public class ReceiverRepository
    {

        public List<ReceiverData> GetReceivers(string userId)
        {
            using (var context=new ApplicationDbContext())
            {
                return context.ReceiverDatas.Where(x=>x.UserId==userId).ToList();
            }
        }
    }
}