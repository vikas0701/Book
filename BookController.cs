using BookWebApp.DAL;
using BookWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookWebApp.Controllers
{
    public class BookController : Controller
    {
        DBBookContext entities = new DBBookContext();

        // GET: Book
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book b)
        {
            if (ModelState.IsValid)
            {
                //product.Id = 8989;
                entities.Books.Add(b);
                entities.SaveChanges();
                return RedirectToAction("booklist","home");
            }
            return View(b);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var b1 = entities.Books.SingleOrDefault(b => b.Id == id);
            if (b1 == null)
            {
                return HttpNotFound();
            }
            return View(b1);
        }

        [HttpPost]
        public ActionResult Update(Book b1)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(b1).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("booklist","home");
            }
            return View(b1);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var book = entities.Books.SingleOrDefault(b => b.Id == id);
            entities.Books.Remove(book ?? throw new InvalidOperationException());
            entities.SaveChanges();
            return RedirectToAction("booklist", "home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}