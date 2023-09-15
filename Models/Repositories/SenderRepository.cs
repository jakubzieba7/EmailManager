using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public SenderPersonalData GetSenderPersonalData(int senderId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.SendersPersonalData.Single(x => x.UserId == userId && x.Id == senderId);
            }
        }

        public void Add(SenderPersonalData sender)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.SendersPersonalData.Add(sender);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                
            }
        }

        public void Update(SenderPersonalData sender)
        {
            using (var context = new ApplicationDbContext())
            {
                var senderToUpdate = context.SendersPersonalData.Single(x => x.Id == sender.Id && x.UserId == sender.UserId);

                senderToUpdate.Name = sender.Name;
                senderToUpdate.CompanyPositionPl = sender.CompanyPositionPl;
                senderToUpdate.CompanyPositionEn = sender.CompanyPositionEn;
                senderToUpdate.PhoneNumber = sender.PhoneNumber;

                context.SaveChanges();
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