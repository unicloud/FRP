#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:51:18
// 文件名：IActionCategoryQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ActionCategoryQueries
{
    /// <summary>
    /// 活动类型查询接口
    /// </summary>
    public interface IActionCategoryQuery
    {
        /// <summary>
        ///     活动类型查询。
        /// </summary>
        /// <param name="query">查询表达式</param>s
        /// <returns>ActionCategoryDTO集合</returns>
        IQueryable<ActionCategoryDTO> ActionCategoryDTOQuery(QueryBuilder<ActionCategory> query);
    }
}
