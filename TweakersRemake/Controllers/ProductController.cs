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
        public ActionResult AddToWishList(int Id, int Wens)
        {
            WenslijstViewModel wens = new WenslijstViewModel();
            wens.Id = Wens;
            if (wens.AddProduct(Id))
            {
                return RedirectToAction("Index");
            }
           
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddWishList(WenslijstViewModel wens)
        {
            wens.AddWishList(User.Identity.Name);
            return RedirectToAction("AddWishList", "Product");
        }

        public ActionResult AddWishList()
        {
            List<WenslijstViewModel> list = new List<WenslijstViewModel>();
           
            list = Database.GetWenslijsten(User.Identity.Name);
            return View(list);
        }

        [HttpPost]
        public ActionResult RemoveWishList(int Wenslijst)
        {
            Database.RemoveWishList(Wenslijst);
            return RedirectToAction("AddWishList");
        }

        [HttpGet]
        public ActionResult GetWishList(string wenslijst)
        {
            ProductViewModel p = new ProductViewModel();
            p.Products = Database.GetProductsWenslijst(wenslijst, User.Identity.Name);
            p.Wenslijst = new List<WenslijstViewModel>();
            p.Wenslijst.Add(new WenslijstViewModel {Naam = wenslijst});
            return View(p);
        }

      

        [HttpPost]
        public ActionResult RemoveProduct(Product pr,string Wenslijst)
        {
            pr.RemoveProduct(User.Identity.Name, Wenslijst);
            return RedirectToAction("GetWishList",new {Wenslijst});
        }


        [HttpPost]
        public ActionResult AddToCompare(Product p)
        {
            p.AddToCompare(User.Identity.Name);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult GetCompare()
        {
            List<ProductViewModel> p = new List<ProductViewModel>();
            
            return View(p);
        }

     
    }
}