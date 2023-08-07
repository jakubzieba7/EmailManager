﻿using EmailManager.Models;
using EmailManager.Models.Domains;
using EmailManager.Models.Repositories;
using EmailManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private SenderRepository _senderRepository = new SenderRepository();
        private ReceiverRepository _receiverRepository = new ReceiverRepository();
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
                //Senders = new List<SenderPersonalData>
                //{
                //    new SenderPersonalData
                //    {
                //        Id = 1,
                //        //SendersPersonalData = new List<SenderPersonalData>
                //        //{
                //        //    new SenderPersonalData { Id = 1, Name = "Jakub Zięba", CompanyPositionPl = "Konsultant techniczny" }
                //        //},
                //        //SenderEmailsParams = new List<SenderEmailParams>
                //        //{
                //        //    new SenderEmailParams { Id = 1, HostSmtp = "smtp.gmail.com", Port = 587, EnableSsl = true, SenderEmail = "services.report.new@gmail.com", SenderEmailPassword = "", SenderName = "Jakub Zięba" }
                //        //}
                //    }
                //},
                Footers = new List<FooterData> {
                    new FooterData { Id = 1, ComplimentaryClose = "Pozdrawiam" },
                    new FooterData { Id = 2, ComplimentaryClose = "Best regards" }
                },
                Receivers = new List<ReceiverData>
                {
                    new ReceiverData { Id = 1, Name = "Jacek Stokłosa", EmailAddress = "jacek.stoklosa@email.com" },
                    new ReceiverData { Id = 2, Name = "Jakub Zięba", EmailAddress = "jakubzieba7@gmail.com" }
                },
                Heading = "Edycja maila",
                Email = new Email()
                {
                    FooterId = 1,
                    SenderId = 1,
                    Id = 1,
                    AttachmentId = 1,
                    //Senders = new List<Sender>
                    //{
                    //    new Sender
                    //    {
                    //        Id = 1,
                    //        SendersPersonalData = new List<SenderPersonalData>
                    //        {
                    //            new SenderPersonalData { Id = 1, Name = "Jakub Zięba", CompanyPositionPl = "Konsultant techniczny" }
                    //        },
                    //        SenderEmailsParams = new List<SenderEmailParams>
                    //        {
                    //            new SenderEmailParams { Id = 1, HostSmtp = "smtp.gmail.com", Port = 587, EnableSsl = true, SenderEmail = "services.report.new@gmail.com", SenderEmailPassword = "", SenderName = "Jakub Zięba" }
                    //        }
                    //    }
                    //},
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
                Footers = _footerRepository.GetFooters(userId),
                Receivers = _receiverRepository.GetReceivers(userId),
                Attachments = _attachmentRepository.GetAttachments(userId)
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
            var emailAttachment = attachmentId == 0 ? GetNewAttachment(emailId, attachmentId) : _emailRepository.GetAttachment(userId, attachmentId);
            var vm = PrepareAttachmentVM(emailAttachment);

            return View(vm);
        }

        private EditEmailAttachmentViewModel PrepareAttachmentVM(Attachment emailAttachment)
        {
            return new EditEmailAttachmentViewModel
            {
                Attachment = emailAttachment,
                //Attachments = _attachmentRepository.GetAttachments(emailAttachment.Email.UserId),
                Heading = emailAttachment.Id == 0 ? "Nowy załącznik" : "Załącznik",
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
        public ActionResult EmailAttachment(EditEmailAttachmentViewModel attachmentVM)
        {
            var userId = User.Identity.GetUserId();

            if (attachmentVM.Attachment.Id == 0)
                _emailRepository.AddEmailAttachment(attachmentVM, userId);
            else
                _emailRepository.UpdateEmailAttachment(attachmentVM.Attachment, userId);

            return RedirectToAction("Email", new { id = attachmentVM.Attachment.EmailId });
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
        public ActionResult DeleteAttachment(Email email, int attachmentID)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteAttachment(email.Id, attachmentID, userId);
                _emailRepository.Update(email);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true, Email = email });
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