namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPostModelToDb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JobPosts", newName: "JobPostModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.JobPostModels", newName: "JobPosts");
        }
    }
}
