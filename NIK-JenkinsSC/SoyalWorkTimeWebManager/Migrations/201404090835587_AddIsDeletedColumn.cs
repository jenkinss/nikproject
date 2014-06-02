namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WTCards", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonGroups", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.FacilityLocations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sites", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Nodes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkTimeEvents", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.LoggedEvents", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoggedEvents", "IsDeleted");
            DropColumn("dbo.WorkTimeEvents", "IsDeleted");
            DropColumn("dbo.Nodes", "IsDeleted");
            DropColumn("dbo.Sites", "IsDeleted");
            DropColumn("dbo.FacilityLocations", "IsDeleted");
            DropColumn("dbo.PersonGroups", "IsDeleted");
            DropColumn("dbo.WTCards", "IsDeleted");
            DropColumn("dbo.People", "IsDeleted");
        }
    }
}
