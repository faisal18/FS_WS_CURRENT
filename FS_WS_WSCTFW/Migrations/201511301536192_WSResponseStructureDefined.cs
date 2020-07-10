namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WSResponseStructureDefined : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WSResponse",
                c => new
                    {
                        WSResponseID = c.Int(nullable: false, identity: true),
                        RequestDetails = c.String(nullable: false),
                        ResponseDetails = c.String(),
                        WSResponseHeaderStatusCode = c.Int(nullable: false),
                        WSResponseTime = c.Int(nullable: false),
                        WSResponseSize = c.Int(nullable: false),
                        WSRequestID = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        WSRequestsModels_WSWebServiceRequestID = c.Int(),
                    })
                .PrimaryKey(t => t.WSResponseID)
                .ForeignKey("dbo.WSRequests", t => t.WSRequestsModels_WSWebServiceRequestID)
                .Index(t => t.WSRequestsModels_WSWebServiceRequestID);
            
            AddColumn("dbo.WSRequests", "WSRequestSize", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WSResponse", "WSRequestsModels_WSWebServiceRequestID", "dbo.WSRequests");
            DropIndex("dbo.WSResponse", new[] { "WSRequestsModels_WSWebServiceRequestID" });
            DropColumn("dbo.WSRequests", "WSRequestSize");
            DropTable("dbo.WSResponse");
        }
    }
}
