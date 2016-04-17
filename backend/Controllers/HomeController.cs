using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talagozis.Web.Controllers;
using whatTheHack2.App_Plugins;

namespace whatTheHack2.Controllers
{
    //[RedirectOnAuthenticated("my")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [ActionName("index")]
        public ActionResult Index()
        {
            return View("~/views/account/login.cshtml");
        }
        
    }
}