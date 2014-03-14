#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/14 15:09:47
// 文件名：IRoleQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/14 15:09:47
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;

namespace UniCloud.Application.BaseManagementBC.Query.RoleQueries
{
    /// <summary>
    /// Role查询接口
    /// </summary>
    public interface IRoleQuery
    {
        /// <summary>
        /// Role查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>RoleDTO集合</returns>
        IQueryable<RoleDTO> RolesQuery(QueryBuilder<Role> query);
    }
}
