using BlogTeste1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTeste1.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private PostDao dao = new PostDao();

        // GET: Admin/Post
        public ActionResult Index()
        {
            var model = dao.Lista();
            return View(model);
        }

        [HttpGet]
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Novo(Post post)
        {
            if (ModelState.IsValid)
            {
                this.dao.Adiciona(post);
                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Post post = dao.BuscaPorId(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                dao.Atualiza(post);
                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
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