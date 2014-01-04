namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FRP.ActionCategory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActionType = c.String(),
                        ActionName = c.String(),
                        NeedRequest = c.Boolean(nullable: false),
                        NetIncrement = c.Int(nullable: false),
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
                "FRP.Aircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegNumber = c.String(),
                        SerialNumber = c.String(),
                        IsOperation = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FactoryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ImportDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                "FRP.AircraftType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ManufacturerId = c.Guid(nullable: false),
                        AircraftCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCategory", t => t.AircraftCategoryId)
                .ForeignKey("FRP.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.AircraftCategoryId)
                .Index(t => t.ManufacturerId);
            
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
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StopDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TechReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReceiptDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TechDeliveryDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OnHireDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                        AircraftId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                        ExportCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ActionCategory", t => t.ExportCategoryId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Aircraft", t => t.AircraftId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ExportCategoryId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.AircraftId);
            
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
                "FRP.Part",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        IssuedUnitId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manager", t => t.IssuedUnitId)
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
                        AcTypeId = c.Guid(nullable: false),
                        AircraftCategoryId = c.Guid(nullable: false),
                        AirProgrammingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftCategory", t => t.AircraftCategoryId)
                .ForeignKey("FRP.AirProgramming", t => t.AirProgrammingId)
                .Index(t => t.AircraftCategoryId)
                .Index(t => t.AirProgrammingId);
            
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
                        DispatchUnitId = c.Guid(nullable: false),
                        CaacDocumentId = c.Guid(),
                        NdrcDocumentId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manager", t => t.DispatchUnitId)
                .Index(t => t.DispatchUnitId);
            
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
                        IssuedUnitId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Manager", t => t.IssuedUnitId)
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
                        Note = c.String(),
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
                "FRP.Trade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
                "FRP.DocumentPath",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsLeaf = c.Boolean(nullable: false),
                        Extension = c.String(),
                        DocumentGuid = c.Guid(),
                        PathSource = c.Int(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.DocumentPath", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "FRP.Document",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FileStorage = c.Binary(),
                        Abstract = c.String(),
                        Note = c.String(),
                        Uploader = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        IsFinished = c.Boolean(nullable: false),
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
                "FRP.InvoiceLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        InvoiceId = c.Int(nullable: false),
                        OrderLineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.OrderLineId)
                .ForeignKey("FRP.Invoice", t => t.InvoiceId)
                .Index(t => t.OrderLineId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "FRP.Invoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
                        OrderId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        PaymentScheduleLineId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Order", t => t.OrderId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.CurrencyId)
                .Index(t => t.OrderId)
                .Index(t => t.SupplierId);
            
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
                "FRP.MaintainInvoiceLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaintainItem = c.Int(nullable: false),
                        ItemName = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        MaintainInvoiceId = c.Int(nullable: false),
                        PartID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Part", t => t.PartID)
                .ForeignKey("FRP.MaintainInvoice", t => t.MaintainInvoiceId)
                .Index(t => t.PartID)
                .Index(t => t.MaintainInvoiceId);
            
            CreateTable(
                "FRP.MaintainInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
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
                        DocumentName = c.String(),
                        DocumentId = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Currency", t => t.CurrencyId)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.CurrencyId)
                .Index(t => t.SupplierId);
            
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.InvoiceId)
                .ForeignKey("FRP.PaymentSchedule", t => t.PaymentScheduleId)
                .Index(t => t.InvoiceId)
                .Index(t => t.PaymentScheduleId);
            
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
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.Annual", t => t.AnnualId)
                .Index(t => t.AirlinesId)
                .Index(t => t.AnnualId);
            
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
                        PlanAircraftId = c.Guid(),
                        PlanId = c.Guid(nullable: false),
                        ActionCategoryId = c.Guid(nullable: false),
                        TargetCategoryId = c.Guid(nullable: false),
                        AircraftTypeId = c.Guid(nullable: false),
                        AirlinesId = c.Guid(nullable: false),
                        PerformAnnualId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ActionCategoryId)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.Annual", t => t.PerformAnnualId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftId)
                .ForeignKey("FRP.ActionCategory", t => t.TargetCategoryId)
                .ForeignKey("FRP.AircraftPlan", t => t.PlanId)
                .Index(t => t.ActionCategoryId)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.AirlinesId)
                .Index(t => t.PerformAnnualId)
                .Index(t => t.PlanAircraftId)
                .Index(t => t.TargetCategoryId)
                .Index(t => t.PlanId);
            
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
                        RaDocNumber = c.String(),
                        SawsDocNumber = c.String(),
                        CaacDocNumber = c.String(),
                        Status = c.Int(nullable: false),
                        CaacNote = c.String(),
                        RaNote = c.String(),
                        SawsNote = c.String(),
                        ApprovalDocId = c.Guid(),
                        RaDocumentId = c.Guid(),
                        SawsDocumentId = c.Guid(),
                        CaacDocumentId = c.Guid(),
                        AirlinesId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Airlines", t => t.AirlinesId)
                .ForeignKey("FRP.ApprovalDoc", t => t.ApprovalDocId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ApprovalDocId);
            
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
                "FRP.ContentTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.ChangePlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AircraftBusinessId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PlanHistory", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.OperationPlan",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OperationHistoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PlanHistory", t => t.ID)
                .Index(t => t.ID);
            
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
                "FRP.CreditNoteInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.LeaseInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PurchaseInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FRP.PrepaymentInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Invoice", t => t.ID)
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
                        PartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.ID)
                .ForeignKey("FRP.Part", t => t.PartID)
                .Index(t => t.ID)
                .Index(t => t.PartID);
            
            CreateTable(
                "FRP.EngineMaterial",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Material", t => t.ID)
                .ForeignKey("FRP.Part", t => t.PartID)
                .Index(t => t.ID)
                .Index(t => t.PartID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("FRP.MaintainSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.EnginePurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.EngineLeaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.BFEPurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.AircraftPurchaseSupplier", "ID", "FRP.SupplierRole");
            DropForeignKey("FRP.AircraftLeaseSupplier", "ID", "FRP.SupplierRole");
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
            DropForeignKey("FRP.EngineMaterial", "PartID", "FRP.Part");
            DropForeignKey("FRP.EngineMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.BFEMaterial", "PartID", "FRP.Part");
            DropForeignKey("FRP.BFEMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.AircraftMaterial", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.UndercartMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.EngineMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.APUMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.AirframeMaintainInvoice", "ID", "FRP.MaintainInvoice");
            DropForeignKey("FRP.UndercartMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.EngineMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.APUMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.PrepaymentInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.PurchaseInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.LeaseInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.CreditNoteInvoice", "ID", "FRP.Invoice");
            DropForeignKey("FRP.MaintainGuarantee", "ID", "FRP.Guarantee");
            DropForeignKey("FRP.LeaseGuarantee", "ID", "FRP.Guarantee");
            DropForeignKey("FRP.StandardDocument", "ID", "FRP.Document");
            DropForeignKey("FRP.OfficialDocument", "ID", "FRP.Document");
            DropForeignKey("FRP.PurchaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.LeaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.PurchaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.LeaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.OperationPlan", "ID", "FRP.PlanHistory");
            DropForeignKey("FRP.ChangePlan", "ID", "FRP.PlanHistory");
            DropForeignKey("FRP.SupplierRole", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.ApprovalHistory", "RequestId", "FRP.Request");
            DropForeignKey("FRP.ApprovalHistory", "RequestDeliverAnnualId", "FRP.Annual");
            DropForeignKey("FRP.ApprovalHistory", "PlanAircraftId", "FRP.PlanAircraft");
            DropForeignKey("FRP.ApprovalHistory", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ApprovalHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Request", "ApprovalDocId", "FRP.ApprovalDoc");
            DropForeignKey("FRP.Request", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Reception", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ReceptionSchedule", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.ReceptionLine", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.PlanHistory", "PlanId", "FRP.AircraftPlan");
            DropForeignKey("FRP.PlanHistory", "TargetCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.PlanHistory", "PlanAircraftId", "FRP.PlanAircraft");
            DropForeignKey("FRP.PlanHistory", "PerformAnnualId", "FRP.Annual");
            DropForeignKey("FRP.PlanHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PlanHistory", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.PlanHistory", "ActionCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.AircraftPlan", "AnnualId", "FRP.Annual");
            DropForeignKey("FRP.AircraftPlan", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PaymentSchedule", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentScheduleLine", "PaymentScheduleId", "FRP.PaymentSchedule");
            DropForeignKey("FRP.PaymentScheduleLine", "InvoiceId", "FRP.Invoice");
            DropForeignKey("FRP.PaymentSchedule", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.PaymentNotice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentNoticeLine", "PaymentNoticeId", "FRP.PaymentNotice");
            DropForeignKey("FRP.PaymentNotice", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.PaymentNotice", "BankAccountId", "FRP.BankAccount");
            DropForeignKey("FRP.MaintainInvoice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.MaintainInvoiceLine", "MaintainInvoiceId", "FRP.MaintainInvoice");
            DropForeignKey("FRP.MaintainInvoiceLine", "PartID", "FRP.Part");
            DropForeignKey("FRP.MaintainInvoice", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.MaintainContract", "SignatoryId", "FRP.Supplier");
            DropForeignKey("FRP.Invoice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Invoice", "OrderId", "FRP.Order");
            DropForeignKey("FRP.InvoiceLine", "InvoiceId", "FRP.Invoice");
            DropForeignKey("FRP.InvoiceLine", "OrderLineId", "FRP.OrderLine");
            DropForeignKey("FRP.Invoice", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.Guarantee", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Guarantee", "CurrencyId", "FRP.Currency");
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
            DropForeignKey("FRP.PlanAircraft", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.PlanAircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.PlanAircraft", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.CaacProgramming", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.CaacProgramming", "IssuedUnitId", "FRP.Manager");
            DropForeignKey("FRP.CaacProgrammingLine", "CaacProgrammingId", "FRP.CaacProgramming");
            DropForeignKey("FRP.CaacProgrammingLine", "AircraftCategoryId", "FRP.AircraftCategory");
            DropForeignKey("FRP.ApprovalDoc", "DispatchUnitId", "FRP.Manager");
            DropForeignKey("FRP.Annual", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.AirProgramming", "ProgrammingId", "FRP.Programming");
            DropForeignKey("FRP.AirProgramming", "IssuedUnitId", "FRP.Manager");
            DropForeignKey("FRP.AirProgrammingLine", "AirProgrammingId", "FRP.AirProgramming");
            DropForeignKey("FRP.AirProgrammingLine", "AircraftCategoryId", "FRP.AircraftCategory");
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
            DropForeignKey("FRP.OperationHistory", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Aircraft", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.Aircraft", "AirlinesId", "FRP.Airlines");
            DropForeignKey("FRP.Aircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftBusiness", "AircraftId", "FRP.Aircraft");
            DropForeignKey("FRP.AircraftBusiness", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.AircraftBusiness", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftType", "ManufacturerId", "FRP.Manufacturer");
            DropForeignKey("FRP.AircraftType", "AircraftCategoryId", "FRP.AircraftCategory");
            DropIndex("FRP.MaintainSupplier", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.EngineLeaseSupplier", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseSupplier", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseSupplier", new[] { "ID" });
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
            DropIndex("FRP.EngineMaterial", new[] { "PartID" });
            DropIndex("FRP.EngineMaterial", new[] { "ID" });
            DropIndex("FRP.BFEMaterial", new[] { "PartID" });
            DropIndex("FRP.BFEMaterial", new[] { "ID" });
            DropIndex("FRP.AircraftMaterial", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftMaterial", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.EngineMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.APUMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.AirframeMaintainInvoice", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainContract", new[] { "ID" });
            DropIndex("FRP.EngineMaintainContract", new[] { "ID" });
            DropIndex("FRP.APUMaintainContract", new[] { "ID" });
            DropIndex("FRP.PrepaymentInvoice", new[] { "ID" });
            DropIndex("FRP.PurchaseInvoice", new[] { "ID" });
            DropIndex("FRP.LeaseInvoice", new[] { "ID" });
            DropIndex("FRP.CreditNoteInvoice", new[] { "ID" });
            DropIndex("FRP.MaintainGuarantee", new[] { "ID" });
            DropIndex("FRP.LeaseGuarantee", new[] { "ID" });
            DropIndex("FRP.StandardDocument", new[] { "ID" });
            DropIndex("FRP.OfficialDocument", new[] { "ID" });
            DropIndex("FRP.PurchaseContractEngine", new[] { "ID" });
            DropIndex("FRP.LeaseContractEngine", new[] { "ID" });
            DropIndex("FRP.PurchaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.LeaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.OperationPlan", new[] { "ID" });
            DropIndex("FRP.ChangePlan", new[] { "ID" });
            DropIndex("FRP.SupplierRole", new[] { "SupplierCompanyId" });
            DropIndex("FRP.ApprovalHistory", new[] { "RequestId" });
            DropIndex("FRP.ApprovalHistory", new[] { "RequestDeliverAnnualId" });
            DropIndex("FRP.ApprovalHistory", new[] { "PlanAircraftId" });
            DropIndex("FRP.ApprovalHistory", new[] { "ImportCategoryId" });
            DropIndex("FRP.ApprovalHistory", new[] { "AirlinesId" });
            DropIndex("FRP.Request", new[] { "ApprovalDocId" });
            DropIndex("FRP.Request", new[] { "AirlinesId" });
            DropIndex("FRP.Reception", new[] { "SupplierId" });
            DropIndex("FRP.ReceptionSchedule", new[] { "ReceptionId" });
            DropIndex("FRP.ReceptionLine", new[] { "ReceptionId" });
            DropIndex("FRP.PlanHistory", new[] { "PlanId" });
            DropIndex("FRP.PlanHistory", new[] { "TargetCategoryId" });
            DropIndex("FRP.PlanHistory", new[] { "PlanAircraftId" });
            DropIndex("FRP.PlanHistory", new[] { "PerformAnnualId" });
            DropIndex("FRP.PlanHistory", new[] { "AirlinesId" });
            DropIndex("FRP.PlanHistory", new[] { "AircraftTypeId" });
            DropIndex("FRP.PlanHistory", new[] { "ActionCategoryId" });
            DropIndex("FRP.AircraftPlan", new[] { "AnnualId" });
            DropIndex("FRP.AircraftPlan", new[] { "AirlinesId" });
            DropIndex("FRP.PaymentSchedule", new[] { "SupplierId" });
            DropIndex("FRP.PaymentScheduleLine", new[] { "PaymentScheduleId" });
            DropIndex("FRP.PaymentScheduleLine", new[] { "InvoiceId" });
            DropIndex("FRP.PaymentSchedule", new[] { "CurrencyId" });
            DropIndex("FRP.PaymentNotice", new[] { "SupplierId" });
            DropIndex("FRP.PaymentNoticeLine", new[] { "PaymentNoticeId" });
            DropIndex("FRP.PaymentNotice", new[] { "CurrencyId" });
            DropIndex("FRP.PaymentNotice", new[] { "BankAccountId" });
            DropIndex("FRP.MaintainInvoice", new[] { "SupplierId" });
            DropIndex("FRP.MaintainInvoiceLine", new[] { "MaintainInvoiceId" });
            DropIndex("FRP.MaintainInvoiceLine", new[] { "PartID" });
            DropIndex("FRP.MaintainInvoice", new[] { "CurrencyId" });
            DropIndex("FRP.MaintainContract", new[] { "SignatoryId" });
            DropIndex("FRP.Invoice", new[] { "SupplierId" });
            DropIndex("FRP.Invoice", new[] { "OrderId" });
            DropIndex("FRP.InvoiceLine", new[] { "InvoiceId" });
            DropIndex("FRP.InvoiceLine", new[] { "OrderLineId" });
            DropIndex("FRP.Invoice", new[] { "CurrencyId" });
            DropIndex("FRP.Guarantee", new[] { "SupplierId" });
            DropIndex("FRP.Guarantee", new[] { "CurrencyId" });
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
            DropIndex("FRP.PlanAircraft", new[] { "AirlinesId" });
            DropIndex("FRP.PlanAircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.PlanAircraft", new[] { "AircraftId" });
            DropIndex("FRP.CaacProgramming", new[] { "ProgrammingId" });
            DropIndex("FRP.CaacProgramming", new[] { "IssuedUnitId" });
            DropIndex("FRP.CaacProgrammingLine", new[] { "CaacProgrammingId" });
            DropIndex("FRP.CaacProgrammingLine", new[] { "AircraftCategoryId" });
            DropIndex("FRP.ApprovalDoc", new[] { "DispatchUnitId" });
            DropIndex("FRP.Annual", new[] { "ProgrammingId" });
            DropIndex("FRP.AirProgramming", new[] { "ProgrammingId" });
            DropIndex("FRP.AirProgramming", new[] { "IssuedUnitId" });
            DropIndex("FRP.AirProgrammingLine", new[] { "AirProgrammingId" });
            DropIndex("FRP.AirProgrammingLine", new[] { "AircraftCategoryId" });
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
            DropIndex("FRP.OperationHistory", new[] { "AirlinesId" });
            DropIndex("FRP.Aircraft", new[] { "ImportCategoryId" });
            DropIndex("FRP.Aircraft", new[] { "AirlinesId" });
            DropIndex("FRP.Aircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftBusiness", new[] { "AircraftId" });
            DropIndex("FRP.AircraftBusiness", new[] { "ImportCategoryId" });
            DropIndex("FRP.AircraftBusiness", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftType", new[] { "ManufacturerId" });
            DropIndex("FRP.AircraftType", new[] { "AircraftCategoryId" });
            DropTable("FRP.MaintainSupplier");
            DropTable("FRP.EnginePurchaseSupplier");
            DropTable("FRP.EngineLeaseSupplier");
            DropTable("FRP.BFEPurchaseSupplier");
            DropTable("FRP.AircraftPurchaseSupplier");
            DropTable("FRP.AircraftLeaseSupplier");
            DropTable("FRP.EnginePurchaseReceptionLine");
            DropTable("FRP.EnginePurchaseReception");
            DropTable("FRP.EngineLeaseReceptionLine");
            DropTable("FRP.EngineLeaseReception");
            DropTable("FRP.AircraftPurchaseReceptionLine");
            DropTable("FRP.AircraftPurchaseReception");
            DropTable("FRP.AircraftLeaseReceptionLine");
            DropTable("FRP.AircraftLeaseReception");
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
            DropTable("FRP.UndercartMaintainInvoice");
            DropTable("FRP.EngineMaintainInvoice");
            DropTable("FRP.APUMaintainInvoice");
            DropTable("FRP.AirframeMaintainInvoice");
            DropTable("FRP.UndercartMaintainContract");
            DropTable("FRP.EngineMaintainContract");
            DropTable("FRP.APUMaintainContract");
            DropTable("FRP.PrepaymentInvoice");
            DropTable("FRP.PurchaseInvoice");
            DropTable("FRP.LeaseInvoice");
            DropTable("FRP.CreditNoteInvoice");
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
            DropTable("FRP.ContentTag");
            DropTable("FRP.XmlSetting");
            DropTable("FRP.XmlConfig");
            DropTable("FRP.SupplierRole");
            DropTable("FRP.ApprovalHistory");
            DropTable("FRP.Request");
            DropTable("FRP.RelatedDoc");
            DropTable("FRP.Reception");
            DropTable("FRP.ReceptionSchedule");
            DropTable("FRP.ReceptionLine");
            DropTable("FRP.PlanHistory");
            DropTable("FRP.AircraftPlan");
            DropTable("FRP.PaymentSchedule");
            DropTable("FRP.PaymentScheduleLine");
            DropTable("FRP.PaymentNoticeLine");
            DropTable("FRP.PaymentNotice");
            DropTable("FRP.MaintainInvoice");
            DropTable("FRP.MaintainInvoiceLine");
            DropTable("FRP.MaintainContract");
            DropTable("FRP.MailAddress");
            DropTable("FRP.Invoice");
            DropTable("FRP.InvoiceLine");
            DropTable("FRP.Guarantee");
            DropTable("FRP.EngineOwnershipHistory");
            DropTable("FRP.EngineBusinessHistory");
            DropTable("FRP.Engine");
            DropTable("FRP.PlanEngine");
            DropTable("FRP.EngineType");
            DropTable("FRP.EnginePlanHistory");
            DropTable("FRP.EnginePlan");
            DropTable("FRP.Document");
            DropTable("FRP.DocumentPath");
            DropTable("FRP.Trade");
            DropTable("FRP.ContractEngine");
            DropTable("FRP.PlanAircraft");
            DropTable("FRP.ContractAircraft");
            DropTable("FRP.OrderLine");
            DropTable("FRP.Linkman");
            DropTable("FRP.Forwarder");
            DropTable("FRP.Currency");
            DropTable("FRP.ContractContent");
            DropTable("FRP.Order");
            DropTable("FRP.ContractAircraftBFE");
            DropTable("FRP.CaacProgrammingLine");
            DropTable("FRP.CaacProgramming");
            DropTable("FRP.ApprovalDoc");
            DropTable("FRP.Annual");
            DropTable("FRP.Programming");
            DropTable("FRP.Manager");
            DropTable("FRP.AirProgrammingLine");
            DropTable("FRP.AirProgramming");
            DropTable("FRP.Part");
            DropTable("FRP.Material");
            DropTable("FRP.SupplierCompanyMaterial");
            DropTable("FRP.SupplierCompany");
            DropTable("FRP.BankAccount");
            DropTable("FRP.Supplier");
            DropTable("FRP.OwnershipHistory");
            DropTable("FRP.OperationHistory");
            DropTable("FRP.Airlines");
            DropTable("FRP.Manufacturer");
            DropTable("FRP.AircraftType");
            DropTable("FRP.AircraftBusiness");
            DropTable("FRP.Aircraft");
            DropTable("FRP.AircraftCategory");
            DropTable("FRP.ActionCategory");
        }
    }
}
