#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class RequestBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                //.UserCaching()
                .CreateLog()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())

      

                #region 申请相关配置，包括查询，应用服务，仓储注册

                .Register<IRequestQuery, RequestQuery>()
                .Register<IRequestAppService, RequestAppService>()
                .Register<IRequestRepository, RequestRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetRequests()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IRequestAppService>();

            // Act
            var result = service.GetRequests().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
        #endregion
    }
}