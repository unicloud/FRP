#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：zhangnx 时间：2013/9/5 15:03:04
// 文件名：IDbConn
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

using System.Data;

namespace UniCloud.DataService.Connection
{
    public interface IDbConn
    {
        /// <summary>
        ///     通过SQL获取DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        DataTable GetDataTable(string strSql);

        /// <summary>
        ///     设置连接串
        /// </summary>
        /// <param name="strConn"></param>
        void SetConnectionString(string strConn);
    }
}