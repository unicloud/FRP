﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/18 13:56:50
// 文件名：ContractEngnieBCTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.Query.ContractEngineQueries;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class ContractEngnieBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IContractEngineRepository, ContractEngineRepository>()
                .RegisterType<IContractEngineAppService, ContractEngineAppService>()
                .RegisterType<IContractEngineQuery, ContractEngineQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetContractEngines()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IContractEngineAppService>();

            // Act
            var result = service.GetContractEngines().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

    }
}
