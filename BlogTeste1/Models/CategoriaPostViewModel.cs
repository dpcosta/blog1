using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class CategoriaPostViewModel
    {
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public int TotalPosts { get; set; }
    }
}