namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nogroupreq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonPersonGroups", "Person_ID", "dbo.People");
            DropForeignKey("dbo.PersonPersonGroups", "PersonGroup_ID", "dbo.PersonGroups");
            DropIndex("dbo.PersonPersonGroups", new[] { "Person_ID" });
            DropIndex("dbo.PersonPersonGroups", new[] { "PersonGroup_ID" });
            CreateTable(
                "dbo.PersonGroupPersons",
                c => new
                    {
                        PersonGroup_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonGroup_ID, t.Person_ID })
                .ForeignKey("dbo.PersonGroups", t => t.PersonGroup_ID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.PersonGroup_ID)
                .Index(t => t.Person_ID);
            
            DropTable("dbo.PersonPersonGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PersonPersonGroups",
                c => new
                    {
                        Person_ID = c.Int(nullable: false),
                        PersonGroup_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_ID, t.PersonGroup_ID });
            
            DropIndex("dbo.PersonGroupPersons", new[] { "Person_ID" });
            DropIndex("dbo.PersonGroupPersons", new[] { "PersonGroup_ID" });
            DropForeignKey("dbo.PersonGroupPersons", "Person_ID", "dbo.People");
            DropForeignKey("dbo.PersonGroupPersons", "PersonGroup_ID", "dbo.PersonGroups");
            DropTable("dbo.PersonGroupPersons");
            CreateIndex("dbo.PersonPersonGroups", "PersonGroup_ID");
            CreateIndex("dbo.PersonPersonGroups", "Person_ID");
            AddForeignKey("dbo.PersonPersonGroups", "PersonGroup_ID", "dbo.PersonGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PersonPersonGroups", "Person_ID", "dbo.People", "ID", cascadeDelete: true);
        }
    }
}
