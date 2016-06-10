using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMap(string map)
        {
            return View();
        }

        public ActionResult GetPosts(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(Post p, int Chain, int id)
        {
            return RedirectToAction("GetPosts", new {id});
        }

        public ActionResult PostPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostPost(Post P)
        {
            return View();
        }



        [HttpPost]
        public ActionResult DeletePost()
        {
            return RedirectToAction("Index");
        }
    }
}