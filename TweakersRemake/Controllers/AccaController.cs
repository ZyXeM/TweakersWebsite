using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;
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

        [HttpGet]
        public ActionResult Login()
        {
            Account A = new Account();
            return View(A);
        }

        [HttpPost]
        public ActionResult Login(Account A)
        {
            if (ModelState.IsValid)
            {//Checked Of de state valid is
                if (A.Isvalid())
                {//Checked in de database Of hij bestaat en vervolgens In de Authenticator gezet
                    FormsAuthentication.SetAuthCookie(A.ProfielNaam, true);
                }
            }
            return View(A);
        }

        public ActionResult Logout()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            Account A = new Account();
            
            return View(A);
        }

        [HttpPost]
        public ActionResult Register(Account A)
        {
            if (A.Register())
            {
                Login(A);
                return RedirectToAction("Index", "Home");
            }
            
            return View(A);
        }
    }
}