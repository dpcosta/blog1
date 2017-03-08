using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BlogTeste1.Models
{
    public class CategoriaDao
    {
        private BlogDb _db;

        public CategoriaDao(BlogDb context)
        {
            this._db = context;
        }

        public IList<Categoria> Lista()
        {
            return _db.Categorias.ToList();
        }

        public void Adiciona(Categoria model)
        {
            _db.Categorias.Add(model);
            _db.SaveChanges();
        }

        public void Atualiza(Categoria model)
        {
            DbEntityEntry entity = _db.Entry(model);
            entity.State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(int id)
        {
            _db.Categorias.Remove(_db.Categorias.Find(id));
            _db.SaveChanges();
        }

        public Categoria BuscaPorId(int id)
        {
            return _db.Categorias.Find(id);
        }
    }
}