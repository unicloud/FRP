#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 16:06:55
// 文件名：IOrganizationQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 16:06:55
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;

namespace UniCloud.Application.BaseManagementBC.Query.OrganizationQueries
{
    /// <summary>
    /// Organization查询接口
    /// </summary>
    public interface IOrganizationQuery
    {
        /// <summary>
        /// Organization查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>OrganizationDTO集合</returns>
        IQueryable<OrganizationDTO> OrganizationsQuery(QueryBuilder<Organization> query);
    }
}
