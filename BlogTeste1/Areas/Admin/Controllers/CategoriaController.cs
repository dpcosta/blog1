using BlogTeste1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTeste1.Areas.Admin.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaDao dao;

        public CategoriaController(CategoriaDao dao)
        {
            this.dao = dao;
        }

        // GET: Admin/Categoria
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
        public ActionResult Novo(Categoria model)
        {
            if (ModelState.IsValid)
            {
                dao.Adiciona(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = dao.BuscaPorId(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Categoria model)
        {
            if (ModelState.IsValid)
            {
                dao.Atualiza(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Remove(int id)
        {
            dao.Remove(id);
            return RedirectToAction("Index");
        }
    }
}