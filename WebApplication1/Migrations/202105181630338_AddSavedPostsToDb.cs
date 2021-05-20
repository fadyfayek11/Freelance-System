namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSavedPostsToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Saveds",
                c => new
                    {
                        IdOfSavedPost = c.Int(nullable: false, identity: true),
                        IsMarked = c.Boolean(),
                        IdOfTheUser = c.String(maxLength: 128),
                        IdOfThePost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOfSavedPost)
                .ForeignKey("dbo.AspNetUsers", t => t.IdOfTheUser)
                .Index(t => t.IdOfTheUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Saveds", "IdOfTheUser", "dbo.AspNetUsers");
            DropIndex("dbo.Saveds", new[] { "IdOfTheUser" });
            DropTable("dbo.Saveds");
        }
    }
}
