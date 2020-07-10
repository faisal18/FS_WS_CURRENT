namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoapActionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSMethods", "SOAPAction", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSMethods", "SOAPAction");
        }
    }
}
