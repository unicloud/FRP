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

using UniCloud.Application.PurchaseBC.ActionCategoryServices;
using UniCloud.Application.PurchaseBC.AircraftTypeServices;
using UniCloud.Application.PurchaseBC.ContractAircraftServices;
using UniCloud.Application.PurchaseBC.ContractEngineServices;
using UniCloud.Application.PurchaseBC.ContractServices;
using UniCloud.Application.PurchaseBC.CurrencyServices;
using UniCloud.Application.PurchaseBC.ForwarderServices;
using UniCloud.Application.PurchaseBC.MaterialServices;
using UniCloud.Application.PurchaseBC.PartServices;
using UniCloud.Application.PurchaseBC.PlanAircraftServices;
using UniCloud.Application.PurchaseBC.Query.ActionCategoryQueries;
using UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries;
using UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries;
using UniCloud.Application.PurchaseBC.Query.ContractEngineQueries;
using UniCloud.Application.PurchaseBC.Query.ContractQueries;
using UniCloud.Application.PurchaseBC.Query.CurrencyQueries;
using UniCloud.Application.PurchaseBC.Query.ForwarderQueries;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
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
using UniCloud.Domain.PurchaseBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
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

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftTypeQuery, AircraftTypeQuery>()
                .Register<IAircraftTypeAppService, AircraftTypeAppService>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
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

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .Register<IActionCategoryQuery, ActionCategoryQuery>()
                .Register<IActionCategoryAppService, ActionCategoryAppService>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion

                #region 计划飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IPlanAircraftQuery, PlanAircraftQuery>()
                .Register<IPlanAircraftAppService, PlanAircraftAppService>()
                .Register<IPlanAircraftRepository, PlanAircraftRepository>()
                #endregion

                #region 合同飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IContractAircraftQuery, ContractAircraftQuery>()
                .Register<IContractAircraftAppService, ContractAircraftAppService>()
                .Register<ILeaseContractAircraftQuery, LeaseContractAircraftQuery>()
                .Register<IPurchaseContractAircraftQuery, PurchaseContractAircraftQuery>()
                .Register<ILeaseContractAircraftAppService, LeaseContractAircraftAppService>()
                .Register<IPurchaseContractAircraftAppService, PurchaseContractAircraftAppService>()
                .Register<IContractAircraftRepository, ContractAircraftRepository>()
                #endregion 

                #region 合同发动机相关配置，包括查询，应用服务，仓储注册
                         .Register<IContractEngineQuery, ContractEngineQuery>()
                         .Register<IContractEngineAppService, ContractEngineAppService>()
                         .Register<ILeaseContractEngineQuery, LeaseContractEngineQuery>()
                         .Register<IPurchaseContractEngineQuery, PurchaseContractEngineQuery>()
                         .Register<ILeaseContractEngineAppService, LeaseContractEngineAppService>()
                         .Register<IPurchaseContractEngineAppService, PurchaseContractEngineAppService>()
                         .Register<IContractEngineRepository, ContractEngineRepository>()
                #endregion 

                #region   关联文档相关配置，包括查询，应用服务，仓储注册

                .Register<IRelatedDocQuery, RelatedDocQuery>()
                .Register<IRelatedDocAppService, RelatedDocAppService>()
                .Register<IRelatedDocRepository, RelatedDocRepository>()
                #endregion 

                #region   币种相关配置，包括查询，应用服务，仓储注册

                .Register<ICurrencyQuery, CurrencyQuery>()
                .Register<ICurrencyAppService, CurrencyAppService>()
                .Register<ICurrencyRepository, CurrencyRepository>()
                #endregion

                ;
        }

        #endregion
    }
}