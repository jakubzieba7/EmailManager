using EmailManager.Models;
using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailManager.Controllers
{
    public class ReceiverRepository
    {

        public List<Receiver> GetReceivers(string userId)
        {
            using (var context=new ApplicationDbContext())
            {
                return context.Receivers.Where(x=>x.UserId==userId).ToList();
            }
        }
    }
}