namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("FRP.BasicConfig", "BasicConfigGroupId", "FRP.BasicConfigGroup");
            DropIndex("FRP.BasicConfig", new[] { "BasicConfigGroupId" });
            CreateIndex("FRP.BasicConfig", "BasicConfigGroupId");
            AddForeignKey("FRP.BasicConfig", "BasicConfigGroupId", "FRP.BasicConfigGroup", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("FRP.BasicConfig", "BasicConfigGroupId", "FRP.BasicConfigGroup");
            DropIndex("FRP.BasicConfig", new[] { "BasicConfigGroupId" });
            CreateIndex("FRP.BasicConfig", "BasicConfigGroupId");
            AddForeignKey("FRP.BasicConfig", "BasicConfigGroupId", "FRP.BasicConfigGroup", "ID");
        }
    }
}
