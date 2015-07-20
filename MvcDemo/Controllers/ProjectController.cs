using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorktileSDK;
using WorktileSDK.Entity;

namespace MvcDemo.Controllers
{
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/

        public ActionResult Index(string uid)
        {
           // IEnumerable<Project> Projects = WTClient.API.TeamAPI.GetTeams();
            return View();
        }

    }
}
