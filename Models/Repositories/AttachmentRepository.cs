using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Repositories
{
    public class AttachmentRepository
    {
        public List<Attachment> GetAttachments()
        {
            using (var context=new ApplicationDbContext())
            {
                return context.Attachments.ToList();
            }
        }

        public byte[] GetAttachmentContent(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}