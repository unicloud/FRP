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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
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
using UniCloud.Infrastructure.Utilities.Container;

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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())



                #region 申请相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IFleetTransferService, FleetTransferService>()
                .RegisterType<IAirlinesRepository, AirlinesRepository>()
                .RegisterType<IAircraftRepository, AircraftRepository>()
                .RegisterType<IApprovalDocRepository, ApprovalDocRepository>()
                .RegisterType<IDocumentRepository, DocumentRepository>()
                .RegisterType<IMailAddressRepository, MailAddressRepository>()
                .RegisterType<IPlanRepository, PlanRepository>()
                .RegisterType<IPlanAircraftRepository, PlanAircraftRepository>()
                .RegisterType<IPlanHistoryRepository, PlanHistoryRepository>()
                .RegisterType<IRequestRepository, RequestRepository>()
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
            var service = DefaultContainer.Resolve<IFleetTransferService>();
            Guid curAirlines = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F");
            Guid curReq = Guid.Parse("A729C166-BA4B-401D-BD72-B5A3DC6B44F1");
            // Act
            var result = service.TransferRequest(curAirlines, curReq);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}