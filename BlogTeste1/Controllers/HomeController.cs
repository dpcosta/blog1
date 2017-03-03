using BlogTeste1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace BlogTeste1.Controllers
{
    public class HomeController : Controller
    {

        private PostDao dao = new PostDao();

        private BlogInfo blog = new BlogInfo
        {
            Titulo = "Resenha Cultural",
            Descricao = "Críticas e artigos sobre filmes, livros, séries e tudo o que agita o cenário cultural. "
        };

 
        // GET: Home
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                Info = blog,
                Posts = dao.ListaPublicados()
            };
            return View(model);
        }

        public ActionResult Busca(string termo)
        {
            var model = new HomeViewModel
            {
                Info = blog,
                InfoAdicional = string.Format("Você pesquisou por {0}...", termo),
                Posts = dao.PesquisarPorTermo(termo)
            };
            return View("Index", model);
        }
    }
}