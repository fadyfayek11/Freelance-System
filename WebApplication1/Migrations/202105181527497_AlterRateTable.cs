namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterRateTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PostsModels", new[] { "Test" });
            AddColumn("dbo.PostsModels", "RateId", c => c.Int(nullable: false, identity: false));
            AddColumn("dbo.PostsModels", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "PostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostsModels", "RateId");
            CreateIndex("dbo.PostsModels", "PostId");
            AddForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs", "Id", cascadeDelete: true);
            DropColumn("dbo.PostsModels", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostsModels", "Test", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs");
            DropIndex("dbo.PostsModels", new[] { "PostId" });
            DropPrimaryKey("dbo.PostsModels");
            DropColumn("dbo.PostsModels", "PostId");
            DropColumn("dbo.PostsModels", "Rating");
            DropColumn("dbo.PostsModels", "RateId");
            AddPrimaryKey("dbo.PostsModels", "Test");
        }
    }
}
