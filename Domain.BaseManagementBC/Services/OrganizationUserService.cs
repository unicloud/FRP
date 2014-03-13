#region 命名空间

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationUserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Services
{
    public class OrganizationUserService : IOrganizationUserService
    {
        private readonly IOrganizationUserRepository _organizationUserRepository;

        public OrganizationUserService(IOrganizationUserRepository organizationUserRepository)
        {
            _organizationUserRepository = organizationUserRepository;
        }


        public OrganizationUser AssignOrganization(User user, Organization organization)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (organization == null)
                throw new ArgumentNullException("organization");
            var organizationUser = _organizationUserRepository.GetFiltered(p => p.UserId == user.Id)
                .FirstOrDefault();
            if (organizationUser == null)
            {
                organizationUser = new OrganizationUser(user.Id, organization.Id);
                _organizationUserRepository.Add(organizationUser);
            }
            else
            {
                organizationUser.SetOrganizationId(organization.Id);
                _organizationUserRepository.Modify(organizationUser);
            }
            return organizationUser;
        }

        public void UnassignOrganization(User user, Organization organization = null)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Expression<Func<OrganizationUser, bool>> specExpression;
            if (organization == null)
                specExpression = ur => ur.UserId == user.Id;
            else
                specExpression = ur => ur.UserId == user.Id && ur.OrganizationId == organization.Id;

            var organizationUser = _organizationUserRepository.GetFiltered(specExpression).FirstOrDefault();

            if (organizationUser != null)
            {
                _organizationUserRepository.Remove(organizationUser);
            }
        }
    }
}