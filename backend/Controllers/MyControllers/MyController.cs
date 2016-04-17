using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talagozis.Web.Controllers;
using whatTheHack2.App_Plugins.Service;
using whatTheHack2.Models.Database;

namespace whatTheHack2.Controllers.MyControllers
{
    [Authorize]
    public class MyController : BaseController
    {

        [HttpGet]
        [Authorize(Roles = "Doctor")]
        [ActionName("doctor")]
        public ActionResult Doctor()
        {
            UserService userService = new UserService();

            AspNetUsers user = userService.findById(User.Identity.GetUserId());

            return View("~/views/my/doctor.cshtml", user);
        }


        [HttpGet]
        [Authorize(Roles = "Patient")]
        [ActionName("patient")]
        public ActionResult Patient()
        {
            UserService userService = new UserService();

            AspNetUsers user = userService.findById(User.Identity.GetUserId());

            return View("~/views/browser/index.cshtml", user);
        }

    }
}