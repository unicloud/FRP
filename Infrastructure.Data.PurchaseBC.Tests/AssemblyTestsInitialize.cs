#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:33
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class AssemblyTestsInitialize
    {
        /// <summary>
        ///     初始化测试用的Unity容器
        /// </summary>
        /// <param name="context">MS TEST 上下文</param>
        [AssemblyInitialize]
        public static void InitializeContainer(TestContext context)
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                .Register<ICurrencyRepository, CurrencyRepository>()
                .Register<ITradeRepository, TradeRepository>()
                .Register<ILinkmanRepository, LinkmanRepository>()
                .Register<IForwarderRepository, ForwarderRepository>()
                .Register<IOrderRepository, OrderRepository>()
                .Register<IMaintainContractRepository, MaintainContractRepository>()
                .Register<IMaterialRepository, MaterialRepository>()
                .Register<IContractAircraftRepository, ContractAircraftRepository>()
                .Register<IContractAircraftBFERepository, ContractAircraftBFERepository>()
                .Register<IRelatedDocRepository, RelatedDocRepository>()
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .Register<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>();
        }

        /// <summary>
        ///     清理测试用的Unity容器
        /// </summary>
        [AssemblyCleanup]
        public static void CleanupContainer()
        {
            UniContainer.Cleanup();
        }
    }
}