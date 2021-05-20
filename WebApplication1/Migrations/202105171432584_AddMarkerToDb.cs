namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarkerToDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markeds", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Markeds", "UserName", c => c.String(nullable: false));
            AddColumn("dbo.Markeds", "JobType", c => c.String(nullable: false));
            AddColumn("dbo.Markeds", "JobBudget", c => c.Double(nullable: false));
            AddColumn("dbo.Markeds", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Markeds", "Discription", c => c.String(nullable: false));
            AddColumn("dbo.Markeds", "IsStillAvilavble", c => c.Boolean(nullable: false));
            AddColumn("dbo.Markeds", "NumberOfSubmitted", c => c.Int(nullable: false));
            AddColumn("dbo.Markeds", "Rate", c => c.Int(nullable: false));
            AddColumn("dbo.Markeds", "IsAvilavbleAtWall", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Markeds", "UserId");
            AddForeignKey("dbo.Markeds", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Markeds", "MyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markeds", "MyName", c => c.String(nullable: false));
            DropForeignKey("dbo.Markeds", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Markeds", new[] { "UserId" });
            DropColumn("dbo.Markeds", "IsAvilavbleAtWall");
            DropColumn("dbo.Markeds", "Rate");
            DropColumn("dbo.Markeds", "NumberOfSubmitted");
            DropColumn("dbo.Markeds", "IsStillAvilavble");
            DropColumn("dbo.Markeds", "Discription");
            DropColumn("dbo.Markeds", "CreationDate");
            DropColumn("dbo.Markeds", "JobBudget");
            DropColumn("dbo.Markeds", "JobType");
            DropColumn("dbo.Markeds", "UserName");
            DropColumn("dbo.Markeds", "UserId");
        }
    }
}
