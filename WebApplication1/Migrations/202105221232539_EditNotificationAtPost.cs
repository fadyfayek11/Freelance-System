namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNotificationAtPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostJobs", "CountOfPostsRequest", c => c.Int());
            AddColumn("dbo.PostJobs", "IsNotificationOfPostsRequestSeen", c => c.Boolean());
            AddColumn("dbo.PostJobs", "CountOfProposalRequest", c => c.Int());
            AddColumn("dbo.PostJobs", "IsNotificationOfProposalRequestSeen", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostJobs", "IsNotificationOfProposalRequestSeen");
            DropColumn("dbo.PostJobs", "CountOfProposalRequest");
            DropColumn("dbo.PostJobs", "IsNotificationOfPostsRequestSeen");
            DropColumn("dbo.PostJobs", "CountOfPostsRequest");
        }
    }
}
