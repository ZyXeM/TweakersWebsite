using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;
using TweakersRemake.Models;

namespace TweakersRemake.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult GetMap(string Hoofd)
        {
            List<Mappy> map =  new List<Mappy>();
            map = Database.GetMappy(Hoofd);
            //Alle mapjes verkrijgen uit de standaard onderwerpen
            return View(map);
        }

        public ActionResult GetPosts(Mappy mappy)
        {
            mappy.GetMainPost();
            return View(mappy);
        }

        public ActionResult GetConversation(Mappy mappy, string Onderwerp)
        {
            mappy.GetChainPost(Onderwerp);
            return View(mappy);
        }



        [HttpPost]
        public ActionResult Post(Post p, int id)
        {
            p.Mappy = id;
            Database.AddPosts(p, User.Identity.Name);
            return RedirectToAction("GetPosts", new {id});
        }

      

        [HttpPost]
        public ActionResult PostPost(Post P,int prepost)
        {
            P.PrePost.Id = prepost;
            Database.ReactPosts(P);
            return View();
        }



        [HttpPost]
        public ActionResult DeletePost()
        {
            return RedirectToAction("Index");
        }
    }
}