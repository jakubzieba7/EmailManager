using EmailManager.Models;
using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public ReceiverData GetReceiverData(int receiverId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.ReceiverDatas.Single(x => x.UserId == userId && x.Id == receiverId);
            }
        }

        public void Add(ReceiverData receiverData)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.ReceiverDatas.Add(receiverData);
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

        public void Update(ReceiverData receiverData)
        {
            using (var context = new ApplicationDbContext())
            {
                var receiverToUpdate = context.ReceiverDatas.Single(x => x.Id == receiverData.Id && x.UserId == receiverData.UserId);

                receiverToUpdate.Name = receiverData.Name;
                receiverToUpdate.EmailAddress = receiverData.EmailAddress;

                context.SaveChanges();
            }
        }

        public void DeleteReceiverData(int receiverId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var receiverToDelete = context.ReceiverDatas.Single(x => x.Id == receiverId && x.UserId == userId);

                context.ReceiverDatas.Remove(receiverToDelete);
                context.SaveChanges();
            }
        }
    }
}