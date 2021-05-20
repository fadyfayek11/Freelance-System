namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AltTestTableToDb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PostsModels", newName: "TestModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TestModels", newName: "PostsModels");
        }
    }
}
