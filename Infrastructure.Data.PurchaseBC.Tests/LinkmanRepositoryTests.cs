#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/20，14:27
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC.Tests
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
using UniCloud.Domain.Common.ValueObjects;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class LinkmanRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<ILinkmanRepository, LinkmanRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateLinkmanTest()
        {
            // Arrange
            var linkmanRep = DefaultContainer.Resolve<ILinkmanRepository>();
            var linkman = LinkmanFactory.CreateLinkman("DDD", "12345", "3333", null, "abc@3g",
                new Address("成都", null, null, null), Guid.NewGuid());

            //// Act
            //linkmanRep.Add(linkman);
            //linkmanRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void GetLinkman()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ILinkmanRepository>();

            // Act
            var result = service.GetAll().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}