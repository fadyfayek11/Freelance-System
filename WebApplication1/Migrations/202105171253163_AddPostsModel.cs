namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserName = c.String(nullable: false),
                        JobType = c.String(nullable: false),
                        JobBudget = c.Double(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Discription = c.String(nullable: false),
                        IsStillAvilavble = c.Boolean(nullable: false),
                        NumberOfSubmitted = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        IsAvilavbleAtWall = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostsModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PostsModels", new[] { "UserId" });
            DropTable("dbo.PostsModels");
        }
    }
}
