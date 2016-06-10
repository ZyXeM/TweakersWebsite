using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;
using TweakersRemake.Models;

namespace TweakersRemake.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        
        public ActionResult Index()
        {
            ProductViewModel Model = new ProductViewModel();
            Model.Products = Database.GetProducts();
            if (Request.IsAuthenticated)
            {
                Model.Wenslijst = Database.GetWenslijsten(User.Identity.Name);
            }
            return View(Model);
        }

        
        public ActionResult Prijzen(Product Pro)
        {
           Pro.FillLink();
            return View(Pro);
        }

        [HttpPost]
        public ActionResult AddToWishList(Product Pro)
        {
            return RedirectToAction("Prijzen");
        }

        public ActionResult AddWishList()
        {
            return View();
        }

        public ActionResult AddWishList(string Wenslijst)
        {
            return View();
        }

        public ActionResult RemoveWishList(string Wenslijst)
        {
            return RedirectToAction("AddWishList");
        }

        [HttpGet]
        public ActionResult GetWishList(string wenslijst)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoveProduct(Product pr,string Wenslijst)
        {
            return RedirectToAction("GetWishList",new {wenlijst = Wenslijst});
        }

    }
}