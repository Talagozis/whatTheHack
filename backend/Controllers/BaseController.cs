using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Talagozis.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private const string COOKIE_NAME = "extLang";


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Assembly assembly = GetWebEntryAssembly() ?? Assembly.GetExecutingAssembly();

            ViewBag.startTime = DateTime.Now;
            ViewBag.AssemblyCopyright = ((AssemblyCopyrightAttribute)(assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false))[0]).Copyright.Trim();
            ViewBag.AssemblyName = assembly.GetName().Name.Trim();
            ViewBag.AssemblyVersion = assembly.GetName().Version.ToString().Trim() + ", Core: " + Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim();
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected void setLanguage()
        {
            DateTime startTime = DateTime.Now;

            string language = string.Empty;


            if (!Request.Cookies.AllKeys.Contains(COOKIE_NAME))
            {
                string ip = Request.UserHostAddress; // ServerVariables["REMOTE_ADDR"];

                try
                {
                    WebClient client = new WebClient();
                    JObject json = JObject.Parse(client.DownloadString("http://freegeoip.net/json/" + ip));
                    language = json["country_code"].ToString();
                }
                catch (Exception e)
                {
                    //if (!EventLog.SourceExists("GymAssistant"))
                    //EventLog.CreateEventSource("GymAssistant", "GymAssistant");

                    //EventLog.WriteEntry("Gym", ip);
                }


            }
            else
            {
                language = Request.Cookies[COOKIE_NAME].Value;
            }


            HttpCookie cookie = new HttpCookie(COOKIE_NAME);
            if (language == "GR")
            {
                cookie.Value = language;
                HttpContext.Items["currentUserLanguage"] = "greek";

            }
            else
            {
                cookie.Value = "EN";
                HttpContext.Items["currentUserLanguage"] = "english";
            }

            cookie.Expires = cookie.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(cookie);

            //LogService.createLogPerformance(startTime, "setExteriorLanguage");
        }

        protected object getHttpContextItem(string key)
        {
            if (HttpContext.Items[key] != null)
            {
                return HttpContext.Items[key];
            }

            return null;
        }

        private static Assembly GetWebEntryAssembly()
        {
            if (System.Web.HttpContext.Current == null ||
                System.Web.HttpContext.Current.ApplicationInstance == null)
            {
                return null;
            }

            var type = System.Web.HttpContext.Current.ApplicationInstance.GetType();
            while (type != null && type.Namespace == "ASP")
            {
                type = type.BaseType;
            }

            return type == null ? null : type.Assembly;
        }


    }
}
