#region NameSpace

using System.Linq;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Repositories
{
    /// <summary>
    ///     Role仓储实现
    /// </summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override Role Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Role>();
            return set.Include(t => t.RoleFunctions).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        /// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var roles = currentUnitOfWork.CreateSet<Role>();
            role.RoleFunctions.ToList().ForEach(DeleteRoleFunction);
            roles.Remove(role);
        }

        /// <summary>
        /// 删除RoleFunction
        /// </summary>
        /// <param name="roleFunction"></param>
        public void DeleteRoleFunction(RoleFunction roleFunction)
        {
            var currentUnitOfWork = UnitOfWork as BaseManagementBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var roleFunctions = currentUnitOfWork.CreateSet<RoleFunction>();
            roleFunctions.Remove(roleFunction);
        }
    }
}