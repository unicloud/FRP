#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:12
// 方案：FRP
// 项目：Infrastructure.Data.BaseManagementBC
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
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork
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

                #region FunctionItemAgg

                .Add(new FunctionItemEntityConfiguration())

                #endregion
                #region OrganizationUserAgg

                .Add(new OrganizationUserEntityConfiguration())

                #endregion
                #region OrganizationRoleAgg

                .Add(new OrganizationRoleEntityConfiguration())

                #endregion
                #region OrganizationAgg

                .Add(new OrganizationEntityConfiguration())

                #endregion
                #region RoleFunctionAgg

                .Add(new RoleFunctionEntityConfiguration())

                #endregion
                #region RoleAgg

                .Add(new RoleEntityConfiguration())

                #endregion
                #region UserRoleAgg

                .Add(new UserRoleEntityConfiguration())

                #endregion
                #region UserAgg

                .Add(new UserEntityConfiguration())

                #endregion
                #region BusinessLicenseAgg

                .Add(new BusinessLicenseEntityConfiguration())
                #endregion
                #region XmlSettingAgg

                .Add(new XmlSettingEntityConfiguration())

                #endregion

                .Add(new AircraftCabinTypeEntityConfiguration())
                ;
        }

        #endregion
    }
}