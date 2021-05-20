namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRateingTableToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StarRatings",
                c => new
                    {
                        IdOfRate = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOfRate)
                .ForeignKey("dbo.PostJobs", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarRatings", "PostId", "dbo.PostJobs");
            DropIndex("dbo.StarRatings", new[] { "PostId" });
            DropTable("dbo.StarRatings");
        }
    }
}
