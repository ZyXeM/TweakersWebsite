using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Controllers
{
    public class AccaController : Controller
    {
        // GET: Acca
        public ActionResult Index(int id)
        {
            Account A = new Account();
            A = Database.GetAccount(id);
            return View(A);
        }
    }
}