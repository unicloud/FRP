#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:13
// 方案：FRP
// 项目：Infrastructure.Data.FleetPlanBC
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork
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

                #region AircraftSeriesAgg

                .Add(new AircraftSeriesEntityConfiguration())

                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftCategoryEntityConfiguration())

                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftConfigurationEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())
                .Add(new AircraftBusinessEntityConfiguration())
                .Add(new OperationHistoryEntityConfiguration())
                .Add(new OwnershipHistoryEntityConfiguration())
                .Add(new AcConfigHistoryEntityConfiguration())
                #endregion

                #region AircraftPlanAgg

                .Add(new PlanEntityConfiguration())
                .Add(new PlanHistoryEntityConfiguration())
                .Add(new ChangePlanEntityConfiguration())
                .Add(new OperationPlanEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())

                #endregion

                #region AirlinesAgg

                .Add(new AirlinesEntityConfiguration())

                #endregion

                #region AirProgrammingAgg

                .Add(new AirProgrammingEntityConfiguration())
                .Add(new AirProgrammingLineEntityConfiguration())
                #endregion

                #region AnnualAgg

                .Add(new AnnualEntityConfiguration())

                #endregion

                #region ApprovalDocAgg

                .Add(new ApprovalDocEntityConfiguration())

                #endregion

                #region CAACAircraftTypeAgg

                .Add(new CAACAircraftTypeEntityConfiguration())

                #endregion

                #region CaacProgrammingAgg

                .Add(new CaacProgrammingEntityConfiguration())
                .Add(new CaacProgrammingLineEntityConfiguration())

                #endregion

                #region DocumentAgg

                .Add(new DocumentEntityConfiguration())

                #endregion

                #region EngineAgg

                .Add(new EngineEntityConfiguration())
                .Add(new EngineBusinessHistoryEntityConfiguration())
                .Add(new EngineOwnershipHistoryEntityConfiguration())

                #endregion

                #region EnginePlanAgg

                .Add(new EnginePlanEntityConfiguration())
                .Add(new EnginePlanHistoryEntityConfiguration())

                #endregion

                #region EngineTypeAgg

                .Add(new EngineTypeEntityConfiguration())

                #endregion

                #region IssuedUnitAgg

                .Add(new IssuedUnitEntityConfiguration())
                #endregion

                #region MailAddressAgg

                .Add(new MailAddressEntityConfiguration())

                #endregion

                #region ManagerAgg

                .Add(new ManagerEntityConfiguration())

                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

                #endregion

                #region PlanAircraftAgg

                .Add(new PlanAircraftEntityConfiguration())

                #endregion

                #region PlanEngineAgg

                .Add(new PlanEngineEntityConfiguration())

                #endregion

                #region ProgrammingAgg

                .Add(new ProgrammingEntityConfiguration())

                #endregion

                #region ProgrammingAgg

                .Add(new ProgrammingFileEntityConfiguration())

                #endregion

                #region RelatedDocAgg

                .Add(new RelatedDocEntityConfiguration())

                #endregion

                #region RequestAgg

                .Add(new RequestEntityConfiguration())
                .Add(new ApprovalHistoryEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion

                #region XmlConfigAgg

                .Add(new XmlConfigEntityConfiguration())

                #endregion

                #region XmlSettingAgg

                #endregion

                ;
        }

        #endregion
    }
}