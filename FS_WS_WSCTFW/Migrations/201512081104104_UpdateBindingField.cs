namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBindingField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSMethods", "Binding", c => c.String());
            DropColumn("dbo.WSMethods", "MethodaType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WSMethods", "MethodaType", c => c.String());
            DropColumn("dbo.WSMethods", "Binding");
        }
    }
}
