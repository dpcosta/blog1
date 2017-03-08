namespace BlogTeste1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogTeste1.Models.BlogDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BlogTeste1.Models.BlogDb";
        }

        protected override void Seed(BlogTeste1.Models.BlogDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("admin");
            Usuario user = new Usuario
            {
                UserName = "admin",
                PasswordHash = password,
                UltimoLogin = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            context.Users.AddOrUpdate(u => u.UserName, user);

            Categoria livros = new Categoria { Id = 1, Nome = "Livros" };
            Categoria filmes = new Categoria { Id = 2, Nome = "Filmes" };
            Categoria musicas = new Categoria { Id = 3, Nome = "Músicas" };
            Categoria series = new Categoria { Id = 4, Nome = "Séries" };
            context.Categorias.AddOrUpdate( c => c.Id, livros, filmes, musicas, series );

            context.Posts.AddOrUpdate(
                p => p.Id,
                new Post { Id = 1, Titulo = "Harry Potter", Resumo = "E a pedra filosofal", Categorias = new List<Categoria> { livros, filmes } },
                new Post { Id = 2, Titulo = "O Senhor dos Anéis", Resumo = "O Retorno do Rei", Categorias = new List<Categoria> { livros, filmes } },
                new Post { Id = 3, Titulo = "O Monge e o Executivo", Resumo = "Romance sobre Liderança", Categorias = new List<Categoria> { livros } },
                new Post { Id = 4, Titulo = "Game of Thrones", Resumo = "Série popular da Fox", Categorias = new List<Categoria> { series } },
                new Post { Id = 5, Titulo = "New York, New York", Resumo = "Sucesso de Frank Sinatra", Categorias = new List<Categoria> { musicas } }
            );
        }
    }
}
