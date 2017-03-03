namespace BlogTeste1.Migrations
{
    using Models;
    using System;
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


            context.Posts.AddOrUpdate(
                p => p.Id,
                new Post { Id = 1, Titulo = "Harry Potter", Resumo = "E a pedra filosofal", Categoria = "Livros, Filmes" },
                new Post { Id = 2, Titulo = "O Senhor dos Anéis", Resumo = "O Retorno do Rei", Categoria = "Livros, Filmes" },
                new Post { Id = 3, Titulo = "O Monge e o Executivo", Resumo = "Romance sobre Liderança", Categoria = "Livros" },
                new Post { Id = 4, Titulo = "Game of Thrones", Resumo = "Série popular da Fox", Categoria = "Séries" },
                new Post { Id = 5, Titulo = "New York, New York", Resumo = "Sucesso de Frank Sinatra", Categoria = "Músicas" }
            );
        }
    }
}
