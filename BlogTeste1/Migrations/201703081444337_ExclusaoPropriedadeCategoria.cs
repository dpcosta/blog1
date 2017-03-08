namespace BlogTeste1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExclusaoPropriedadeCategoria : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Categoria", c => c.String(maxLength: 100));
        }
    }
}
