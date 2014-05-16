#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:41:50
// 文件名：IMaintainCostQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:41:50
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.MaintainCostQueries
{
    public interface IMaintainCostQuery
    {
        /// <summary>
        ///     定检维修成本查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>定检维修成本DTO集合</returns>
        IQueryable<RegularCheckMaintainCostDTO> RegularCheckMaintainCostDTOQuery(
            QueryBuilder<RegularCheckMaintainCost> query);

        /// <summary>
        ///     起落架维修成本查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>起落架维修成本DTO集合</returns>
        IQueryable<UndercartMaintainCostDTO> UndercartMaintainCostDTOQuery(
            QueryBuilder<UndercartMaintainCost> query);
    }
}
