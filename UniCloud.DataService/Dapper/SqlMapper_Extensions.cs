#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10，13:11
// 方案：FRP
// 项目：SqlMapper
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Dapper
{
    /// <summary>Dapper extensions by tangxuehua, 2012-11-21
    /// </summary>
    public partial class SqlMapper
    {
        private static ConcurrentDictionary<Type, List<string>> _paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public static IEnumerable<T> QueryAll<T>(this IDbConnection connection, string sqlString, string table, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            string sql = sqlString;
            return SqlMapper.Query<T>(connection, sql, null, transaction, true, commandTimeout);
        }

        public static IEnumerable<T> QueryAll<T>(this IDbConnection connection, string sqlString) where T : class
        {
            string sql = sqlString;
            return SqlMapper.Query<T>(connection, sql);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection connection, object condition, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = condition as object;
            var properties = GetProperties(obj);
            var whereFields = string.Join(" and ", properties.Select(p => p + " = @" + p));
            var sql = string.Format("select * from [{0}] where {1}", table, whereFields);

            return SqlMapper.Query<T>(connection, sql, obj, transaction, true, commandTimeout);
        }

        public static void TryExecute(this IDbConnection connection, Action<IDbConnection> action)
        {
            connection.Open();
            action(connection);

        }
        public static T TryExecute<T>(this IDbConnection connection, Func<IDbConnection, T> func)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return func(connection);
        }
        public static void TryExecuteInTransaction(this IDbConnection connection, Action<IDbConnection, IDbTransaction> action)
        {
            using (connection)
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    action(connection, transaction);
                    transaction.Commit();
                }
                catch
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw;
                }
            }
        }
        public static T TryExecuteInTransaction<T>(this IDbConnection connection, Func<IDbConnection, IDbTransaction, T> func)
        {
            using (connection)
            {
                IDbTransaction transaction = null;
                T result;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    result = func(connection, transaction);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw;
                }
            }
        }

        private static List<string> GetProperties(object o)
        {
            if (o is DynamicParameters)
            {
                return (o as DynamicParameters).ParameterNames.ToList();
            }

            List<string> properties;
            if (!_paramNameCache.TryGetValue(o.GetType(), out properties))
            {
                properties = new List<string>();
                foreach (var prop in o.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
                {
                    properties.Add(prop.Name);
                }
                _paramNameCache[o.GetType()] = properties;
            }
            return properties;
        }
    }
}
