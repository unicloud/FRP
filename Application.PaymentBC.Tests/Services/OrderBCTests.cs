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
using UniCloud.Application.PaymentBC.OrderServices;
using UniCloud.Application.PaymentBC.Query.OrderQueries;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class OrderBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IOrderRepository, OrderRepository>()
                .Register<IOrderAppService, OrderAppService>()
                .Register<IOrderQuery, OrderQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetAircraftPurchaseOrders()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IOrderAppService>();

            // Act
            var result = service.GetAircraftPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGetAllOrders()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IOrderAppService>();

            // Act
            var result = service.GetOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGetPurchaseOrders()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IOrderAppService>();

            // Act
            var result = service.GetPurchaseOrders().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
