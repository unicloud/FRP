#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/07，21:04
// 文件名：IInstallControllerQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.InstallControllerQueries
{
    public interface IInstallControllerQuery
    {
        /// <summary>
        ///     装机控制查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>装机控制DTO集合</returns>
        IQueryable<InstallControllerDTO> InstallControllerDTOQuery(
            QueryBuilder<InstallController> query);
    }
}