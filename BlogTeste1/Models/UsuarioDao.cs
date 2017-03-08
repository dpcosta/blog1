using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class UsuarioDao
    {
        private BlogDb context;

        public UsuarioDao(BlogDb ctx)
        {
            this.context = ctx;
        }

        public IList<Usuario> Lista()
        {
            return context.Users.ToList();
        }

        public Usuario BuscaPorId(string id)
        {
            return context.Users.Find(id);
        }
    }
}