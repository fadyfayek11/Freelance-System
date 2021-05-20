namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTestTableToDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs");
            DropIndex("dbo.PostsModels", new[] { "PostId" });
            DropPrimaryKey("dbo.PostsModels");
            AddColumn("dbo.PostsModels", "Test", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PostsModels", "Test");
            DropColumn("dbo.PostsModels", "RateId");
            DropColumn("dbo.PostsModels", "Rating");
            DropColumn("dbo.PostsModels", "PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostsModels", "PostId", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "RateId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.PostsModels");
            DropColumn("dbo.PostsModels", "Test");
            AddPrimaryKey("dbo.PostsModels", "RateId");
            CreateIndex("dbo.PostsModels", "PostId");
            AddForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs", "Id", cascadeDelete: true);
        }
    }
}
