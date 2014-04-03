#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:52:35
// 文件名：IPlanAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.PlanAircraftQueries
{
    /// <summary>
    /// 计划飞机查询接口
    /// </summary>
    public interface IPlanAircraftQuery
    {
        /// <summary>
        ///     计划飞机查询。
        /// </summary>
        /// <param name="query">查询表达式</param>s
        /// <returns>PlanAircraftDTO集合</returns>
        IQueryable<PlanAircraftDTO> PlanAircraftDTOQuery(QueryBuilder<PlanAircraft> query);
    }
}
