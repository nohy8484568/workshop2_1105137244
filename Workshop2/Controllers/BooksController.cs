using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Workshop2.Models;
using Workshop2.Models.VM;

namespace Workshop2.Controllers
{
    public class BooksController : Controller
    {
        private HungEntities db = new HungEntities();

        // GET: Books
        public ActionResult Index()
        {
            ViewBag.BookNum = db.Book.Count();

            BookMemberVM bm = new BookMemberVM();
            bm.Book = db.Book.OrderByDescending(x=>x.Book_PurchaseTime).Take(7).ToList();

            foreach (var item in bm.Book)
            {
                if (item.Member_Code != null)
                {
                    item.Member_Name = db.Member.Find(item.Member_Code).Member_Name;
                }
            }
            
            bm.BookClass = db.BookClass.ToList();
            return View(bm);
        }

        public ActionResult Search()
        {
            BookMemberVM bm =new BookMemberVM();
            bm.BookClass = db.BookClass.ToList();
            bm.Member = db.Member.ToList();
            return View(bm);
        }
        public ActionResult SearchResult(string name, string category, string status,string member)
        {
            List<Book> books = db.Book.ToList();
            
            if (name != "0")
            {
                books = books.Where(x => x.Book_Name.Contains(name)).OrderByDescending(x=>x.Book_PurchaseTime).ToList();
            }
            if (category != "0")
            {
                books = books.Where(x => x.Book_Class == category).OrderByDescending(x => x.Book_PurchaseTime).ToList();
            }
            if (status != "0")
            {
                books = books.Where(x => x.Book_Status == status).OrderByDescending(x => x.Book_PurchaseTime).ToList();
            }
            if (member != "0")
            {
                var code = Convert.ToInt32(member);
                books = books.Where(x => x.Member_Code== code).OrderByDescending(x => x.Book_PurchaseTime).ToList();
            }

            if (books != null)
            {
                foreach (var item in books)
                {
                    if (item.Member_Code != null)
                    {
                        item.Member_Name = db.Member.Find(item.Member_Code).Member_Name;
                    }
                }
            }
            return View(books);
        }
       
        [HttpPost]
        public ActionResult Search(BookMemberVM bm)
        {
            return View();
        }

        [HttpPost]
        public JsonResult PageInfo(int? id)
        {
            List<Book> books = new List<Book>();
            if (id == 1)
            {
                books=db.Book.OrderByDescending(x => x.Book_PurchaseTime).Take(7).ToList();
            }
            else
            {
                var p = id ?? default(int);
                books = db.Book.OrderByDescending(x => x.Book_PurchaseTime).Skip((p-1) * 7).ToList();
            }
            foreach (var item in books)
            {
                if (item.Member_Code != null)
                {
                    item.Member_Name = db.Member.Find(item.Member_Code).Member_Name;
                }
            }

            return Json(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            BookMemberVM bm = new BookMemberVM();
            bm.BookClass = db.BookClass.ToList();
            bm.Member = db.Member.ToList();
            return View(bm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Book_Status = "A";
                db.Book.Add(book);
                db.SaveChanges();
                return Redirect("~/Books/Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            BookMemberVM bm = new BookMemberVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            bm.Book = db.Book.Where(x=>x.Book_Code == id).ToList();
            bm.BookClass = db.BookClass.ToList();
            bm.Member = db.Member.ToList();
            
            if (bm.Book == null)
            {
                return HttpNotFound();
            }

            if (bm.Book[0].Member_Code != null)
            {
                bm.Book[0].Member_Name = db.Member.Find(bm.Book[0].Member_Code).Member_Name;
            }
     
            return View(bm);
        }

        // POST: Books/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("~/Books/Index");
            }
            return View();
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
            db.SaveChanges();
            return Redirect("~/Books/Index");
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
