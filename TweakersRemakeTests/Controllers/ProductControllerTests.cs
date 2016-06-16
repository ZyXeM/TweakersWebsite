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
    public class ProductControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMaster";
            ac.Wachtwoord = "password7";
            a.Login(ac);
            ProductController p = new ProductController();
            
            ViewResult result = p.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PrijzenTest()
        {
            ProductController p = new ProductController();
            Product pr = new Product();
            pr.Id = 1;
            ViewResult result = p.Prijzen(pr) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AddToWishListTest()
        {
            ProductController p = new ProductController();
            ViewResult result = p.AddToWishList(1,5) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AddWishListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddWishListTest1()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMaster";
            ac.Wachtwoord = "Password7";
            a.Login();
            ProductController p = new ProductController();
            WenslijstViewModel w = new WenslijstViewModel();
            w.Naam = "Wensy";
            ViewResult result = p.AddWishList(w) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void RemoveWishListTest()
        {
            AccountController a = new AccountController();
            Account ac = new Account();
            ac.ProfielNaam = "ZyXeMaster";
            ac.Wachtwoord = "Password7";
            a.Login();
            ProductController p = new ProductController();
            WenslijstViewModel w = new WenslijstViewModel();
            w.Naam = "Wensy";
            ViewResult result = p.AddWishList(w) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetWishListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddToCompareTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCompareTest()
        {
            Assert.Fail();
        }
    }
}