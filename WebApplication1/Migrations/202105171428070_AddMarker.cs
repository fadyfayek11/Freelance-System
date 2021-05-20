namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markeds", "MyName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markeds", "MyName");
        }
    }
}
