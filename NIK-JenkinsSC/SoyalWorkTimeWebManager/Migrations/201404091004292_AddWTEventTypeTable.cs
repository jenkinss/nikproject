namespace SoyalWorkTimeWebManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWTEventTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WTEventTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventTypeName = c.String(),
                        Prefix = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WTEventTypes");
        }
    }
}
