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

        private PostDao dao;
        private CategoriaDao categoriaDao;
        private UsuarioDao usuarioDao;

        public HomeController(PostDao dao, CategoriaDao catDao, UsuarioDao usuarioDao)
        {
            this.dao = dao;
            this.categoriaDao = catDao;
            this.usuarioDao = usuarioDao;
        }

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

        public ActionResult Categoria(int id)
        {
            Categoria categoria = categoriaDao.BuscaPorId(id);
            var model = new HomeViewModel
            {
                Info = blog,
                InfoAdicional = string.Format("Posts da categoria {0}...", categoria.Nome),
                Posts = categoria.Posts.ToList()
            };
            return View("Index", model);
        }

        public ActionResult Autor(string id)
        {
            Usuario usuario = usuarioDao.BuscaPorId(id);
            var model = new HomeViewModel
            {
                Info = blog,
                InfoAdicional = string.Format("Posts do autor {0}...", usuario.UserName),
                Posts = usuario.Posts.ToList()
            };
            return View("Index", model);
        }

        [ChildActionOnly]
        public ActionResult MenuCategorias()
        {
            var catPosts = from c in categoriaDao.Lista()
                           from p in c.Posts
                           where p.Publicado
                           select new { CategoriaId = c.Id, Categoria = c.Nome, PostId = p.Id };
            var model = from cp in catPosts
                        group cp by new { cp.CategoriaId, cp.Categoria } into g
                        select new CategoriaPostViewModel { CategoriaId = g.Key.CategoriaId, Categoria = g.Key.Categoria, TotalPosts = g.Count() };
            return View(model.ToList());
        }

        [ChildActionOnly]
        public ActionResult MenuAutores()
        {
            var usuPosts = from u in usuarioDao.Lista()
                           from p in u.Posts
                           where p.Publicado
                           select new { AutorId = u.Id, AutorNome = u.UserName, PostId = p.Id };
            var model = from a in usuPosts
                        group a by new { a.AutorId, a.AutorNome } into g
                        select new AutorPostViewModel { AutorId = g.Key.AutorId, AutorNome = g.Key.AutorNome, TotalPosts = g.Count() };
            return View(model.ToList());
        }
    }
}