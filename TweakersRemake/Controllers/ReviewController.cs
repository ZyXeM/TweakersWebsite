using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductReview(Product Pr)
        {

            Pr.FillReview();
            return View(Pr);
        }

        [HttpPost]
        public ActionResult AddReview(Preview Pr)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAReview(Aureview Au)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAReview()
        {
            return View();
        }
    }
}