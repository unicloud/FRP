namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            AlterColumn("FRP.Document", "ID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("FRP.Document", "ID", c => c.Guid(nullable: false, identity: true));
        }
    }
}
