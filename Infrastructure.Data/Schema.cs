#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:37
// 方案：FRP
// 项目：Infrastructure.Data
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     数据库架构。
    /// </summary>
    public class Schema
    {
        private static string _schema;

        /// <summary>
        ///     获取架构名称，SQL SERVER默认返回dbo。
        ///     Oracle 返回第一个连接串的架构名
        /// </summary>
        /// <returns></returns>
        public static string Get()
        {
            if (!string.IsNullOrWhiteSpace(_schema)) return _schema;
            var schemaOracle = string.Empty;
            var schemaSql = string.Empty;
            var connList = ConfigurationManager.ConnectionStrings;
            var connOracle =
                connList.Cast<object>()
                    .Select(item => item as ConnectionStringSettings)
                    .FirstOrDefault(conn => conn.ProviderName.Equals("Oracle.DataAccess.Client"));
            if (connOracle != null)
            {
                var mOracle = Regex.Match(connOracle.ConnectionString, @"user id=(?<Schema>[^;]*)",
                    RegexOptions.IgnoreCase);
                schemaOracle = mOracle.Success ? mOracle.Groups["Schema"].Value.Trim() : "Oracle";
            }

            var connSql = connList.Cast<object>()
                .Select(item => item as ConnectionStringSettings)
                .LastOrDefault(conn => conn.ProviderName.Equals("System.Data.SqlClient"));
            if (connSql != null)
            {
                var mSql = Regex.Match(connSql.ConnectionString, @"Database=(?<Schema>[^;]*)",
                    RegexOptions.IgnoreCase);
                schemaSql = mSql.Success ? mSql.Groups["Schema"].Value.Trim() : "dbo";
            }
            _schema = ConfigurationManager.AppSettings["DatabaseType"].ToLower() == "oracle"
                ? schemaOracle
                : schemaSql;
            return _schema;
        }
    }
}