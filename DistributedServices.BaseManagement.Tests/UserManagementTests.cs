#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:13
// 方案：FRP
// 项目：DistributedServices.BaseManagement.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.DistributedServices.BaseManagement.Tests.BaseManagement;

#endregion

namespace UniCloud.DistributedServices.BaseManagement.Tests
{
    [TestClass]
    public class UserManagementTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            // Arrange
            const string queryString = "CreateUser?userName='admin'&password='1'&email=''&question='first'&answer='first'";
            var uri = new Uri("http://localhost/DistributedServices.BaseManagement/BaseManagementDataService.svc/");
            var context = new BaseManagementData(uri);

            // Act
            context.Execute<string>(new Uri(queryString, UriKind.Relative));
        }
    }
}