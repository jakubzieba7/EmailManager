using EmailManager.Models.Domains;
using EmailManager.Models.Repositories;
using EmailManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private SenderRepository _senderRepository = new SenderRepository();
        private FooterRepository _footerRepository = new FooterRepository();
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _emailRepository.GetEmails(userId);


            //var emails = new List<Email>
            //{
            //    new Email
            //    {
            //        Id = 1,
            //        MessageSubject="Nowa wiadomość",
            //        MessageBody="Przykładowy tekst tej wiadomości.",
            //        EmailSendDate= DateTime.Now,
            //        Receivers=new List<Receiver>
            //        {
            //            new Receiver
            //            {
            //                Id = 1,
            //                Name="Jarek",
            //                Surname="Kot",
            //                EmailAddress="jarek.kot@mail.com",
            //            },
            //            new Receiver
            //            {
            //                Id = 2,
            //                Name="Dariusz",
            //                Surname="Sowa",
            //                EmailAddress="darek.sowa@mail.com",
            //            }
            //        },
            //        Sender=new Sender
            //        {
            //            Id = 2,
            //            SendersPersonalData=new List<SenderPersonalData>
            //            {
            //                new SenderPersonalData
            //                {
            //                    Id=1,
            //                    Name="Władek",
            //                    Surname="Kłoda"
            //                }
            //            }
            //        }
            //    },
            //    new Email
            //    {
            //        Id = 2,
            //        MessageSubject="Stara wiadomość",
            //        MessageBody="Przykładowy tekst kolejnej wiadomości.",
            //        EmailSendDate= DateTime.Now,
            //        Receivers=new List<Receiver>
            //        {
            //            new Receiver
            //            {
            //                Id=3,
            //                Name="Bogdan",
            //                Surname="Jastrząb",
            //                EmailAddress="bogdan.jastrzab@mail.com",
            //            },
            //            new Receiver
            //            {
            //                Id = 4,
            //                Name="Mariusz",
            //                Surname="Klocek",
            //                EmailAddress="mariusz.klocek@mail.com",
            //            }
            //        },
            //        Sender=new Sender
            //        {
            //            Id=1,
            //            SendersPersonalData=new List<SenderPersonalData>
            //            {
            //                new SenderPersonalData
            //                    {
            //                        Id=1,
            //                        Name="Bolesław",
            //                        Surname="Dąb"
            //                    }
            //            }
            //        }
            //    }
            //};

            return View(emails);
        }

        private EditEmailViewModel ViewModel()
        {
            var vm = new EditEmailViewModel
            {
                Senders = new List<Sender>
                {
                    new Sender
                    {
                        Id = 1,
                        SendersPersonalData = new List<SenderPersonalData>
                        {
                            new SenderPersonalData { Id = 1, Name = "Jakub", Surname = "Zięba", CompanyPositionPl = "Konsultant techniczny" }
                        },
                        SenderEmailsParams = new List<SenderEmailParams>
                        {
                            new SenderEmailParams { Id = 1, HostSmtp = "smtp.gmail.com", Port = 587, EnableSsl = true, SenderEmail = "services.report.new@gmail.com", SenderEmailPassword = "", SenderName = "Jakub Zięba" }
                        }
                    }
                },
                Footers = new List<Footer> {
                    new Footer { Id = 1, ComplimentaryClose = "Pozdrawiam", SenderId = 1 },
                    new Footer { Id = 2, ComplimentaryClose = "Best regards", SenderId = 1 }
                },
                Receivers = new List<Receiver>
                {
                    new Receiver { Id = 1, Name = "Jacek", Surname = "Stokłosa", EmailAddress = "jacek.stoklosa@email.com" },
                    new Receiver { Id = 2, Name = "Jakub", Surname = "Zięba", EmailAddress = "jakubzieba7@gmail.com" }
                },
                Heading = "Edycja maila",
                Email = new Email()
                {
                    FooterId = 1,
                    SenderId = 1,
                    Id = 1,
                    AttachmentId = 1,
                    Senders = new List<Sender>
                    {
                        new Sender
                        {
                            Id = 1,
                            SendersPersonalData = new List<SenderPersonalData>
                            {
                                new SenderPersonalData { Id = 1, Name = "Jakub", Surname = "Zięba", CompanyPositionPl = "Konsultant techniczny" }
                            },
                            SenderEmailsParams = new List<SenderEmailParams>
                            {
                                new SenderEmailParams { Id = 1, HostSmtp = "smtp.gmail.com", Port = 587, EnableSsl = true, SenderEmail = "services.report.new@gmail.com", SenderEmailPassword = "", SenderName = "Jakub Zięba" }
                            }
                        }
                    },
                    Attachments = new List<Attachment>
                    {
                        new Attachment
                        {
                            Id = 1,
                            Lp=1,
                            EmailId=1,
                            FileName="Załącznik 1",
                            ContentType="PDF",
                            FileData=Encoding.ASCII.GetBytes("asasa"),
                            Email=new Email() {Id=1,AttachmentId=1},
                            FilePath=""
                        },
                        new Attachment
                        {
                            Id = 2,
                            Lp=2,
                            EmailId=1,
                            FileName="Załącznik 2",
                            ContentType="DXF",
                            FileData=Encoding.ASCII.GetBytes("asasa"),
                            Email=new Email() {Id=1,AttachmentId=2},
                            FilePath=""
                        }
                    }
                }
            };

            return vm;
        }

        //public ActionResult EmailAttachment()
        //{
        //    var vm = new EditEmailViewModel
        //    {
        //        Heading = "Dodaj załącznik"
        //    };
        //    return View(vm);
        //}

        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var email = id == 0 ? GetNewEmail(userId) : _emailRepository.GetEmail(id, userId);

            var vm = PrepareEmailVm(email, userId);

            return View(vm);


            //if (id == 0)
            //{
            //    return View(ViewModel());

            //}
            //else
            //{
            //    return View(ViewModel());
            //}
        }

        private object PrepareEmailVm(Email email, string userId)
        {
            return new EditEmailViewModel
            {
                Email = email,
                Heading = email.Id == 0 ? "Nowy email" : "Email",
                Senders = _senderRepository.GetSenders(userId),
                Footers = _footerRepository.GetFooters()
            };
        }

        private Email GetNewEmail(string userId)
        {
            return new Email
            {
                UserId = userId,
                EmailSendDate = DateTime.Now
            };
        }

        public ActionResult EmailAttachment(int emailId, int attachmentId = 0)
        {
            var userId = User.Identity.GetUserId();
            var emailAttachment = attachmentId == 0 ? GetNewAttachment(emailId, attachmentId) : _emailRepository.GetAttachment(userId);
            var vm = PrepareAttachmentVM(emailAttachment);

            return View(ViewModel());
        }

        private EditEmailAttachmentViewModel PrepareAttachmentVM(Attachment emailAttachment)
        {
            return new EditEmailAttachmentViewModel
            {
                Attachment = emailAttachment,
                Attachments = _attachmentRepository.GetAttachments(),
                Heading = emailAttachment.Id == 0 ? "Nowy załącznik" : "Załącznik"
            };
        }

        private Attachment GetNewAttachment(int emailId, int attachmentId)
        {
            return new Attachment
            {
                Id = attachmentId,
                EmailId = emailId
            };
        }

        [HttpPost]
        public ActionResult Email(Email email)
        {
            var userId = User.Identity.GetUserId();
            email.UserId = userId;

            if (email.Id == 0)
                _emailRepository.Add(email);
            else
                _emailRepository.Update(email);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult EmailAttachment(Attachment attachment)
        {
            var userId = User.Identity.GetUserId();
            var attachmentContent = _attachmentRepository.GetAttachmentContent(attachment.Id);
            attachment.FileData = attachmentContent;

            if (attachment.Id == 0)
                _attachmentRepository.AddAttachment(attachment, userId);
            else
                _attachmentRepository.UpdateAttachment(attachment, userId);

            _emailRepository.UpdateEmailAttachment(attachment.EmailId, userId);

            return RedirectToAction("Email", new { id = attachment.EmailId });
        }

        [HttpPost]
        public ActionResult DeleteEmail(int emailId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteEmail(emailId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult DeleteAttachment(int emailId, int attachmentID)
        {
            var attachment = new Attachment();

            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteAttachment(emailId, userId);
                attachment = _emailRepository.UpdateEmailAttachment(emailId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true, Attachment = attachment });
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Strona o mnie";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Informacje kontaktowe";

            return View();
        }
    }
}