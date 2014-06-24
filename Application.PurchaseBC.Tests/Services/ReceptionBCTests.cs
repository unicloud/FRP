#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/19 18:11:10
// 文件名：ReceptionBCTests
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Application.PurchaseBC.ReceptionServices;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class ReceptionBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IReceptionRepository, ReceptionRepository>()
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<IContractAircraftRepository, ContractAircraftRepository>()
                .Register<IAircraftLeaseReceptionAppService, AircraftLeaseReceptionAppService>()
                .Register<IAircraftPurchaseReceptionAppService, AircraftPurchaseReceptionAppService>()
                .Register<IEngineLeaseReceptionAppService, EngineLeaseReceptionAppService>()
                .Register<IEnginePurchaseReceptionAppService, EnginePurchaseReceptionAppService>()
                .Register<IAircraftLeaseReceptionQuery, AircraftLeaseReceptionQuery>()
                .Register<IAircraftPurchaseReceptionQuery, AircraftPurchaseReceptionQuery>()
                .Register<IEngineLeaseReceptionQuery, EngineLeaseReceptionQuery>()
                .Register<IEnginePurchaseReceptionQuery, EnginePurchaseReceptionQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetAircraftLeaseReceptions()
        {
            // Arrange
            var service = UniContainer.Resolve<IAircraftLeaseReceptionAppService>();

            // Act
            var result = service.GetAircraftLeaseReceptions().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestModifyAircraftLeaseReception()
        {
            // Arrange
            var service = UniContainer.Resolve<IAircraftLeaseReceptionAppService>();

            // Act
            var result = service.GetAircraftLeaseReceptions().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TesInsertAircraftLeaseReception()
        {
            // Arrange
            var service = UniContainer.Resolve<IAircraftLeaseReceptionAppService>();
            //Data
            var reception = new AircraftLeaseReceptionDTO
            {
                ReceptionNumber = "20131209001",
                Description = "miaoshu",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsClosed = false,
                CloseDate = DateTime.Now,
                SupplierName = "空客",
                SupplierId = 2,
                ReceptionLines = new List<AircraftLeaseReceptionLineDTO>
                {
                    new AircraftLeaseReceptionLineDTO
                    {
                        MSN = "1231",
                        ReceivedAmount = 123,
                        AcceptedAmount = 121,
                        IsCompleted = false,
                        Note = "note",
                        DeliverDate = DateTime.Now,
                        DeliverPlace = "xiamen",
                        ContractAircraftId = 2,
                    }
                },
                ReceptionSchedules = new List<ReceptionScheduleDTO>
                {
                    new ReceptionScheduleDTO
                    {
                        Subject = "123",
                        Body = "123453",
                        Importance = "高级别",
                        Start = new DateTime(2013, 09, 01),
                        End = DateTime.Now,
                        Group = "机务组",
                        Tempo = "已完成",
                    }
                },
            };
            var result = service.GetAircraftLeaseReceptions();
            //service.InsertAircraftLeaseReception(reception);
            // Act
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}