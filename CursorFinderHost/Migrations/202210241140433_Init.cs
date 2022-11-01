namespace CursorFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CursorPositions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            XPos = c.Int(nullable: false),
            //            YPos = c.Int(nullable: false),
            //            DateTime = c.DateTime(nullable: false),
            //            ActionType = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.CursorPositions");
        }
    }
}
