using BookWebApp.DAL;
using BookWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookWebApp.Controllers
{
    public class AccountController : Controller
    {
        DBBookContext entities = new DBBookContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnURL)
        {
            var customer = entities.Customers.SingleOrDefault(c => c.CustomerName == username && c.CustomerPassword == password);
            if (customer != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnURL ?? Url.Action("booklist", "home"));
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or Password");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("login", "account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer user)
        {
            if (ModelState.IsValid)
            {
                entities.Customers.Add(user);
                entities.SaveChanges();
                return RedirectToAction("login", "account");
            }
            return View(user);
        }
    }
}