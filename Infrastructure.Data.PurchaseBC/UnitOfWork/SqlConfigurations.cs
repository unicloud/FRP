#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:13
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork
{
    public class SqlConfigurations : IModelConfiguration
    {
        #region IModelConfiguration 成员

        public string GetDatabaseType()
        {
            return "Sql";
        }

        public DbConnection GetDbConnection()
        {
            var connString = ConfigurationManager.ConnectionStrings["SqlFRP"].ToString();
            var connectionString = Cryptography.GetConnString(connString);
            var dbConnection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
            if (dbConnection != null)
            {
                dbConnection.ConnectionString = connectionString;
            }
            return dbConnection;
        }

        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations

                #region ActionCategoryAgg

                .Add(new ActionCategoryEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())

                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftCategoryEntityConfiguration())
                
                #endregion

                #region AircraftPlanHistoryAgg

                .Add(new PlanHistoryEntityConfiguration())

                #endregion

                #region AircraftPlanAgg

                .Add(new PlanEntityConfiguration())
                
                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())

                #endregion

                #region AnnualAgg

                .Add(new AnnualEntityConfiguration())

                #endregion

                #region BankAccountAgg

                .Add(new BankAccountEntityConfiguration())

                #endregion

                #region ContentTagAgg

                .Add(new ContentTagEntityConfiguration())

                #endregion

                #region ContractAircraftAgg

                .Add(new ContractAircraftEntityConfiguration())
                .Add(new LeaseContractAircraftEntityConfiguration())
                .Add(new PurchaseContractAircraftEntityConfiguration())

                #endregion

                #region ContractAircraftBFEAgg

                .Add(new ContractAircraftBFEEntityConfiguration())

                #endregion

                #region ContractEngineAgg

                .Add(new ContractEngineEntityConfiguration())
                .Add(new LeaseContractEngineEntityConfiguration())
                .Add(new PurchaseContractEngineEntityConfiguration())

                #endregion

                #region CurrencyAgg

                .Add(new CurrencyEntityConfiguration())

                #endregion

                #region DocumentAgg

                .Add(new DocumentEntityConfiguration())
                .Add(new OfficialDocumentEntityConfiguration())
                .Add(new StandardDocumentEntityConfiguration())

                #endregion

                #region DocumentPathAgg

                .Add(new DocumentPathEntityConfiguration())

                #endregion

                #region ForwarderAgg

                .Add(new ForwarderEntityConfiguration())

                #endregion

                #region LinkmanAgg

                .Add(new LinkmanEntityConfiguration())

                #endregion

                #region MaintainContractAgg

                .Add(new MaintainContractEntityConfiguration())
                .Add(new APUMaintainContractEntityConfiguration())
                .Add(new EngineMaintainContractEntityConfiguration())
                .Add(new UndercartMaintainContractEntityConfiguration())
                .Add(new AirframeMaintainContractEntityConfiguration())
                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

                #endregion

                #region MaterialAgg

                .Add(new MaterialEntityConfiguration())
                .Add(new AircraftMaterialEntityConfiguration())
                .Add(new BFEMaterialEntityConfiguration())
                .Add(new EngineMaterialEntityConfiguration())

                #endregion

                #region OrderAgg

                .Add(new OrderEntityConfiguration())
                .Add(new ContractContentEntityConfiguration())
                .Add(new OrderLineEntityConfiguration())
                .Add(new AircraftLeaseOrderEntityConfiguration())
                .Add(new AircraftLeaseOrderLineEntityConfiguration())
                .Add(new AircraftPurchaseOrderEntityConfiguration())
                .Add(new AircraftPurchaseOrderLineEntityConfiguration())
                .Add(new BFEPurchaseOrderEntityConfiguration())
                .Add(new BFEPurchaseOrderLineEntityConfiguration())
                .Add(new EngineLeaseOrderEntityConfiguration())
                .Add(new EngineLeaseOrderLineEntityConfiguration())
                .Add(new EnginePurchaseOrderEntityConfiguration())
                .Add(new EnginePurchaseOrderLineEntityConfiguration())

                #endregion

                #region PnRegAgg

                .Add(new PnRegEntityConfiguration())

                #endregion

                #region PlanAircraftAgg

                .Add(new PlanAircraftEntityConfiguration())

                #endregion

                #region ReceptionAgg

                .Add(new ReceptionEntityConfiguration())
                .Add(new ReceptionLineEntityConfiguration())
                .Add(new ReceptionScheduleEntityConfiguration())
                .Add(new AircraftLeaseReceptionEntityConfiguration())
                .Add(new AircraftLeaseReceptionLineEntityConfiguration())
                .Add(new AircraftPurchaseReceptionEntityConfiguration())
                .Add(new AircraftPurchaseReceptionLineEntityConfiguration())
                .Add(new EngineLeaseReceptionEntityConfiguration())
                .Add(new EngineLeaseReceptionLineEntityConfiguration())
                .Add(new EnginePurchaseReceptionEntityConfiguration())
                .Add(new EnginePurchaseReceptionLineEntityConfiguration())

                #endregion

                #region RelatedDocAgg

                .Add(new RelatedDocEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion

                #region SupplierCompanyAgg

                .Add(new SupplierCompanyEntityConfiguration())

                #endregion

                #region SupplierCompanyMaterialAgg

                .Add(new SupplierCompanyMaterialEntityConfiguration())

                #endregion

                #region SupplierRoleAgg

                .Add(new SupplierRoleEntityConfiguration())
                .Add(new AircraftLeaseSupplierEntityConfiguration())
                .Add(new AircraftPurchaseSupplierEntityConfiguration())
                .Add(new BFEPurchaseSupplierEntityConfiguration())
                .Add(new EngineLeaseSupplierEntityConfiguration())
                .Add(new EnginePurchaseSupplierEntityConfiguration())
                .Add(new MaintainSupplierEntityConfiguration())
                .Add(new OtherSupplierEntityConfiguration())
                #endregion

                #region TradeAgg

                .Add(new TradeEntityConfiguration())

                #endregion

                .Add(new AddressConfiguration());
        }

        #endregion
    }
}