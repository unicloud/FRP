#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/20 15:21:31
// 文件名：MaintainContractRepositoryTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class MaintainContractRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IMaintainContractRepository, MaintainContractRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateMaintainContractTest()
        {
            // Arrange
            var contractRep = UniContainer.Resolve<IMaintainContractRepository>();
            var maintainContract = MaintainContractFactory.CreateEngineMaintainContract();
            MaintainContractFactory.SetMaintainContract(maintainContract, "12", "12", "12", DateTime.Now, "12", 1,
                new Guid(), "aa.docx");
            // Act
            contractRep.Add(maintainContract);
            contractRep.UnitOfWork.Commit();
        }
    }
}