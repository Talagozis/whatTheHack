using System.Web.Mvc;
using System.Web.Routing;

namespace whatTheHack2.App_Plugins
{
    public class RedirectOnAuthenticated : ActionFilterAttribute
    {
        private string controller { get; set; }
        private string action { get; set; }


        public RedirectOnAuthenticated(string controller, string action = "index", object parameters = null)
        {
            this.controller = controller;
            this.action = action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                RouteValueDictionary routeValueDictionary = new RouteValueDictionary() {
                    { "controller", controller },
                    { "action", action }
                };

                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }
        }

    }
}
