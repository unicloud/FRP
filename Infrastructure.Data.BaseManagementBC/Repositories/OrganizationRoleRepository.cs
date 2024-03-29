﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/18 9:34:13

// 文件名：OrganizationRoleRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Repositories
{
   /// <summary>
   /// OrganizationRole仓储实现
   /// </summary>
   public class OrganizationRoleRepository: Repository<OrganizationRole>, IOrganizationRoleRepository
   {
      public OrganizationRoleRepository(IQueryableUnitOfWork unitOfWork)
      : base(unitOfWork)
      {
         
      }
      
         #region 方法重载
      #endregion
   }
}
