namespace FS_WS_WSCTFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WSEnvironment_PublicCheckbox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSEnvironments", "isPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSEnvironments", "isPublic");
        }
    }
}
