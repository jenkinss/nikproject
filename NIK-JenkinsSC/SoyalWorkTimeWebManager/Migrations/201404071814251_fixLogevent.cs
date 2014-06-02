namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixLogevent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkTimeEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        SiteID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        Direction = c.String(),
                        EventType = c.String(),
                        EventNote = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.SiteID)
                .Index(t => t.PersonID);
            
            DropColumn("dbo.LoggedEvents", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoggedEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.WorkTimeEvents", new[] { "PersonID" });
            DropIndex("dbo.WorkTimeEvents", new[] { "SiteID" });
            DropForeignKey("dbo.WorkTimeEvents", "PersonID", "dbo.People");
            DropForeignKey("dbo.WorkTimeEvents", "SiteID", "dbo.Sites");
            DropTable("dbo.WorkTimeEvents");
        }
    }
}
