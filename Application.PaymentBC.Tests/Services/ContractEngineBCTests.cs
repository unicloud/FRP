#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/17 16:59:03
// 文件名：OrderBCTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.OrderServices;
using UniCloud.Application.PaymentBC.Query.ContractEngineQueries;
using UniCloud.Application.PaymentBC.Query.OrderQueries;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class ContractEngineBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                #region 合同发动机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IContractEngineQuery, ContractEngineQuery>()
                .RegisterType<IContractEngineAppService, ContractEngineAppService>()
                #endregion

                ;
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
