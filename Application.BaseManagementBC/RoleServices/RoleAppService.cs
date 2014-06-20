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
using System.Collections.Generic;
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
        private readonly IRoleQuery _roleQuery;
        private readonly IRoleRepository _roleRepository;

        public RoleAppService(IRoleQuery roleQuery, IRoleRepository roleRepository)
        {
            _roleQuery = roleQuery;
            _roleRepository = roleRepository;
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
            //Role newRole = RoleFactory.CreateRole();
            //RoleFactory.SetRole(newRole, role.Name, role.Name);

            //role.RoleFunctions.ToList().ForEach(roleFunction => InsertRoleFunction(newRole, roleFunction));
            //_roleRepository.Add(newRole);
        }


        /// <summary>
        ///     更新Role。
        /// </summary>
        /// <param name="role">RoleDTO。</param>
        [Update(typeof (RoleDTO))]
        public void ModifyRole(RoleDTO role)
        {
            //Role updateRole = _roleRepository.Get(role.Id); //获取需要更新的对象。
            //RoleFactory.SetRole(updateRole, role.Name, role.Name);

            //List<RoleFunctionDTO> dtoRoleFunctions = role.RoleFunctions;
            //ICollection<RoleFunction> roleFunctions = updateRole.RoleFunctions;
            //DataHelper.DetailHandle(dtoRoleFunctions.ToArray(),
            //    roleFunctions.ToArray(),
            //    c => c.Id, p => p.Id,
            //    i => InsertRoleFunction(updateRole, i),
            //    UpdateRoleFunction,
            //    d => _roleRepository.DeleteRoleFunction(d));
            //_roleRepository.Modify(updateRole);
        }

        /// <summary>
        ///     删除Role。
        /// </summary>
        /// <param name="role">RoleDTO。</param>
        [Delete(typeof (RoleDTO))]
        public void DeleteRole(RoleDTO role)
        {
            //Role deleteRole = _roleRepository.Get(role.Id); //获取需要删除的对象。
            //_roleRepository.DeleteRole(deleteRole); //删除Role。
        }

        /// <summary>
        ///     插入RoleFunction
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="roleFunctionDto">RoleFunctionDTO</param>
        private void InsertRoleFunction(Role role, RoleFunctionDTO roleFunctionDto)
        {
            // 添加RoleFunction
            //RoleFunction roleFunction = role.AddNewRoleFunction();
            //RoleFactory.SetRoleFunction(roleFunction, roleFunctionDto.RoleId, roleFunctionDto.FunctionItemId);
        }

        /// <summary>
        ///     更新RoleFunction
        /// </summary>
        /// <param name="roleFunctionDto">RoleFunctionDTO</param>
        /// <param name="roleFunction">RoleFunction</param>
        private void UpdateRoleFunction(RoleFunctionDTO roleFunctionDto, RoleFunction roleFunction)
        {
            // 更新RoleFunction
            //RoleFactory.SetRoleFunction(roleFunction, roleFunctionDto.RoleId, roleFunctionDto.FunctionItemId);
        }

        #endregion
    }
}