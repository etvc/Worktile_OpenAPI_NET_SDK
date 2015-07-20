using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorktileSDK;

namespace MvcDemo.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected Client WTClient { get; private set; }
        public BaseController()
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            this.WTClient = filterContext.HttpContext.Session["client"] as Client;
        }
    }
}
