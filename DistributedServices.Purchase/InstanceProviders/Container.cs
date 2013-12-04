#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，13:11
// 方案：FRP
// 项目：DistributedServices.Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Application.PurchaseBC.ContractServices;
using UniCloud.Application.PurchaseBC.ForwarderServices;
using UniCloud.Application.PurchaseBC.MaterialServices;
using UniCloud.Application.PurchaseBC.PartServices;
using UniCloud.Application.PurchaseBC.Query.ContractQueries;
using UniCloud.Application.PurchaseBC.Query.ForwarderQueries;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Application.PurchaseBC.Query.PartQueries;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.Query.TradeQueries;
using UniCloud.Application.PurchaseBC.ReceptionServices;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Application.PurchaseBC.TradeServices;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Purchase.InstanceProviders
{
    /// <summary>
    ///     DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            Configuration.Create()
                         .UseAutofac()
                         .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 承运人相关配置，包括查询，应用服务，仓储注册

                         .Register<IForwarderQuery, ForwarderQuery>()
                         .Register<IForwarderAppService, ForwarderAppService>()
                         .Register<IForwarderRepository, ForwarderRepository>()
                #endregion

                #region 维修合同相关配置，包括查询，应用服务，仓储注册

                         .Register<IMaintainContractQuery, MaintainContractQuery>()
                         .Register<IMaintainContractAppService, MaintainContractAppService>()
                         .Register<IMaintainContractRepository, MaintainContractRepository>()

                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                         .Register<ISupplierQuery, SupplierQuery>()
                         .Register<ISupplierAppService, SupplierAppService>()
                         .Register<ISupplierCompanyRepository, SupplierCompanyRepository>()
                         .Register<ISupplierRepository, SupplierRepository>()
                         .Register<ILinkmanRepository, LinkmanRepository>()
                         .Register<ISupplierRoleRepository, SupplierRoleRepository>()
                         .Register<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>()
                #endregion

                #region 交易相关配置，包括查询，应用服务，仓储注册

                         .Register<ITradeQuery, TradeQuery>()
                .Register<IOrderQuery, OrderQuery>()
                         .Register<ITradeAppService, TradeAppService>()
                         .Register<ITradeRepository, TradeRepository>()
                         .Register<IOrderRepository, OrderRepository>()

                #endregion

                #region 接机项目相关配置，包括查询，应用服务，仓储注册

                         .Register<IAircraftLeaseReceptionQuery, AircraftLeaseReceptionQuery>()
                         .Register<IAircraftPurchaseReceptionQuery, AircraftPurchaseReceptionQuery>()
                         .Register<IEngineLeaseReceptionQuery, EngineLeaseReceptionQuery>()
                         .Register<IEnginePurchaseReceptionQuery, EnginePurchaseReceptionQuery>()
                         .Register<IAircraftLeaseReceptionAppService, AircraftLeaseReceptionAppService>()
                         .Register<IAircraftPurchaseReceptionAppService, AircraftPurchaseReceptionAppService>()
                         .Register<IEngineLeaseReceptionAppService, EngineLeaseReceptionAppService>()
                         .Register<IEnginePurchaseReceptionAppService, EnginePurchaseReceptionAppService>()
                         .Register<IReceptionRepository, ReceptionRepository>()
                #endregion

                #region 物料相关配置，包括查询，应用服务，仓储注册

                         .Register<IMaterialQuery, MaterialQuery>()
                         .Register<IMaterialAppService, MaterialAppService>()
                         .Register<IMaterialRepository, MaterialRepository>()
                #endregion

                #region 部件相关配置，包括查询，应用服务，仓储注册

                         .Register<IPartQuery, PartQuery>()
                         .Register<IPartAppService, PartAppService>()
                #endregion

                ;
        }

        #endregion
    }
}