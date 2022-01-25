using BookWebApp.DAL;
using BookWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace BookWebApp.Controllers
{
    
    public class HomeController : Controller
    {
        DBBookContext entities = new DBBookContext();
        // GET: Home
       
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult BookList()
        {
            List<Book> books = entities.Books.ToList();
            this.ViewData["books"] = books;
            return View();
        }
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult BookDetail(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = entities.Books.SingleOrDefault(b => b.Id == Id);
            if (book == null)
            {
                return HttpNotFound();
            }
            this.ViewData["book"] = book;
            return View();
        }
        public ActionResult InvalidUser()
        {
            return View();
        }
    }
}