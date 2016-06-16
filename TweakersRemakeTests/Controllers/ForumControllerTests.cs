using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweakersRemake.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;
using TweakersRemake.Models;

namespace TweakersRemake.Controllers.Tests
{
    [TestClass()]
    public class ForumControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            ForumController f = new ForumController();
            Mappy m = new Mappy();
            m.Id = 1;
            ViewResult result = f.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetMapTest()
        {
            ForumController f = new ForumController();
            Mappy m = new Mappy();
            m.Id = 1;
            ViewResult result = f.GetMap("GAMES") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetPostsTest()
        {
            ForumController f = new ForumController();
            Mappy m = new Mappy();
            m.Id = 1;
           ViewResult result = f.GetPosts(m) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetConversationTest()
        {
            ForumController f = new ForumController();
            Mappy m = new Mappy();
            m.Id = 1;
            ViewResult result = f.GetConversation(m,"Games") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PostTest()
        {
            Post p = new Post();
            p.Mappy = 1;
            p.Message = 
            "ajaj";
            p.Onderwerp = "asdas";
           Assert.AreEqual(true, p.AddPost("ZyXeMaster"));
            
        }

        [TestMethod()]
        public void PostPostTest()
        {
            Post p = new Post();
            Post pre = new Post();
            p.Mappy = 1;
            p.Message =
            "ajaj";
            p.Onderwerp = "asdas";
            pre.Id = 6;
            p.PrePost = pre;
            Assert.AreEqual(true, p.ReactPosts("ZyXeMaster"));
        }

        [TestMethod()]
        public void DeletePostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteChainPostTest()
        {
            Assert.Fail();
        }
    }
}