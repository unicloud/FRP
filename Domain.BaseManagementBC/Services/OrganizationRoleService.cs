#region 命名空间

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    public class OrganizationRoleService : IOrganizationRoleService
    {
        private readonly IOrganizationRoleRepository _organizationRoleRepository;

        public OrganizationRoleService(IOrganizationRoleRepository organizationRoleRepository)
        {
            _organizationRoleRepository = organizationRoleRepository;
        }

        public OrganizationRole AssignRole(Organization organization, Role role)
        {
            if (organization == null)
                throw new ArgumentNullException("organization");
            if (role == null)
                throw new ArgumentNullException("role");
            var organizationRole = _organizationRoleRepository.GetFiltered(p => p.OrganizationId == organization.Id)
                .FirstOrDefault();
            if (organizationRole == null)
            {
                organizationRole = new OrganizationRole(organization.Id, role.Id);
                _organizationRoleRepository.Add(organizationRole);
            }
            else
            {
                organizationRole.SetRoleId(role.Id);
                _organizationRoleRepository.Modify(organizationRole);
            }
            return organizationRole;
        }

        public void UnassignRole(User organization, Role role = null)
        {
            if (organization == null)
                throw new ArgumentNullException("organization");
            Expression<Func<OrganizationRole, bool>> specExpression;
            if (role == null)
                specExpression = ur => ur.OrganizationId == organization.Id;
            else
                specExpression = ur => ur.OrganizationId == organization.Id && ur.RoleId == role.Id;

            var userRole = _organizationRoleRepository.GetFiltered(specExpression).FirstOrDefault();

            if (userRole != null)
            {
                _organizationRoleRepository.Remove(userRole);
            }
        }
    }
}