using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Models.Repositories
{
    public class EmailRepository
    {
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();

        public List<Email> GetEmails(string userId)
        {

            using (var context = new ApplicationDbContext())
            {
                var emailsListIncludingReceiverCCs=context.Emails.
                Include(x => x.Sender.SenderPersonalData).
                Include(x => x.Receiver.ReceiverData).
                Include(x => x.ReceiverCC.ReceiverData).
                Where(x => x.UserId == userId).ToList();

                var allEmails = context.Emails.
                Include(x => x.Sender.SenderPersonalData).
                Include(x => x.Receiver.ReceiverData).
                Where(x => x.UserId == userId).ToList();

                var allEmailsSubtractedThoseIncludingReceiverCCs = allEmails.Except(emailsListIncludingReceiverCCs).ToList();

                var allEmailsIncludingReceiverCCData = new List<Email>(emailsListIncludingReceiverCCs.Count +
                                    allEmailsSubtractedThoseIncludingReceiverCCs.Count);
                allEmailsIncludingReceiverCCData.AddRange(emailsListIncludingReceiverCCs);
                allEmailsIncludingReceiverCCData.AddRange(allEmailsSubtractedThoseIncludingReceiverCCs);

                return allEmailsIncludingReceiverCCData.OrderBy(x=>x.Id).ToList();
                
            }
        }

        public Email GetEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var email = context.Emails.Single(x => x.UserId == userId && x.Id == emailId);
                var receiverCC = context.ReceiverCCs.Single(x => x.Id == email.ReceiverCCId);

                if (receiverCC.ReceiverDataId == null) 
                {
                    return context.Emails
                        .Include(x => x.Attachments)
                        .Include(x => x.Sender.SenderPersonalData)
                        .Include(x => x.User)
                        .Include(x => x.User.Address)
                        .Include(x => x.Receivers)
                        .Include(x => x.Receiver.ReceiverData)
                        .Include(x => x.Footer.FooterData)
                        .Single(x => x.UserId == userId && x.Id == emailId);
                }
                else
                {
                    return context.Emails
                        .Include(x => x.Attachments)
                        .Include(x => x.Sender.SenderPersonalData)
                        .Include(x => x.User)
                        .Include(x => x.User.Address)
                        .Include(x => x.Receivers)
                        .Include(x => x.Receiver.ReceiverData)
                        .Include(x => x.ReceiverCCs)
                        .Include(x => x.ReceiverCC.ReceiverData)
                        .Include(x => x.Footer.FooterData)
                        .Single(x => x.UserId == userId && x.Id == emailId);
                }
            }
        }

        public Attachment GetAttachment(string userId, int attachmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attachments
                    .Include(x => x.Email)
                    .Single(x => x.Email.UserId == userId && x.Email.AttachmentId == attachmentId);
            }
        }

        public void Add(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                email.EmailSendDate = DateTime.Now;
                email.SenderId = email.Sender.Id;
                email.ReceiverId = email.Receiver.Id;
                email.ReceiverCCId = email.ReceiverCC.Id;
                email.FooterId = email.Footer.Id;
                context.Emails.Add(email);
                context.SaveChanges();
            }
        }

        //public static bool CheckReceiverCCExist(Email email)
        //{
        //    using (var context=new ApplicationDbContext())
        //    {

        //    var emailToUpdate = context.Emails.Single(x => x.Id == email.Id && x.UserId == email.UserId);
        //    var receiverCC = context.ReceiverCCs.Single(x => x.Id == emailToUpdate.ReceiverCCId);

        //        if (receiverCC.ReceiverDataId is null)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        public void Update(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToUpdate = context.Emails.Single(x => x.Id == email.Id && x.UserId == email.UserId);
                var receiverCC = context.ReceiverCCs.Single(x => x.Id == emailToUpdate.ReceiverCCId);

                if (receiverCC.ReceiverDataId == null)
                {
                    emailToUpdate = context.Emails.
                        Include(x => x.Sender).
                        Include(x => x.Sender.SenderPersonalData).
                        Include(x => x.Receiver).
                        Include(x => x.Receiver.ReceiverData).
                        Include(x => x.Footer).
                        Include(x => x.Footer.FooterData).
                        Single(x => x.Id == email.Id && x.UserId == email.UserId);
                }
                else
                {
                    emailToUpdate = context.Emails.
                        Include(x => x.Sender).
                        Include(x => x.Sender.SenderPersonalData).
                        Include(x => x.Receiver).
                        Include(x => x.Receiver.ReceiverData).
                        Include(x => x.ReceiverCC).
                        Include(x => x.ReceiverCC.ReceiverData).
                        Include(x => x.Footer).
                        Include(x => x.Footer.FooterData).
                        Single(x => x.Id == email.Id && x.UserId == email.UserId);
                }

                emailToUpdate.Sender.SenderPersonalDataId = email.Sender.SenderPersonalDataId;
                emailToUpdate.MessageSubject = email.MessageSubject;
                emailToUpdate.MessageBody = email.MessageBody;
                emailToUpdate.Footer.FooterDataId = email.Footer.FooterDataId;
                emailToUpdate.Receiver.ReceiverDataId = email.Receiver.ReceiverDataId;
                emailToUpdate.ReceiverCC.ReceiverDataId = email.ReceiverCC.ReceiverDataId;
                emailToUpdate.EmailSendDate = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void AddEmailAttachment(EditEmailAttachmentViewModel attachmentVM, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                //weryfikacja czy dany rekord istnieje w bazie danych
                var email = context.Emails.Single(x => x.Id == attachmentVM.Attachment.EmailId && x.UserId == userId);
                //var attachmentLp = email.Attachments.Select(x => x.Lp).Last();

                //attachmentVM.Attachment.Lp = attachmentLp + 1;
                attachmentVM.Attachment.FileData = _attachmentRepository.GetAttachmentContent(attachmentVM);
                attachmentVM.Attachment.FileName = GetFileData.FileNameFromStreamFileName(attachmentVM.InputAttachmentData.FileName);
                attachmentVM.Attachment.ContentType = GetFileData.FileExtension(attachmentVM.InputAttachmentData.FileName);

                context.Attachments.Add(attachmentVM.Attachment);
                context.SaveChanges();
            }
        }

        public void UpdateEmailAttachment(Attachment attachment, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var attachmentToUpdate = context.Attachments.Include(x => x.Email).Include(x => x.FileData).Single(x => x.Id == attachment.Id && x.EmailId == attachment.EmailId && x.Email.UserId == userId);

                attachmentToUpdate.Lp = attachment.Lp;
                attachmentToUpdate.ContentType = attachment.ContentType;
                attachmentToUpdate.FileName = attachment.FileName;
                attachmentToUpdate.FileData = attachment.FileData;

                context.SaveChanges();
            }
        }

        public void DeleteAttachment(int attachmentId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var attachmentToDelete = context.Attachments.Single(x => x.Id == attachmentId && x.Email.UserId == userId);

                context.Attachments.Remove(attachmentToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToDelete = context.Emails.Single(x => x.Id == emailId && x.UserId == userId);

                context.Emails.Remove(emailToDelete);
                context.SaveChanges();
            }
        }
    }
}