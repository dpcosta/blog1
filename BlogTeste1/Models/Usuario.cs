using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime? UltimoLogin { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}