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
            OAuth auth = new OAuth("dae7cc17d8084ca1a2d6b8e27f2bbc98", "http://123.57.224.195:8012/home/auth");
            ViewBag.url = auth.GetAuthorizeURL();
            return View();
        }

        public ActionResult Auth(string code)
        {

            OAuth auth = new OAuth("dae7cc17d8084ca1a2d6b8e27f2bbc98", "http://123.57.224.195:8012/home/auth");
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
