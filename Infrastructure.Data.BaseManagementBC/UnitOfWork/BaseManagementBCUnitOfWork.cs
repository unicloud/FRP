#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationUserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.XmlSettingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork
{
    public class BaseManagementBCUnitOfWork : UniContext<BaseManagementBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<AircraftCabinType> _aircraftCabinTypes;
        private IDbSet<BusinessLicense> _businessLicenses;
        private IDbSet<FunctionItem> _functionItems;
        private IDbSet<OrganizationRole> _organizationRoles;
        private IDbSet<OrganizationUser> _organizationUsers;
        private IDbSet<Organization> _organizations;
        private IDbSet<RoleFunction> _roleFunctions;
        private IDbSet<Role> _roles;
        private IDbSet<UserRole> _userRoles;
        private IDbSet<User> _users;
        private IDbSet<XmlSetting> _xmlSettings;

        public IDbSet<FunctionItem> FunctionItems
        {
            get { return _functionItems ?? (_functionItems = Set<FunctionItem>()); }
        }

        public IDbSet<Organization> Organizations
        {
            get { return _organizations ?? (_organizations = Set<Organization>()); }
        }

        public IDbSet<OrganizationRole> OrganizationRoles
        {
            get { return _organizationRoles ?? (_organizationRoles = Set<OrganizationRole>()); }
        }

        public IDbSet<OrganizationUser> OrganizationUsers
        {
            get { return _organizationUsers ?? (_organizationUsers = Set<OrganizationUser>()); }
        }

        public IDbSet<Role> Roles
        {
            get { return _roles ?? (_roles = Set<Role>()); }
        }

        public IDbSet<RoleFunction> RoleFunctions
        {
            get { return _roleFunctions ?? (_roleFunctions = Set<RoleFunction>()); }
        }

        public IDbSet<User> Users
        {
            get { return _users ?? (_users = Set<User>()); }
        }

        public IDbSet<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = Set<UserRole>()); }
        }

        public IDbSet<BusinessLicense> BusinessLicenses
        {
            get { return _businessLicenses ?? (_businessLicenses = Set<BusinessLicense>()); }
        }

        public IDbSet<AircraftCabinType> AircraftCabinTypes
        {
            get { return _aircraftCabinTypes ?? (_aircraftCabinTypes = Set<AircraftCabinType>()); }
        }

        public IDbSet<XmlSetting> XmlSettings
        {
            get { return _xmlSettings ?? (_xmlSettings = base.Set<XmlSetting>()); }
        }

        #endregion
    }
}