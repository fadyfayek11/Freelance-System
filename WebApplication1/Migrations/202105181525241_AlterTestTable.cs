namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTestTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs");
            DropIndex("dbo.PostsModels", new[] { "PostId" });
            DropPrimaryKey("dbo.PostsModels");
            AddColumn("dbo.PostsModels", "Test", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.PostsModels", "Test");
            DropColumn("dbo.PostsModels", "RateId");
            DropColumn("dbo.PostsModels", "Rateing");
            DropColumn("dbo.PostsModels", "PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostsModels", "PostId", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "Rateing", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "RateId", c => c.Int(nullable: false, identity: false));
            DropPrimaryKey("dbo.PostsModels");
            DropColumn("dbo.PostsModels", "Test");
            AddPrimaryKey("dbo.PostsModels", "RateId");
            CreateIndex("dbo.PostsModels", "PostId");
            AddForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs", "Id", cascadeDelete: true);
        }
    }
}
