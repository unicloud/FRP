#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ActionCategoryAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.ActionCategoryQueries;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.ActionCategoryServices
{
    /// <summary>
    ///     实现活动类型服务接口。
    ///     用于处理活动类型相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class ActionCategoryAppService : IActionCategoryAppService
    {
        private readonly IActionCategoryQuery _actionCategoryQuery;

        public ActionCategoryAppService(IActionCategoryQuery actionCategoryQuery)
        {
            _actionCategoryQuery = actionCategoryQuery;
        }

        #region ActionCategoryDTO

        /// <summary>
        ///     获取所有活动类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<ActionCategoryDTO> GetActionCategories()
        {
            var queryBuilder =
                new QueryBuilder<ActionCategory>();
            return _actionCategoryQuery.ActionCategoryDTOQuery(queryBuilder);
        }

        #endregion
    }
}
