#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，11:39
// 方案：FRP
// 项目：Application.PartBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.OilUserAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.OilMonitorQueries
{
    /// <summary>
    ///     滑油监控查询
    /// </summary>
    public interface IOilMonitorQuery
    {
        /// <summary>
        ///     发动机滑油查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>发动机滑油DTO集合</returns>
        IQueryable<EngineOilDTO> EngineOilDTOQuery(QueryBuilder<OilUser> query);

        /// <summary>
        ///     APU滑油查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>APU滑油DTO集合</returns>
        IQueryable<APUOilDTO> APUOilDTOQuery(QueryBuilder<OilUser> query);

        /// <summary>
        ///     滑油消耗数据查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>滑油消耗数据集合</returns>
        IQueryable<OilMonitorDTO> OilMonitorDTOQuery(QueryBuilder<OilMonitor> query);
    }
}