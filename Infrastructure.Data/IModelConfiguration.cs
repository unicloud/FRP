#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:36
// 方案：FRP
// 项目：Infrastructure.Data
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Common;
using System.Data.Entity;

#endregion

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     模型映射配置器的接口。
    /// </summary>
    public interface IModelConfiguration
    {
        /// <summary>
        ///     获取数据库类型
        /// </summary>
        /// <returns></returns>
        string GetDatabaseType();

        /// <summary>
        ///     获取数据库连接
        /// </summary>
        DbConnection GetDbConnection();

        /// <summary>
        ///     增加数据库表配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        void AddConfiguration(DbModelBuilder modelBuilder);
    }
}