﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.UserDataService.FoundLdapUserInfo;

namespace UniCloud.UserDataService
{
    public class UserDataSync
    {
        private readonly UserAppService _userAppService;
        public UserDataSync()
        {
            _userAppService = new UserAppService(new UserQuery(new BaseManagementBCUnitOfWork()), new UserRepository(new BaseManagementBCUnitOfWork()));
        }

        public void SyncUserInfo()
        {
            var users = new List<UserDTO>();
            var found = new FoundLdapUserInfoClient();
            string message = found.foundLdapUsers(ConfigurationManager.AppSettings["SyncUserLdapHost"], ConfigurationManager.AppSettings["SyncUserLdapPort"],
                ConfigurationManager.AppSettings["SyncUserAdminAccount"], ConfigurationManager.AppSettings["SyncUserAdminPwd"], ConfigurationManager.AppSettings["SyncUserOrgNo"]);
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
