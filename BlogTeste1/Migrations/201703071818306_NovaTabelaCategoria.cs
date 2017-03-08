namespace BlogTeste1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovaTabelaCategoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCategorias",
                c => new
                    {
                        Post_Id = c.Int(nullable: false),
                        Categoria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_Id, t.Categoria_Id })
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Post_Id)
                .Index(t => t.Categoria_Id);

            Sql("INSERT INTO Categorias (Nome) SELECT DISTINCT Categoria FROM Posts WHERE Categoria IS NOT NULL");

            Sql("INSERT INTO Categorias_Posts (IdCategoria, IdPost) SELECT post.Id, cat.Id FROM Posts post INNER JOIN Categorias cat ON post.Categoria=cat.Nome");


        }

        public override void Down()
        {
            Sql("UPDATE Posts p SET Categoria = (SELECT COALESCE(c.Nome+',','') FROM Categorias c INNER JOIN PostCategorias pc ON pc.Categoria_Id = c.Id WHERE pc.Post_Id = p.Id");

            DropForeignKey("dbo.PostCategorias", "Categoria_Id", "dbo.Categorias");
            DropForeignKey("dbo.PostCategorias", "Post_Id", "dbo.Posts");
            DropIndex("dbo.PostCategorias", new[] { "Categoria_Id" });
            DropIndex("dbo.PostCategorias", new[] { "Post_Id" });
            DropTable("dbo.PostCategorias");
            DropTable("dbo.Categorias");
        }
    }
}
