using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorktileSDK.Entity;

namespace MvcDemo.Controllers
{
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/

        public ActionResult ProjectInfo(string pid)
        {
            Project p = WTClient.API.ProjectAPI.GetProjectInfo(pid);
            ViewBag.entries = WTClient.API.EntryAPI.GetEntries(pid);
            return View(p);
        }

        [HttpPost]
        public ActionResult NewTask(string pid, string name, string entry_id)
        {
            WTClient.API.TaskAPI.CreateTask(pid, name, entry_id);
            return RedirectToAction("UserProfile", "User");
        }

        [HttpPost]
        public ActionResult UpdateEntryName(string entry_id, string pid, string name)
        {
            WTClient.API.EntryAPI.UpdateProjectEntryName(entry_id, pid, name);
            return RedirectToAction("UserProfile", "User");
        }

        public ActionResult DeleteEntry(string entry_id, string pid)
        {
            WTClient.API.EntryAPI.RemoveProjectEntry(entry_id, pid);
            return RedirectToAction("UserProfile", "User");
        }
    }
}
