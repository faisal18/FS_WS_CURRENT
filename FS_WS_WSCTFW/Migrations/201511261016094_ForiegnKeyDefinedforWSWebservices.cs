namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForiegnKeyDefinedforWSWebservices : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WSWebService", "WSEnvID");
            AddForeignKey("dbo.WSWebService", "WSEnvID", "dbo.WSEnvironments", "WSEnvID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WSWebService", "WSEnvID", "dbo.WSEnvironments");
            DropIndex("dbo.WSWebService", new[] { "WSEnvID" });
        }
    }
}
