#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:45:05
// 文件名：IAnnualMaintainPlanQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:45:05
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.AnnualMaintainPlanQueries
{
    /// <summary>
    /// AnnualMaintainPlan查询接口
    /// </summary>
    public interface IAnnualMaintainPlanQuery
    {
        /// <summary>
        /// EngineMaintainPlan查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>EngineMaintainPlanDTO集合</returns>
        IQueryable<EngineMaintainPlanDTO> EngineMaintainPlanDTOQuery(QueryBuilder<EngineMaintainPlan> query);

        /// <summary>
        /// AircraftMaintainPlan查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AircraftMaintainPlanDTO集合</returns>
        IQueryable<AircraftMaintainPlanDTO> AircraftMaintainPlanDTOQuery(QueryBuilder<AircraftMaintainPlan> query);
    }
}
