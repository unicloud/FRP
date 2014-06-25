#region 命名空间

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Unity;
using UniCloud.UserDataService.FoundLdapGroupInfo;
using UniCloud.UserDataService.FoundLdapUserInfo;

#endregion

namespace UniCloud.UserDataService
{
    public class UserDataSync
    {
        private readonly IOrganizationAppService _organizationApp;
        private readonly IUserAppService _userAppService;

        public UserDataSync()
        {
            _userAppService = UniContainer.Resolve<IUserAppService>();
            _organizationApp = UniContainer.Resolve<IOrganizationAppService>();
        }

        public void SyncUserInfo()
        {
            var users = new List<UserDTO>();
            var found = new FoundLdapUserInfoClient();
            var message = found.foundLdapUsers(ConfigurationManager.AppSettings["SyncUserLdapHost"],
                ConfigurationManager.AppSettings["SyncUserLdapPort"],
                ConfigurationManager.AppSettings["SyncUserAdminAccount"],
                ConfigurationManager.AppSettings["SyncUserAdminPwd"], ConfigurationManager.AppSettings["SyncUserOrgNo"]);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var doc = new XmlDocument();
            doc.LoadXml(message);
            var userNodeList = doc.SelectNodes("/root/row");
            if (userNodeList != null)
            {
                foreach (var userNode in userNodeList)
                {
                    var user = new UserDTO();
                    var element = userNode as XmlElement;
                    if (element != null)
                        foreach (var childNode in element.ChildNodes)
                        {
                            var childElement = childNode as XmlElement;
                            if (childElement != null)
                                switch (childElement.Name.ToLower())
                                {
                                    case "empid":
                                        user.Id = Int32.Parse(childElement.InnerText);
                                        break;
                                    case "empno":
                                        user.UserName = childElement.InnerText;
                                        break;
                                    case "empname":
                                        user.DisplayName = childElement.InnerText;
                                        break;
                                    case "emppwd":
                                        user.Password = childElement.InnerText;
                                        break;
                                    case "orgno":
                                        user.OrganizationNo = childElement.InnerText;
                                        break;
                                }
                        }
                    users.Add(user);
                }
                _userAppService.SyncUserInfo(users);
            }
        }

        public void SyncOrganizationInfo()
        {
            var organizations = new List<OrganizationDTO>();
            var found = new FoundLdapGroupInfoClient();
            var message = found.foundLdapGroup(ConfigurationManager.AppSettings["SyncUserLdapHost"],
                ConfigurationManager.AppSettings["SyncUserLdapPort"],
                ConfigurationManager.AppSettings["SyncUserAdminAccount"],
                ConfigurationManager.AppSettings["SyncUserAdminPwd"], ConfigurationManager.AppSettings["SyncUserOrgNo"]);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var doc = new XmlDocument();
            doc.LoadXml(message);
            var organizationNodeList = doc.SelectNodes("/root/row");
            if (organizationNodeList != null)
            {
                foreach (var organizationNode in organizationNodeList)
                {
                    var organization = new OrganizationDTO();
                    var element = organizationNode as XmlElement;
                    if (element != null)
                        foreach (var childNode in element.ChildNodes)
                        {
                            var childElement = childNode as XmlElement;
                            if (childElement != null)
                                switch (childElement.Name.ToLower())
                                {
                                    case "empid":
                                        organization.Id = Int32.Parse(childElement.InnerText);
                                        break;
                                    case "deptno":
                                        organization.Code = childElement.InnerText;
                                        break;
                                    case "deptname":
                                        organization.Name = childElement.InnerText;
                                        break;
                                    case "deptsortno":
                                        organization.Sort = Int32.Parse(childElement.InnerText);
                                        break;
                                }
                        }
                    organizations.Add(organization);
                }
                _organizationApp.SyncOrganizationInfo(organizations);
            }
        }
    }
}