using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BlogTeste1.Models
{
    public class UsuarioManager : UserManager<Usuario>
    {
        public UsuarioManager(IUserStore<Usuario> store) : base(store)
        {
        }

        public static UsuarioManager Create(IdentityFactoryOptions<UsuarioManager> options, IOwinContext context)
        {
            var userStore = new UserStore<Usuario>(new BlogDb());
            return new UsuarioManager(userStore);
        }
    }
}