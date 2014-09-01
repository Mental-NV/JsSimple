namespace Pastebin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pastes", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pastes", "Content", c => c.String());
        }
    }
}
