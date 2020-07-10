namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MOnitoringTables : DbMigration
    {
        public override void Up()
        {
           
            
            CreateTable(
                "dbo.Monitoring_ApplicationStatuses",
                c => new
                    {
                        Monitoring_ApplicationStatuses_ID = c.Int(nullable: false, identity: true),
                        ApplicationName = c.String(nullable: false),
                        Status = c.String(),
                        isUI = c.Boolean(nullable: false),
                        CheckingTime = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Monitoring_ApplicationStatuses_ID);
            
            CreateTable(
                "dbo.Monitoring_TransactionCount",
                c => new
                    {
                        Monitoring_TransactionCount_ID = c.Int(nullable: false, identity: true),
                        ApplicationName = c.String(nullable: false),
                        CheckingTime = c.DateTime(),
                        Count = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Monitoring_TransactionCount_ID);
            
        
            
        }
        
        public override void Down()
        {
         
            DropTable("dbo.Monitoring_TransactionCount");
            DropTable("dbo.Monitoring_ApplicationStatuses");
           
        }
    }
}
