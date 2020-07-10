namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WSRequestParametersTableAdded2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WSRequestParameters",
                c => new
                    {
                        WSRequestParametersID = c.Int(nullable: false, identity: true),
                        ParameterValue = c.String(nullable: false),
                        WSRequestID = c.Int(nullable: false),
                        WSMethodParametersID = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        WSRequestsModels_WSWebServiceRequestID = c.Int(),
                    })
                .PrimaryKey(t => t.WSRequestParametersID)
                .ForeignKey("dbo.WSMethodParameters", t => t.WSMethodParametersID, cascadeDelete: true)
                .ForeignKey("dbo.WSRequests", t => t.WSRequestsModels_WSWebServiceRequestID)
                .Index(t => t.WSMethodParametersID)
                .Index(t => t.WSRequestsModels_WSWebServiceRequestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WSRequestParameters", "WSRequestsModels_WSWebServiceRequestID", "dbo.WSRequests");
            DropForeignKey("dbo.WSRequestParameters", "WSMethodParametersID", "dbo.WSMethodParameters");
            DropIndex("dbo.WSRequestParameters", new[] { "WSRequestsModels_WSWebServiceRequestID" });
            DropIndex("dbo.WSRequestParameters", new[] { "WSMethodParametersID" });
            DropTable("dbo.WSRequestParameters");
        }
    }
}
