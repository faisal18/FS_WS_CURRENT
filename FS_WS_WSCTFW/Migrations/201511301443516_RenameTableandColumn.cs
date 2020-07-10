namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTableandColumn : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WSWebService", newName: "WSRequests");
            //DropPrimaryKey("dbo.WSRequests");
            //AddColumn("dbo.WSRequests", "WSWebServiceRequestID", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.WSRequests", "WSWebServiceRequestID");
            //DropColumn("dbo.WSRequests", "WSWebServiceID");
            RenameColumn("WSRequests", "WSWebServiceID", "WSWebServiceRequestID");
        }
        
        public override void Down()
        {
            RenameColumn("WSRequests", "WSWebServiceRequestID", "WSWebServiceID");
            //AddColumn("dbo.WSRequests", "WSWebServiceID", c => c.Int(nullable: false, identity: true));
            //DropPrimaryKey("dbo.WSRequests");
            //DropColumn("dbo.WSRequests", "WSWebServiceRequestID");
            //AddPrimaryKey("dbo.WSRequests", "WSWebServiceID");
            RenameTable(name: "dbo.WSRequests", newName: "WSWebService");
        }
    }
}
