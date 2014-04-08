#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/18 11:05:46
// 文件名：PaymentScheduleBCTests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PaymentBC.PaymentScheduleServices;
using UniCloud.Application.PaymentBC.Query.PaymentScheduleQueries;
using UniCloud.Domain;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;
using UniCloud.Infrastructure.Crosscutting.InterceptionBehaviors;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class PaymentScheduleBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IPaymentScheduleRepository, PaymentScheduleRepository>()
                #region   付款计划相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPaymentScheduleQuery, PaymentScheduleQuery>()
                .RegisterType<IPaymentScheduleAppService, PaymentScheduleAppService>()
                .RegisterType<IPaymentScheduleRepository, PaymentScheduleRepository>()

                #endregion

                .RegisterType<IPaymentScheduleQuery, PaymentScheduleQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetPaymentSchedules()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IPaymentScheduleAppService>();

            // Act
            var result = service.GetPaymentSchedules().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAcPaymentSchedules()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IPaymentScheduleAppService>();
            // Act
            var result = service.GetAcPaymentSchedules().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}