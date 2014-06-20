#region 版本控制
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:39
// 方案：FRP
// 项目：Application.BaseManagementBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Linq;
using System.Web.Hosting;
using System.Web.Security;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Application.BaseManagementBC
{
    /// <summary>
    ///     自定义角色管理程序
    /// </summary>
    public class UniRoleProvider : RoleProvider
    {
        #region 声明、初始化

        private readonly string _pName;
        private string _pApplicationName;
        private bool _pWriteExceptionsToEventLog;

        #region DI

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        #endregion

        public UniRoleProvider()
        {
            _pApplicationName = "unicloud";
            _pName = "UniRoleProvider";
            _userRepository = DefaultContainer.Resolve<IUserRepository>();
            _userRoleRepository = DefaultContainer.Resolve<IUserRoleRepository>();
            _roleRepository = DefaultContainer.Resolve<IRoleRepository>();
        }

        #endregion

        public bool WriteExceptionsToEventLog
        {
            get { return _pWriteExceptionsToEventLog; }
            set { _pWriteExceptionsToEventLog = value; }
        }

        #region 重写RoleProvider属性

        public override string ApplicationName
        {
            get { return _pApplicationName; }
            set { _pApplicationName = value; }
        }

        public override string Name
        {
            get { return _pName; }
        }

        #endregion

        #region 重写RoleProvider方法

        public override void Initialize(string name, NameValueCollection config)
        {
            try
            {
                // 从web.config文件初始化相关值
                if (name.Length == 0) name = "UniRoleProvider";
                if (String.IsNullOrEmpty(config["description"]))
                {
                    config.Remove("description");
                    config.Add("description", "UniCloud Role provider");
                }

                base.Initialize(name, config);

                if (config["applicationName"] == null || config["applicationName"].Trim() == "")
                {
                    _pApplicationName = HostingEnvironment.ApplicationVirtualPath;
                }
                else
                {
                    _pApplicationName = config["applicationName"];
                }

                if (config["writeExceptionsToEventLog"] == null) return;
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                {
                    _pWriteExceptionsToEventLog = true;
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("初始化出错", ex);
                throw;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                if (roleNames.Any(rolename => !RoleExists(rolename)))
                    throw new ProviderException("角色不存在！");

                foreach (var username in usernames)
                {
                    if (username.Contains(","))
                        throw new ArgumentException("用户名称不能包含逗号！");
                    if (roleNames.Any(rolename => IsUserInRole(username, rolename)))
                        throw new ProviderException("角色中已存在该用户！");
                }

                var users = _userRepository.GetFiltered(u => usernames.Contains(u.UserName)).ToList();
                var roles = _roleRepository.GetFiltered(r => roleNames.Contains(r.Name)).ToList();
                roles.ForEach(r => users.ForEach(u => _userRoleRepository.Add(new UserRole(u.Id, r.Id))));
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("添加用户到角色出错", ex);
                throw;
            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                if (roleName.Contains(","))
                    throw new ArgumentException("角色名称不能包含逗号！");

                if (RoleExists(roleName))
                    throw new ProviderException("角色名称已存在！");

                var role = RoleFactory.CreateRole(roleName);
                _roleRepository.Add(role);
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("创建角色出错", ex);
                throw;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                if (!RoleExists(roleName))
                    throw new ProviderException("角色不存在！");
                if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
                    throw new ProviderException("无法删除在用的角色！");
                var role = _roleRepository.GetFiltered(r => r.Name == roleName).FirstOrDefault();
                _roleRepository.Remove(role);
                return true;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("删除角色出错", ex);
                return false;
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var role = _roleRepository.GetFiltered(r => r.Name == roleName).FirstOrDefault();
            var usersInRole =
                _userRoleRepository.GetFiltered(ur => ur.RoleId == role.Id).Select(ur => ur.UserId).ToArray();
            return _userRepository.GetFiltered(u => usersInRole.Contains(u.Id)).Select(u => u.UserName).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _roleRepository.GetAll().Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
            if (user == null) return null;
            var userRoles = user.UserRoles.Select(ur => ur.RoleId).ToArray();
            return _roleRepository.GetFiltered(r => userRoles.Contains(r.Id)).Select(r => r.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = _roleRepository.GetFiltered(r => r.Name == roleName).FirstOrDefault();
            if (role == null) return null;
            var userRoles =
                _userRoleRepository.GetFiltered(ur => ur.RoleId == role.Id).Select(ur => ur.UserId).ToArray();
            return _userRepository.GetFiltered(u => userRoles.Contains(u.Id)).Select(u => u.UserName).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
            if (user == null) return false;
            var role = _roleRepository.GetFiltered(r => r.Name == roleName).FirstOrDefault();
            return role != null && _userRoleRepository.GetAll().Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                if (roleNames.Any(rolename => !RoleExists(rolename)))
                    throw new ProviderException("角色不存在！");
                if (
                    (from username in usernames
                     from rolename in roleNames
                     where !IsUserInRole(username, rolename)
                     select username).Any())
                    throw new ProviderException("用户不在角色中！");

                var userIds =
                    _userRepository.GetFiltered(u => usernames.Contains(u.UserName)).Select(u => u.Id).ToArray();
                var roleIds = _roleRepository.GetFiltered(r => roleNames.Contains(r.Name)).Select(r => r.Id).ToArray();

                _userRoleRepository.GetFiltered(ur => userIds.Contains(ur.UserId) && roleIds.Contains(ur.RoleId))
                    .ToList()
                    .ForEach(ur => _userRoleRepository.Remove(ur));
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("删除角色用户出错", ex);
                throw;
            }
        }

        public override bool RoleExists(string roleName)
        {
            return _roleRepository.GetAll().Any(r => r.Name == roleName);
        }

        #endregion

        #region 私有方法

        /// <summary>
        ///     记录操作日志。
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="e">异常</param>
        private static void WriteToEventLog(string action, Exception e)
        {
            var log = new EventLog { Source = "UniMembershipProvider", Log = "Application" };

            var message = "发生异常。\n\n";
            message += "操作: " + action + "\n\n";
            message += "异常: " + e;
            log.WriteEntry(message);
        }

        #endregion
    }
}