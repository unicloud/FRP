#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/18 9:34:13

// 文件名：UserRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Repositories
{
    /// <summary>
    /// User仓储实现
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        public override User Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<User>();
            return set.Include(t => t.UserRoles).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        public User GetUser(Expression<Func<User, bool>> condition)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<User>();
            return set.FirstOrDefault(condition);
        }

        /// <summary>
        /// 删除UserRole
        /// </summary>
        /// <param name="userRole"></param>
        public void DeleteUserRole(UserRole userRole)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var userRoles = currentUnitOfWork.CreateSet<UserRole>();
            userRoles.Remove(userRole);
        }
    }
}
