#region 命名空间

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationRoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationUserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork
{
    public class BaseManagementBCUnitOfWork : BaseContext<BaseManagementBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<FunctionItem> _functionItems;
        private IDbSet<OrganizationRole> _organizationRoles;
        private IDbSet<OrganizationUser> _organizationUsers;
        private IDbSet<Organization> _organizations;
        private IDbSet<RoleFunction> _roleFunctions;
        private IDbSet<Role> _roles;
        private IDbSet<UserRole> _userRoles;
        private IDbSet<User> _users;

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

        #endregion

        #region DbContext 重载

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除不需要的公约
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 添加通过“TypeConfiguration”类的方式建立的配置
            if (DbConfig.DbUniCloud.Contains("Oracle"))
            {
                OracleConfigurations(modelBuilder);
            }
            else if (DbConfig.DbUniCloud.Contains("Sql"))
            {
                SqlConfigurations(modelBuilder);
            }
        }

        /// <summary>
        ///     Oracle数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void OracleConfigurations(DbModelBuilder modelBuilder)
        {
        }

        /// <summary>
        ///     SqlServer数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SqlConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations

                #region FunctionItemAgg

                .Add(new FunctionItemEntityConfiguration())

                #endregion
                #region OrganizationUserAgg

                .Add(new OrganizationUserEntityConfiguration())

                #endregion
                #region OrganizationRoleAgg

                .Add(new OrganizationRoleEntityConfiguration())

                #endregion
                #region OrganizationAgg

                .Add(new OrganizationEntityConfiguration())

                #endregion
                #region RoleFunctionAgg

                .Add(new RoleFunctionEntityConfiguration())

                #endregion
                #region RoleAgg

                .Add(new RoleEntityConfiguration())

                #endregion
                #region UserRoleAgg

                .Add(new UserRoleEntityConfiguration())

                #endregion
                #region UserAgg

                .Add(new UserEntityConfiguration())

                #endregion

                ;
        }

        #endregion
    }
}