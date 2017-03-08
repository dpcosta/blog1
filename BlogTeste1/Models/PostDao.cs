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
            _db.Entry(post).State = EntityState.Modified;

            var postExistente = _db.Posts.Include("Categorias").Where(p => p.Id == post.Id).FirstOrDefault<Post>();

            var categoriasDeletadas = new List<Categoria>();
            foreach (var item in postExistente.Categorias)
            {
                if (!post.Categorias.Contains(item))
                {
                    categoriasDeletadas.Add(item);
                }
            }

            var categoriasAdicionadas = new List<Categoria>();
            foreach (var item in post.Categorias)
            {
                if (!postExistente.Categorias.Contains(item))
                {
                    categoriasAdicionadas.Add(item);
                }
            }

            categoriasDeletadas.ForEach(c => postExistente.Categorias.Remove(c));

            foreach (Categoria item in categoriasAdicionadas)
            {
                if (_db.Entry(item).State == EntityState.Detached)
                {
                    _db.Categorias.Attach(item);
                }
                postExistente.Categorias.Add(item);
            }

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