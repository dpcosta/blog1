using BlogTeste1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTeste1.Areas.Admin.Controllers
{
    //TODO: colocar TextArea no campo de Resumo
    //TODO: criar campo Texto no post, com possibilidade de escrever markdown
    //TODO: refazer barra lateral, com as categorias vindas do banco, com total de posts por categoria
    //TODO: refazer barra lateral, com os posts do mês vindos do banco, com total de posts por categoria
    [Authorize]
    public class PostController : Controller
    {
        private PostDao dao;
        private CategoriaDao categoriaDao;

        public PostController(PostDao postDao, CategoriaDao catDao)
        {
            this.dao = postDao;
            this.categoriaDao = catDao;
        }

        // GET: Admin/Post
        public ActionResult Index()
        {
            var model = dao.Lista();
            return View(model);
        }

        [HttpGet]
        public ActionResult Novo()
        {
            ViewBag.Categorias = categoriaDao.Lista();
            return View();
        }

        [HttpPost]
        public ActionResult Novo(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Titulo = model.Titulo,
                    Resumo = model.Resumo,
                    Categorias = model.CategoriasId.Select(c => categoriaDao.BuscaPorId(c)).ToList(),
                    Autor = dao.BuscaAutorPeloId(User.Identity.GetUserId())
                };
                this.dao.Adiciona(post);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = categoriaDao.Lista();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categorias = categoriaDao.Lista();
            Post post = dao.BuscaPorId(id);
            var model = new PostViewModel
            {
                Id = post.Id,
                Titulo = post.Titulo,
                Resumo = post.Resumo,
                CategoriasId = post.Categorias != null ? post.Categorias.Select(c => c.Id).ToArray() : null
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Id = model.Id,
                    Titulo = model.Titulo,
                    Resumo = model.Resumo,
                    Categorias = model.CategoriasId != null ? model.CategoriasId.Select(c => new Categoria { Id = c }).ToList() : null,
                    Autor = new Usuario { Id = User.Identity.GetUserId() }
                };
                dao.Atualiza(post);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = categoriaDao.Lista();
                return View(model);
            }
        }

        public ActionResult Remove(int id)
        {
            dao.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult Publicar(int id)
        {
            dao.Publica(id);
            return RedirectToAction("Index");
        }

    }
}