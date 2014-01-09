namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FRP.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PlannedStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PlannedEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Duration = c.Time(nullable: false, precision: 7),
                        DeadLine = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsMileStone = c.Boolean(nullable: false),
                        IsSummary = c.Boolean(nullable: false),
                        HasRisk = c.Boolean(nullable: false),
                        TimeZoneId = c.String(),
                        Note = c.String(),
                        ProjectId = c.Int(nullable: false),
                        TaskStandardId = c.Int(),
                        RelatedId = c.Int(),
                        ParentId = c.Int(),
                        Subject = c.String(),
                        Body = c.String(),
                        Importance = c.String(),
                        Tempo = c.String(),
                        Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        TaskStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Task", t => t.ParentId)
                .ForeignKey("FRP.Project", t => t.ProjectId)
                .Index(t => t.ParentId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "FRP.ProjectTemp",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.TaskTemp",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Start = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                        IsMileStone = c.Boolean(nullable: false),
                        IsSummary = c.Boolean(nullable: false),
                        TaskStandardId = c.Int(),
                        ParentId = c.Int(),
                        ProjectTempId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.TaskTemp", t => t.ParentId)
                .ForeignKey("FRP.ProjectTemp", t => t.ProjectTempId)
                .Index(t => t.ParentId)
                .Index(t => t.ProjectTempId);
            
            CreateTable(
                "FRP.TaskStandard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OptimisticTime = c.Time(nullable: false, precision: 7),
                        PessimisticTime = c.Time(nullable: false, precision: 7),
                        NormalTime = c.Time(nullable: false, precision: 7),
                        SourceGuid = c.Guid(nullable: false),
                        IsCustom = c.Boolean(nullable: false),
                        TaskType = c.Int(nullable: false),
                        WorkGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.WorkGroup", t => t.WorkGroupId)
                .Index(t => t.WorkGroupId);
            
            CreateTable(
                "FRP.TaskCase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        TaskStandardId = c.Int(nullable: false),
                        RelatedId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.TaskStandard", t => t.TaskStandardId)
                .Index(t => t.TaskStandardId);
            
            CreateTable(
                "FRP.WorkGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Member", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "FRP.Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsManager = c.Boolean(nullable: false),
                        WorkGroupId = c.Int(nullable: false),
                        MemberUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.WorkGroup", t => t.WorkGroupId)
                .Index(t => t.WorkGroupId);
            
            CreateTable(
                "FRP.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        FirstName = c.String(),
                        LaseName = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("FRP.OperationHistory", "Status", c => c.Int(nullable: false));
            AddColumn("FRP.ApprovalDoc", "CaacDocumentName", c => c.String());
            AddColumn("FRP.ApprovalDoc", "NdrcDocumentName", c => c.String());
            AddColumn("FRP.Trade", "TradeType", c => c.String());
            AlterColumn("FRP.Aircraft", "ImportDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.OperationHistory", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.AircraftPlan", "DocumentId", c => c.Guid());
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
            DropForeignKey("FRP.TaskStandard", "WorkGroupId", "FRP.WorkGroup");
            DropForeignKey("FRP.Member", "WorkGroupId", "FRP.WorkGroup");
            DropForeignKey("FRP.WorkGroup", "ManagerId", "FRP.Member");
            DropForeignKey("FRP.TaskCase", "TaskStandardId", "FRP.TaskStandard");
            DropForeignKey("FRP.TaskTemp", "ProjectTempId", "FRP.ProjectTemp");
            DropForeignKey("FRP.TaskTemp", "ParentId", "FRP.TaskTemp");
            DropForeignKey("FRP.Task", "ProjectId", "FRP.Project");
            DropForeignKey("FRP.Task", "ParentId", "FRP.Task");
            DropIndex("FRP.OperationPlan", new[] { "OperationHistoryId" });
            DropIndex("FRP.ChangePlan", new[] { "AircraftBusinessId" });
            DropIndex("FRP.TaskStandard", new[] { "WorkGroupId" });
            DropIndex("FRP.Member", new[] { "WorkGroupId" });
            DropIndex("FRP.WorkGroup", new[] { "ManagerId" });
            DropIndex("FRP.TaskCase", new[] { "TaskStandardId" });
            DropIndex("FRP.TaskTemp", new[] { "ProjectTempId" });
            DropIndex("FRP.TaskTemp", new[] { "ParentId" });
            DropIndex("FRP.Task", new[] { "ProjectId" });
            DropIndex("FRP.Task", new[] { "ParentId" });
            AlterColumn("FRP.AircraftPlan", "DocumentId", c => c.Guid(nullable: false));
            AlterColumn("FRP.OperationHistory", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.Aircraft", "ImportDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("FRP.Trade", "TradeType");
            DropColumn("FRP.ApprovalDoc", "NdrcDocumentName");
            DropColumn("FRP.ApprovalDoc", "CaacDocumentName");
            DropColumn("FRP.OperationHistory", "Status");
            DropTable("FRP.User");
            DropTable("FRP.Member");
            DropTable("FRP.WorkGroup");
            DropTable("FRP.TaskCase");
            DropTable("FRP.TaskStandard");
            DropTable("FRP.TaskTemp");
            DropTable("FRP.ProjectTemp");
            DropTable("FRP.Task");
            DropTable("FRP.Project");
        }
    }
}
