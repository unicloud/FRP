#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:51:01
// 文件名：ActionCategoryQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ActionCategoryQueries
{
    /// <summary>
    /// 活动类型查询
    /// </summary>
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
        /// <param name="query">查询表达式</param>s
        /// <returns>ActionCategoryDTO集合</returns>
        public IQueryable<ActionCategoryDTO> ActionCategoryDTOQuery(QueryBuilder<ActionCategory> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<ActionCategory>()).Select(p => new ActionCategoryDTO
            {
                Id = p.Id,
                ActionType = p.ActionType,
                ActionName = p.ActionName,
            });
        }
    }
}