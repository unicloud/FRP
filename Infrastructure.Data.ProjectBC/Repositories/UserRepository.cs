﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，16:37
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.Repositories
{
    /// <summary>
    ///     用户仓储实现
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}