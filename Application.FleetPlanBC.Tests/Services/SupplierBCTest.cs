#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/7/2 17:06:39
// 文件名：SupplierBCTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/7/2 17:06:39
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
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Application.FleetPlanBC.Query.SupplierQueries;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Application.FleetPlanBC.SupplierServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class SupplierBCTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")


            #region 供应商相关配置，包括查询，应用服务，仓储注册

                .Register<ISupplierQuery, SupplierQuery>()
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<ISupplierRoleRepository,SupplierRoleRepository>()
                .Register<ISupplierCompanyRepository,SupplierCompanyRepository>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetAircraftSuppliers()
        {
            // Arrange
            var service = UniContainer.Resolve<ISupplierAppService>();

            // Act
            var result = service.GetAircraftSuppliers().ToList();

            var result2 = service.GetEngineSuppliers().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        #endregion
    }
}
