namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "FRP.Plan", newName: "AircraftPlan");
            DropForeignKey("FRP.PaymentNoticeLine", "InvoiceId", "FRP.Invoice");
            DropIndex("FRP.PaymentNoticeLine", new[] { "InvoiceId" });
            AddColumn("FRP.AirProgramming", "DocName", c => c.String());
            AddColumn("FRP.ApprovalDoc", "CaacExamineDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("FRP.ApprovalDoc", "NdrcExamineDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("FRP.CaacProgramming", "DocName", c => c.String());
            AddColumn("FRP.EnginePlan", "DocName", c => c.String());
            AddColumn("FRP.AircraftPlan", "DocName", c => c.String());
            AlterColumn("FRP.AirProgramming", "DocumentId", c => c.Guid(nullable: false));
            AlterColumn("FRP.CaacProgramming", "DocumentId", c => c.Guid(nullable: false));
            AlterColumn("FRP.EnginePlan", "DocumentId", c => c.Guid(nullable: false));
            AlterColumn("FRP.AircraftPlan", "DocumentId", c => c.Guid(nullable: false));
            DropColumn("FRP.ApprovalDoc", "ExamineDate");
        }
        
        public override void Down()
        {
            AddColumn("FRP.ApprovalDoc", "ExamineDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.AircraftPlan", "DocumentId", c => c.Guid());
            AlterColumn("FRP.EnginePlan", "DocumentId", c => c.Guid());
            AlterColumn("FRP.CaacProgramming", "DocumentId", c => c.Guid());
            AlterColumn("FRP.AirProgramming", "DocumentId", c => c.Guid());
            DropColumn("FRP.AircraftPlan", "DocName");
            DropColumn("FRP.EnginePlan", "DocName");
            DropColumn("FRP.CaacProgramming", "DocName");
            DropColumn("FRP.ApprovalDoc", "NdrcExamineDate");
            DropColumn("FRP.ApprovalDoc", "CaacExamineDate");
            DropColumn("FRP.AirProgramming", "DocName");
            CreateIndex("FRP.PaymentNoticeLine", "InvoiceId");
            AddForeignKey("FRP.PaymentNoticeLine", "InvoiceId", "FRP.Invoice", "ID");
            RenameTable(name: "FRP.AircraftPlan", newName: "Plan");
        }
    }
}
