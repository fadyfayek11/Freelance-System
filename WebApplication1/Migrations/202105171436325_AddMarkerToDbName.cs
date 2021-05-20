namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarkerToDbName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Markeds", newName: "PostJobs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostJobs", newName: "Markeds");
        }
    }
}
