#region NameSpace

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    public class RoleFunctionService : IRoleFunctionService
    {
        private readonly IRoleFunctionRepository _roleFunctionRepository;

        public RoleFunctionService(IRoleFunctionRepository roleFunctionRepository)
        {
            _roleFunctionRepository = roleFunctionRepository;
        }

        public RoleFunction AssignRole(FunctionItem function, Role role)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            if (role == null)
                throw new ArgumentNullException("role");
            var roleFunction = _roleFunctionRepository.GetFiltered(p => p.FunctionItemId == function.Id)
                .FirstOrDefault();
            if (roleFunction == null)
            {
                roleFunction = new RoleFunction(function.Id, role.Id);
                _roleFunctionRepository.Add(roleFunction);
            }
            else
            {
                roleFunction.SetRoleId(role.Id);
                _roleFunctionRepository.Modify(roleFunction);
            }
            return roleFunction;
        }

        public void UnassignRole(FunctionItem function, Role role = null)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            Expression<Func<RoleFunction, bool>> specExpression;
            if (role == null)
                specExpression = ur => ur.FunctionItemId == function.Id;
            else
                specExpression = ur => ur.FunctionItemId == function.Id && ur.RoleId == role.Id;

            var roleFunction = _roleFunctionRepository.GetFiltered(specExpression).FirstOrDefault();

            if (roleFunction != null)
            {
                _roleFunctionRepository.Remove(roleFunction);
            }
        }
    }
}