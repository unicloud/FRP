#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/18 9:34:13

// 文件名：OrganizationRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Repositories
{
    /// <summary>
    /// Organization仓储实现
    /// </summary>
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion

        public Organization GetOrganization(Expression<Func<Organization, bool>> condition)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Organization>();
            return set.FirstOrDefault(condition);
        }

        /// <summary>
        /// 删除OrganizationRole
        /// </summary>
        /// <param name="organizationRole"></param>
        public void DeleteOrganizationRole(OrganizationRole organizationRole)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var organizationRoles = currentUnitOfWork.CreateSet<OrganizationRole>();
            organizationRoles.Remove(organizationRole);
        }
    }
}
