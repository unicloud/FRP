using System;
using System.Collections.Generic;
using System.Xml;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.UserDataService.FoundLdapUserInfo;

namespace UniCloud.UserDataService
{
    public class UserDataSync
    {
        private readonly UserAppService _userAppService;
        public UserDataSync(IUserQuery userQuery, IUserRepository userRepository)
        {
            _userAppService = new UserAppService(userQuery, userRepository);
        }

        public void SyncUserInfo()
        {
            var users = new List<UserDTO>();
            var found = new FoundLdapUserInfoClient();
            string message = found.foundLdapUsers("172.18.8.167", "389", "cn=root", "root", "000003");
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var doc = new XmlDocument();
            doc.LoadXml(message);
            XmlNodeList userNodeList = doc.SelectNodes("/root/row");
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
                                    case "empid": user.Id = Int32.Parse(childElement.InnerText); break;
                                    case "empno": user.EmployeeCode = childElement.InnerText; break;
                                    case "empname": user.DisplayName = childElement.InnerText; break;
                                    case "emppwd": user.Password = childElement.InnerText; break;
                                    case "orgno": user.OrganizationNo = childElement.InnerText; break;
                                }
                        }
                    users.Add(user);
                }
                _userAppService.SyncUserInfo(users);
            }
        }
    }
}
