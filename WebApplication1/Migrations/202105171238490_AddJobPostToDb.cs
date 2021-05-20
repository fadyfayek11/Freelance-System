namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPostToDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JobPosts", "IsMarked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobPosts", "IsMarked", c => c.Boolean(nullable: false));
        }
    }
}
