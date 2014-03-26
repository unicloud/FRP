#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ActionCategoryQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.ActionCategoryQueries
{
    public class ActionCategoryQuery : IActionCategoryQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ActionCategoryQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     活动类型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>活动类型DTO集合。</returns>
        public IQueryable<ActionCategoryDTO> ActionCategoryDTOQuery(
            QueryBuilder<ActionCategory> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<ActionCategory>()).Select(p => new ActionCategoryDTO
            {
                Id = p.Id,
                ActionType = p.ActionType,
                ActionName = p.ActionName,
                NeedRequest = p.NeedRequest,
            });
        }
    }
}