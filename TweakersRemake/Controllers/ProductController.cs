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
            return View(Model);
        }

        
        public ActionResult Prijzen(Product Pro)
        {
           Pro.FillLink();
            return View(Pro);
        }
       
    }
}