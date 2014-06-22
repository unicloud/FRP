#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:44
// 方案：FRP
// 项目：Application.PortalBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PortalBC.DTO;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Application.PortalBC.Query
{
    public interface IPortalQuery
    {
        /// <summary>
        ///     飞机系列查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机系列DTO集合</returns>
        IQueryable<AircraftSeriesDTO> AircraftSeriesDTOQuery(QueryBuilder<AircraftSeries> query);
    }
}