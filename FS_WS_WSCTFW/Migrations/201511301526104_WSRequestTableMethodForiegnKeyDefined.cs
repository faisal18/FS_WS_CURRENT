namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WSRequestTableMethodForiegnKeyDefined : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSRequests", "WSMethodID", c => c.Int(nullable: false));
            AddColumn("dbo.WSRequests", "WSMethodsModels_WSMethodsID", c => c.Int());
            CreateIndex("dbo.WSRequests", "WSMethodsModels_WSMethodsID");
            AddForeignKey("dbo.WSRequests", "WSMethodsModels_WSMethodsID", "dbo.WSMethods", "WSMethodsID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WSRequests", "WSMethodsModels_WSMethodsID", "dbo.WSMethods");
            DropIndex("dbo.WSRequests", new[] { "WSMethodsModels_WSMethodsID" });
            DropColumn("dbo.WSRequests", "WSMethodsModels_WSMethodsID");
            DropColumn("dbo.WSRequests", "WSMethodID");
        }
    }
}
