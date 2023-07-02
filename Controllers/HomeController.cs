using EmailManager.Models.Domains;
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

        public ActionResult Email()
        {
            return View();
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