namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameUa = c.String(nullable: false),
                        CardBoardNumber = c.Int(nullable: false),
                        Birth = c.DateTime(nullable: false),
                        CardID = c.Int(),
                        ProfessionalClass = c.String(nullable: false),
                        Post = c.String(nullable: false),
                        SubUnit = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.WTCards", t => t.CardID)
                .Index(t => t.CardID);
            
            CreateTable(
                "dbo.WTCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Code = c.Int(nullable: false),
                        AntiPassBack = c.Boolean(nullable: false),
                        UserAddress = c.Int(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PinCode = c.Int(nullable: false),
                        Mode = c.Int(nullable: false),
                        TimeZone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PersonGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FacilityLocations", t => t.LocationID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.FacilityLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        NodeID = c.Int(),
                        Name = c.String(),
                        FacilityLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nodes", t => t.NodeID)
                .ForeignKey("dbo.FacilityLocations", t => t.FacilityLocation_ID)
                .Index(t => t.NodeID)
                .Index(t => t.FacilityLocation_ID);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        IP = c.String(nullable: false),
                        Port = c.Int(nullable: false),
                        Number = c.String(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LoggedEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        SiteID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        Direction = c.String(),
                        EventType = c.String(),
                        EventNote = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.SiteID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.FacilityLocationPersons",
                c => new
                    {
                        FacilityLocation_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacilityLocation_ID, t.Person_ID })
                .ForeignKey("dbo.FacilityLocations", t => t.FacilityLocation_ID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.FacilityLocation_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.PersonPersonGroups",
                c => new
                    {
                        Person_ID = c.Int(nullable: false),
                        PersonGroup_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_ID, t.PersonGroup_ID })
                .ForeignKey("dbo.People", t => t.Person_ID, cascadeDelete: true)
                .ForeignKey("dbo.PersonGroups", t => t.PersonGroup_ID, cascadeDelete: true)
                .Index(t => t.Person_ID)
                .Index(t => t.PersonGroup_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PersonPersonGroups", new[] { "PersonGroup_ID" });
            DropIndex("dbo.PersonPersonGroups", new[] { "Person_ID" });
            DropIndex("dbo.FacilityLocationPersons", new[] { "Person_ID" });
            DropIndex("dbo.FacilityLocationPersons", new[] { "FacilityLocation_ID" });
            DropIndex("dbo.LoggedEvents", new[] { "PersonID" });
            DropIndex("dbo.LoggedEvents", new[] { "SiteID" });
            DropIndex("dbo.Sites", new[] { "FacilityLocation_ID" });
            DropIndex("dbo.Sites", new[] { "NodeID" });
            DropIndex("dbo.PersonGroups", new[] { "LocationID" });
            DropIndex("dbo.People", new[] { "CardID" });
            DropForeignKey("dbo.PersonPersonGroups", "PersonGroup_ID", "dbo.PersonGroups");
            DropForeignKey("dbo.PersonPersonGroups", "Person_ID", "dbo.People");
            DropForeignKey("dbo.FacilityLocationPersons", "Person_ID", "dbo.People");
            DropForeignKey("dbo.FacilityLocationPersons", "FacilityLocation_ID", "dbo.FacilityLocations");
            DropForeignKey("dbo.LoggedEvents", "PersonID", "dbo.People");
            DropForeignKey("dbo.LoggedEvents", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Sites", "FacilityLocation_ID", "dbo.FacilityLocations");
            DropForeignKey("dbo.Sites", "NodeID", "dbo.Nodes");
            DropForeignKey("dbo.PersonGroups", "LocationID", "dbo.FacilityLocations");
            DropForeignKey("dbo.People", "CardID", "dbo.WTCards");
            DropTable("dbo.PersonPersonGroups");
            DropTable("dbo.FacilityLocationPersons");
            DropTable("dbo.LoggedEvents");
            DropTable("dbo.Nodes");
            DropTable("dbo.Sites");
            DropTable("dbo.FacilityLocations");
            DropTable("dbo.PersonGroups");
            DropTable("dbo.WTCards");
            DropTable("dbo.People");
        }
    }
}
