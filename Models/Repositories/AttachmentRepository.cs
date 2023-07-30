using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Repositories
{
    public class AttachmentRepository
    {
        public List<Attachment> GetAttachments(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attachments.Where(x => x.Email.UserId == userId).ToList();
            }
        }

        byte[] filebyte = null;

        public byte[] GetAttachmentContent(EditEmailAttachmentViewModel attachmentViewModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var attachment = attachmentViewModel.InputAttachmentData;
                var attachmentLoadPath = attachmentViewModel.InputAttachmentData.FileName;

                using (var stream = new FileStream(attachmentLoadPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        //limitation to only one file send as byte array
                        //first from the list will be send
                        filebyte = reader.ReadBytes((int)stream.Length);
                    }
                }

                //save files to local disk
                if (attachment.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(attachment.FileName);
                    var path = Path.Combine("~/AppData/uploads", fileName);
                    attachment.SaveAs(path);
                }

                //IEnumerable<HttpPostedFileBase> attachments;

                //foreach (var file in attachments)
                //{
                //    if (file.ContentLength > 0)
                //    {
                //        var fileName = Path.GetFileName(file.FileName);
                //        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                //        file.SaveAs(path);
                //    }
                //}


                return filebyte;
            }

        }
    }

}