#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.MaterialServices;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class MaterialBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                         .UseAutofac()
                         .CreateLog()
                         .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>
                (new WcfPerRequestLifetimeManager())
                #region 物料相关配置，包括查询，应用服务，仓储注册

                         .Register<IMaterialQuery, MaterialQuery>()
                         .Register<IMaterialAppService, MaterialAppService>()
                         .Register<IMaterialRepository, MaterialRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetEngineMaterials()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaterialAppService>();
            // Act
            var result = service.GetEngineMaterials().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGetBFEMaterials()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaterialAppService>();
            // Act
            var result = service.GetBFEMaterials().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}