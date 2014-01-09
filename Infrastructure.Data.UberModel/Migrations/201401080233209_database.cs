namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            AddColumn("FRP.OperationHistory", "Status", c => c.Int(nullable: false));
            AlterColumn("FRP.Aircraft", "ImportDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.OperationHistory", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("FRP.ChangePlan", "AircraftBusinessId");
            CreateIndex("FRP.OperationPlan", "OperationHistoryId");
            AddForeignKey("FRP.ChangePlan", "AircraftBusinessId", "FRP.AircraftBusiness", "ID");
            AddForeignKey("FRP.OperationPlan", "OperationHistoryId", "FRP.OperationHistory", "ID");
            DropColumn("FRP.ActionCategory", "NetIncrement");
        }
        
        public override void Down()
        {
            AddColumn("FRP.ActionCategory", "NetIncrement", c => c.Int(nullable: false));
            DropForeignKey("FRP.OperationPlan", "OperationHistoryId", "FRP.OperationHistory");
            DropForeignKey("FRP.ChangePlan", "AircraftBusinessId", "FRP.AircraftBusiness");
            DropIndex("FRP.OperationPlan", new[] { "OperationHistoryId" });
            DropIndex("FRP.ChangePlan", new[] { "AircraftBusinessId" });
            AlterColumn("FRP.OperationHistory", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.Aircraft", "ImportDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("FRP.OperationHistory", "Status");
        }
    }
}
