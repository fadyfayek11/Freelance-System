namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProposalToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProposalModels",
                c => new
                    {
                        IdOfProposal = c.Int(nullable: false, identity: true),
                        TheProposal = c.String(nullable: false),
                        FreeLancerId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOfProposal)
                .ForeignKey("dbo.PostJobs", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FreeLancerId)
                .Index(t => t.FreeLancerId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProposalModels", "FreeLancerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProposalModels", "PostId", "dbo.PostJobs");
            DropIndex("dbo.ProposalModels", new[] { "PostId" });
            DropIndex("dbo.ProposalModels", new[] { "FreeLancerId" });
            DropTable("dbo.ProposalModels");
        }
    }
}
