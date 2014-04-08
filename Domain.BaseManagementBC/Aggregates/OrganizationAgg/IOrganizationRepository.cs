#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 17:11:30
// 文件名：IRelatedDocRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

using System;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;

namespace UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Organization GetOrganization(Expression<Func<Organization, bool>> condition);

        /// <summary>
        /// 删除OrganizationRole
        /// </summary>
        /// <param name="organizationRole"></param>
        void DeleteOrganizationRole(OrganizationRole organizationRole);
    }
}