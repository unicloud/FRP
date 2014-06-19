namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FRP.AcDailyUtilization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegNumber = c.String(maxLength: 100),
                        CalculatedHour = c.Decimal(nullable: false, precision: 16, scale: 4),
                        CalculatedCycle = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmendHour = c.Decimal(nullable: false, precision: 16, scale: 4),
                        AmendCycle = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.ActionCategory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActionType = c.String(),
                        ActionName = c.String(),
                        NeedRequest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AdSb",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AircraftSeries = c.String(),
                        FileType = c.String(),
                        FileNo = c.String(),
                        FileVersion = c.String(),
                        ComplyAircraft = c.String(),
                        ComplyStatus = c.String(),
                        ComplyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ComplyNotes = c.String(),
                        ComplyClose = c.String(),
                        ComplyFee = c.Decimal(precision: 16, scale: 4),
                        ComplyFeeNotes = c.String(),
                        ComplyFeeCurrency = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AirBusScn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CSCNumber = c.String(maxLength: 100),
                        ModNumber = c.String(maxLength: 100),
                        ScnNumber = c.String(maxLength: 100),
                        ScnStatus = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftCabin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SeatNumber = c.Int(nullable: false),
                        Note = c.String(),
                        AircraftConfiguratonId = c.Int(nullable: false),
                        AircraftCabinTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCabinType", t => t.AircraftCabinTypeId)
                .ForeignKey("FRP.AircraftConfiguration", t => t.AircraftConfiguratonId)
                .Index(t => t.AircraftCabinTypeId)
                .Index(t => t.AircraftConfiguratonId);
            
            CreateTable(
                "FRP.AircraftCabinType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftCategory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Category = c.String(),
                        Regional = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftConfiguration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConfigCode = c.String(),
                        Description = c.String(),
                        MTW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BWF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BWI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BEW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MTOW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MLW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MZFW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MMFW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MCC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FileContent = c.Binary(),
                        FileName = c.String(),
                        AircraftTypeId = c.Guid(nullable: false),
                        AircraftSeriesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftSeries", t => t.AircraftSeriesId)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .Index(t => t.AircraftSeriesId)
                .Index(t => t.AircraftTypeId);
            
            CreateTable(
                "FRP.AircraftSeries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ManufacturerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "FRP.Ata",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ATA = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(nullable: false),
                        AircraftSeriesId = c.Guid(nullable: false),
                        ParentAta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Ata", t => t.ParentId)
                .ForeignKey("FRP.Ata", t => t.ParentAta_Id)
                .ForeignKey("FRP.AircraftSeries", t => t.AircraftSeriesId)
                .Index(t => t.ParentId)
                .Index(t => t.ParentAta_Id)
                .Index(t => t.AircraftSeriesId);
            
            CreateTable(
                "FRP.Manufacturer",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CnName = c.String(),
                        EnName = c.String(),
                        CnShortName = c.String(),
                        EnShortName = c.String(),
                        Note = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        AircraftSeriesId = c.Guid(nullable: false),
                        AircraftCategoryId = c.Guid(nullable: false),
                        ManufacturerId = c.Guid(nullable: false),
                        CaacAircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCategory", t => t.AircraftCategoryId)
                .ForeignKey("FRP.AircraftSeries", t => t.AircraftSeriesId)
                .ForeignKey("FRP.CAACAircraftType", t => t.CaacAircraftTypeId)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.AircraftCategoryId)
                .Index(t => t.AircraftSeriesId)
                .Index(t => t.CaacAircraftTypeId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "FRP.CAACAircraftType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        AircraftSeriesId = c.Guid(nullable: false),
                        AircraftCategoryId = c.Guid(nullable: false),
                        ManufacturerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCategory", t => t.AircraftCategoryId)
                .ForeignKey("FRP.AircraftSeries", t => t.AircraftSeriesId)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.AircraftCategoryId)
                .Index(t => t.AircraftSeriesId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "FRP.AircraftLicense",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IssuedUnit = c.String(),
                        IssuedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ValidMonths = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        State = c.Int(nullable: false),
                        FileContent = c.Binary(),
                        FileName = c.String(),
                        AircraftId = c.Guid(nullable: false),
                        LicenseTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.LicenseType", t => t.LicenseTypeId)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.LicenseType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                        HasFile = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AnnualMaintainPlan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnnualId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftMaintainPlanDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AircraftNumber = c.String(),
                        AircraftType = c.String(),
                        Level = c.String(),
                        InDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OutDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cycle = c.Int(nullable: false),
                        AircraftMaintainPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftMaintainPlan", t => t.AircraftMaintainPlanId)
                .Index(t => t.AircraftMaintainPlanId);
            
            CreateTable(
                "FRP.Aircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegNumber = c.String(),
                        SerialNumber = c.String(),
                        IsOperation = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FactoryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ExportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SeatingCapacity = c.Int(nullable: false),
                        CarryingCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierId = c.Int(),
                        AircraftTypeId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.AcConfigHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AircraftId = c.Guid(nullable: false),
                        AircraftConfigurationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.AircraftBusiness",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SeatingCapacity = c.Int(nullable: false),
                        CarryingCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.Airlines",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CnName = c.String(),
                        EnName = c.String(),
                        CnShortName = c.String(),
                        EnShortName = c.String(),
                        ICAOCode = c.String(),
                        IATACode = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.OperationHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegNumber = c.String(),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        StopDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TechReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TechDeliveryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OnHireDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                        ExportCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ApprovalHistory", t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ExportCategoryId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ID)
                .Index(t => t.ExportCategoryId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.ApprovalHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        SeatingCapacity = c.Int(nullable: false),
                        CarryingCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequestDeliverMonth = c.Int(nullable: false),
                        Note = c.String(),
                        RequestId = c.Guid(nullable: false),
                        PlanAircraftId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                        RequestDeliverAnnualId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftId)
                .ForeignKey("FRP.Annual", t => t.RequestDeliverAnnualId)
                .ForeignKey("FRP.Request", t => t.RequestId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PlanAircraftId)
                .Index(t => t.RequestDeliverAnnualId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "FRP.PlanAircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IsLock = c.Boolean(nullable: false),
                        IsOwn = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        AircraftId = c.Guid(),
                        AircraftTypeId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .Index(t => t.AircraftId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.AirlinesId);
            
            CreateTable(
                "FRP.Annual",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                        IsOpen = c.Boolean(nullable: false),
                        ProgrammingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Programming", t => t.ProgrammingId)
                .Index(t => t.ProgrammingId);
            
            CreateTable(
                "FRP.Programming",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.OwnershipHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.SupplierId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SupplierType = c.Int(nullable: false),
                        Code = c.String(),
                        CnName = c.String(),
                        EnName = c.String(),
                        CnShortName = c.String(),
                        EnShortName = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        Note = c.String(),
                        AirlineGuid = c.Guid(),
                        SupplierCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierCompany", t => t.SupplierCompanyId)
                .Index(t => t.SupplierCompanyId);
            
            CreateTable(
                "FRP.BankAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Name = c.String(),
                        Bank = c.String(),
                        Branch = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                        CustCode = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.SupplierCompany",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        LinkmanId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.SupplierCompanyMaterial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        SupplierCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.MaterialId)
                .ForeignKey("FRP.SupplierCompany", t => t.SupplierCompanyId)
                .Index(t => t.MaterialId)
                .Index(t => t.SupplierCompanyId);
            
            CreateTable(
                "FRP.Material",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ManufacturerID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerID)
                .Index(t => t.ManufacturerID);
            
            CreateTable(
                "FRP.AirProgramming",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IssuedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                        DocName = c.String(),
                        ProgrammingId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        IssuedUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.IssuedUnit", t => t.IssuedUnitId)
                .ForeignKey("FRP.Programming", t => t.ProgrammingId)
                .Index(t => t.IssuedUnitId)
                .Index(t => t.ProgrammingId);
            
            CreateTable(
                "FRP.AirProgrammingLine",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                        BuyNum = c.Int(nullable: false),
                        ExportNum = c.Int(nullable: false),
                        LeaseNum = c.Int(nullable: false),
                        AircraftSeriesId = c.Guid(nullable: false),
                        AirProgrammingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftSeries", t => t.AircraftSeriesId)
                .ForeignKey("FRP.AirProgramming", t => t.AirProgrammingId)
                .Index(t => t.AircraftSeriesId)
                .Index(t => t.AirProgrammingId);
            
            CreateTable(
                "FRP.IssuedUnit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CnName = c.String(),
                        CnShortName = c.String(),
                        IsInner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AirStructureDamage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AircraftReg = c.String(),
                        AircraftType = c.String(),
                        AircraftSeries = c.String(),
                        ReportNo = c.String(),
                        ReportType = c.Int(nullable: false),
                        AuditTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Description = c.String(),
                        TotalCost = c.Decimal(nullable: false, precision: 16, scale: 4),
                        IsDefer = c.Boolean(nullable: false),
                        TecAssess = c.String(),
                        TreatResult = c.String(),
                        Status = c.Int(nullable: false),
                        CloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Source = c.String(),
                        Level = c.Int(nullable: false),
                        RepairDeadline = c.String(),
                        DocumentName = c.String(),
                        AircraftId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.AircraftId);
            
            CreateTable(
                "FRP.ApprovalDoc",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CaacExamineDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        NdrcExamineDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CaacApprovalNumber = c.String(),
                        NdrcApprovalNumber = c.String(),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                        CaacDocumentName = c.String(),
                        NdrcDocumentName = c.String(),
                        DispatchUnitId = c.Guid(nullable: false),
                        CaacDocumentId = c.Guid(nullable: false),
                        NdrcDocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manager", t => t.DispatchUnitId)
                .Index(t => t.DispatchUnitId);
            
            CreateTable(
                "FRP.Manager",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CnName = c.String(),
                        EnName = c.String(),
                        CnShortName = c.String(),
                        EnShortName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Invoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceType = c.Int(nullable: false),
                        InvoiceNumber = c.String(),
                        InvoideCode = c.String(),
                        InvoiceDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SupplierName = c.String(),
                        InvoiceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OperatorName = c.String(),
                        Reviewer = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReviewDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        OrderId = c.Int(),
                        SupplierId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        PaymentScheduleLineId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.PaymentScheduleLine", t => t.PaymentScheduleLineId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.CurrencyId)
                .Index(t => t.PaymentScheduleLineId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.Currency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CnName = c.String(),
                        EnName = c.String(),
                        Symbol = c.String(),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.InvoiceLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.PaymentScheduleLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ScheduleDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        PaymentScheduleId = c.Int(nullable: false),
                        InvoiceId = c.Int(),
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
                .ForeignKey("FRP.Invoice", t => t.InvoiceId)
                .ForeignKey("FRP.PaymentSchedule", t => t.PaymentScheduleId)
                .Index(t => t.InvoiceId)
                .Index(t => t.PaymentScheduleId);
            
            CreateTable(
                "FRP.BasicConfigGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        GroupNo = c.String(maxLength: 100),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .Index(t => t.AircraftTypeId);
            
            CreateTable(
                "FRP.BasicConfigHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ContractAircraftId = c.Int(nullable: false),
                        BasicConfigGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasicConfigGroup", t => t.BasicConfigGroupId)
                .Index(t => t.BasicConfigGroupId);
            
            CreateTable(
                "FRP.AcConfig",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Position = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        ItemId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        RootId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AcConfig", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "FRP.BusinessLicense",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IssuedUnit = c.String(),
                        IssuedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ValidMonths = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        State = c.Int(nullable: false),
                        FileContent = c.Binary(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.CaacProgramming",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IssuedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocNumber = c.String(),
                        Note = c.String(),
                        DocName = c.String(),
                        ProgrammingId = c.Guid(nullable: false),
                        IssuedUnitId = c.Int(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.IssuedUnit", t => t.IssuedUnitId)
                .ForeignKey("FRP.Programming", t => t.ProgrammingId)
                .Index(t => t.IssuedUnitId)
                .Index(t => t.ProgrammingId);
            
            CreateTable(
                "FRP.CaacProgrammingLine",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        AircraftCategoryId = c.Guid(nullable: false),
                        CaacProgrammingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCategory", t => t.AircraftCategoryId)
                .ForeignKey("FRP.CaacProgramming", t => t.CaacProgrammingId)
                .Index(t => t.AircraftCategoryId)
                .Index(t => t.CaacProgrammingId);
            
            CreateTable(
                "FRP.ContractAircraftBFE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContractAircraftId = c.Int(nullable: false),
                        BFEPurchaseOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BFEPurchaseOrder", t => t.BFEPurchaseOrderId)
                .ForeignKey("FRP.ContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.BFEPurchaseOrderId)
                .Index(t => t.ContractAircraftId);
            
            CreateTable(
                "FRP.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Version = c.Int(nullable: false),
                        ContractNumber = c.String(),
                        Name = c.String(),
                        OperatorName = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrderDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RepealDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        ContractName = c.String(),
                        ContractDocGuid = c.Guid(nullable: false),
                        SourceGuid = c.Guid(nullable: false),
                        Note = c.String(),
                        TradeId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        LinkmanId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Linkman", t => t.LinkmanId)
                .ForeignKey("FRP.Trade", t => t.TradeId)
                .Index(t => t.CurrencyId)
                .Index(t => t.LinkmanId)
                .Index(t => t.TradeId);
            
            CreateTable(
                "FRP.ContractContent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContentTags = c.String(),
                        Description = c.String(),
                        ContentDoc = c.Binary(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "FRP.Forwarder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CnName = c.String(),
                        EnName = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        Tel = c.String(),
                        Fax = c.String(),
                        Attn = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Linkman",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        TelePhone = c.String(),
                        Mobile = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Department = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        SourceId = c.Guid(nullable: false),
                        CustCode = c.String(),
                        Note = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.OrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstimateDeliveryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsCompleted = c.Boolean(nullable: false),
                        Note = c.String(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "FRP.ContractAircraft",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContractName = c.String(),
                        ContractNumber = c.String(),
                        RankNumber = c.String(),
                        CSCNumber = c.String(),
                        SerialNumber = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        ReceivedAmount = c.Int(nullable: false),
                        AcceptedAmount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                        PlanAircraftID = c.Guid(),
                        ImportCategoryId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PlanAircraftID)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.ContractEngine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContractName = c.String(),
                        ContractNumber = c.String(),
                        RankNumber = c.String(),
                        SerialNumber = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        ReceivedAmount = c.Int(nullable: false),
                        AcceptedAmount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        PartID = c.Int(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Part", t => t.PartID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PartID)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.Part",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Trade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TradeType = c.String(),
                        TradeNumber = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsClosed = c.Boolean(nullable: false),
                        CloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        Signatory = c.String(),
                        Note = c.String(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
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
                "FRP.DocumentPath",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsLeaf = c.Boolean(nullable: false),
                        Extension = c.String(),
                        DocumentGuid = c.Guid(),
                        Path = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.DocumentPath", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "FRP.DocumentType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Document",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FileStorage = c.Binary(),
                        FileContent = c.String(),
                        Abstract = c.String(),
                        Note = c.String(),
                        Uploader = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        DocumentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.DocumentType", t => t.DocumentTypeId)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "FRP.EngineMaintainPlanDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EngineNumber = c.String(),
                        InMaintainDate = c.String(),
                        OutMaintainDate = c.String(),
                        MaintainLevel = c.String(),
                        ChangeLlpNumber = c.Int(nullable: false),
                        TsnCsn = c.String(),
                        TsrCsr = c.String(),
                        NonFhaFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PartFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeLlpFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomsTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FreightFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        FeeLittleSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeeTotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetToalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EngineMaintainPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.EngineMaintainPlan", t => t.EngineMaintainPlanId)
                .Index(t => t.EngineMaintainPlanId);
            
            CreateTable(
                "FRP.EnginePlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        DocNumber = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        VersionNumber = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                        DocName = c.String(),
                        AirlinesId = c.Guid(nullable: false),
                        AnnualId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.Annual", t => t.AnnualId)
                .Index(t => t.AirlinesId)
                .Index(t => t.AnnualId);
            
            CreateTable(
                "FRP.EnginePlanHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PerformMonth = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        MaxThrust = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                        EngineTypeId = c.Guid(nullable: false),
                        EnginePlanId = c.Guid(nullable: false),
                        PerformAnnualId = c.Guid(nullable: false),
                        PlanEngineId = c.Guid(),
                        ActionCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ActionCategoryId)
                .ForeignKey("FRP.EngineType", t => t.EngineTypeId)
                .ForeignKey("FRP.Annual", t => t.PerformAnnualId)
                .ForeignKey("FRP.PlanEngine", t => t.PlanEngineId)
                .ForeignKey("FRP.EnginePlan", t => t.EnginePlanId)
                .Index(t => t.ActionCategoryId)
                .Index(t => t.EngineTypeId)
                .Index(t => t.PerformAnnualId)
                .Index(t => t.PlanEngineId)
                .Index(t => t.EnginePlanId);
            
            CreateTable(
                "FRP.EngineType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        ManufacturerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "FRP.PlanEngine",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EngineId = c.Guid(),
                        EngineTypeId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.Engine", t => t.EngineId)
                .ForeignKey("FRP.EngineType", t => t.EngineTypeId)
                .Index(t => t.AirlinesId)
                .Index(t => t.EngineId)
                .Index(t => t.EngineTypeId);
            
            CreateTable(
                "FRP.Engine",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FactoryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ExportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SerialNumber = c.String(),
                        MaxThrust = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EngineTypeId = c.Guid(nullable: false),
                        SupplierId = c.Int(),
                        AirlinesId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.EngineType", t => t.EngineTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.AirlinesId)
                .Index(t => t.EngineTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.EngineBusinessHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MaxThrust = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EngineId = c.Guid(nullable: false),
                        EngineTypeId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.EngineType", t => t.EngineTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Engine", t => t.EngineId)
                .Index(t => t.EngineTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.EngineId);
            
            CreateTable(
                "FRP.EngineOwnershipHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EngineId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .ForeignKey("FRP.Engine", t => t.EngineId)
                .Index(t => t.SupplierId)
                .Index(t => t.EngineId);
            
            CreateTable(
                "FRP.FlightLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FlightNum = c.String(),
                        LogNo = c.String(),
                        LegNo = c.String(),
                        AcReg = c.String(),
                        MSN = c.String(),
                        FlightType = c.String(),
                        FlightDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BlockOn = c.String(),
                        TakeOff = c.String(),
                        Landing = c.String(),
                        BlockStop = c.String(),
                        TotalFH = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalBH = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FlightHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BlockHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCycles = c.Int(nullable: false),
                        Cycle = c.Int(nullable: false),
                        DepartureAirport = c.String(),
                        ArrivalAirport = c.String(),
                        ToGoNumber = c.Int(nullable: false),
                        ApuCycle = c.Int(nullable: false),
                        ApuMM = c.Int(nullable: false),
                        ENG1OilDep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG1OilArr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG2OilDep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG2OilArr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG3OilDep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG3OilArr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG4OilDep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENG4OilArr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApuOilDep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApuOilArr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.FunctionItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentItemId = c.Int(),
                        IsLeaf = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        Description = c.String(),
                        NaviUrl = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ImageUrl = c.String(maxLength: 100),
                        IsButton = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.FunctionItem", t => t.ParentItemId)
                .Index(t => t.ParentItemId);
            
            CreateTable(
                "FRP.Guarantee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierName = c.String(),
                        OperatorName = c.String(),
                        Reviewer = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReviewDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.CurrencyId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.InstallController",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ItemId = c.Int(nullable: false),
                        PnRegId = c.Int(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.Item", t => t.ItemId)
                .ForeignKey("FRP.PnReg", t => t.PnRegId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.ItemId)
                .Index(t => t.PnRegId);
            
            CreateTable(
                "FRP.Dependency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(maxLength: 100),
                        InstallControllerId = c.Int(nullable: false),
                        DependencyPnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.InstallController", t => t.InstallControllerId)
                .Index(t => t.InstallControllerId);
            
            CreateTable(
                "FRP.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ItemNo = c.String(),
                        FiNumber = c.String(),
                        IsLife = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.PnReg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(maxLength: 100),
                        IsLife = c.Boolean(nullable: false),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MailAddress",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SmtpHost = c.String(),
                        Pop3Host = c.String(),
                        SendPort = c.Int(nullable: false),
                        ReceivePort = c.Int(nullable: false),
                        LoginUser = c.String(),
                        LoginPassword = c.String(),
                        Address = c.String(),
                        DisplayName = c.String(),
                        SendSSL = c.Boolean(nullable: false),
                        StartTLS = c.Boolean(nullable: false),
                        ReceiveSSL = c.Boolean(nullable: false),
                        ServerType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.MaintainContract",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Signatory = c.String(),
                        SignDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Abstract = c.String(),
                        DocumentName = c.String(),
                        SignatoryId = c.Int(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SignatoryId)
                .Index(t => t.SignatoryId);
            
            CreateTable(
                "FRP.MaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        InMaintainTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OutMaintainTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TotalDays = c.Int(nullable: false),
                        DepartmentDeclareAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinancialApprovalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinancialApprovalAmountNonTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaintainInvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.MaintainInvoiceId)
                .Index(t => t.MaintainInvoiceId);
            
            CreateTable(
                "FRP.MaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CtrlStrategy = c.Int(nullable: false),
                        Description = c.String(),
                        CtrlDetail = c.String(storeType: "xml"),
                        MaintainWorkId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainWork", t => t.MaintainWorkId)
                .Index(t => t.MaintainWorkId);
            
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
                "FRP.OilMonitor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TSN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TSR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IntervalRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeltaIntervalRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AverageRate3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AverageRate7 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SnRegID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.OrganizationRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Organization", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "FRP.Organization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        LastUpdateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        ParentCode = c.Int(),
                        Sort = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Organization", t => t.ParentCode)
                .Index(t => t.ParentCode);
            
            CreateTable(
                "FRP.OrganizationUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.PaymentNotice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoticeNumber = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeadLine = c.DateTime(nullable: false),
                        SupplierName = c.String(),
                        OperatorName = c.String(),
                        Reviewer = c.String(),
                        ReviewDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BankAccount", t => t.BankAccountId)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.BankAccountId)
                .Index(t => t.CurrencyId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.PaymentNoticeLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceType = c.Int(nullable: false),
                        InvoiceNumber = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        PaymentNoticeId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentNotice", t => t.PaymentNoticeId)
                .Index(t => t.PaymentNoticeId);
            
            CreateTable(
                "FRP.PaymentSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SupplierName = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.CurrencyId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.AircraftPlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        VersionNumber = c.Int(nullable: false),
                        IsCurrentVersion = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DocNumber = c.String(),
                        IsFinished = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        PublishStatus = c.Int(nullable: false),
                        DocName = c.String(),
                        AirlinesId = c.Guid(nullable: false),
                        AnnualId = c.Guid(nullable: false),
                        DocumentId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.Annual", t => t.AnnualId)
                .Index(t => t.AirlinesId)
                .Index(t => t.AnnualId);
            
            CreateTable(
                "FRP.ProgrammingFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IssuedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocNumber = c.String(),
                        DocName = c.String(),
                        Type = c.Int(nullable: false),
                        IssuedUnitId = c.Int(nullable: false),
                        ProgrammingId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.IssuedUnit", t => t.IssuedUnitId)
                .ForeignKey("FRP.Programming", t => t.ProgrammingId)
                .Index(t => t.IssuedUnitId)
                .Index(t => t.ProgrammingId);
            
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
                .ForeignKey("FRP.Project", t => t.ProjectId, cascadeDelete: true)
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
                .ForeignKey("FRP.ProjectTemp", t => t.ProjectTempId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.ProjectTempId);
            
            CreateTable(
                "FRP.ReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReceivedAmount = c.Int(nullable: false),
                        AcceptedAmount = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Note = c.String(),
                        DeliverDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeliverPlace = c.String(),
                        ReceptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Reception", t => t.ReceptionId)
                .Index(t => t.ReceptionId);
            
            CreateTable(
                "FRP.ReceptionSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Group = c.String(),
                        ReceptionId = c.Int(nullable: false),
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
                .ForeignKey("FRP.Reception", t => t.ReceptionId)
                .Index(t => t.ReceptionId);
            
            CreateTable(
                "FRP.Reception",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReceptionNumber = c.String(),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsClosed = c.Boolean(nullable: false),
                        CloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        SourceId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "FRP.RelatedDoc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SourceId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        DocumentName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Request",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SubmitDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsFinished = c.Boolean(nullable: false),
                        Title = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CaacDocNumber = c.String(),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                        CaacDocumentName = c.String(),
                        ApprovalDocId = c.Guid(),
                        CaacDocumentId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ApprovalDoc", t => t.ApprovalDocId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ApprovalDocId);
            
            CreateTable(
                "FRP.RoleFunction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FunctionItemId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.FunctionItem", t => t.FunctionItemId)
                .ForeignKey("FRP.Role", t => t.RoleId)
                .Index(t => t.FunctionItemId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "FRP.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LevelCode = c.String(maxLength: 100),
                        Code = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Scn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CheckDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CSCNumber = c.String(maxLength: 100),
                        ModNumber = c.String(maxLength: 100),
                        RfcNumber = c.String(maxLength: 100),
                        ValidDate = c.String(maxLength: 100),
                        Cost = c.Decimal(nullable: false, precision: 16, scale: 4),
                        ScnNumber = c.String(maxLength: 100),
                        Type = c.Int(nullable: false),
                        ScnStatus = c.Int(nullable: false),
                        ScnType = c.Int(nullable: false),
                        Description = c.String(),
                        ScnDocName = c.String(),
                        ReceiveDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AuditOrganization = c.String(),
                        Auditor = c.String(),
                        AuditTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        AuditNotes = c.String(),
                        AuditHistory = c.String(),
                        ScnDocumentId = c.Guid(nullable: false),
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
                "FRP.SnHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sn = c.String(maxLength: 100),
                        Pn = c.String(maxLength: 100),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ActionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Position = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        CSN = c.Int(nullable: false),
                        TSN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CSN2 = c.Int(nullable: false),
                        TSN2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        AircraftId = c.Guid(nullable: false),
                        SnRegId = c.Int(nullable: false),
                        PnRegId = c.Int(nullable: false),
                        RemInstRecordId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SnRemInstRecord", t => t.RemInstRecordId)
                .Index(t => t.RemInstRecordId);
            
            CreateTable(
                "FRP.SnRemInstRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionNo = c.String(maxLength: 100),
                        ActionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ActionType = c.Int(nullable: false),
                        Reason = c.String(maxLength: 100),
                        AircraftId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.SnReg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sn = c.String(maxLength: 100),
                        AllSnName = c.String(),
                        InstallDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Pn = c.String(maxLength: 100),
                        AllPnName = c.String(),
                        IsStop = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RegNumber = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        IsLife = c.Boolean(nullable: false),
                        IsLifeCst = c.Boolean(nullable: false),
                        TimeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CycleRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PnRegId = c.Int(nullable: false),
                        AircraftId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.LifeMonitor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkDescription = c.String(maxLength: 100),
                        MointorStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MointorEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SnRegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SnReg", t => t.SnRegId)
                .Index(t => t.SnRegId);
            
            CreateTable(
                "FRP.Thrust",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.SupplierRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsValid = c.Boolean(nullable: false),
                        SupplierCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierCompany", t => t.SupplierCompanyId)
                .Index(t => t.SupplierCompanyId);
            
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
                .ForeignKey("FRP.TaskStandard", t => t.TaskStandardId, cascadeDelete: true)
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
                .ForeignKey("FRP.WorkGroup", t => t.WorkGroupId, cascadeDelete: true)
                .Index(t => t.WorkGroupId);
            
            CreateTable(
                "FRP.Threshold",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotalThreshold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IntervalThreshold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeltaIntervalThreshold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Average3Threshold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Average7Threshold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PnRegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "FRP.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        LoweredUserName = c.String(maxLength: 100),
                        OrganizationNo = c.String(),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        DisplayName = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        PasswordFormat = c.Int(nullable: false),
                        PasswordQuestion = c.String(maxLength: 100),
                        PasswordAnswer = c.String(maxLength: 100),
                        Email = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Comment = c.String(maxLength: 200),
                        IsApproved = c.Boolean(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        FailedPasswordAnswerAttemptCount = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastPasswordChangedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastLockoutDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FailedPasswordAttemptWindowStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FailedPasswordAnswerAttemptWindowStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastActivityDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastLoginDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.XmlConfig",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ConfigType = c.String(),
                        ConfigContent = c.String(storeType: "xml"),
                        VersionNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.XmlSetting",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SettingType = c.String(),
                        SettingContent = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.PlanHistory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SeatingCapacity = c.Int(nullable: false),
                        CarryingCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerformMonth = c.Int(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        IsSubmit = c.Boolean(nullable: false),
                        Note = c.String(),
                        CanRequest = c.Int(nullable: false),
                        CanDeliver = c.Int(nullable: false),
                        PlanAircraftId = c.Guid(),
                        PlanId = c.Guid(nullable: false),
                        ActionCategoryId = c.Guid(nullable: false),
                        TargetCategoryId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                        CaacAircraftTypeId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                        PerformAnnualId = c.Guid(nullable: false),
                        ApprovalHistoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ActionCategoryId)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.CAACAircraftType", t => t.CaacAircraftTypeId)
                .ForeignKey("FRP.Annual", t => t.PerformAnnualId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftId)
                .ForeignKey("FRP.ActionCategory", t => t.TargetCategoryId)
                .Index(t => t.ActionCategoryId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.AirlinesId)
                .Index(t => t.CaacAircraftTypeId)
                .Index(t => t.PerformAnnualId)
                .Index(t => t.PlanAircraftId)
                .Index(t => t.TargetCategoryId);
            
            CreateTable(
                "FRP.ContentTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.AircraftMaintainPlan",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        FirstHalfYear = c.Int(nullable: false),
                        SecondHalfYear = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AnnualMaintainPlan", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.BasePurchaseInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.BasicConfig",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        BasicConfigGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AcConfig", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineMaintainPlan",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MaintainPlanType = c.Int(nullable: false),
                        DollarRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompanyLeader = c.String(),
                        DepartmentLeader = c.String(),
                        BudgetManager = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AnnualMaintainPlan", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SerialNumber = c.String(),
                        DocumentName = c.String(),
                        InMaintainTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OutMaintainTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TotalDays = c.Int(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.SpecialConfig",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AcConfig", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.ChangePlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AircraftBusinessId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PlanHistory", t => t.ID)
                .ForeignKey("FRP.AircraftBusiness", t => t.AircraftBusinessId)
                .Index(t => t.ID)
                .Index(t => t.AircraftBusinessId);
            
            CreateTable(
                "FRP.OperationPlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OperationHistoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PlanHistory", t => t.ID)
                .ForeignKey("FRP.OperationHistory", t => t.OperationHistoryId)
                .Index(t => t.ID)
                .Index(t => t.OperationHistoryId);
            
            CreateTable(
                "FRP.LeaseContractAircraft",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ContractAircraft", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseContractAircraft",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AirframePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefitCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnginePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BFEPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ContractAircraft", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.LeaseContractEngine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ContractEngine", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseContractEngine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ContractEngine", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.OfficialDocument",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ReferenceNumber = c.String(),
                        DispatchUnit = c.String(),
                        DispatchDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Document", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.StandardDocument",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Document", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.LeaseGuarantee",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Guarantee", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainGuarantee",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MaintainContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Guarantee", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseCreditNoteInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainCreditNoteInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.LeaseInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchasePrepaymentInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainPrepaymentInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseInvoiceLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        OrderLineId = c.Int(),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.InvoiceLine", t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.InvoiceId)
                .Index(t => t.ID)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "FRP.SundryInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.BasePurchaseInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.SpecialRefitInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.APUMaintainContract",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainContract", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineMaintainContract",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainContract", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.UndercartMaintainContract",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainContract", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AirframeMaintainContract",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainContract", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.ItemMaintainCtrl",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ItemNo = c.String(maxLength: 100),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCtrl", t => t.ID)
                .ForeignKey("FRP.Item", t => t.ItemId)
                .Index(t => t.ID)
                .Index(t => t.ItemId);
            
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
                .ForeignKey("FRP.PnReg", t => t.PnRegId)
                .Index(t => t.ID)
                .Index(t => t.PnRegId);
            
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
            
            CreateTable(
                "FRP.AirframeMaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.APUMaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineMaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.UndercartMaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainInvoiceLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MaintainItem = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.InvoiceLine", t => t.ID)
                .ForeignKey("FRP.MaintainInvoice", t => t.InvoiceId)
                .Index(t => t.ID)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "FRP.AircraftMaterial",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .Index(t => t.ID)
                .Index(t => t.AircraftTypeId);
            
            CreateTable(
                "FRP.BFEMaterial",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Pn = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineMaterial",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Pn = c.String(),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftLeaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftLeaseOrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AircraftMaterialId = c.Int(nullable: false),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.AircraftMaterial", t => t.AircraftMaterialId)
                .ForeignKey("FRP.LeaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
                .Index(t => t.AircraftMaterialId)
                .Index(t => t.ContractAircraftId);
            
            CreateTable(
                "FRP.AircraftPurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftPurchaseOrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AirframePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefitCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnginePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AircraftMaterialId = c.Int(nullable: false),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.AircraftMaterial", t => t.AircraftMaterialId)
                .ForeignKey("FRP.PurchaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
                .Index(t => t.AircraftMaterialId)
                .Index(t => t.ContractAircraftId);
            
            CreateTable(
                "FRP.BFEPurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ForwarderLinkman = c.String(),
                        ForwarderId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.ID)
                .ForeignKey("FRP.Forwarder", t => t.ForwarderId)
                .Index(t => t.ID)
                .Index(t => t.ForwarderId);
            
            CreateTable(
                "FRP.BFEPurchaseOrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        BFEMaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.BFEMaterial", t => t.BFEMaterialId)
                .Index(t => t.ID)
                .Index(t => t.BFEMaterialId);
            
            CreateTable(
                "FRP.EngineLeaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineLeaseOrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EngineMaterialId = c.Int(nullable: false),
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.EngineMaterial", t => t.EngineMaterialId)
                .ForeignKey("FRP.LeaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
                .Index(t => t.EngineMaterialId)
                .Index(t => t.ContractEngineId);
            
            CreateTable(
                "FRP.EnginePurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EnginePurchaseOrderLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EngineMaterialId = c.Int(nullable: false),
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.EngineMaterial", t => t.EngineMaterialId)
                .ForeignKey("FRP.PurchaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
                .Index(t => t.EngineMaterialId)
                .Index(t => t.ContractEngineId);
            
            CreateTable(
                "FRP.AircraftPaymentSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentSchedule", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EnginePaymentSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentSchedule", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.StandardPaymentSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentSchedule", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainPaymentSchedule",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentSchedule", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftLeaseReception",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Reception", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftLeaseReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DailNumber = c.String(),
                        FlightNumber = c.String(),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ReceptionLine", t => t.ID)
                .ForeignKey("FRP.LeaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
                .Index(t => t.ContractAircraftId);
            
            CreateTable(
                "FRP.AircraftPurchaseReception",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Reception", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftPurchaseReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DailNumber = c.String(),
                        FlightNumber = c.String(),
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ReceptionLine", t => t.ID)
                .ForeignKey("FRP.PurchaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
                .Index(t => t.ContractAircraftId);
            
            CreateTable(
                "FRP.EngineLeaseReception",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Reception", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineLeaseReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ReceptionLine", t => t.ID)
                .ForeignKey("FRP.LeaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
                .Index(t => t.ContractEngineId);
            
            CreateTable(
                "FRP.EnginePurchaseReception",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Reception", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EnginePurchaseReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ReceptionLine", t => t.ID)
                .ForeignKey("FRP.PurchaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
                .Index(t => t.ContractEngineId);
            
            CreateTable(
                "FRP.EngineReg",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NeedMonitor = c.Boolean(nullable: false),
                        MonitorStatus = c.Int(nullable: false),
                        ThrustId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SnReg", t => t.ID)
                .ForeignKey("FRP.Thrust", t => t.ThrustId)
                .Index(t => t.ID)
                .Index(t => t.ThrustId);
            
            CreateTable(
                "FRP.APUReg",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NeedMonitor = c.Boolean(nullable: false),
                        MonitorStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SnReg", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftLeaseSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.AircraftPurchaseSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.BFEPurchaseSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EngineLeaseSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.EnginePurchaseSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.MaintainSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.OtherSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierRole", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.RegularCheckMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        RegularCheckType = c.Int(nullable: false),
                        RegularCheckLevel = c.String(),
                        AircraftId = c.Guid(nullable: false),
                        ActionCategoryId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.UndercartMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Part = c.Int(nullable: false),
                        MaintainFeeEur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaintainFeeRmb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FreightFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReplaceFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Custom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValueRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AircraftId = c.Guid(nullable: false),
                        ActionCategoryId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.SpecialRefitMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Project = c.String(),
                        Info = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.NonFhaMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EngineNumber = c.String(),
                        Type = c.Int(nullable: false),
                        ContractRepairt = c.Int(nullable: false),
                        MaintainLevel = c.Int(nullable: false),
                        ChangeLlpNumber = c.Int(nullable: false),
                        Tsr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Csr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NonFhaFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PartFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeLlpFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeeLittleSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeeTotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Custom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValueRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomsTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FreightFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        ActualMaintainLevel = c.Int(nullable: false),
                        ActualChangeLlpNumber = c.Int(nullable: false),
                        ActualTsr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualCsr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AircraftId = c.Guid(nullable: false),
                        ActionCategoryId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.ApuMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NameType = c.String(),
                        Type = c.String(),
                        YearBudgetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HourPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractRepairFeeUsd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractRepairFeeRmb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValueRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncludeAddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.FhaMaintainCost",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EngineProperty = c.String(),
                        Jx = c.String(),
                        YearBudgetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AirHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HourPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FhaFeeUsd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FhaFeeRmb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Custom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomAddedRmb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValueRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncludeAddedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomAdded = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.MaintainCost", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("FRP.FhaMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.ApuMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.NonFhaMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.SpecialRefitMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.UndercartMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.RegularCheckMaintainCost", "ID", "FRP.MaintainCost");
            DropForeignKey("FRP.OtherSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.MaintainSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.EnginePurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.EngineLeaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.BFEPurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.AircraftPurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.AircraftLeaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.APUReg", "ID", "FRP.SnReg");
            DropForeignKey("FRP.EngineReg", "ThrustId", "FRP.Thrust");
            DropForeignKey("FRP.EngineReg", "ID", "FRP.SnReg");
            DropForeignKey("FRP.EnginePurchaseReceptionLine", "ContractEngineId", "FRP.PurchaseContractEngine");
            DropForeignKey("FRP.EnginePurchaseReceptionLine", "ID", "FRP.ReceptionLine");
            DropForeignKey("FRP.EnginePurchaseReception", "ID", "FRP.Reception");
            DropForeignKey("FRP.EngineLeaseReceptionLine", "ContractEngineId", "FRP.LeaseContractEngine");
            DropForeignKey("FRP.EngineLeaseReceptionLine", "ID", "FRP.ReceptionLine");
            DropForeignKey("FRP.EngineLeaseReception", "ID", "FRP.Reception");
            DropForeignKey("FRP.AircraftPurchaseReceptionLine", "ContractAircraftId", "FRP.PurchaseContractAircraft");
            DropForeignKey("FRP.AircraftPurchaseReceptionLine", "ID", "FRP.ReceptionLine");
            DropForeignKey("FRP.AircraftPurchaseReception", "ID", "FRP.Reception");
            DropForeignKey("FRP.AircraftLeaseReceptionLine", "ContractAircraftId", "FRP.LeaseContractAircraft");
            DropForeignKey("FRP.AircraftLeaseReceptionLine", "ID", "FRP.ReceptionLine");
            DropForeignKey("FRP.AircraftLeaseReception", "ID", "FRP.Reception");
            DropForeignKey("FRP.MaintainPaymentSchedule", "ID", "FRP.PaymentSchedule");
            DropForeignKey("FRP.StandardPaymentSchedule", "ID", "FRP.PaymentSchedule");
            DropForeignKey("FRP.EnginePaymentSchedule", "ID", "FRP.PaymentSchedule");
            DropForeignKey("FRP.AircraftPaymentSchedule", "ID", "FRP.PaymentSchedule");
            DropForeignKey("FRP.EnginePurchaseOrderLine", "ContractEngineId", "FRP.PurchaseContractEngine");
            DropForeignKey("FRP.EnginePurchaseOrderLine", "EngineMaterialId", "FRP.EngineMaterial");
            DropForeignKey("FRP.EnginePurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.EnginePurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.EngineLeaseOrderLine", "ContractEngineId", "FRP.LeaseContractEngine");
            DropForeignKey("FRP.EngineLeaseOrderLine", "EngineMaterialId", "FRP.EngineMaterial");
            DropForeignKey("FRP.EngineLeaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.EngineLeaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.BFEPurchaseOrderLine", "BFEMaterialId", "FRP.BFEMaterial");
            DropForeignKey("FRP.BFEPurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.BFEPurchaseOrder", "ForwarderId", "FRP.Forwarder");
            DropForeignKey("FRP.BFEPurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.AircraftPurchaseOrderLine", "ContractAircraftId", "FRP.PurchaseContractAircraft");
            DropForeignKey("FRP.AircraftPurchaseOrderLine", "AircraftMaterialId", "FRP.AircraftMaterial");
            DropForeignKey("FRP.AircraftPurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.AircraftPurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.AircraftLeaseOrderLine", "ContractAircraftId", "FRP.LeaseContractAircraft");
            DropForeignKey("FRP.AircraftLeaseOrderLine", "AircraftMaterialId", "FRP.AircraftMaterial");
            DropForeignKey("FRP.AircraftLeaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.AircraftLeaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.EngineMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.BFEMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.AircraftMaterial", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.MaintainInvoiceLine", "InvoiceId", "FRP.MaintainInvoice");
            DropForeignKey("FRP.MaintainInvoiceLine", "ID", "FRP.InvoiceLine");
            DropForeignKey("FRP.UndercartMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.EngineMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.APUMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.AirframeMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.SnMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.PnMaintainCtrl", "PnRegId", "FRP.PnReg");
            DropForeignKey("FRP.PnMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.ItemMaintainCtrl", "ItemId", "FRP.Item");
            DropForeignKey("FRP.ItemMaintainCtrl", "ID", "FRP.MaintainCtrl");
            DropForeignKey("FRP.AirframeMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.UndercartMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.EngineMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.APUMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.SpecialRefitInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.SundryInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.PurchaseInvoiceLine", "InvoiceId", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.PurchaseInvoiceLine", "ID", "FRP.InvoiceLine");
            DropForeignKey("FRP.MaintainPrepaymentInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.PurchasePrepaymentInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.PurchaseInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.LeaseInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.MaintainCreditNoteInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.PurchaseCreditNoteInvoice", "ID", "FRP.BasePurchaseInvoice");
            DropForeignKey("FRP.MaintainGuarantee", "ID", "FRP.Guarantee");
            DropForeignKey("FRP.LeaseGuarantee", "ID", "FRP.Guarantee");
            DropForeignKey("FRP.StandardDocument", "ID", "FRP.Document");
            DropForeignKey("FRP.OfficialDocument", "ID", "FRP.Document");
            DropForeignKey("FRP.PurchaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.LeaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.PurchaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.LeaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.OperationPlan", "OperationHistoryId", "FRP.OperationHistory");
            DropForeignKey("FRP.OperationPlan", "ID", "FRP.PlanHistory");
            DropForeignKey("FRP.ChangePlan", "AircraftBusinessId", "FRP.AircraftBusiness");
            DropForeignKey("FRP.ChangePlan", "ID", "FRP.PlanHistory");
            DropForeignKey("FRP.SpecialConfig", "ID", "FRP.AcConfig");
            DropForeignKey("FRP.MaintainInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.EngineMaintainPlan", "ID", "FRP.AnnualMaintainPlan");
            DropForeignKey("FRP.BasicConfig", "ID", "FRP.AcConfig");
            DropForeignKey("FRP.BasePurchaseInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.AircraftMaintainPlan", "ID", "FRP.AnnualMaintainPlan");
            DropForeignKey("FRP.PlanHistory", "TargetCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.PlanHistory", "PlanAircraftId", "FRP.PlanAircraft");
            DropForeignKey("FRP.PlanHistory", "PerformAnnualId", "FRP.Annual");
            DropForeignKey("FRP.PlanHistory", "CaacAircraftTypeId", "FRP.CAACAircraftType");
            DropForeignKey("FRP.PlanHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PlanHistory", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.PlanHistory", "ActionCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.UserRole", "UserId", "FRP.User");
            DropForeignKey("FRP.TaskStandard", "WorkGroupId", "FRP.WorkGroup");
            DropForeignKey("FRP.Member", "WorkGroupId", "FRP.WorkGroup");
            DropForeignKey("FRP.WorkGroup", "ManagerId", "FRP.Member");
            DropForeignKey("FRP.TaskCase", "TaskStandardId", "FRP.TaskStandard");
            DropForeignKey("FRP.SupplierRole", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.LifeMonitor", "SnRegId", "FRP.SnReg");
            DropForeignKey("FRP.SnHistory", "RemInstRecordId", "FRP.SnRemInstRecord");
            DropForeignKey("FRP.ApplicableAircraft", "ScnId", "FRP.Scn");
            DropForeignKey("FRP.ApplicableAircraft", "ContractAircraftId", "FRP.ContractAircraft");
            DropForeignKey("FRP.RoleFunction", "RoleId", "FRP.Role");
            DropForeignKey("FRP.RoleFunction", "FunctionItemId", "FRP.FunctionItem");
            DropForeignKey("FRP.ApprovalHistory", "RequestId", "FRP.Request");
            DropForeignKey("FRP.Request", "ApprovalDocId", "FRP.ApprovalDoc");
            DropForeignKey("FRP.Request", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Reception", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ReceptionSchedule", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.ReceptionLine", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.TaskTemp", "ProjectTempId", "FRP.ProjectTemp");
            DropForeignKey("FRP.TaskTemp", "ParentId", "FRP.TaskTemp");
            DropForeignKey("FRP.Task", "ProjectId", "FRP.Project");
            DropForeignKey("FRP.Task", "ParentId", "FRP.Task");
            DropForeignKey("FRP.ProgrammingFile", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.ProgrammingFile", "IssuedUnitId", "FRP.IssuedUnit");
            DropForeignKey("FRP.AircraftPlan", "AnnualId", "FRP.Annual");
            DropForeignKey("FRP.AircraftPlan", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PaymentSchedule", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentScheduleLine", "PaymentScheduleId", "FRP.PaymentSchedule");
            DropForeignKey("FRP.PaymentSchedule", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.PaymentNotice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentNoticeLine", "PaymentNoticeId", "FRP.PaymentNotice");
            DropForeignKey("FRP.PaymentNotice", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.PaymentNotice", "BankAccountId", "FRP.BankAccount");
            DropForeignKey("FRP.Organization", "ParentCode", "FRP.Organization");
            DropForeignKey("FRP.OrganizationRole", "OrganizationId", "FRP.Organization");
            DropForeignKey("FRP.MaintainCtrl", "MaintainWorkId", "FRP.MaintainWork");
            DropForeignKey("FRP.MaintainCost", "MaintainInvoiceId", "FRP.MaintainInvoice");
            DropForeignKey("FRP.MaintainContract", "SignatoryId", "FRP.Supplier");
            DropForeignKey("FRP.InstallController", "PnRegId", "FRP.PnReg");
            DropForeignKey("FRP.InstallController", "ItemId", "FRP.Item");
            DropForeignKey("FRP.Dependency", "InstallControllerId", "FRP.InstallController");
            DropForeignKey("FRP.InstallController", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.Guarantee", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Guarantee", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.FunctionItem", "ParentItemId", "FRP.FunctionItem");
            DropForeignKey("FRP.EnginePlanHistory", "EnginePlanId", "FRP.EnginePlan");
            DropForeignKey("FRP.EnginePlanHistory", "PlanEngineId", "FRP.PlanEngine");
            DropForeignKey("FRP.PlanEngine", "EngineTypeId", "FRP.EngineType");
            DropForeignKey("FRP.PlanEngine", "EngineId", "FRP.Engine");
            DropForeignKey("FRP.Engine", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Engine", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.Engine", "EngineTypeId", "FRP.EngineType");
            DropForeignKey("FRP.EngineOwnershipHistory", "EngineId", "FRP.Engine");
            DropForeignKey("FRP.EngineOwnershipHistory", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.EngineBusinessHistory", "EngineId", "FRP.Engine");
            DropForeignKey("FRP.EngineBusinessHistory", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.EngineBusinessHistory", "EngineTypeId", "FRP.EngineType");
            DropForeignKey("FRP.Engine", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PlanEngine", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.EnginePlanHistory", "PerformAnnualId", "FRP.Annual");
            DropForeignKey("FRP.EnginePlanHistory", "EngineTypeId", "FRP.EngineType");
            DropForeignKey("FRP.EngineType", "ManufacturerId", "FRP.Manufacturer");
            DropForeignKey("FRP.EnginePlanHistory", "ActionCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.EnginePlan", "AnnualId", "FRP.Annual");
            DropForeignKey("FRP.EnginePlan", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.EngineMaintainPlanDetail", "EngineMaintainPlanId", "FRP.EngineMaintainPlan");
            DropForeignKey("FRP.Document", "DocumentTypeId", "FRP.DocumentType");
            DropForeignKey("FRP.DocumentPath", "ParentId", "FRP.DocumentPath");
            DropForeignKey("FRP.ContractEngine", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ContractEngine", "PartID", "FRP.Part");
            DropForeignKey("FRP.ContractEngine", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ContractAircraftBFE", "ContractAircraftId", "FRP.ContractAircraft");
            DropForeignKey("FRP.ContractAircraft", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ContractAircraft", "PlanAircraftID", "FRP.PlanAircraft");
            DropForeignKey("FRP.ContractAircraft", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ContractAircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.ContractAircraftBFE", "BFEPurchaseOrderId", "FRP.BFEPurchaseOrder");
            DropForeignKey("FRP.Trade", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Order", "TradeId", "FRP.Trade");
            DropForeignKey("FRP.OrderLine", "OrderId", "FRP.Order");
            DropForeignKey("FRP.Order", "LinkmanId", "FRP.Linkman");
            DropForeignKey("FRP.Order", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.ContractContent", "OrderId", "FRP.Order");
            DropForeignKey("FRP.CaacProgramming", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.CaacProgramming", "IssuedUnitId", "FRP.IssuedUnit");
            DropForeignKey("FRP.CaacProgrammingLine", "CaacProgrammingId", "FRP.CaacProgramming");
            DropForeignKey("FRP.CaacProgrammingLine", "AircraftCategoryId", "FRP.AircraftCategory");
            DropForeignKey("FRP.AcConfig", "ParentId", "FRP.AcConfig");
            DropForeignKey("FRP.BasicConfigHistory", "BasicConfigGroupId", "FRP.BasicConfigGroup");
            DropForeignKey("FRP.BasicConfigGroup", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.PaymentScheduleLine", "InvoiceId", "FRP.Invoice");
            DropForeignKey("FRP.Invoice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Invoice", "PaymentScheduleLineId", "FRP.PaymentScheduleLine");
            DropForeignKey("FRP.Invoice", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.ApprovalDoc", "DispatchUnitId", "FRP.Manager");
            DropForeignKey("FRP.AirStructureDamage", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.AirProgramming", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.AirProgramming", "IssuedUnitId", "FRP.IssuedUnit");
            DropForeignKey("FRP.AirProgrammingLine", "AirProgrammingId", "FRP.AirProgramming");
            DropForeignKey("FRP.AirProgrammingLine", "AircraftSeriesId", "FRP.AircraftSeries");
            DropForeignKey("FRP.Aircraft", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.OwnershipHistory", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.OwnershipHistory", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Supplier", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.SupplierCompanyMaterial", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.SupplierCompanyMaterial", "MaterialId", "FRP.Material");
            DropForeignKey("FRP.Material", "ManufacturerID", "FRP.Manufacturer");
            DropForeignKey("FRP.BankAccount", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.OperationHistory", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.OperationHistory", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.OperationHistory", "ExportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.OperationHistory", "ID", "FRP.ApprovalHistory");
            DropForeignKey("FRP.ApprovalHistory", "RequestDeliverAnnualId", "FRP.Annual");
            DropForeignKey("FRP.Annual", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.ApprovalHistory", "PlanAircraftId", "FRP.PlanAircraft");
            DropForeignKey("FRP.PlanAircraft", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PlanAircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.PlanAircraft", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.ApprovalHistory", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ApprovalHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.OperationHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.AircraftLicense", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.Aircraft", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.Aircraft", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Aircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftBusiness", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.AircraftBusiness", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.AircraftBusiness", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AcConfigHistory", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.AircraftMaintainPlanDetail", "AircraftMaintainPlanId", "FRP.AircraftMaintainPlan");
            DropForeignKey("FRP.AircraftLicense", "LicenseTypeId", "FRP.LicenseType");
            DropForeignKey("FRP.AircraftConfiguration", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftType", "ManufacturerId", "FRP.Manufacturer");
            DropForeignKey("FRP.AircraftType", "CaacAircraftTypeId", "FRP.CAACAircraftType");
            DropForeignKey("FRP.CAACAircraftType", "ManufacturerId", "FRP.Manufacturer");
            DropForeignKey("FRP.CAACAircraftType", "AircraftSeriesId", "FRP.AircraftSeries");
            DropForeignKey("FRP.CAACAircraftType", "AircraftCategoryId", "FRP.AircraftCategory");
            DropForeignKey("FRP.AircraftType", "AircraftSeriesId", "FRP.AircraftSeries");
            DropForeignKey("FRP.AircraftType", "AircraftCategoryId", "FRP.AircraftCategory");
            DropForeignKey("FRP.AircraftConfiguration", "AircraftSeriesId", "FRP.AircraftSeries");
            DropForeignKey("FRP.AircraftSeries", "ManufacturerId", "FRP.Manufacturer");
            DropForeignKey("FRP.Ata", "AircraftSeriesId", "FRP.AircraftSeries");
            DropForeignKey("FRP.Ata", "ParentAta_Id", "FRP.Ata");
            DropForeignKey("FRP.Ata", "ParentId", "FRP.Ata");
            DropForeignKey("FRP.AircraftCabin", "AircraftConfiguratonId", "FRP.AircraftConfiguration");
            DropForeignKey("FRP.AircraftCabin", "AircraftCabinTypeId", "FRP.AircraftCabinType");
            DropIndex("FRP.FhaMaintainCost", new[] { "ID" });
            DropIndex("FRP.ApuMaintainCost", new[] { "ID" });
            DropIndex("FRP.NonFhaMaintainCost", new[] { "ID" });
            DropIndex("FRP.SpecialRefitMaintainCost", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainCost", new[] { "ID" });
            DropIndex("FRP.RegularCheckMaintainCost", new[] { "ID" });
            DropIndex("FRP.OtherSupplier", new[] { "ID" });
            DropIndex("FRP.MaintainSupplier", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.EngineLeaseSupplier", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseSupplier", new[] { "ID" });
            DropIndex("FRP.APUReg", new[] { "ID" });
            DropIndex("FRP.EngineReg", new[] { "ThrustId" });
            DropIndex("FRP.EngineReg", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseReceptionLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EnginePurchaseReceptionLine", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseReception", new[] { "ID" });
            DropIndex("FRP.EngineLeaseReceptionLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EngineLeaseReceptionLine", new[] { "ID" });
            DropIndex("FRP.EngineLeaseReception", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseReceptionLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftPurchaseReceptionLine", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseReception", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseReceptionLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftLeaseReceptionLine", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseReception", new[] { "ID" });
            DropIndex("FRP.MaintainPaymentSchedule", new[] { "ID" });
            DropIndex("FRP.StandardPaymentSchedule", new[] { "ID" });
            DropIndex("FRP.EnginePaymentSchedule", new[] { "ID" });
            DropIndex("FRP.AircraftPaymentSchedule", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseOrderLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EnginePurchaseOrderLine", new[] { "EngineMaterialId" });
            DropIndex("FRP.EnginePurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseOrder", new[] { "ID" });
            DropIndex("FRP.EngineLeaseOrderLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EngineLeaseOrderLine", new[] { "EngineMaterialId" });
            DropIndex("FRP.EngineLeaseOrderLine", new[] { "ID" });
            DropIndex("FRP.EngineLeaseOrder", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseOrderLine", new[] { "BFEMaterialId" });
            DropIndex("FRP.BFEPurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseOrder", new[] { "ForwarderId" });
            DropIndex("FRP.BFEPurchaseOrder", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseOrderLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftPurchaseOrderLine", new[] { "AircraftMaterialId" });
            DropIndex("FRP.AircraftPurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseOrder", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseOrderLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftLeaseOrderLine", new[] { "AircraftMaterialId" });
            DropIndex("FRP.AircraftLeaseOrderLine", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseOrder", new[] { "ID" });
            DropIndex("FRP.EngineMaterial", new[] { "ID" });
            DropIndex("FRP.BFEMaterial", new[] { "ID" });
            DropIndex("FRP.AircraftMaterial", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftMaterial", new[] { "ID" });
            DropIndex("FRP.MaintainInvoiceLine", new[] { "InvoiceId" });
            DropIndex("FRP.MaintainInvoiceLine", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.EngineMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.APUMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.AirframeMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.SnMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.PnMaintainCtrl", new[] { "PnRegId" });
            DropIndex("FRP.PnMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.ItemMaintainCtrl", new[] { "ItemId" });
            DropIndex("FRP.ItemMaintainCtrl", new[] { "ID" });
            DropIndex("FRP.AirframeMaintainContract", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainContract", new[] { "ID" });
            DropIndex("FRP.EngineMaintainContract", new[] { "ID" });
            DropIndex("FRP.APUMaintainContract", new[] { "ID" });
            DropIndex("FRP.SpecialRefitInvoice", new[] { "ID" });
            DropIndex("FRP.SundryInvoice", new[] { "ID" });
            DropIndex("FRP.PurchaseInvoiceLine", new[] { "InvoiceId" });
            DropIndex("FRP.PurchaseInvoiceLine", new[] { "ID" });
            DropIndex("FRP.MaintainPrepaymentInvoice", new[] { "ID" });
            DropIndex("FRP.PurchasePrepaymentInvoice", new[] { "ID" });
            DropIndex("FRP.PurchaseInvoice", new[] { "ID" });
            DropIndex("FRP.LeaseInvoice", new[] { "ID" });
            DropIndex("FRP.MaintainCreditNoteInvoice", new[] { "ID" });
            DropIndex("FRP.PurchaseCreditNoteInvoice", new[] { "ID" });
            DropIndex("FRP.MaintainGuarantee", new[] { "ID" });
            DropIndex("FRP.LeaseGuarantee", new[] { "ID" });
            DropIndex("FRP.StandardDocument", new[] { "ID" });
            DropIndex("FRP.OfficialDocument", new[] { "ID" });
            DropIndex("FRP.PurchaseContractEngine", new[] { "ID" });
            DropIndex("FRP.LeaseContractEngine", new[] { "ID" });
            DropIndex("FRP.PurchaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.LeaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.OperationPlan", new[] { "OperationHistoryId" });
            DropIndex("FRP.OperationPlan", new[] { "ID" });
            DropIndex("FRP.ChangePlan", new[] { "AircraftBusinessId" });
            DropIndex("FRP.ChangePlan", new[] { "ID" });
            DropIndex("FRP.SpecialConfig", new[] { "ID" });
            DropIndex("FRP.MaintainInvoice", new[] { "ID" });
            DropIndex("FRP.EngineMaintainPlan", new[] { "ID" });
            DropIndex("FRP.BasicConfig", new[] { "ID" });
            DropIndex("FRP.BasePurchaseInvoice", new[] { "ID" });
            DropIndex("FRP.AircraftMaintainPlan", new[] { "ID" });
            DropIndex("FRP.PlanHistory", new[] { "TargetCategoryId" });
            DropIndex("FRP.PlanHistory", new[] { "PlanAircraftId" });
            DropIndex("FRP.PlanHistory", new[] { "PerformAnnualId" });
            DropIndex("FRP.PlanHistory", new[] { "CaacAircraftTypeId" });
            DropIndex("FRP.PlanHistory", new[] { "AirlinesId" });
            DropIndex("FRP.PlanHistory", new[] { "AircraftTypeId" });
            DropIndex("FRP.PlanHistory", new[] { "ActionCategoryId" });
            DropIndex("FRP.UserRole", new[] { "UserId" });
            DropIndex("FRP.TaskStandard", new[] { "WorkGroupId" });
            DropIndex("FRP.Member", new[] { "WorkGroupId" });
            DropIndex("FRP.WorkGroup", new[] { "ManagerId" });
            DropIndex("FRP.TaskCase", new[] { "TaskStandardId" });
            DropIndex("FRP.SupplierRole", new[] { "SupplierCompanyId" });
            DropIndex("FRP.LifeMonitor", new[] { "SnRegId" });
            DropIndex("FRP.SnHistory", new[] { "RemInstRecordId" });
            DropIndex("FRP.ApplicableAircraft", new[] { "ScnId" });
            DropIndex("FRP.ApplicableAircraft", new[] { "ContractAircraftId" });
            DropIndex("FRP.RoleFunction", new[] { "RoleId" });
            DropIndex("FRP.RoleFunction", new[] { "FunctionItemId" });
            DropIndex("FRP.ApprovalHistory", new[] { "RequestId" });
            DropIndex("FRP.Request", new[] { "ApprovalDocId" });
            DropIndex("FRP.Request", new[] { "AirlinesId" });
            DropIndex("FRP.Reception", new[] { "SupplierId" });
            DropIndex("FRP.ReceptionSchedule", new[] { "ReceptionId" });
            DropIndex("FRP.ReceptionLine", new[] { "ReceptionId" });
            DropIndex("FRP.TaskTemp", new[] { "ProjectTempId" });
            DropIndex("FRP.TaskTemp", new[] { "ParentId" });
            DropIndex("FRP.Task", new[] { "ProjectId" });
            DropIndex("FRP.Task", new[] { "ParentId" });
            DropIndex("FRP.ProgrammingFile", new[] { "ProgrammingId" });
            DropIndex("FRP.ProgrammingFile", new[] { "IssuedUnitId" });
            DropIndex("FRP.AircraftPlan", new[] { "AnnualId" });
            DropIndex("FRP.AircraftPlan", new[] { "AirlinesId" });
            DropIndex("FRP.PaymentSchedule", new[] { "SupplierId" });
            DropIndex("FRP.PaymentScheduleLine", new[] { "PaymentScheduleId" });
            DropIndex("FRP.PaymentSchedule", new[] { "CurrencyId" });
            DropIndex("FRP.PaymentNotice", new[] { "SupplierId" });
            DropIndex("FRP.PaymentNoticeLine", new[] { "PaymentNoticeId" });
            DropIndex("FRP.PaymentNotice", new[] { "CurrencyId" });
            DropIndex("FRP.PaymentNotice", new[] { "BankAccountId" });
            DropIndex("FRP.Organization", new[] { "ParentCode" });
            DropIndex("FRP.OrganizationRole", new[] { "OrganizationId" });
            DropIndex("FRP.MaintainCtrl", new[] { "MaintainWorkId" });
            DropIndex("FRP.MaintainCost", new[] { "MaintainInvoiceId" });
            DropIndex("FRP.MaintainContract", new[] { "SignatoryId" });
            DropIndex("FRP.InstallController", new[] { "PnRegId" });
            DropIndex("FRP.InstallController", new[] { "ItemId" });
            DropIndex("FRP.Dependency", new[] { "InstallControllerId" });
            DropIndex("FRP.InstallController", new[] { "AircraftTypeId" });
            DropIndex("FRP.Guarantee", new[] { "SupplierId" });
            DropIndex("FRP.Guarantee", new[] { "CurrencyId" });
            DropIndex("FRP.FunctionItem", new[] { "ParentItemId" });
            DropIndex("FRP.EnginePlanHistory", new[] { "EnginePlanId" });
            DropIndex("FRP.EnginePlanHistory", new[] { "PlanEngineId" });
            DropIndex("FRP.PlanEngine", new[] { "EngineTypeId" });
            DropIndex("FRP.PlanEngine", new[] { "EngineId" });
            DropIndex("FRP.Engine", new[] { "SupplierId" });
            DropIndex("FRP.Engine", new[] { "ImportCategoryId" });
            DropIndex("FRP.Engine", new[] { "EngineTypeId" });
            DropIndex("FRP.EngineOwnershipHistory", new[] { "EngineId" });
            DropIndex("FRP.EngineOwnershipHistory", new[] { "SupplierId" });
            DropIndex("FRP.EngineBusinessHistory", new[] { "EngineId" });
            DropIndex("FRP.EngineBusinessHistory", new[] { "ImportCategoryId" });
            DropIndex("FRP.EngineBusinessHistory", new[] { "EngineTypeId" });
            DropIndex("FRP.Engine", new[] { "AirlinesId" });
            DropIndex("FRP.PlanEngine", new[] { "AirlinesId" });
            DropIndex("FRP.EnginePlanHistory", new[] { "PerformAnnualId" });
            DropIndex("FRP.EnginePlanHistory", new[] { "EngineTypeId" });
            DropIndex("FRP.EngineType", new[] { "ManufacturerId" });
            DropIndex("FRP.EnginePlanHistory", new[] { "ActionCategoryId" });
            DropIndex("FRP.EnginePlan", new[] { "AnnualId" });
            DropIndex("FRP.EnginePlan", new[] { "AirlinesId" });
            DropIndex("FRP.EngineMaintainPlanDetail", new[] { "EngineMaintainPlanId" });
            DropIndex("FRP.Document", new[] { "DocumentTypeId" });
            DropIndex("FRP.DocumentPath", new[] { "ParentId" });
            DropIndex("FRP.ContractEngine", new[] { "SupplierId" });
            DropIndex("FRP.ContractEngine", new[] { "PartID" });
            DropIndex("FRP.ContractEngine", new[] { "ImportCategoryId" });
            DropIndex("FRP.ContractAircraftBFE", new[] { "ContractAircraftId" });
            DropIndex("FRP.ContractAircraft", new[] { "SupplierId" });
            DropIndex("FRP.ContractAircraft", new[] { "PlanAircraftID" });
            DropIndex("FRP.ContractAircraft", new[] { "ImportCategoryId" });
            DropIndex("FRP.ContractAircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.ContractAircraftBFE", new[] { "BFEPurchaseOrderId" });
            DropIndex("FRP.Trade", new[] { "SupplierId" });
            DropIndex("FRP.Order", new[] { "TradeId" });
            DropIndex("FRP.OrderLine", new[] { "OrderId" });
            DropIndex("FRP.Order", new[] { "LinkmanId" });
            DropIndex("FRP.Order", new[] { "CurrencyId" });
            DropIndex("FRP.ContractContent", new[] { "OrderId" });
            DropIndex("FRP.CaacProgramming", new[] { "ProgrammingId" });
            DropIndex("FRP.CaacProgramming", new[] { "IssuedUnitId" });
            DropIndex("FRP.CaacProgrammingLine", new[] { "CaacProgrammingId" });
            DropIndex("FRP.CaacProgrammingLine", new[] { "AircraftCategoryId" });
            DropIndex("FRP.AcConfig", new[] { "ParentId" });
            DropIndex("FRP.BasicConfigHistory", new[] { "BasicConfigGroupId" });
            DropIndex("FRP.BasicConfigGroup", new[] { "AircraftTypeId" });
            DropIndex("FRP.PaymentScheduleLine", new[] { "InvoiceId" });
            DropIndex("FRP.Invoice", new[] { "SupplierId" });
            DropIndex("FRP.Invoice", new[] { "PaymentScheduleLineId" });
            DropIndex("FRP.Invoice", new[] { "CurrencyId" });
            DropIndex("FRP.ApprovalDoc", new[] { "DispatchUnitId" });
            DropIndex("FRP.AirStructureDamage", new[] { "AircraftId" });
            DropIndex("FRP.AirProgramming", new[] { "ProgrammingId" });
            DropIndex("FRP.AirProgramming", new[] { "IssuedUnitId" });
            DropIndex("FRP.AirProgrammingLine", new[] { "AirProgrammingId" });
            DropIndex("FRP.AirProgrammingLine", new[] { "AircraftSeriesId" });
            DropIndex("FRP.Aircraft", new[] { "SupplierId" });
            DropIndex("FRP.OwnershipHistory", new[] { "AircraftId" });
            DropIndex("FRP.OwnershipHistory", new[] { "SupplierId" });
            DropIndex("FRP.Supplier", new[] { "SupplierCompanyId" });
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "SupplierCompanyId" });
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "MaterialId" });
            DropIndex("FRP.Material", new[] { "ManufacturerID" });
            DropIndex("FRP.BankAccount", new[] { "SupplierId" });
            DropIndex("FRP.OperationHistory", new[] { "AircraftId" });
            DropIndex("FRP.OperationHistory", new[] { "ImportCategoryId" });
            DropIndex("FRP.OperationHistory", new[] { "ExportCategoryId" });
            DropIndex("FRP.OperationHistory", new[] { "ID" });
            DropIndex("FRP.ApprovalHistory", new[] { "RequestDeliverAnnualId" });
            DropIndex("FRP.Annual", new[] { "ProgrammingId" });
            DropIndex("FRP.ApprovalHistory", new[] { "PlanAircraftId" });
            DropIndex("FRP.PlanAircraft", new[] { "AirlinesId" });
            DropIndex("FRP.PlanAircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.PlanAircraft", new[] { "AircraftId" });
            DropIndex("FRP.ApprovalHistory", new[] { "ImportCategoryId" });
            DropIndex("FRP.ApprovalHistory", new[] { "AirlinesId" });
            DropIndex("FRP.OperationHistory", new[] { "AirlinesId" });
            DropIndex("FRP.AircraftLicense", new[] { "AircraftId" });
            DropIndex("FRP.Aircraft", new[] { "ImportCategoryId" });
            DropIndex("FRP.Aircraft", new[] { "AirlinesId" });
            DropIndex("FRP.Aircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftBusiness", new[] { "AircraftId" });
            DropIndex("FRP.AircraftBusiness", new[] { "ImportCategoryId" });
            DropIndex("FRP.AircraftBusiness", new[] { "AircraftTypeId" });
            DropIndex("FRP.AcConfigHistory", new[] { "AircraftId" });
            DropIndex("FRP.AircraftMaintainPlanDetail", new[] { "AircraftMaintainPlanId" });
            DropIndex("FRP.AircraftLicense", new[] { "LicenseTypeId" });
            DropIndex("FRP.AircraftConfiguration", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftType", new[] { "ManufacturerId" });
            DropIndex("FRP.AircraftType", new[] { "CaacAircraftTypeId" });
            DropIndex("FRP.CAACAircraftType", new[] { "ManufacturerId" });
            DropIndex("FRP.CAACAircraftType", new[] { "AircraftSeriesId" });
            DropIndex("FRP.CAACAircraftType", new[] { "AircraftCategoryId" });
            DropIndex("FRP.AircraftType", new[] { "AircraftSeriesId" });
            DropIndex("FRP.AircraftType", new[] { "AircraftCategoryId" });
            DropIndex("FRP.AircraftConfiguration", new[] { "AircraftSeriesId" });
            DropIndex("FRP.AircraftSeries", new[] { "ManufacturerId" });
            DropIndex("FRP.Ata", new[] { "AircraftSeriesId" });
            DropIndex("FRP.Ata", new[] { "ParentAta_Id" });
            DropIndex("FRP.Ata", new[] { "ParentId" });
            DropIndex("FRP.AircraftCabin", new[] { "AircraftConfiguratonId" });
            DropIndex("FRP.AircraftCabin", new[] { "AircraftCabinTypeId" });
            DropTable("FRP.FhaMaintainCost");
            DropTable("FRP.ApuMaintainCost");
            DropTable("FRP.NonFhaMaintainCost");
            DropTable("FRP.SpecialRefitMaintainCost");
            DropTable("FRP.UndercartMaintainCost");
            DropTable("FRP.RegularCheckMaintainCost");
            DropTable("FRP.OtherSupplier");
            DropTable("FRP.MaintainSupplier");
            DropTable("FRP.EnginePurchaseSupplier");
            DropTable("FRP.EngineLeaseSupplier");
            DropTable("FRP.BFEPurchaseSupplier");
            DropTable("FRP.AircraftPurchaseSupplier");
            DropTable("FRP.AircraftLeaseSupplier");
            DropTable("FRP.APUReg");
            DropTable("FRP.EngineReg");
            DropTable("FRP.EnginePurchaseReceptionLine");
            DropTable("FRP.EnginePurchaseReception");
            DropTable("FRP.EngineLeaseReceptionLine");
            DropTable("FRP.EngineLeaseReception");
            DropTable("FRP.AircraftPurchaseReceptionLine");
            DropTable("FRP.AircraftPurchaseReception");
            DropTable("FRP.AircraftLeaseReceptionLine");
            DropTable("FRP.AircraftLeaseReception");
            DropTable("FRP.MaintainPaymentSchedule");
            DropTable("FRP.StandardPaymentSchedule");
            DropTable("FRP.EnginePaymentSchedule");
            DropTable("FRP.AircraftPaymentSchedule");
            DropTable("FRP.EnginePurchaseOrderLine");
            DropTable("FRP.EnginePurchaseOrder");
            DropTable("FRP.EngineLeaseOrderLine");
            DropTable("FRP.EngineLeaseOrder");
            DropTable("FRP.BFEPurchaseOrderLine");
            DropTable("FRP.BFEPurchaseOrder");
            DropTable("FRP.AircraftPurchaseOrderLine");
            DropTable("FRP.AircraftPurchaseOrder");
            DropTable("FRP.AircraftLeaseOrderLine");
            DropTable("FRP.AircraftLeaseOrder");
            DropTable("FRP.EngineMaterial");
            DropTable("FRP.BFEMaterial");
            DropTable("FRP.AircraftMaterial");
            DropTable("FRP.MaintainInvoiceLine");
            DropTable("FRP.UndercartMaintainInvoice");
            DropTable("FRP.EngineMaintainInvoice");
            DropTable("FRP.APUMaintainInvoice");
            DropTable("FRP.AirframeMaintainInvoice");
            DropTable("FRP.SnMaintainCtrl");
            DropTable("FRP.PnMaintainCtrl");
            DropTable("FRP.ItemMaintainCtrl");
            DropTable("FRP.AirframeMaintainContract");
            DropTable("FRP.UndercartMaintainContract");
            DropTable("FRP.EngineMaintainContract");
            DropTable("FRP.APUMaintainContract");
            DropTable("FRP.SpecialRefitInvoice");
            DropTable("FRP.SundryInvoice");
            DropTable("FRP.PurchaseInvoiceLine");
            DropTable("FRP.MaintainPrepaymentInvoice");
            DropTable("FRP.PurchasePrepaymentInvoice");
            DropTable("FRP.PurchaseInvoice");
            DropTable("FRP.LeaseInvoice");
            DropTable("FRP.MaintainCreditNoteInvoice");
            DropTable("FRP.PurchaseCreditNoteInvoice");
            DropTable("FRP.MaintainGuarantee");
            DropTable("FRP.LeaseGuarantee");
            DropTable("FRP.StandardDocument");
            DropTable("FRP.OfficialDocument");
            DropTable("FRP.PurchaseContractEngine");
            DropTable("FRP.LeaseContractEngine");
            DropTable("FRP.PurchaseContractAircraft");
            DropTable("FRP.LeaseContractAircraft");
            DropTable("FRP.OperationPlan");
            DropTable("FRP.ChangePlan");
            DropTable("FRP.SpecialConfig");
            DropTable("FRP.MaintainInvoice");
            DropTable("FRP.EngineMaintainPlan");
            DropTable("FRP.BasicConfig");
            DropTable("FRP.BasePurchaseInvoice");
            DropTable("FRP.AircraftMaintainPlan");
            DropTable("FRP.ContentTag");
            DropTable("FRP.PlanHistory");
            DropTable("FRP.XmlSetting");
            DropTable("FRP.XmlConfig");
            DropTable("FRP.User");
            DropTable("FRP.UserRole");
            DropTable("FRP.Threshold");
            DropTable("FRP.Member");
            DropTable("FRP.WorkGroup");
            DropTable("FRP.TaskCase");
            DropTable("FRP.TaskStandard");
            DropTable("FRP.SupplierRole");
            DropTable("FRP.Thrust");
            DropTable("FRP.LifeMonitor");
            DropTable("FRP.SnReg");
            DropTable("FRP.SnRemInstRecord");
            DropTable("FRP.SnHistory");
            DropTable("FRP.ApplicableAircraft");
            DropTable("FRP.Scn");
            DropTable("FRP.Role");
            DropTable("FRP.RoleFunction");
            DropTable("FRP.Request");
            DropTable("FRP.RelatedDoc");
            DropTable("FRP.Reception");
            DropTable("FRP.ReceptionSchedule");
            DropTable("FRP.ReceptionLine");
            DropTable("FRP.TaskTemp");
            DropTable("FRP.ProjectTemp");
            DropTable("FRP.Task");
            DropTable("FRP.Project");
            DropTable("FRP.ProgrammingFile");
            DropTable("FRP.AircraftPlan");
            DropTable("FRP.PaymentSchedule");
            DropTable("FRP.PaymentNoticeLine");
            DropTable("FRP.PaymentNotice");
            DropTable("FRP.OrganizationUser");
            DropTable("FRP.Organization");
            DropTable("FRP.OrganizationRole");
            DropTable("FRP.OilMonitor");
            DropTable("FRP.Mod");
            DropTable("FRP.MaintainWork");
            DropTable("FRP.MaintainCtrl");
            DropTable("FRP.MaintainCost");
            DropTable("FRP.MaintainContract");
            DropTable("FRP.MailAddress");
            DropTable("FRP.PnReg");
            DropTable("FRP.Item");
            DropTable("FRP.Dependency");
            DropTable("FRP.InstallController");
            DropTable("FRP.Guarantee");
            DropTable("FRP.FunctionItem");
            DropTable("FRP.FlightLog");
            DropTable("FRP.EngineOwnershipHistory");
            DropTable("FRP.EngineBusinessHistory");
            DropTable("FRP.Engine");
            DropTable("FRP.PlanEngine");
            DropTable("FRP.EngineType");
            DropTable("FRP.EnginePlanHistory");
            DropTable("FRP.EnginePlan");
            DropTable("FRP.EngineMaintainPlanDetail");
            DropTable("FRP.Document");
            DropTable("FRP.DocumentType");
            DropTable("FRP.DocumentPath");
            DropTable("FRP.CtrlUnit");
            DropTable("FRP.Trade");
            DropTable("FRP.Part");
            DropTable("FRP.ContractEngine");
            DropTable("FRP.ContractAircraft");
            DropTable("FRP.OrderLine");
            DropTable("FRP.Linkman");
            DropTable("FRP.Forwarder");
            DropTable("FRP.ContractContent");
            DropTable("FRP.Order");
            DropTable("FRP.ContractAircraftBFE");
            DropTable("FRP.CaacProgrammingLine");
            DropTable("FRP.CaacProgramming");
            DropTable("FRP.BusinessLicense");
            DropTable("FRP.AcConfig");
            DropTable("FRP.BasicConfigHistory");
            DropTable("FRP.BasicConfigGroup");
            DropTable("FRP.PaymentScheduleLine");
            DropTable("FRP.InvoiceLine");
            DropTable("FRP.Currency");
            DropTable("FRP.Invoice");
            DropTable("FRP.Manager");
            DropTable("FRP.ApprovalDoc");
            DropTable("FRP.AirStructureDamage");
            DropTable("FRP.IssuedUnit");
            DropTable("FRP.AirProgrammingLine");
            DropTable("FRP.AirProgramming");
            DropTable("FRP.Material");
            DropTable("FRP.SupplierCompanyMaterial");
            DropTable("FRP.SupplierCompany");
            DropTable("FRP.BankAccount");
            DropTable("FRP.Supplier");
            DropTable("FRP.OwnershipHistory");
            DropTable("FRP.Programming");
            DropTable("FRP.Annual");
            DropTable("FRP.PlanAircraft");
            DropTable("FRP.ApprovalHistory");
            DropTable("FRP.OperationHistory");
            DropTable("FRP.Airlines");
            DropTable("FRP.AircraftBusiness");
            DropTable("FRP.AcConfigHistory");
            DropTable("FRP.Aircraft");
            DropTable("FRP.AircraftMaintainPlanDetail");
            DropTable("FRP.AnnualMaintainPlan");
            DropTable("FRP.LicenseType");
            DropTable("FRP.AircraftLicense");
            DropTable("FRP.CAACAircraftType");
            DropTable("FRP.AircraftType");
            DropTable("FRP.Manufacturer");
            DropTable("FRP.Ata");
            DropTable("FRP.AircraftSeries");
            DropTable("FRP.AircraftConfiguration");
            DropTable("FRP.AircraftCategory");
            DropTable("FRP.AircraftCabinType");
            DropTable("FRP.AircraftCabin");
            DropTable("FRP.AirBusScn");
            DropTable("FRP.AdSb");
            DropTable("FRP.ActionCategory");
            DropTable("FRP.AcDailyUtilization");
        }
    }
}
