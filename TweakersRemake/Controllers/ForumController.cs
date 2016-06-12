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
        public ActionResult Post(Post p,int Idee)
        {
            p.Mappy = Idee;
            p.AddPost(User.Identity.Name);
            return RedirectToAction("Index");
        }

      

        [HttpPost]
        public ActionResult PostPost(Post P,int prepost, int Mappy)
        {
            P.Mappy = Mappy;
            P.PrePost = new Post();
            P.PrePost.Id = prepost;
            P.ReactPosts(User.Identity.Name);
            Models.Mappy m = new Mappy();
            m.Id = P.Id;
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult DeletePost( int idee)
        {
            // Ik krijg het unieke onderwerp van de layout plus de map waar hij in zit om alle Posts daarin te kunnen verwijdern De chains dus
            Post p = new Post();
            p.Id = idee;
            p.DeletePost();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteChainPost(Mappy m, Post p)
        {
            p.DeleteChainPost(m.Id);
            return RedirectToAction("Index");
        }
    }
}