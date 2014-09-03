namespace Pastebin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AzureTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pastes", "PartitionKey", c => c.String());
            AddColumn("dbo.Pastes", "RowKey", c => c.String());
            AddColumn("dbo.Pastes", "Timestamp", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Pastes", "ETag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pastes", "ETag");
            DropColumn("dbo.Pastes", "Timestamp");
            DropColumn("dbo.Pastes", "RowKey");
            DropColumn("dbo.Pastes", "PartitionKey");
        }
    }
}
