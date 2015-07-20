using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorktileSDK;
using WorktileSDK.Entity;

namespace MvcDemo.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult UserProfile()
        {
            User user = WTClient.API.UserAPI.GetUserProfile();
            ViewBag.teams = WTClient.API.TeamAPI.GetTeams();
            return View(user);
        }

    }
}
