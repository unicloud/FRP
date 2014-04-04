#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/10/21 9:32:10
// 文件名：SqlServerConn
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

namespace UniCloud.DataService.Connection
{
    public class SqlServerConn : IDisposable, IDbConn
    {
        // private string strConn = @"DataSource=172.18.8.117; Database=UniClod.FRP;UserId=UniCloud;Password=fleet@XMZZ;";

        private SqlConnection _conn;
        private string _connString;
        private bool _disposed;

        public SqlServerConn(string strConn)
        {
            _connString = strConn;
            _conn = new SqlConnection(_connString);
        }

        public DbConnection Connection
        {
            get { return _conn; }
        }

        public void SetConnectionString(string strConn)
        {
            _connString = strConn;
            _conn =
                new SqlConnection(_connString);
        }


        public DataTable GetDataTable(string strSql)
        {
            throw new NotImplementedException();
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

        public void UpdateData(string strSql)
        {
            var cmd = new SqlCommand(strSql, _conn);
            _conn.Open();
            var adp = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            adp.Fill(ds);
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

        ~SqlServerConn()
        {
            Dispose(false);
        }
    }
}