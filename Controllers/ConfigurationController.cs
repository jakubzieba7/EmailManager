using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main() 
        {
            return View();
        }

        public ActionResult SenderPersonalDataInput()
        {
            return View();
        }

        public ActionResult SenderPersonalData() 
        {
            return View();
        }
    }
}