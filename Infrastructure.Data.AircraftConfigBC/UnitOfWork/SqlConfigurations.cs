#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:13
// 方案：FRP
// 项目：Infrastructure.Data.AircraftConfigBC
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
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork
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
                .Add(new AtaEntityConfiguration())
                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftCategoryEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())
                .Add(new CAACAircraftTypeEntityConfiguration())
                #endregion

                #region AirlinesAgg

                .Add(new AirlinesEntityConfiguration())

                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion

                #region AircraftLicenseAgg

                .Add(new LicenseTypeEntityConfiguration())
                .Add(new AircraftLicenseEntityConfiguration())
                #endregion

                #region AircraftConfigurationAgg

                .Add(new AircraftConfigurationEntityConfiguration())
                .Add(new AircraftCabinEntityConfiguration())
                #endregion

                ;
        }

        #endregion
    }
}