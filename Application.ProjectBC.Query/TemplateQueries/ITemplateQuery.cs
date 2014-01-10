#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，10:08
// 方案：FRP
// 项目：Application.ProjectBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;

#endregion

namespace UniCloud.Application.ProjectBC.Query.TemplateQueries
{
    public interface ITemplateQuery
    {
        /// <summary>
        ///     查询任务模板
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>任务模板的集合</returns>
        IQueryable<TaskStandardDTO> TaskStandardDTOQuery(QueryBuilder<TaskStandard> query);

        /// <summary>
        ///     查询工作组
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>工作组集合</returns>
        IQueryable<WorkGroupDTO> WorkGroupDTOQuery(QueryBuilder<WorkGroup> query);

        /// <summary>
        ///     查询用户
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>用户集合</returns>
        IQueryable<UserDTO> UserDTOQuery(QueryBuilder<User> query);
    }
}