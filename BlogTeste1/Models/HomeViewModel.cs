using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class HomeViewModel
    {
        public BlogInfo Info { get; set; }
        public string InfoAdicional { get; set; }
        public IList<Post> Posts { get; set; }
    }
}