namespace BlogTeste1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publicacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Publicado", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Posts", "DataPublicacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DataPublicacao");
            DropColumn("dbo.Posts", "Publicado");
        }
    }
}
