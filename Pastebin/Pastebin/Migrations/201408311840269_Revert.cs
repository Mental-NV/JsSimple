namespace Pastebin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Revert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pastes",
                c => new
                    {
                        PasteId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                        UserId = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PasteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pastes");
        }
    }
}
