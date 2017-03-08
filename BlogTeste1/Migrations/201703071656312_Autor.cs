namespace BlogTeste1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autor : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "Usuario_Id", newName: "Autor_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_Usuario_Id", newName: "IX_Autor_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_Autor_Id", newName: "IX_Usuario_Id");
            RenameColumn(table: "dbo.Posts", name: "Autor_Id", newName: "Usuario_Id");
        }
    }
}
