#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：zhangnx 时间：2013/9/4 13:38:11
// 文件名：Db2Connection
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dapper;
using IBM.Data.DB2.iSeries;

namespace UniCloud.DataService.Connection
{
    public class Db2Conn : IDisposable, IDbConn
    {
        // private string strConn = @"DataSource=172.18.8.117; Database=Ifr;UserId=amsutst915;Password=amsutst915;";

        private iDB2Connection _conn;
        private string _connString;
        private bool _disposed;

        public Db2Conn(string strConn)
        {
            _connString = strConn;
            _conn = new iDB2Connection(_connString);
        }

        public DbConnection Connection
        {
            get { return _conn; }
        }

        public void SetConnectionString(string strConn)
        {
            _connString = strConn;
            _conn =
                new iDB2Connection(_connString);
        }

        public DataTable GetDataTable(string strSql)
        {
            var cmd = new iDB2Command(strSql, _conn);
            try
            {
                _conn.Open();
                var adp = new iDB2DataAdapter(cmd);
                var ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> GetSqlDatas<T>(string sql) where T : class
        {
            try
            {
                return _conn.TryExecute(connection =>
                {
                    IEnumerable<T> items = connection.QueryAll<T>(sql);
                    return items;
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // Release managed resources
                _conn.Dispose();
            }
            // Release unmanaged resources
            _disposed = true;
        }

        ~Db2Conn()
        {
            Dispose(false);
        }


        public IEnumerable<dynamic> GetSqlDatas(string strSql, string tableName)
        {
            throw new NotImplementedException();
        }
    }
}