using EmailManager.Models.Domains;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailManager.Models;
using System.IO;
using System.Linq;
using EmailManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;

namespace EmailManager.Controllers
{
    public class EmailSendingController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private SavingAttachmentsHelper _savingAttachmentsHelper = new SavingAttachmentsHelper();
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;

        private async Task Send(Email email)
        {
            int attachmentId = 0;

            var emailParams = new SenderEmailParams
            {
                Id = 1,
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderEmail = "services.report.new@gmail.com",
                SenderEmailPassword = "",
                SenderName = "Jakub Zięba"
            };

            _hostSmtp = emailParams.HostSmtp;
            _enableSsl = emailParams.EnableSsl;
            _port = emailParams.Port;
            _senderEmail = emailParams.SenderEmail;
            _senderEmailPassword = emailParams.SenderEmailPassword;
            _senderName = emailParams.SenderName;

            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.To.Add(new MailAddress(email.Receiver.ReceiverData.EmailAddress));

            var isReceiverCCNotExist = ReferenceEquals(email.ReceiverCC, null);

            if (!isReceiverCCNotExist)
                _mail.To.Add(new MailAddress(email.ReceiverCC.ReceiverData.EmailAddress));

            _mail.IsBodyHtml = true;
            _mail.Subject = email.MessageSubject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            var attachmentsData = _attachmentRepository.GetAttachments(email);

            if (_attachmentRepository.GetAttachments(email).Count() > 0)
            {
                _savingAttachmentsHelper.AttachmentsFilePath(email, attachmentId);

                foreach (var attachmentData in attachmentsData)
                {
                    _mail.Attachments.Add(new System.Net.Mail.Attachment(_savingAttachmentsHelper.attachmentDownloadFolderPath + '\\' + attachmentData.FileName + '.' + attachmentData.ContentType));
                }
            }

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.MessageBody, null, MediaTypeNames.Text.Plain));

            string HtmlContent = RazorViewToStringFormat.RenderRazorViewToString(this, "EmailTemplate", email);

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(HtmlContent, null, MediaTypeNames.Text.Html));

            _smtp = new SmtpClient
            {
                Host = _hostSmtp,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
            };

            _smtp.SendCompleted += OnSendCompleted;

            await _smtp.SendMailAsync(_mail);
        }

        private void SaveAttachmentsToSelectedFolder(Email email, int attachmentId = 0)
        {
            foreach (var attachmentFilePath in _savingAttachmentsHelper.AttachmentsFilePath(email, attachmentId))
            {
                FileInfo file = new FileInfo(attachmentFilePath); // full file path on disk
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AppendHeader("Content-Length", file.Length.ToString());
                //Response.ContentType = "text/plain";
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(file.FullName);
                Response.Flush();
                Response.End();
            }
        }

        public FileResult SaveAttachments(int emailId)
        {
            var userId = User.Identity.GetUserId();
            var fileName = string.Format("{0}_SpakowaneZałączniki_{1}.zip", "Email_nr" + emailId, DateTime.Today.Date.ToString("dd-MM-yyyy"));
            string tempOutPutPath = _savingAttachmentsHelper.attachmentDownloadFolderPath + fileName; ;
            int attachmentId = 0;


            //SaveAttachmentsToSelectedFolder(_emailRepository.GetEmail(emailId, userId));

            using (ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                byte[] buffer = new byte[4096];

                var ImageList = new List<string>();

                foreach (var attachmentFilePath in _savingAttachmentsHelper.AttachmentsFilePath(_emailRepository.GetEmail(emailId, userId), attachmentId))
                {
                    ImageList.Add(attachmentFilePath);

                }

                for (int i = 0; i < ImageList.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(ImageList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);

                    using (FileStream fs = System.IO.File.OpenRead(ImageList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Flush();
                s.Close();

                byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
                if (System.IO.File.Exists(tempOutPutPath))
                    System.IO.File.Delete(tempOutPutPath);

                if (finalResult == null || !finalResult.Any())
                    throw new Exception(String.Format("Brak załączników do załadowania"));
                
                return File(finalResult, "application/zip", fileName);
            }

        }

        public ActionResult SaveAttachment(int attachmentId, int emailId)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                SaveAttachmentsToSelectedFolder(_emailRepository.GetEmail(emailId, userId), attachmentId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SendEmail(int emailId)
        {
            var userId = User.Identity.GetUserId();
            Email email;

            try
            {
                email = _emailRepository.GetEmail(emailId, userId);
                await Send(email);
            }
            catch (Exception)
            {
                return View("_SystemExceptionError");
            }

            ViewBag.SuccessfulSentMailMessage = "Mail został wysłany";

            return View("EmailTemplate", email);
        }

        public ActionResult EmailTemplate(int emailId)
        {
            var userId = User.Identity.GetUserId();

            return View(_emailRepository.GetEmail(emailId, userId));
        }
    }
}