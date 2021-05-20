namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNotificationAtPostToDbProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalModels", "IsNotificationOfProposalRequestSeen", c => c.Boolean());
            DropColumn("dbo.PostJobs", "IsNotificationOfProposalRequestSeen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostJobs", "IsNotificationOfProposalRequestSeen", c => c.Boolean());
            DropColumn("dbo.ProposalModels", "IsNotificationOfProposalRequestSeen");
        }
    }
}
