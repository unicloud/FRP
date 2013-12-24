namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("FRP.ContractContent", "OrderId", "FRP.Order");
            DropForeignKey("FRP.OrderLine", "OrderId", "FRP.Order");
            DropIndex("FRP.ContractContent", new[] { "OrderId" });
            DropIndex("FRP.OrderLine", new[] { "OrderId" });
            AddColumn("FRP.ContractContent", "Description", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Subject", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Body", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Importance", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Tempo", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Start", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("FRP.PaymentScheduleLine", "End", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("FRP.PaymentScheduleLine", "IsAllDayEvent", c => c.Boolean(nullable: false));
            AlterColumn("FRP.ContractAircraft", "SupplierId", c => c.Int(nullable: false));
            AlterColumn("FRP.ContractEngine", "SupplierId", c => c.Int(nullable: false));
            AlterColumn("FRP.PaymentNotice", "ReviewDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("FRP.ContractAircraft", "SupplierId");
            CreateIndex("FRP.ContractEngine", "SupplierId");
            CreateIndex("FRP.ContractContent", "OrderId");
            CreateIndex("FRP.OrderLine", "OrderId");
            AddForeignKey("FRP.ContractAircraft", "SupplierId", "FRP.Supplier", "ID");
            AddForeignKey("FRP.ContractEngine", "SupplierId", "FRP.Supplier", "ID");
            AddForeignKey("FRP.ContractContent", "OrderId", "FRP.Order", "ID", cascadeDelete: true);
            AddForeignKey("FRP.OrderLine", "OrderId", "FRP.Order", "ID", cascadeDelete: true);
            DropColumn("FRP.PaymentScheduleLine", "Note");
            DropColumn("FRP.ReceptionSchedule", "Location");
            DropColumn("FRP.ReceptionSchedule", "UniqueId");
            DropColumn("FRP.ReceptionSchedule", "Url");
        }
        
        public override void Down()
        {
            AddColumn("FRP.ReceptionSchedule", "Url", c => c.String());
            AddColumn("FRP.ReceptionSchedule", "UniqueId", c => c.String());
            AddColumn("FRP.ReceptionSchedule", "Location", c => c.String());
            AddColumn("FRP.PaymentScheduleLine", "Note", c => c.String());
            DropForeignKey("FRP.OrderLine", "OrderId", "FRP.Order");
            DropForeignKey("FRP.ContractContent", "OrderId", "FRP.Order");
            DropForeignKey("FRP.ContractEngine", "SupplierId", "FRP.Supplier");
            DropForeignKey("FRP.ContractAircraft", "SupplierId", "FRP.Supplier");
            DropIndex("FRP.OrderLine", new[] { "OrderId" });
            DropIndex("FRP.ContractContent", new[] { "OrderId" });
            DropIndex("FRP.ContractEngine", new[] { "SupplierId" });
            DropIndex("FRP.ContractAircraft", new[] { "SupplierId" });
            AlterColumn("FRP.PaymentNotice", "ReviewDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("FRP.ContractEngine", "SupplierId", c => c.Int());
            AlterColumn("FRP.ContractAircraft", "SupplierId", c => c.Int());
            DropColumn("FRP.PaymentScheduleLine", "IsAllDayEvent");
            DropColumn("FRP.PaymentScheduleLine", "End");
            DropColumn("FRP.PaymentScheduleLine", "Start");
            DropColumn("FRP.PaymentScheduleLine", "Tempo");
            DropColumn("FRP.PaymentScheduleLine", "Importance");
            DropColumn("FRP.PaymentScheduleLine", "Body");
            DropColumn("FRP.PaymentScheduleLine", "Subject");
            DropColumn("FRP.ContractContent", "Description");
            CreateIndex("FRP.OrderLine", "OrderId");
            CreateIndex("FRP.ContractContent", "OrderId");
            AddForeignKey("FRP.OrderLine", "OrderId", "FRP.Order", "ID");
            AddForeignKey("FRP.ContractContent", "OrderId", "FRP.Order", "ID");
        }
    }
}
