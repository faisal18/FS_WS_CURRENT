namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldBindingAddressWSMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSMethods", "BindingAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSMethods", "BindingAddress");
        }
    }
}
