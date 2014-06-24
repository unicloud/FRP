#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:14
// 方案：FRP
// 项目：Infrastructure.Data.PartBC
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
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork
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

                #region AcDailyUtilizationAgg

                .Add(new AcDailyUtilizationEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())
                .Add(new AircraftSeriesEntityConfiguration())

                #endregion

                #region AnnualMaintainPlan

                .Add(new AnnualEntityConfiguration())
                .Add(new AnnualMaintainPlanEntityConfiguration())
                .Add(new EngineMaintainPlanEntityConfiguration())
                .Add(new EngineMaintainPlanDetailEntityConfiguration())
                .Add(new AircraftMaintainPlanEntityConfiguration())
                .Add(new AircraftMaintainPlanDetailEntityConfiguration())
                #endregion

                #region BasicConfigGroupAgg

                .Add(new BasicConfigGroupEntityConfiguration())
                .Add(new BasicConfigEntityConfiguration())
                .Add(new BasicConfigHistoryEntityConfiguration())

                #endregion

                #region ContractAircraftAgg

                .Add(new ContractAircraftEntityConfiguration())

                #endregion

                #region CtrlUnitAgg

                .Add(new CtrlUnitEntityConfiguration())

                #endregion

                #region FlightLogAgg

                .Add(new FlightLogEntityConfiguration())

                #endregion

                #region ItemAgg

                .Add(new ItemEntityConfiguration())
                #endregion

                #region ItemAgg

                .Add(new InstallControllerEntityConfiguration())
                .Add(new DependencyEntityConfiguration())
                #endregion

                #region MaintainCtrlAgg

                .Add(new MaintainCtrlEntityConfiguration())
                .Add(new ItemMaintainCtrlEntityConfiguration())
                .Add(new PnMaintainCtrlEntityConfiguration())
                .Add(new SnMaintainCtrlEntityConfiguration())

                #endregion

                #region MaintainWorkAgg

                .Add(new MaintainWorkEntityConfiguration())

                #endregion

                #region ModAgg

                .Add(new ModEntityConfiguration())

                #endregion

                #region OilMonitorAgg

                .Add(new OilMonitorEntityConfiguration())

                #endregion


                #region PnRegAgg

                .Add(new PnRegEntityConfiguration())
                #endregion

                #region ScnAgg

                .Add(new ScnEntityConfiguration())
                .Add(new ApplicableAircraftEntityConfiguration())
                .Add(new AirBusScnEntityConfiguration())

                #endregion

                #region SnRegAgg

                .Add(new SnRegEntityConfiguration())
                .Add(new LifeMonitorEntityConfiguration())
                .Add(new EngineRegEntityConfiguration())
                .Add(new APURegEntityConfiguration())

                #endregion

                #region SnHistoryAgg

                .Add(new SnHistoryEntityConfiguration())

                #endregion

                #region SnRemInstRecordAgg

                .Add(new SnRemInstRecordEntityConfiguration())

                #endregion

                #region SpecialConfigAgg

                .Add(new AcConfigEntityConfiguration())
                .Add(new SpecialConfigEntityConfiguration())

                #endregion

                #region ThrustAgg

                .Add(new ThrustEntityConfiguration())

                #endregion

                #region ThresholdAgg

                .Add(new ThresholdEntityConfiguration())

                #endregion


                #region AirStructureDamageAgg

                .Add(new AirStructureDamageEntityConfiguration())

                #endregion

                #region  AdSbAgg

                .Add(new AdSbEntityConfiguration())

                #endregion

                ;
        }

        #endregion
    }
}