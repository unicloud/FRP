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

#region 命名空间

using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 删除UserRole
        /// </summary>
        /// <param name="userRole"></param>
        void DeleteUserRole(UserRole userRole);
    }
}