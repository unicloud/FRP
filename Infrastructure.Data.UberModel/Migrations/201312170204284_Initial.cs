namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "FRP.Part",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pn = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.AircraftType", t => t.AircraftTypeId)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.PlanAircraft", t => t.PlanAircraftID)
                .Index(t => t.AircraftTypeId)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PlanAircraftID);
            
            CreateTable(
                "FRP.PlanAircraft",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.ActionCategory", t => t.ImportCategoryId)
                .ForeignKey("FRP.Part", t => t.PartID)
                .Index(t => t.ImportCategoryId)
                .Index(t => t.PartID);
            
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
                        ReviewDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        ReviewDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                .ForeignKey("FRP.Invoice", t => t.InvoiceId)
                .ForeignKey("FRP.PaymentNotice", t => t.PaymentNoticeId)
                .Index(t => t.InvoiceId)
                .Index(t => t.PaymentNoticeId);
            
            CreateTable(
                "FRP.PaymentScheduleLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ScheduleDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                        PaymentScheduleId = c.Int(nullable: false),
                        InvoiceId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FRP.PaymentSchedule", t => t.PaymentScheduleId)
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
                        Subject = c.String(),
                        Body = c.String(),
                        Importance = c.String(),
                        Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        Group = c.String(),
                        Tempo = c.String(),
                        Location = c.String(),
                        UniqueId = c.String(),
                        Url = c.String(),
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
            DropForeignKey("FRP.SupplierRole", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.Reception", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ReceptionSchedule", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.ReceptionLine", "ReceptionId", "FRP.Reception");
            DropForeignKey("FRP.PaymentSchedule", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentScheduleLine", "PaymentScheduleId", "FRP.PaymentSchedule");
            DropForeignKey("FRP.PaymentSchedule", "CurrencyId", "FRP.Currency");
            DropForeignKey("FRP.PaymentNotice", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.PaymentNoticeLine", "PaymentNoticeId", "FRP.PaymentNotice");
            DropForeignKey("FRP.PaymentNoticeLine", "InvoiceId", "FRP.Invoice");
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
            DropForeignKey("FRP.DocumentPath", "ParentId", "FRP.DocumentPath");
            DropForeignKey("FRP.ContractEngine", "PartID", "FRP.Part");
            DropForeignKey("FRP.ContractEngine", "ImportCategoryId", "FRP.ActionCategory");
            DropForeignKey("FRP.ContractAircraftBFE", "ContractAircraftId", "FRP.ContractAircraft");
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
            DropForeignKey("FRP.SupplierCompanyMaterial", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.Supplier", "SupplierCompanyId", "FRP.SupplierCompany");
            DropForeignKey("FRP.BankAccount", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.SupplierCompanyMaterial", "MaterialId", "FRP.Material");
            DropForeignKey("FRP.Material", "ManufacturerID", "FRP.Manufacturer");
            DropForeignKey("FRP.Aircraft", "AircraftTypeId", "FRP.AircraftType");
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
            DropIndex("FRP.SupplierRole", new[] { "SupplierCompanyId" });
            DropIndex("FRP.Reception", new[] { "SupplierId" });
            DropIndex("FRP.ReceptionSchedule", new[] { "ReceptionId" });
            DropIndex("FRP.ReceptionLine", new[] { "ReceptionId" });
            DropIndex("FRP.PaymentSchedule", new[] { "SupplierId" });
            DropIndex("FRP.PaymentScheduleLine", new[] { "PaymentScheduleId" });
            DropIndex("FRP.PaymentSchedule", new[] { "CurrencyId" });
            DropIndex("FRP.PaymentNotice", new[] { "SupplierId" });
            DropIndex("FRP.PaymentNoticeLine", new[] { "PaymentNoticeId" });
            DropIndex("FRP.PaymentNoticeLine", new[] { "InvoiceId" });
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
            DropIndex("FRP.DocumentPath", new[] { "ParentId" });
            DropIndex("FRP.ContractEngine", new[] { "PartID" });
            DropIndex("FRP.ContractEngine", new[] { "ImportCategoryId" });
            DropIndex("FRP.ContractAircraftBFE", new[] { "ContractAircraftId" });
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
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "SupplierCompanyId" });
            DropIndex("FRP.Supplier", new[] { "SupplierCompanyId" });
            DropIndex("FRP.BankAccount", new[] { "SupplierId" });
            DropIndex("FRP.SupplierCompanyMaterial", new[] { "MaterialId" });
            DropIndex("FRP.Material", new[] { "ManufacturerID" });
            DropIndex("FRP.Aircraft", new[] { "AircraftTypeId" });
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
            DropTable("FRP.ContentTag");
            DropTable("FRP.SupplierRole");
            DropTable("FRP.RelatedDoc");
            DropTable("FRP.Reception");
            DropTable("FRP.ReceptionSchedule");
            DropTable("FRP.ReceptionLine");
            DropTable("FRP.PaymentSchedule");
            DropTable("FRP.PaymentScheduleLine");
            DropTable("FRP.PaymentNoticeLine");
            DropTable("FRP.PaymentNotice");
            DropTable("FRP.MaintainInvoice");
            DropTable("FRP.MaintainInvoiceLine");
            DropTable("FRP.MaintainContract");
            DropTable("FRP.Invoice");
            DropTable("FRP.InvoiceLine");
            DropTable("FRP.Guarantee");
            DropTable("FRP.Document");
            DropTable("FRP.DocumentPath");
            DropTable("FRP.Trade");
            DropTable("FRP.ContractEngine");
            DropTable("FRP.PlanAircraft");
            DropTable("FRP.ContractAircraft");
            DropTable("FRP.Supplier");
            DropTable("FRP.SupplierCompany");
            DropTable("FRP.Part");
            DropTable("FRP.SupplierCompanyMaterial");
            DropTable("FRP.Manufacturer");
            DropTable("FRP.Material");
            DropTable("FRP.OrderLine");
            DropTable("FRP.Linkman");
            DropTable("FRP.Forwarder");
            DropTable("FRP.Currency");
            DropTable("FRP.ContractContent");
            DropTable("FRP.Order");
            DropTable("FRP.ContractAircraftBFE");
            DropTable("FRP.BankAccount");
            DropTable("FRP.AircraftType");
            DropTable("FRP.Aircraft");
            DropTable("FRP.ActionCategory");
        }
    }
}