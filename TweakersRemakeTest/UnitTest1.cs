using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweakersRemake.Models;
using  TweakersRemake.Controllers;
using System.Web.Mvc;

namespace TweakersRemakeTest
{
    [TestClass]
    public class Product
    {
        
        

        [TestMethod]
        public void RemoveProduct()
        {
         ProductController p = new ProductController();
            Product pr = new Product();
            

            //ViewResult v = p.RemoveProduct() as ViewResult;

              
            //Assert.AreEqual(,v.Model);
        }

        [TestMethod]
        public void AddProduct()
        {
          
            
            

         //   Assert.AreEqual(, true);
        }

        [TestMethod]
        public void AddWishList()
        {
         //   Assert.AreEqual(, true);
        }

    }

    [TestClass]
    public class Post
    {



        [TestMethod]
        public void AddPost()
        {
          //  Assert.AreEqual(, true);
        }

        [TestMethod]
        public void ReactPost()
        {
            Post p = new Post();
            
          //  Assert.AreEqual(, true);
        }

        [TestMethod]
        public void DeleteChain()
        {
          //  Assert.AreEqual(, true);
        }

        [TestMethod]
        public void DeletePost()
        {
         //   Assert.AreEqual(, true);
        }

        [TestMethod]
        public void GetMainPost()
        {
           // Assert.AreEqual(, true);
        }

        [TestMethod]
        public void GetChainPost()
        {
          //  Assert.AreEqual(, true);
        }




    }

}
