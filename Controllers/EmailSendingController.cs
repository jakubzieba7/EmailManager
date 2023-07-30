using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailManager.Models.ViewModels;
using EmailManager.Models;
using Attachment = EmailManager.Models.Domains.Attachment;
using System.IO;
using System.Linq;

namespace EmailManager.Controllers
{
    public class EmailSendingController : Controller
    {
        //private EmailRepository _emailRepository = new EmailRepository();
        //private Email _email;
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;

        byte[] filebyte = null;

        private EditEmailViewModel EmailVM()
        {
            var vm = new EditEmailViewModel
            {

                Receivers = new List<ReceiverData>
                {
                    //new Receiver { Id = 1, Name = "Jacek", Surname = "Stokłosa", EmailAddress = "jacek.stoklosa@email.com" },
                    new ReceiverData { Id = 2, Name = "Jakub Zięba", EmailAddress = "jakubzieba7@gmail.com" }
                },
                Heading = "Edycja maila",
                Email = new Email()
                {
                    Id = 1,
                    MessageBody = "Tekst tej wiadomości jest następujący: bla bla bla",
                    MessageSubject = "Temat wiadomoci Email",
                    FooterData = new FooterData { Id = 1, ComplimentaryClose = "Pozdrawiam" },
                    Attachments = new List<Attachment>
                    {
                    new Attachment { Id = 1,FileName="ccc_mta_malekontury",FilePath=@"C:\Users\jakub\OneDrive\Pulpit\ccc_mta_malekontury.dxf"}
                    }
                }
            };

            return vm;
        }

        List<string> filePaths = new List<string>() { };

        public byte[] EmailAttachment()
        {
            filePaths.Add(@"C:\Users\jakub\OneDrive\Pulpit\ccc_mta_malekontury.dxf");
            filePaths.Add(@"C:\Users\jakub\OneDrive\Pulpit\2023-05-19_10h29_56.png");

            //File from disk

            //working ver.1

            foreach (string filePath in filePaths)
            {

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        //limitation to only one file send as byte array
                        //first from the list will be send
                        filebyte = reader.ReadBytes((int)stream.Length);
                    }
                }

            }

            ////working ver.2
            //FileStream fs = new FileStream(@"C:\Users\jakub\OneDrive\Pulpit\ccc_mta_malekontury.dxf", FileMode.Open);
            //MemoryStream ms = new MemoryStream();
            //fs.CopyTo(ms);
            //filebyte = ms.ToArray();

            ////File from model

            //using (Stream inputStream = model.File.InputStream)
            //{
            //    MemoryStream memoryStream = inputStream as MemoryStream;
            //    if (memoryStream == null)
            //    {
            //        model.File.InputStream.Position = 0;
            //        memoryStream = new MemoryStream();
            //        inputStream.CopyTo(memoryStream);
            //    }
            //    filebyte = memoryStream.ToArray();
            //}

            return filebyte;
        }

        public async Task Send(string subject, string body, string to)
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
            _mail.To.Add(new MailAddress(to));
            _mail.IsBodyHtml = true;
            _mail.Subject = subject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            using (var stream = new MemoryStream(EmailAttachment()))
            {

                foreach (string filePath in filePaths)
                {
                    stream.Position = 0;
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, GetFileName.FileName(filePath));
                    string contentID = "File";
                    attachment.ContentId = contentID;
                    attachment.ContentDisposition.Inline = true;
                    attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                    _mail.Attachments.Add(attachment);

                }


                _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Plain));

                string HtmlContent = RazorViewToStringFormat.RenderRazorViewToString(this, "EmailTemplate", EmailVM());

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

            //SaveMessage(_mail, EmailAttachment());
        }

        private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }


        // GET: EmailSending
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SendEmail()
        {
            try
            {
                await SendEmailMet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.SuccessfulSentMailMessage = "Mail został wysłany";

            return View("EmailTemplate", EmailVM());
        }

        private async Task SendEmailMet()
        {
            //var email = _emailRepository.GetEmail(emailID);
            //if (email == null)
            //    return;

            //await Send(EmailVM().Email.MessageSubject, EmailVM().Email.MessageBody, EmailVM().Email.Receivers.Where(x=>x.Id==2).Select(x=>x.EmailAddress).ToString());
            await Send(EmailVM().Email.MessageSubject, EmailVM().Email.MessageBody, "jakubzieba7@gmail.com");
            //await Send(vm.Email.MessageSubject, vm.Email.MessageBody, string.Join(",", vm.Receivers.Select(x => x.EmailAddress)));

        }

        public ActionResult EmailTemplate()
        {
            return View(EmailVM());
        }


    }
}