namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FRP.AcDailyUtilization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegNumber = c.String(maxLength: 100),
                        CalculatedValue = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmendValue = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.CtrlUnit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CtrlStrategy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MaintainCtrlLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StandardInterval = c.String(maxLength: 100),
                        MaxInterval = c.String(maxLength: 100),
                        MinInterval = c.String(maxLength: 100),
                        CtrlUnitId = c.Int(nullable: false),
                        MaintainWorkId = c.Int(nullable: false),
                        MaintainCtrlId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCtrl", t => t.MaintainCtrlId)
                .Index(t => t.MaintainCtrlId);
            
            CreateTable(
                "FRP.MaintainWork",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkCode = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Mod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModNumber = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.PnReg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(maxLength: 100),
                        IsLife = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.ApplicableAircraft",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompleteDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cost = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ContractAircraftId = c.Int(nullable: false),
                        ScnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ContractAircraft", t => t.ContractAircraftId)
                .ForeignKey("FRP.Scn", t => t.ScnId)
                .Index(t => t.ContractAircraftId)
                .Index(t => t.ScnId);
            
            CreateTable(
                "FRP.SnReg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sn = c.String(maxLength: 100),
                        InstallDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Pn = c.String(maxLength: 100),
                        IsStop = c.Boolean(nullable: false),
                        PnRegId = c.Int(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                        LifeMonitorId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.LifeMonitor", t => t.LifeMonitorId)
                .Index(t => t.LifeMonitorId);
            
            CreateTable(
                "FRP.LifeMonitor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkCode = c.String(maxLength: 100),
                        Sn = c.String(maxLength: 100),
                        MointorStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LifeTimeLimit = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.SnHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sn = c.String(maxLength: 100),
                        InstallDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RemoveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FiNumber = c.String(maxLength: 100),
                        CSN = c.String(maxLength: 100),
                        CSR = c.String(maxLength: 100),
                        TSN = c.String(maxLength: 100),
                        TSR = c.String(maxLength: 100),
                        AircraftId = c.Guid(nullable: false),
                        SnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SnReg", t => t.SnId)
                .Index(t => t.SnId);
            
            CreateTable(
                "FRP.TechnicalSolution",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FiNumber = c.String(maxLength: 100),
                        TsNumber = c.String(maxLength: 100),
                        Position = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.TsLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        TsNumber = c.String(maxLength: 100),
                        TsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.TechnicalSolution", t => t.TsId)
                .Index(t => t.TsId);
            
            CreateTable(
                "FRP.Dependency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(maxLength: 100),
                        PnRegId = c.Int(nullable: false),
                        TsLineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.TsLine", t => t.TsLineId)
                .Index(t => t.TsLineId);
            
            CreateTable(
                "FRP.ItemMaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ItemNo = c.String(maxLength: 100),
                        AcConfigId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCtrl", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PnMaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Pn = c.String(maxLength: 100),
                        PnRegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCtrl", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.SnMaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SnScope = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCtrl", t => t.ID)
                .Index(t => t.ID);
            
            AddColumn("FRP.Scn", "CheckDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("FRP.Scn", "CSCNumber", c => c.String(maxLength: 100));
            AddColumn("FRP.Scn", "ModNumber", c => c.String(maxLength: 100));
            AddColumn("FRP.Scn", "TsNumber", c => c.String(maxLength: 100));
            AddColumn("FRP.Scn", "Cost", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("FRP.Scn", "ScnNumber", c => c.String(maxLength: 100));
            AddColumn("FRP.Scn", "ScnType", c => c.Int(nullable: false));
            AddColumn("FRP.Scn", "Description", c => c.String(maxLength: 100));
            AddColumn("FRP.Scn", "ScnDocumentId", c => c.Guid(nullable: false));
            DropTable("FRP.MScn");
            DropTable("FRP.MScnLine");
            DropTable("FRP.ScnLine");
        }
        
        public override void Down()
        {
            CreateTable(
                "FRP.ScnLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MScnLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MScn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("FRP.SnMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.PnMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.ItemMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.TsLine", "TsId", "FRP.TechnicalSolution");
            DropForeignKey("FRP.Dependency", "TsLineId", "FRP.TsLine");
            DropForeignKey("FRP.SnHistory", "SnId", "FRP.SnReg");
            DropForeignKey("FRP.SnReg", "LifeMonitorId", "FRP.LifeMonitor");
            DropForeignKey("FRP.ApplicableAircraft", "ScnId", "FRP.Scn");
            DropForeignKey("FRP.ApplicableAircraft", "ContractAircraftId", "FRP.ContractAircraft");
            DropForeignKey("FRP.MaintainCtrlLine", "MaintainCtrlId", "FRP.MaintainCtrl");
            DropIndex("FRP.SnMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.PnMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.ItemMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.TsLine", new[] { "TsId" });
            DropIndex("FRP.Dependency", new[] { "TsLineId" });
            DropIndex("FRP.SnHistory", new[] { "SnId" });
            DropIndex("FRP.SnReg", new[] { "LifeMonitorId" });
            DropIndex("FRP.ApplicableAircraft", new[] { "ScnId" });
            DropIndex("FRP.ApplicableAircraft", new[] { "ContractAircraftId" });
            DropIndex("FRP.MaintainCtrlLine", new[] { "MaintainCtrlId" });
            DropColumn("FRP.Scn", "ScnDocumentId");
            DropColumn("FRP.Scn", "Description");
            DropColumn("FRP.Scn", "ScnType");
            DropColumn("FRP.Scn", "ScnNumber");
            DropColumn("FRP.Scn", "Cost");
            DropColumn("FRP.Scn", "TsNumber");
            DropColumn("FRP.Scn", "ModNumber");
            DropColumn("FRP.Scn", "CSCNumber");
            DropColumn("FRP.Scn", "CheckDate");
            DropTable("FRP.SnMaintainCtrl");
            DropTable("FRP.PnMaintainCtrl");
            DropTable("FRP.ItemMaintainCtrl");
            DropTable("FRP.Dependency");
            DropTable("FRP.TsLine");
            DropTable("FRP.TechnicalSolution");
            DropTable("FRP.SnHistory");
            DropTable("FRP.LifeMonitor");
            DropTable("FRP.SnReg");
            DropTable("FRP.ApplicableAircraft");
            DropTable("FRP.PnReg");
            DropTable("FRP.Mod");
            DropTable("FRP.MaintainWork");
            DropTable("FRP.MaintainCtrlLine");
            DropTable("FRP.MaintainCtrl");
            DropTable("FRP.CtrlUnit");
            DropTable("FRP.AcDailyUtilization");
        }
    }
}
