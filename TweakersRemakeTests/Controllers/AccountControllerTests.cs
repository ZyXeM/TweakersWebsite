using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweakersRemake.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            AccountController p = new AccountController();
            ViewResult result = p.Index(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ManageTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoginTest()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMaster";
            ac.Wachtwoord = "password7";
           ViewResult result =  a.Login(ac) as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void LoginTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogoutTest()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMaster";
            ac.Wachtwoord = "password7";
            a.Login(ac);
            RedirectResult result = a.Logout() as RedirectResult;
           
           
            Assert.AreEqual(result.Url,"/Home/Index");
        }

        [TestMethod()]
        public void RegisterTest()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMasterTest";
            ac.Wachtwoord = "password7";
            ac.Geslacht = "M";
            ac.Naam = "Mitch";
            ac.Opleiding = "test";
            ac.Woonplaats = "Test";
          var result = (RedirectToRouteResult)  a.Register(ac);
            Assert.AreEqual("Index",result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);


        }

        [TestMethod()]
        public void RegisterTest1()
        {
            Assert.Fail();
        }
    }
}