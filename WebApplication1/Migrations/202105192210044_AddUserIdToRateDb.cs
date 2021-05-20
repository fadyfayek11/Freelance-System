namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToRateDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StarRatings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.StarRatings", "UserId");
            AddForeignKey("dbo.StarRatings", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarRatings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StarRatings", new[] { "UserId" });
            DropColumn("dbo.StarRatings", "UserId");
        }
    }
}
