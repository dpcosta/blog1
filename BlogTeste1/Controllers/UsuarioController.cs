using BlogTeste1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace BlogTeste1.Controllers
{
    //TODO: fazer o CRUD de usuários para a área administrativa
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
                UsuarioManager manager = HttpContext.GetOwinContext().GetUserManager<UsuarioManager>();
                Usuario usuario = manager.Find(model.LoginName, model.Senha);
                if (usuario != null)
                {
                    ClaimsIdentity identity = manager.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { }, identity);
                    return RedirectToAction("Index", "Post", new { area = "Admin" });
                } else
                {
                    ModelState.AddModelError("usuarioInexistente", "Usuário ou Senha não encontrados no sistema.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = model.LoginName,
                    Email = model.Email
                };
                UsuarioManager manager = HttpContext.GetOwinContext().GetUserManager<UsuarioManager>();
                IdentityResult resultado = manager.Create(usuario, model.Senha);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Login", "Usuario");
                } else
                {
                    foreach (var item in resultado.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        [ChildActionOnly]
        public ActionResult UsuarioLogado()
        {
            UsuarioManager manager = HttpContext.GetOwinContext().GetUserManager<UsuarioManager>();
            var user = manager.FindById(User.Identity.GetUserId());
            return View(user);
        }
    }
}