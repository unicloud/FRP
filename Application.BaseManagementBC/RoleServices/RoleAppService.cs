#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/14 15:14:30
// 文件名：RoleAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/14 15:14:30
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.RoleQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.RoleServices
{
    /// <summary>
    ///     实现Role的服务接口。
    ///     用于处理Role相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class RoleAppService : ContextBoundObject, IRoleAppService
    {
        private readonly IRoleFunctionRepository _roleFunctionRepository;
        private readonly IRoleQuery _roleQuery;
        private readonly IRoleRepository _roleRepository;

        public RoleAppService(IRoleQuery roleQuery, IRoleRepository roleRepository,
            IRoleFunctionRepository roleFunctionRepository)
        {
            _roleQuery = roleQuery;
            _roleRepository = roleRepository;
            _roleFunctionRepository = roleFunctionRepository;
        }

        #region RoleDTO

        /// <summary>
        ///     获取所有Role。
        /// </summary>
        public IQueryable<RoleDTO> GetRoles()
        {
            var queryBuilder =
                new QueryBuilder<Role>();
            return _roleQuery.RolesQuery(queryBuilder);
        }

        /// <summary>
        ///     新增Role。
        /// </summary>
        /// <param name="role">RoleDTO。</param>
        [Insert(typeof (RoleDTO))]
        public void InsertRole(RoleDTO role)
        {
            var newRole = RoleFactory.CreateRole(role.Name);
            role.RoleFunctions.ToList()
                .ForEach(roleFunction => _roleFunctionRepository.Add(new RoleFunction(role.Id, roleFunction.Id)));
            _roleRepository.Add(newRole);
        }


        /// <summary>
        ///     更新Role。
        /// </summary>
        /// <param name="role">RoleDTO。</param>
        [Update(typeof (RoleDTO))]
        public void ModifyRole(RoleDTO role)
        {
            var updateRole = RoleFactory.UpdateRole(role.Name, role.Description);
            updateRole.ChangeCurrentIdentity(role.Id);

            var dtoRoleFunctions = role.RoleFunctions;
            var roleFunctions = updateRole.RoleFunctions;
            DataHelper.DetailHandle(dtoRoleFunctions.ToArray(),
                roleFunctions.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertRoleFunction(updateRole, i),
                (c, p) => UpdateRoleFunction(c),
                d => _roleRepository.DeleteRoleFunction(d));
            _roleRepository.Modify(updateRole);
        }

        /// <summary>
        ///     删除Role。
        /// </summary>
        /// <param name="role">RoleDTO。</param>
        [Delete(typeof (RoleDTO))]
        public void DeleteRole(RoleDTO role)
        {
            var deleteRole = _roleRepository.Get(role.Id); //获取需要删除的对象。
            _roleRepository.DeleteRole(deleteRole); //删除Role。
        }

        /// <summary>
        ///     插入角色功能
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="roleFunctionDto">角色功能DTO</param>
        private void InsertRoleFunction(Role role, RoleFunctionDTO roleFunctionDto)
        {
            var persist = _roleFunctionRepository.Get(roleFunctionDto.Id);
            if (persist != null) return;
            var roleFunction = new RoleFunction(role.Id, roleFunctionDto.FunctionItemId);
            _roleFunctionRepository.Add(roleFunction);
        }

        /// <summary>
        ///     更新RoleFunction
        /// </summary>
        /// <param name="roleFunctionDto">RoleFunctionDTO</param>
        private void UpdateRoleFunction(RoleFunctionDTO roleFunctionDto)
        {
            var roleFunction = new RoleFunction(roleFunctionDto.RoleId, roleFunctionDto.FunctionItemId);
            roleFunction.ChangeCurrentIdentity(roleFunctionDto.Id);
            _roleFunctionRepository.Modify(roleFunction);
        }

        #endregion
    }
}