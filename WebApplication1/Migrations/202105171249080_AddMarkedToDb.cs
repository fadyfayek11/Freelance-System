namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarkedToDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobPostModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.JobPostModels", new[] { "UserId" });
            CreateTable(
                "dbo.Markeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.JobPostModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobPostModels",
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
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Markeds");
            CreateIndex("dbo.JobPostModels", "UserId");
            AddForeignKey("dbo.JobPostModels", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
