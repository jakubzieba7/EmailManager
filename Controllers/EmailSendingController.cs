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
using System.Web.Helpers;

namespace EmailManager.Controllers
{
    public class EmailSendingController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;
        private string _filePath;
        string attachmentDownloadFolderPath;

        private async Task Send(Email email)
        {
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

            var isReceiverCCExist = ReferenceEquals(email.ReceiverCC, null);

            if (!isReceiverCCExist)
                _mail.To.Add(new MailAddress(email.ReceiverCC.ReceiverData.EmailAddress));

            _mail.IsBodyHtml = true;
            _mail.Subject = email.MessageSubject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            var attachmentsData = _attachmentRepository.GetAttachments(email).Select(x => new { x.FileData, x.FileName, x.ContentType });

            if (_attachmentRepository.GetAttachments(email).Count() > 0)
            {
                SaveFileBytesFromDBToDisk(email);

                foreach (var attachmentData in attachmentsData)
                {
                    _mail.Attachments.Add(new System.Net.Mail.Attachment(attachmentDownloadFolderPath + '\\' + attachmentData.FileName + '.' + attachmentData.ContentType));
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

        private void SaveFileBytesFromDBToDisk(Email email)
        {
            var attachmentsData = _attachmentRepository.GetAttachments(email).Select(x => new { x.FileData, x.FileName, x.ContentType });
            attachmentDownloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmailSenderApp");

            if (!Directory.Exists(attachmentDownloadFolderPath))
            {
                Directory.CreateDirectory(attachmentDownloadFolderPath);
            }

            foreach (var attachmentData in attachmentsData)
            {
                System.IO.File.WriteAllBytes(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType), attachmentData.FileData);
            }
        }

        private void SaveAttachmentsToSelectedFolder(Email email)
        {
            var attachmentsData = _attachmentRepository.GetAttachments(email).Select(x => new { x.FileData, x.FileName, x.ContentType });
            attachmentDownloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmailSenderApp");

            if (!Directory.Exists(attachmentDownloadFolderPath))
            {
                Directory.CreateDirectory(attachmentDownloadFolderPath);
            }

            string filepath;

            foreach (var attachmentData in attachmentsData)
            {
                System.IO.File.WriteAllBytes(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType), attachmentData.FileData);

                filepath = Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType);
                FileInfo file = new FileInfo(filepath); // full file path on disk
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                //Response.ContentType = "text/plain";
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(file.FullName);
                Response.Flush();
                Response.End();
            }

        }

        private void SaveAttachmentToSelectedFolder(Email email, int attachmentId)
        {
            var attachmentData = _attachmentRepository.GetAttachment(email, attachmentId);

            attachmentDownloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmailSenderApp");

            if (!Directory.Exists(attachmentDownloadFolderPath))
            {
                Directory.CreateDirectory(attachmentDownloadFolderPath);
            }

            string filepath;


            System.IO.File.WriteAllBytes(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType), attachmentData.FileData);

            filepath = Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType);
            FileInfo file = new FileInfo(filepath); // full file path on disk
            Response.ClearContent(); // neded to clear previous (if any) written content
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "text/plain";
            //Response.ContentType = "application/octet-stream";
            Response.TransmitFile(file.FullName);
            Response.Flush();
            Response.End();


        }

        public ActionResult SaveAttachments(int emailId)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                SaveAttachmentsToSelectedFolder(_emailRepository.GetEmail(emailId, userId));
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            return View("Email", new { emailId = emailId });
        }

        public ActionResult SaveAttachment(int attachmentId, int emailId)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                SaveAttachmentToSelectedFolder(_emailRepository.GetEmail(emailId, userId), attachmentId);
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
            var email = _emailRepository.GetEmail(emailId, userId);

            try
            {
                await Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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