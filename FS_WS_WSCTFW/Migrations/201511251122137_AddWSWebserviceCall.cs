namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWSWebserviceCall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WSWebService",
                c => new
                    {
                        WSWebServiceID = c.Int(nullable: false, identity: true),
                        WSEnvironmentID = c.Int(nullable: false),
                        RequestDetails = c.String(nullable: false),
                        ResponseDetails = c.String(),
                        isActive = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.WSWebServiceID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WSWebService");
        }
    }
}
