namespace TvPlays.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Clips", name: "UtilizadoresFK", newName: "UserFK");
            RenameIndex(table: "dbo.Clips", name: "IX_UtilizadoresFK", newName: "IX_UserFK");
            AddColumn("dbo.Emojis", "ShortcutToEmoji", c => c.String());
            AddColumn("dbo.Emojis", "Name", c => c.String());
            AlterColumn("dbo.Payments", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Value", c => c.Double(nullable: false));
            DropColumn("dbo.Emojis", "Name");
            DropColumn("dbo.Emojis", "ShortcutToEmoji");
            RenameIndex(table: "dbo.Clips", name: "IX_UserFK", newName: "IX_UtilizadoresFK");
            RenameColumn(table: "dbo.Clips", name: "UserFK", newName: "UtilizadoresFK");
        }
    }
}
