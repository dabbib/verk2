namespace h37.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eventfix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Events");
            AddColumn("dbo.Events", "eventID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Events", "userID", c => c.String());
            AddPrimaryKey("dbo.Events", "eventID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Events");
            AlterColumn("dbo.Events", "userID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Events", "eventID");
            AddPrimaryKey("dbo.Events", "userID");
        }
    }
}
