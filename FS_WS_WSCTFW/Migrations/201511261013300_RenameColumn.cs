namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.WSWebService", "WSEnvID", c => c.Int(nullable: false));
            //DropColumn("dbo.WSWebService", "WSEnvironmentID");
            RenameColumn("dbo.WSWebService", "WSEnvironmentID", "WSEnvID");
        }
        
        public override void Down()
        {
            //    AddColumn("dbo.WSWebService", "WSEnvironmentID", c => c.Int(nullable: false));
            //    DropColumn("dbo.WSWebService", "WSEnvID");
            //
            RenameColumn("dbo.WSWebService", "WSEnvID", "WSEnvironmentID");
        }
    }
}
