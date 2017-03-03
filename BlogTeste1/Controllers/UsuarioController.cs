using BlogTeste1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTeste1.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Post", new { area = "Admin" });
            }
            return View(model);
        }
    }
}