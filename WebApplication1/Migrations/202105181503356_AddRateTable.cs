namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRateTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostsModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PostsModels", new[] { "UserId" });
            DropPrimaryKey("dbo.PostsModels");
            AddColumn("dbo.PostsModels", "RateId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PostsModels", "Rateing", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "PostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostsModels", "RateId");
            CreateIndex("dbo.PostsModels", "PostId");
            AddForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs", "Id", cascadeDelete: false);
            DropColumn("dbo.PostsModels", "Id");
            DropColumn("dbo.PostsModels", "UserId");
            DropColumn("dbo.PostsModels", "UserName");
            DropColumn("dbo.PostsModels", "JobType");
            DropColumn("dbo.PostsModels", "JobBudget");
            DropColumn("dbo.PostsModels", "CreationDate");
            DropColumn("dbo.PostsModels", "Discription");
            DropColumn("dbo.PostsModels", "IsStillAvilavble");
            DropColumn("dbo.PostsModels", "NumberOfSubmitted");
            DropColumn("dbo.PostsModels", "Rate");
            DropColumn("dbo.PostsModels", "IsAvilavbleAtWall");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostsModels", "IsAvilavbleAtWall", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostsModels", "Rate", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "NumberOfSubmitted", c => c.Int(nullable: false));
            AddColumn("dbo.PostsModels", "IsStillAvilavble", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostsModels", "Discription", c => c.String(nullable: false));
            AddColumn("dbo.PostsModels", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PostsModels", "JobBudget", c => c.Double(nullable: false));
            AddColumn("dbo.PostsModels", "JobType", c => c.String(nullable: false));
            AddColumn("dbo.PostsModels", "UserName", c => c.String(nullable: false));
            AddColumn("dbo.PostsModels", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PostsModels", "Id", c => c.Int(nullable: false, identity: false));
            DropForeignKey("dbo.PostsModels", "PostId", "dbo.PostJobs");
            DropIndex("dbo.PostsModels", new[] { "PostId" });
            DropPrimaryKey("dbo.PostsModels");
            DropColumn("dbo.PostsModels", "PostId");
            DropColumn("dbo.PostsModels", "Rateing");
            DropColumn("dbo.PostsModels", "RateId");
            AddPrimaryKey("dbo.PostsModels", "Id");
            CreateIndex("dbo.PostsModels", "UserId");
            AddForeignKey("dbo.PostsModels", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
