﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogTeste1.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required][StringLength(50, MinimumLength = 5)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [MaxLength(500)]
        [Display(Name = "Resumo")]
        public string Resumo { get; set; }

        public bool Publicado { get; set; }

        [Display(Name = "Data de Publicação")]
        public DateTime? DataPublicacao { get; set; }

        [Display(Name = "Autor")]
        public virtual Usuario Autor { get; set; }

        [Display(Name = "Categorias")]
        public virtual ICollection<Categoria> Categorias { get; set; }

    }
}