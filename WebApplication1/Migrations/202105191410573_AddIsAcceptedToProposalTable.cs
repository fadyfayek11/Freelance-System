namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsAcceptedToProposalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalModels", "IsAccepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProposalModels", "IsAccepted");
        }
    }
}
