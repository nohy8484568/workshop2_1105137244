using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workshop2.Models.ViewModel
{
    /// <summary>
    /// 首頁  /Books/Index
    /// </summary>
    public class BooksIndexViewModel
    {
        public List<Book> Books { get; set; }
        public List<BookClass> BookClasses { get; set; }
        public List<Member> Members { get; set; }
        public List<BookStatus> BookStatuses { get; set; }
    }
    /// <summary>
    /// 修改頁面  /Books/Edit
    /// </summary>
    public class BooksEditViewModel
    {
        public Book Book { get; set; }
        public List<BookClass> BookClasses { get; set; }
        public List<Member> Members { get; set; }
        public List<BookStatus> BookStatuses { get; set; }
    }
    /// <summary>
    /// 新增頁面  /Books/Create
    /// </summary>
    public class BooksCreateViewModel
    {
        public Book Book { get; set; }
        public List<BookClass> BookClasses { get; set; }
    }
}