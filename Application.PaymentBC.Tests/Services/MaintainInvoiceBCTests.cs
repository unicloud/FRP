﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 17:18:03
// 文件名：MaintainInvoiceBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 17:18:03
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.MaintainInvoiceServices;
using UniCloud.Application.PaymentBC.Query.MaintainCostQueries;
using UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class MaintainInvoiceBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IInvoiceRepository, InvoiceRepository>()
                .RegisterType<IMaintainInvoiceAppService, MaintainInvoiceAppService>()
                .RegisterType<ISupplierRepository, SupplierRepository>()
                .RegisterType<IMaintainCostQuery, MaintainCostQuery>()
                .RegisterType<IMaintainCostRepository, MaintainCostRepository>()
                .RegisterType<IMaintainInvoiceQuery, MaintainInvoiceQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetApuMaintainInvoices()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaintainInvoiceAppService>();

            // Act
            var result = service.GetApuMaintainInvoices().FirstOrDefault();
            //var line = result.MaintainInvoiceLines.FirstOrDefault();
            result.InvoiceNumber = "12345";
            service.ModifyApuMaintainInvoice(result);
            //result.MaintainInvoiceLines.Remove(line);
            //service.ModifyApuMaintainInvoice(result);
            //var add = new APUMaintainInvoiceDTO();
            //add.SerialNumber = "11";
            //add.InvoiceNumber = "222";
            //add.InvoideCode = "22";
            //add.SupplierId = 1;
            //add.CurrencyId = 1;
            //add.Status = 1;
            //add.OperatorName = "123";
            //add.InvoiceDate=DateTime.Now;
            //add.InvoiceValue = 1234;
            //add.PaidAmount = 234;
            //service.InsertApuMaintainInvoice(add);
            // Assert
            //Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetInvoices()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaintainInvoiceAppService>();

            // Act
            var result = service.GetInvoices().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}
