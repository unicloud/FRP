#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:27
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using UniCloud.Infrastructure.Security;

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork
{
    public class OracleConfigurations : IModelConfiguration
    {
        #region IModelConfiguration 成员

        public string GetDatabaseType()
        {
            return "Oracle";
        }

        public DbConnection GetDbConnection()
        {
            var connString = ConfigurationManager.ConnectionStrings["OracleFRP"].ToString();
            var connectionString = Cryptography.GetConnString(connString);
            var dbConnection = DbProviderFactories.GetFactory("Oracle.DataAccess.Client").CreateConnection();
            if (dbConnection != null)
            {
                dbConnection.ConnectionString = connectionString;
            }
            return dbConnection;
        }

        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
        }

        #endregion
    }
}