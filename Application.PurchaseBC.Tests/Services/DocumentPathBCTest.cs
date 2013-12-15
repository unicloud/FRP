#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/14，13:12
// 文件名：DocumentPathBCTest.cs
// 程序集：UniCloud.Application.PurchaseBC.Tests
// 版本：VVersionNumber
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.DocumentPathServices;
using UniCloud.Application.PurchaseBC.Query.DocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentPathAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class DocumentPathBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                         .UseAutofac()
                         .CreateLog()
                         .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 文档相关配置，包括查询，应用服务，仓储注册

                         .Register<IDocumentPathAppService, DocumentPathAppService>()
                         .Register<IDocumentPathRepository, DocumentPathRepository>()
                         .Register<IDocumentPathQuery, DocumentPathQuery>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetDocumentPaths()
        {
            try
            {
                // Arrange
                var service = DefaultContainer.Resolve<IDocumentPathAppService>();

                // Act
                var result = service.GetDocumentPaths().ToList();

                // Assert
                Assert.IsTrue(result.Any());
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        #endregion
    }
}