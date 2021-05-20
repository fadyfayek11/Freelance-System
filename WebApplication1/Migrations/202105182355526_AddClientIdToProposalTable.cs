namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientIdToProposalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalModels", "ClientId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProposalModels", "ClientId");
        }
    }
}
