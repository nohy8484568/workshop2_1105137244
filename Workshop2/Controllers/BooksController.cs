using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Workshop2.Models;
using Workshop2.Models.Service;
using Workshop2.Models.ViewModel;

namespace Workshop2.Controllers
{
    public class BooksController : Controller
    {
        private ChiChenHanEntities db = new ChiChenHanEntities();
        private BooksService booksService = new BooksService();

        // GET: Books
        public ActionResult Index()
        {
            ViewBag.status = "";
            ViewBag.member = "";
            ViewBag.name = "";
            ViewBag.category = "";
            return View(booksService.GetBooksIndex());
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Book book)
        {
            #region  設定ViewBag
            if (book.Book_Status != null)
            {
                ViewBag.status = book.Book_Status;
            }
            else
            {
                ViewBag.status = "";
            }

            if (book.Member_Code != null)
            {
                ViewBag.member = book.Member_Code;
            }
            else
            {
                ViewBag.member = "";
            }

            if (book.Book_Name != null)
            {
                ViewBag.name = book.Book_Name;
            }
            else
            {
                ViewBag.name = "";
            }

            if (book.Class_Code != null)
            {
                ViewBag.category = book.Class_Code;
            }
            else
            {
                ViewBag.category = "";
            }
            #endregion

            return View(booksService.SearchBooks(book));
        }
        
        // GET: Books/Create
        public ActionResult Create()
        {
            return View(booksService.GetBooksCreateRelatedObject());
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                booksService.CreateBookDate(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BooksEditViewModel beVM = booksService.GetBooksEditRelatedObject(id);
            if(beVM.Book == null)
            {
                return HttpNotFound();
            }
            return View(beVM);
        }

        // POST: Books/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                booksService.BookSaveChange(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //因為用到的表相同，所以用edit的viewModel
            BooksEditViewModel deVM = booksService.GetBooksEditRelatedObject(id);
            if (deVM.Book == null)
            {
                return HttpNotFound();
            }
            return View(deVM);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
