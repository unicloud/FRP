#region 命名空间

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    /// <summary>
    ///     用户角色领域服务
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public UserRole AssignRole(User user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (role == null)
                throw new ArgumentNullException("role");
            var userRole = _userRoleRepository.GetFiltered(p => p.UserId == user.Id)
                .FirstOrDefault();
            if (userRole == null)
            {
                userRole = new UserRole(user.Id, role.Id);
                _userRoleRepository.Add(userRole);
            }
            else
            {
                userRole.SetRoleId(role.Id);
                _userRoleRepository.Modify(userRole);
            }
            return userRole;
        }

        public void UnassignRole(User user, Role role = null)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Expression<Func<UserRole, bool>> specExpression;
            if (role == null)
                specExpression = ur => ur.UserId == user.Id;
            else
                specExpression = ur => ur.UserId == user.Id && ur.RoleId == role.Id;

            var userRole = _userRoleRepository.GetFiltered(specExpression).FirstOrDefault();

            if (userRole != null)
            {
                _userRoleRepository.Remove(userRole);
            }
        }
    }
}