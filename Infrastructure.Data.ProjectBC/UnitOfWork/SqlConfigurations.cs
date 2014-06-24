#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:13
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
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
using UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork
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

                #region ProjectAgg

                .Add(new ProjectEntityConfiguration())
                .Add(new TaskEntityConfiguration())

                #endregion

                #region ProjectTempAgg

                .Add(new ProjectTempEntityConfiguration())
                .Add(new TaskTempEntityConfiguration())

                #endregion

                #region RelatedDocAgg

                .Add(new RelatedDocEntityConfiguration())

                #endregion

                #region TaskStandardAgg

                .Add(new TaskStandardEntityConfiguration())
                .Add(new TaskCaseEntityConfiguration())

                #endregion

                #region UserAgg

                .Add(new UserEntityConfiguration())

                #endregion

                #region WorkGroupAgg

                .Add(new WorkGroupEntityConfiguration())
                .Add(new MemberEntityConfiguration());

            #endregion
        }

        #endregion
    }
}