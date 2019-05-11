using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Workshop2.Models;
using Workshop2.Models.ViewModel;
namespace Workshop2.Models.Service
{
    public class BooksService
    {
        private ChiChenHanEntities db = new ChiChenHanEntities();

        /// <summary>
        /// 取得首頁書籍相關物件(書籍類別、會員、書籍狀態)
        /// </summary>
        /// <returns></returns>
        public BooksIndexViewModel GetBooksIndexRelatedObject()
        {
            BooksIndexViewModel booksIndexVM = new BooksIndexViewModel();
            booksIndexVM.BookClasses = db.BookClass.ToList();
            booksIndexVM.Members = db.Member.ToList();
            booksIndexVM.BookStatuses = db.BookStatus.ToList();
            return booksIndexVM;
        }
        /// <summary>
        /// 取得全部書籍資料、按日期遞減
        /// </summary>
        /// <returns>BooksIndexViewModel</returns>
        public BooksIndexViewModel GetBooksIndex()
        {
            BooksIndexViewModel booksIndexVM = GetBooksIndexRelatedObject();
            booksIndexVM.Books = db.Book.OrderByDescending(x => x.Book_PurchaseTime).ToList();
            return booksIndexVM;
        }
        /// <summary>
        /// 搜尋書籍
        /// </summary>
        /// <param name="book">書籍資料</param>
        /// <returns></returns>
        public BooksIndexViewModel SearchBooks(Book book)
        {
            BooksIndexViewModel booksIndexVM = GetBooksIndexRelatedObject();
            List<Book> books = db.Book.ToList();
            #region 過濾查詢條件
            if (book.Book_Name != null)
            {
                books = books.Where(x => x.Book_Name.Contains(book.Book_Name)).ToList();
            }
            if (book.Class_Code != null)
            {
                books = books.Where(x => x.Class_Code.Equals(book.Class_Code)).ToList();
            }
            if (book.Book_Status != null)
            {
                books = books.Where(x => x.Book_Status.Equals(book.Book_Status)).ToList();
            }
            if (book.Member_Code != null)
            {
                books = books.Where(x => x.Member_Code.Equals(book.Member_Code)).ToList();
            }
            #endregion
            booksIndexVM.Books = books.OrderByDescending(x=>x.Book_PurchaseTime).ToList();
            return booksIndexVM;
        }

        /// <summary>
        /// 取得新增頁面空白書籍及相關物件(書籍類別)
        /// </summary>
        /// <returns></returns>
        public BooksCreateViewModel GetBooksCreateRelatedObject()
        {
            BooksCreateViewModel BookCreateVM = new BooksCreateViewModel();
            Book book = new Book();
            BookCreateVM.Book = book;
            BookCreateVM.BookClasses = db.BookClass.ToList();
            return BookCreateVM;
        }
        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <param name="book">書籍資料</param>
        public void  CreateBookDate(Book book)
        {
            book.Book_Status = "A";
            db.Book.Add(book);
            db.SaveChanges();
        }

        /// <summary>
        /// 取得修改頁面書籍及相關物件(書籍類別、書籍狀態、會員)
        /// </summary>
        /// <param name="id">書籍編號</param>
        /// <returns></returns>
        public BooksEditViewModel GetBooksEditRelatedObject(int? id)
        {
            BooksEditViewModel booksEditVM = new BooksEditViewModel();
            booksEditVM.Book = db.Book.Find(id);
            booksEditVM.BookClasses = db.BookClass.ToList();
            booksEditVM.BookStatuses = db.BookStatus.ToList();
            booksEditVM.Members = db.Member.ToList();
            return booksEditVM;
        }

        public void BookSaveChange(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}