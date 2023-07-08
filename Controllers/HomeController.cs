using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var emails = new List<Email>
            {
                new Email
                {
                    Id = 1,
                    MessageSubject="Nowa wiadomość",
                    MessageBody="Przykładowy tekst tej wiadomości.",
                    EmailSendDate= DateTime.Now,
                    Receivers=new List<Receiver>
                    {
                        new Receiver
                        {
                            Id = 1,
                            Name="Jarek",
                            Surname="Kot",
                            EmailAddress="jarek.kot@mail.com",
                        },
                        new Receiver
                        {
                            Id = 2,
                            Name="Dariusz",
                            Surname="Sowa",
                            EmailAddress="darek.sowa@mail.com",
                        }
                    },
                    Sender=new Sender
                    {
                        Id = 2,
                        SendersPersonalData=new List<SenderPersonalData>
                        {
                            new SenderPersonalData
                            {
                                Id=1,
                                Name="Władek",
                                Surname="Kłoda"
                            }
                        }
                    }
                },
                new Email
                {
                    Id = 2,
                    MessageSubject="Stara wiadomość",
                    MessageBody="Przykładowy tekst kolejnej wiadomości.",
                    EmailSendDate= DateTime.Now,
                    Receivers=new List<Receiver>
                    {
                        new Receiver
                        {
                            Id=3,
                            Name="Bogdan",
                            Surname="Jastrząb",
                            EmailAddress="bogdan.jastrzab@mail.com",
                        },
                        new Receiver
                        {
                            Id = 4,
                            Name="Mariusz",
                            Surname="Klocek",
                            EmailAddress="mariusz.klocek@mail.com",
                        }
                    },
                    Sender=new Sender
                    {
                        Id=1,
                        SendersPersonalData=new List<SenderPersonalData>
                        {
                            new SenderPersonalData
                                {
                                    Id=1,
                                    Name="Bolesław",
                                    Surname="Dąb"
                                }
                        }
                    }
                }
            };

            return View(emails);
        }

        public ActionResult Email(int id = 0)
        {
            var vm = new EditEmailViewModel
            {
                Senders = new List<Sender>
                {
                new Sender
                    {
                        Id=1,
                        SendersPersonalData=new List<SenderPersonalData>
                        {
                            new SenderPersonalData { Id=1,Name="Jakub",Surname="Zięba",CompanyPositionPl="Konsultant techniczny"}
                        },
                        SenderEmailsParams=new List<SenderEmailParams>
                        {
                            new SenderEmailParams{Id=1,HostSmtp="smtp.gmail.com",Port=587,EnableSsl=true,SenderEmail="services.report.new@gmail.com",SenderEmailPassword="",SenderName="Jakub Zięba" }
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
            };

            return View(vm);
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