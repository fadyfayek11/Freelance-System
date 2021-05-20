namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNotificationAtPostToDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PostJobs", "CountOfPostsRequest");
            DropColumn("dbo.PostJobs", "CountOfProposalRequest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostJobs", "CountOfProposalRequest", c => c.Int());
            AddColumn("dbo.PostJobs", "CountOfPostsRequest", c => c.Int());
        }
    }
}
