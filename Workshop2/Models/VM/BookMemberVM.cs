using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workshop2.Models.VM
{
    public class BookMemberVM
    {
        public List<Book> Book { get; set; }
        public List<Member> Member { get; set; }
        public List<BookClass> BookClass { get; set; }
    }
}