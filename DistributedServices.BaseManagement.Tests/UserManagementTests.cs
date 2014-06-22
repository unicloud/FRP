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
using System.Linq;
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
            var queryString = "CreateAUser?userName='01104'&password='1'";
            var uri = new Uri("http://localhost/DistributedServices.BaseManagement/BaseManagementDataService.svc/");
            var context = new BaseManagementData(uri);

            // Act
            //var result =
            //    context.CreateQuery<string>("CreateUser")
            //        .AddQueryOption("userName", "01104")
            //        .AddQueryOption("password", "1")
            //        .AddQueryOption("email", "dingzi@xiamenair.com")
            //        .AddQueryOption("question", "first")
            //        .AddQueryOption("answer", "first");
            context.Execute<string>(new Uri(queryString, UriKind.Relative));

            // Assert

        }
    }
}