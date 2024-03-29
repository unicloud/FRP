﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/18 9:41:20
// 文件名：CurrencyBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/18 9:41:20
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.Query.CurrencyQueries;
using UniCloud.Application.PaymentBC.Query.SupplierQueries;
using UniCloud.Application.PaymentBC.SupplierServices;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Application.PaymentBC.Tests.Services
{
    [TestClass]
    public class CurrencyBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<ICurrencyRepository, CurrencyRepository>()
                .Register<ICurrencyAppService, CurrencyAppService>()
                .Register<ICurrencyQuery, CurrencyQuery>()
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierQuery, SupplierQuery>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion
    }
}