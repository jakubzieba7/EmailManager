using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public void Add(FooterData footer)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.FooterDatas.Add(footer);
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

        public void Update(FooterData footer)
        {
            using (var context = new ApplicationDbContext())
            {
                var footerToUpdate = context.FooterDatas.Single(x => x.Id == footer.Id && x.UserId == footer.UserId);

                footerToUpdate.ComplimentaryClose = footer.ComplimentaryClose;

                context.SaveChanges();
            }
        }

        public void DeleteFooterData(int footerId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var footerToDelete = context.FooterDatas.Single(x => x.Id == footerId && x.UserId == userId);

                context.FooterDatas.Remove(footerToDelete);
                context.SaveChanges();
            }
        }
    }
}