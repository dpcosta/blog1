using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class PostDao
    {
        private BlogDb _db;

        public PostDao(BlogDb context)
        {
            this._db = context;
        }

        public Post BuscaPorId(int id)
        {
            return this._db.Posts.Find(id);
        }

        public void Adiciona(Post post)
        {
            this._db.Posts.Add(post);
            this._db.SaveChanges();
        }

        public void Remove(int id)
        {
            Post post = this.BuscaPorId(id);
            this._db.Posts.Remove(post);
            this._db.SaveChanges();
        }

        public void Atualiza(Post post)
        {
            this._db.UpdateGraph(post, map => map.AssociatedEntity(p => p.Autor).AssociatedCollection(p => p.Categorias));
            this._db.SaveChanges();
        }

        public void Publica(int id)
        {
            Post post = this.BuscaPorId(id);
            post.Publicado = true;
            post.DataPublicacao = DateTime.Now;
            DbEntityEntry entity = this._db.Entry(post);
            entity.State = EntityState.Modified;
            this._db.SaveChanges();
        }

        public IList<Post> Lista()
        {
            return this._db.Posts.ToList();
        }

        public IList<Post> ListaPublicados()
        {
            var posts = Lista().Where(p => p.Publicado).OrderByDescending(p => p.DataPublicacao).Select(p => p);
            return posts.ToList();
        }

        public IList<Post> PesquisarPorTermo(string termo)
        {
            var posts = ListaPublicados().Where(p => p.Titulo.ToUpper().Contains(termo.ToUpper()) || p.Resumo.ToUpper().Contains(termo.ToUpper())).Select(p => p);
            return posts.ToList();
        }

        public Usuario BuscaAutorPeloId(string id)
        {
            return _db.Users.Find(id);
        }
    }
}