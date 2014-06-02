namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtcWorkStyle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "WorkStyle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "WorkStyle");
        }
    }
}
