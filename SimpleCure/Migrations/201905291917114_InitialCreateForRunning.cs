namespace SimpleCure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateForRunning : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.EditBusinessType_ViewModel");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EditBusinessType_ViewModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EditType = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
