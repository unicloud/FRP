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

using Microsoft.Practices.Unity;
using UniCloud.Application.PurchaseBC.ActionCategoryServices;
using UniCloud.Application.PurchaseBC.AircraftTypeServices;
using UniCloud.Application.PurchaseBC.ContractAircraftServices;
using UniCloud.Application.PurchaseBC.ContractEngineServices;
using UniCloud.Application.PurchaseBC.ContractServices;
using UniCloud.Application.PurchaseBC.CurrencyServices;
using UniCloud.Application.PurchaseBC.DocumentPathServices;
using UniCloud.Application.PurchaseBC.ForwarderServices;
using UniCloud.Application.PurchaseBC.MaterialServices;
using UniCloud.Application.PurchaseBC.OrderDocumentServices;
using UniCloud.Application.PurchaseBC.PartServices;
using UniCloud.Application.PurchaseBC.PlanAircraftServices;
using UniCloud.Application.PurchaseBC.Query.ActionCategoryQueries;
using UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries;
using UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries;
using UniCloud.Application.PurchaseBC.Query.ContractEngineQueries;
using UniCloud.Application.PurchaseBC.Query.ContractQueries;
using UniCloud.Application.PurchaseBC.Query.CurrencyQueries;
using UniCloud.Application.PurchaseBC.Query.DocumentQueries;
using UniCloud.Application.PurchaseBC.Query.ForwarderQueries;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Application.PurchaseBC.Query.OrderDocumentQueries;
using UniCloud.Application.PurchaseBC.Query.PartQueries;
using UniCloud.Application.PurchaseBC.Query.PlanAircraftQueries;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Application.PurchaseBC.Query.RelatedDocQueries;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.Query.TradeQueries;
using UniCloud.Application.PurchaseBC.ReceptionServices;
using UniCloud.Application.PurchaseBC.RelatedDocServices;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Application.PurchaseBC.TradeServices;
using UniCloud.Domain.Events;
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Domain.PurchaseBC.Events;
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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .RegisterType<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())

                #region 领域事件相关配置

                .RegisterType<IPurchaseEvent, PurchaseEvent>(new WcfPerRequestLifetimeManager())

                #endregion

                #region 承运人相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IForwarderQuery, ForwarderQuery>()
                .RegisterType<IForwarderAppService, ForwarderAppService>()
                .RegisterType<IForwarderRepository, ForwarderRepository>()
                #endregion

                #region 维修合同相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IMaintainContractQuery, MaintainContractQuery>()
                .RegisterType<IMaintainContractAppService, MaintainContractAppService>()
                .RegisterType<IMaintainContractRepository, MaintainContractRepository>()

                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ISupplierQuery, SupplierQuery>()
                .RegisterType<ISupplierAppService, SupplierAppService>()
                .RegisterType<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .RegisterType<ISupplierRepository, SupplierRepository>()
                .RegisterType<ILinkmanRepository, LinkmanRepository>()
                .RegisterType<ISupplierRoleRepository, SupplierRoleRepository>()
                .RegisterType<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>()
                #endregion

                #region 交易相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ITradeQuery, TradeQuery>()
                .RegisterType<IOrderQuery, OrderQuery>()
                .RegisterType<ITradeAppService, TradeAppService>()
                .RegisterType<ITradeRepository, TradeRepository>()
                .RegisterType<IOrderRepository, OrderRepository>()

                #endregion

                #region 接机项目相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftLeaseReceptionQuery, AircraftLeaseReceptionQuery>()
                .RegisterType<IAircraftPurchaseReceptionQuery, AircraftPurchaseReceptionQuery>()
                .RegisterType<IEngineLeaseReceptionQuery, EngineLeaseReceptionQuery>()
                .RegisterType<IEnginePurchaseReceptionQuery, EnginePurchaseReceptionQuery>()
                .RegisterType<IAircraftLeaseReceptionAppService, AircraftLeaseReceptionAppService>()
                .RegisterType<IAircraftPurchaseReceptionAppService, AircraftPurchaseReceptionAppService>()
                .RegisterType<IEngineLeaseReceptionAppService, EngineLeaseReceptionAppService>()
                .RegisterType<IEnginePurchaseReceptionAppService, EnginePurchaseReceptionAppService>()
                .RegisterType<IReceptionRepository, ReceptionRepository>()
                #endregion

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                #endregion                

                #region 物料相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IMaterialQuery, MaterialQuery>()
                .RegisterType<IMaterialAppService, MaterialAppService>()
                .RegisterType<IMaterialRepository, MaterialRepository>()
                #endregion

                #region 部件相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPartQuery, PartQuery>()
                .RegisterType<IPartAppService, PartAppService>()
                #endregion

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IActionCategoryQuery, ActionCategoryQuery>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion

                #region 计划飞机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPlanAircraftQuery, PlanAircraftQuery>()
                .RegisterType<IPlanAircraftAppService, PlanAircraftAppService>()
                .RegisterType<IPlanAircraftRepository, PlanAircraftRepository>()
                #endregion

                #region 合同飞机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IContractAircraftQuery, ContractAircraftQuery>()
                .RegisterType<IContractAircraftAppService, ContractAircraftAppService>()
                .RegisterType<ILeaseContractAircraftQuery, LeaseContractAircraftQuery>()
                .RegisterType<IPurchaseContractAircraftQuery, PurchaseContractAircraftQuery>()
                .RegisterType<ILeaseContractAircraftAppService, LeaseContractAircraftAppService>()
                .RegisterType<IPurchaseContractAircraftAppService, PurchaseContractAircraftAppService>()
                .RegisterType<IContractAircraftRepository, ContractAircraftRepository>()
                #endregion 

                #region 合同发动机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IContractEngineQuery, ContractEngineQuery>()
                .RegisterType<IContractEngineAppService, ContractEngineAppService>()
                .RegisterType<ILeaseContractEngineQuery, LeaseContractEngineQuery>()
                .RegisterType<IPurchaseContractEngineQuery, PurchaseContractEngineQuery>()
                .RegisterType<ILeaseContractEngineAppService, LeaseContractEngineAppService>()
                .RegisterType<IPurchaseContractEngineAppService, PurchaseContractEngineAppService>()
                .RegisterType<IContractEngineRepository, ContractEngineRepository>()
                #endregion 

                #region   关联文档相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IRelatedDocQuery, RelatedDocQuery>()
                .RegisterType<IRelatedDocAppService, RelatedDocAppService>()
                .RegisterType<IRelatedDocRepository, RelatedDocRepository>()
                #endregion 

                #region   币种相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ICurrencyQuery, CurrencyQuery>()
                .RegisterType<ICurrencyAppService, CurrencyAppService>()
                .RegisterType<ICurrencyRepository, CurrencyRepository>()
                #endregion

                #region 文档相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IDocumentPathAppService, DocumentPathAppService>()
                .RegisterType<IDocumentPathRepository, DocumentPathRepository>()
                .RegisterType<IDocumentPathQuery, DocumentPathQuery>()

                #endregion

                #region 订单文档相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IContractDocumentAppService, ContractDocumentAppService>()
                .RegisterType<IContractDocumentQuery, ContractDocumentQuery>()
                #endregion

                ;
        }

        #endregion
    }
}