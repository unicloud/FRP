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
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.Aircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AircraftReg = c.String(),
                        AircraftTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .Index(t => t.AircraftTypeId);
            
            CreateTable(
                "FRP.AircraftType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "FRP.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Version = c.Int(nullable: false),
                        Name = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OperatorName = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrderDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RepealDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        ContractDocGuid = c.Guid(nullable: false),
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
                        ContentDoc = c.Binary(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Order", t => t.OrderId)
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
                .ForeignKey("FRP.Order", t => t.OrderId)
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
                        AircraftTypeId = c.Guid(nullable: false),
                        PlanAircraftID = c.Guid(),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftID)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PlanAircraftID);
            
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
                        PartID = c.Int(nullable: false),
                        ImportCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Part", t => t.PartID)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PartID);
            
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
                "FRP.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SupplierType = c.Int(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsValid = c.Boolean(nullable: false),
                        Note = c.String(),
                        SupplierCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.SupplierCompany", t => t.SupplierCompanyId)
                .Index(t => t.SupplierCompanyId);
            
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
                "FRP.Manufacturer",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CnName = c.String(),
                        EnName = c.String(),
                        CnShortName = c.String(),
                        EnShortName = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "FRP.PlanAircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegNumber = c.String(),
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
                "FRP.ReceptionLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReceivedAmount = c.Int(nullable: false),
                        AcceptedAmount = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Note = c.String(),
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
                        Subject = c.String(),
                        Body = c.String(),
                        Importance = c.String(),
                        Tempo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Location = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Url = c.Boolean(nullable: false),
                        UniqueId = c.String(),
                        Tempo1 = c.String(),
                        Location1 = c.String(),
                        UniqueId1 = c.String(),
                        Url1 = c.String(),
                        ReceptionId = c.Int(nullable: false),
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
                        CloseDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
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
                "FRP.ContentTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FRP.ContractAircraftBFE",
                c => new
                    {
                        BFEPurchaseOrder_Id = c.Int(nullable: false),
                        ContractAircraft_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BFEPurchaseOrder_Id, t.ContractAircraft_Id })
                .ForeignKey("FRP.BFEPurchaseOrder", t => t.BFEPurchaseOrder_Id, cascadeDelete: true)
                .ForeignKey("FRP.ContractAircraft", t => t.ContractAircraft_Id, cascadeDelete: true)
                .Index(t => t.BFEPurchaseOrder_Id)
                .Index(t => t.ContractAircraft_Id);
            
            CreateTable(
                "FRP.SupplierCompanyMaterial",
                c => new
                    {
                        SupplierCompany_Id = c.Int(nullable: false),
                        Material_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierCompany_Id, t.Material_Id })
                .ForeignKey("FRP.SupplierCompany", t => t.SupplierCompany_Id, cascadeDelete: true)
                .ForeignKey("FRP.Material", t => t.Material_Id, cascadeDelete: true)
                .Index(t => t.SupplierCompany_Id)
                .Index(t => t.Material_Id);
            
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
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.LeaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
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
                        ContractAircraftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.PurchaseContractAircraft", t => t.ContractAircraftId)
                .Index(t => t.ID)
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .Index(t => t.ID);
            
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
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.LeaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
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
                        ContractEngineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.OrderLine", t => t.ID)
                .ForeignKey("FRP.PurchaseContractEngine", t => t.ContractEngineId)
                .Index(t => t.ID)
                .Index(t => t.ContractEngineId);
            
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
            
        }
        
        public override void Down()
        {
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
            DropForeignKey("FRP.EnginePurchaseOrderLine", "ContractEngineId", "FRP.PurchaseContractEngine");
            DropForeignKey("FRP.EnginePurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.EnginePurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.EngineLeaseOrderLine", "ContractEngineId", "FRP.LeaseContractEngine");
            DropForeignKey("FRP.EngineLeaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.EngineLeaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.BFEPurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.BFEPurchaseOrder", "ForwarderId", "FRP.Forwarder");
            DropForeignKey("FRP.BFEPurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.AircraftPurchaseOrderLine", "ContractAircraftId", "FRP.PurchaseContractAircraft");
            DropForeignKey("FRP.AircraftPurchaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.AircraftPurchaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.AircraftLeaseOrderLine", "ContractAircraftId", "FRP.LeaseContractAircraft");
            DropForeignKey("FRP.AircraftLeaseOrderLine", "ID", "FRP.OrderLine");
            DropForeignKey("FRP.AircraftLeaseOrder", "ID", "FRP.Order");
            DropForeignKey("FRP.EngineMaterial", "PartID", "FRP.Part");
            DropForeignKey("FRP.EngineMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.BFEMaterial", "PartID", "FRP.Part");
            DropForeignKey("FRP.BFEMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.AircraftMaterial", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.AircraftMaterial", "ID", "FRP.Material");
            DropForeignKey("FRP.UndercartMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.EngineMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.APUMaintainContract", "ID", "FRP.MaintainContract");
            DropForeignKey("FRP.PurchaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.LeaseContractEngine", "ID", "FRP.ContractEngine");
            DropForeignKey("FRP.PurchaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.LeaseContractAircraft", "ID", "FRP.ContractAircraft");
            DropForeignKey("FRP.SupplierRole", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.Reception", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ReceptionSchedule", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.ReceptionLine", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.MaintainContract", "SignatoryId", "FRP.Supplier");
            DropForeignKey("FRP.ContractEngine", "PartID", "FRP.Part");
            DropForeignKey("FRP.ContractEngine", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ContractAircraft", "PlanAircraftID", "FRP.PlanAircraft");
            DropForeignKey("FRP.ContractAircraft", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.Trade", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Supplier", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.SupplierCompanyMaterial", "Material_Id", "FRP.Material");
            DropForeignKey("FRP.SupplierCompanyMaterial", "SupplierCompany_Id", "FRP.SupplierCompany");
            DropForeignKey("FRP.Material", "ManufacturerID", "FRP.Manufacturer");
            DropForeignKey("FRP.BankAccount", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.Order", "TradeId", "FRP.Trade");
            DropForeignKey("FRP.OrderLine", "OrderId", "FRP.Order");
            DropForeignKey("FRP.Order", "LinkmanId", "FRP.Linkman");
            DropForeignKey("FRP.Order", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.ContractContent", "OrderId", "FRP.Order");
            DropForeignKey("FRP.ContractAircraftBFE", "ContractAircraft_Id", "FRP.ContractAircraft");
            DropForeignKey("FRP.ContractAircraftBFE", "BFEPurchaseOrder_Id", "FRP.BFEPurchaseOrder");
            DropForeignKey("FRP.ContractAircraft", "AircraftTypeId", "FRP.AircraftType");
            DropForeignKey("FRP.Aircraft", "AircraftTypeId", "FRP.AircraftType");
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
            DropIndex("FRP.EnginePurchaseOrderLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EnginePurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.EnginePurchaseOrder", new[] { "ID" });
            DropIndex("FRP.EngineLeaseOrderLine", new[] { "ContractEngineId" });
            DropIndex("FRP.EngineLeaseOrderLine", new[] { "ID" });
            DropIndex("FRP.EngineLeaseOrder", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.BFEPurchaseOrder", new[] { "ForwarderId" });
            DropIndex("FRP.BFEPurchaseOrder", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseOrderLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftPurchaseOrderLine", new[] { "ID" });
            DropIndex("FRP.AircraftPurchaseOrder", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseOrderLine", new[] { "ContractAircraftId" });
            DropIndex("FRP.AircraftLeaseOrderLine", new[] { "ID" });
            DropIndex("FRP.AircraftLeaseOrder", new[] { "ID" });
            DropIndex("FRP.EngineMaterial", new[] { "PartID" });
            DropIndex("FRP.EngineMaterial", new[] { "ID" });
            DropIndex("FRP.BFEMaterial", new[] { "PartID" });
            DropIndex("FRP.BFEMaterial", new[] { "ID" });
            DropIndex("FRP.AircraftMaterial", new[] { "AircraftTypeId" });
            DropIndex("FRP.AircraftMaterial", new[] { "ID" });
            DropIndex("FRP.UndercartMaintainContract", new[] { "ID" });
            DropIndex("FRP.EngineMaintainContract", new[] { "ID" });
            DropIndex("FRP.APUMaintainContract", new[] { "ID" });
            DropIndex("FRP.PurchaseContractEngine", new[] { "ID" });
            DropIndex("FRP.LeaseContractEngine", new[] { "ID" });
            DropIndex("FRP.PurchaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.LeaseContractAircraft", new[] { "ID" });
            DropIndex("FRP.SupplierRole", new[] { "SupplierCompanyId" });
            DropIndex("FRP.Reception", new[] { "SupplierId" });
            DropIndex("FRP.ReceptionSchedule", new[] { "ReceptionId" });
            DropIndex("FRP.ReceptionLine", new[] { "ReceptionId" });
            DropIndex("FRP.MaintainContract", new[] { "SignatoryId" });
            DropIndex("FRP.ContractEngine", new[] { "PartID" });
            DropIndex("FRP.ContractEngine", new[] { "ImportCategoryId" });
            DropIndex("FRP.ContractAircraft", new[] { "PlanAircraftID" });
            DropIndex("FRP.ContractAircraft", new[] { "ImportCategoryId" });
            DropIndex("FRP.Trade", new[] { "SupplierId" });
            DropIndex("FRP.Supplier", new[] { "SupplierCompanyId" });
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "Material_Id" });
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "SupplierCompany_Id" });
            DropIndex("FRP.Material", new[] { "ManufacturerID" });
            DropIndex("FRP.BankAccount", new[] { "SupplierId" });
            DropIndex("FRP.Order", new[] { "TradeId" });
            DropIndex("FRP.OrderLine", new[] { "OrderId" });
            DropIndex("FRP.Order", new[] { "LinkmanId" });
            DropIndex("FRP.Order", new[] { "CurrencyId" });
            DropIndex("FRP.ContractContent", new[] { "OrderId" });
            DropIndex("FRP.ContractAircraftBFE", new[] { "ContractAircraft_Id" });
            DropIndex("FRP.ContractAircraftBFE", new[] { "BFEPurchaseOrder_Id" });
            DropIndex("FRP.ContractAircraft", new[] { "AircraftTypeId" });
            DropIndex("FRP.Aircraft", new[] { "AircraftTypeId" });
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
            DropTable("FRP.UndercartMaintainContract");
            DropTable("FRP.EngineMaintainContract");
            DropTable("FRP.APUMaintainContract");
            DropTable("FRP.PurchaseContractEngine");
            DropTable("FRP.LeaseContractEngine");
            DropTable("FRP.PurchaseContractAircraft");
            DropTable("FRP.LeaseContractAircraft");
            DropTable("FRP.SupplierCompanyMaterial");
            DropTable("FRP.ContractAircraftBFE");
            DropTable("FRP.ContentTag");
            DropTable("FRP.SupplierRole");
            DropTable("FRP.Reception");
            DropTable("FRP.ReceptionSchedule");
            DropTable("FRP.ReceptionLine");
            DropTable("FRP.MaintainContract");
            DropTable("FRP.PlanAircraft");
            DropTable("FRP.Material");
            DropTable("FRP.Manufacturer");
            DropTable("FRP.SupplierCompany");
            DropTable("FRP.Supplier");
            DropTable("FRP.Trade");
            DropTable("FRP.Part");
            DropTable("FRP.ContractEngine");
            DropTable("FRP.ContractAircraft");
            DropTable("FRP.OrderLine");
            DropTable("FRP.Linkman");
            DropTable("FRP.Forwarder");
            DropTable("FRP.Currency");
            DropTable("FRP.ContractContent");
            DropTable("FRP.Order");
            DropTable("FRP.BankAccount");
            DropTable("FRP.AircraftType");
            DropTable("FRP.Aircraft");
            DropTable("FRP.ActionCategory");
        }
    }
}
