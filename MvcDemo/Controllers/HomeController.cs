using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorktileSDK;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            OAuth auth = new OAuth("e324954800fd49e28ed905fd35c40581", "http://localhost:10438/home/auth");
            ViewBag.url = auth.GetAuthorizeURL();
            return View();
        }

        public ActionResult Auth(string code)
        {
            OAuth auth = new OAuth("e324954800fd49e28ed905fd35c40581", "http://localhost:10438/home/auth");
            string msg;
            if (auth.GetAccessTokenByCode(code, out msg))
            {
                Client client = new Client(auth);
                Session["client"] = client;
            }
            ViewBag.msg = msg;
            return View();
        }
    }
}
