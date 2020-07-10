namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WSParametersAddedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSMethodParameters", "ParameterDataType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSMethodParameters", "ParameterDataType");
        }
    }
}
