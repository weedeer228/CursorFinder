namespace CursorFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTokenField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CursorPositions", "UserToken", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CursorPositions", "UserToken");
        }
    }
}
