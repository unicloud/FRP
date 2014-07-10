#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 16:32:38
// 文件名：TransferMailTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 16:32:38
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.FleetPlanBC.FleetTransferServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.DocumentAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.FleetPlanBC.Tests.Services
{
    [TestClass]
    public class TransferMailTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")


                #region 申请相关配置，包括查询，应用服务，仓储注册

                .Register<IFleetTransferService, FleetTransferService>()
                .Register<IAirlinesRepository, AirlinesRepository>()
                .Register<IAircraftRepository, AircraftRepository>()
                .Register<IApprovalDocRepository, ApprovalDocRepository>()
                .Register<IDocumentRepository, DocumentRepository>()
                .Register<IMailAddressRepository, MailAddressRepository>()
                .Register<IPlanRepository, PlanRepository>()
                .Register<IPlanAircraftRepository, PlanAircraftRepository>()
                .Register<IPlanHistoryRepository, PlanHistoryRepository>()
                .Register<IRequestRepository, RequestRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TransferPlan()
        {
            // Arrange
            var service = UniContainer.Resolve<IFleetTransferService>();
            var curAirlines = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F");
            var curPlan = Guid.Parse("3791B2DA-1E62-47CD-8115-0DC248765B89");
            // Act
            var result = service.TransferPlan(curAirlines, curPlan);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}