//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using UniCloud.Application.FleetPlanBC;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AirProgrammingServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.CaacProgrammingServices;
using UniCloud.Application.FleetPlanBC.EnginePlanServices;
using UniCloud.Application.FleetPlanBC.EngineServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.MailAddressServices;
using UniCloud.Application.FleetPlanBC.ManagerServices;
using UniCloud.Application.FleetPlanBC.ManufacturerServices;
using UniCloud.Application.FleetPlanBC.PlanAircraftServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.ProgrammingServices;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.AirProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Application.FleetPlanBC.Query.CaacProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.EnginePlanQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.MailAddressQueries;
using UniCloud.Application.FleetPlanBC.Query.ManagerQueries;
using UniCloud.Application.FleetPlanBC.Query.ManufacturerQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Application.FleetPlanBC.Query.SupplierQueries;
using UniCloud.Application.FleetPlanBC.Query.XmlConfigQueries;
using UniCloud.Application.FleetPlanBC.Query.XmlSettingQueries;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Application.FleetPlanBC.SupplierServices;
using UniCloud.Application.FleetPlanBC.XmlConfigServices;
using UniCloud.Application.FleetPlanBC.XmlSettingServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.FleetPlan.InstanceProviders
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
                //.UserCaching()
                .CreateLog()
                .Register<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IStaticLoad,StaticLoad>()

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .Register<IActionCategoryQuery, ActionCategoryQuery>()
                .Register<IActionCategoryAppService, ActionCategoryAppService>()
                .Register<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion

                #region 实际飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftQuery, AircraftQuery>()
                .Register<IAircraftAppService, AircraftAppService>()
                .Register<IAircraftRepository, AircraftRepository>()
                #endregion

                #region 座级相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftCategoryQuery, AircraftCategoryQuery>()
                .Register<IAircraftCategoryAppService, AircraftCategoryAppService>()
                .Register<IAircraftCategoryRepository, AircraftCategoryRepository>()
                #endregion

                #region 飞机计划相关配置，包括查询，应用服务，仓储注册

                .Register<IPlanQuery, PlanQuery>()
                .Register<IPlanAppService, PlanAppService>()
                .Register<IPlanRepository, PlanRepository>()
                #endregion

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .Register<IAircraftTypeQuery, AircraftTypeQuery>()
                .Register<IAircraftTypeAppService, AircraftTypeAppService>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                #endregion

                #region 航空公司相关配置，包括查询，应用服务，仓储注册

                .Register<IAirlinesQuery, AirlinesQuery>()
                .Register<IAirlinesAppService, AirlinesAppService>()
                .Register<IAirlinesRepository, AirlinesRepository>()
                #endregion

                #region 航空公司五年规划相关配置，包括查询，应用服务，仓储注册

                .Register<IAirProgrammingQuery, AirProgrammingQuery>()
                .Register<IAirProgrammingAppService, AirProgrammingAppService>()
                .Register<IAirProgrammingRepository, AirProgrammingRepository>()
                #endregion

                #region 计划年度相关配置，包括查询，应用服务，仓储注册

                .Register<IAnnualQuery, AnnualQuery>()
                .Register<IAnnualAppService, AnnualAppService>()
                .Register<IAnnualRepository, AnnualRepository>()
                #endregion
                
                #region 批文相关配置，包括查询，应用服务，仓储注册

                .Register<IApprovalDocQuery, ApprovalDocQuery>()
                .Register<IApprovalDocAppService, ApprovalDocAppService>()
                .Register<IApprovalDocRepository, ApprovalDocRepository>()
                #endregion

                #region 民航局五年规划相关配置，包括查询，应用服务，仓储注册

                .Register<ICaacProgrammingQuery, CaacProgrammingQuery>()
                .Register<ICaacProgrammingAppService, CaacProgrammingAppService>()
                .Register<ICaacProgrammingRepository, CaacProgrammingRepository>()
                #endregion

                #region 备发计划相关配置，包括查询，应用服务，仓储注册

                .Register<IEnginePlanQuery, EnginePlanQuery>()
                .Register<IEnginePlanAppService, EnginePlanAppService>()
                .Register<IEnginePlanRepository, EnginePlanRepository>()
                #endregion

                #region 发动机相关配置，包括查询，应用服务，仓储注册

                .Register<IEngineQuery, EngineQuery>()
                .Register<IEngineAppService, EngineAppService>()
                .Register<IEngineRepository, EngineRepository>()
                #endregion

                #region 发动机类型相关配置，包括查询，应用服务，仓储注册

                .Register<IEngineTypeQuery, EngineTypeQuery>()
                .Register<IEngineTypeAppService, EngineTypeAppService>()
                .Register<IEngineTypeRepository, EngineTypeRepository>()
                #endregion

                #region 邮箱账号相关配置，包括查询，应用服务，仓储注册

                .Register<IMailAddressQuery, MailAddressQuery>()
                .Register<IMailAddressAppService, MailAddressAppService>()
                .Register<IMailAddressRepository, MailAddressRepository>()
                #endregion

                #region 管理者相关配置，包括查询，应用服务，仓储注册

                .Register<IManagerQuery, ManagerQuery>()
                .Register<IManagerAppService, ManagerAppService>()
                .Register<IManagerRepository, ManagerRepository>()
                #endregion

                #region 制造商相关配置，包括查询，应用服务，仓储注册

                .Register<IManufacturerQuery, ManufacturerQuery>()
                .Register<IManufacturerAppService, ManufacturerAppService>()
                .Register<IManufacturerRepository, ManufacturerRepository>()
                #endregion

                #region 计划飞相关配置，包括查询，应用服务，仓储注册

                .Register<IPlanAircraftQuery, PlanAircraftQuery>()
                .Register<IPlanAircraftAppService, PlanAircraftAppService>()
                .Register<IPlanAircraftRepository, PlanAircraftRepository>()
                #endregion

                #region 计划发动机相关配置，包括查询，应用服务，仓储注册

                .Register<IPlanEngineQuery, PlanEngineQuery>()
                .Register<IPlanEngineAppService, PlanEngineAppService>()
                .Register<IPlanEngineRepository, PlanEngineRepository>()
                #endregion

                #region 规划期间相关配置，包括查询，应用服务，仓储注册

                .Register<IProgrammingQuery, ProgrammingQuery>()
                .Register<IProgrammingAppService, ProgrammingAppService>()
                .Register<IProgrammingRepository, ProgrammingRepository>()
                #endregion

                #region 申请相关配置，包括查询，应用服务，仓储注册

                .Register<IRequestQuery, RequestQuery>()
                .Register<IRequestAppService, RequestAppService>()
                .Register<IRequestRepository, RequestRepository>()
                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .Register<ISupplierQuery, SupplierQuery>()
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierRepository, SupplierRepository>()
                #endregion

                #region 分析数据相关的xml相关配置，包括查询，应用服务，仓储注册

                .Register<IXmlConfigQuery, XmlConfigQuery>()
                .Register<IXmlConfigAppService, XmlConfigAppService>()
                .Register<IXmlConfigRepository, XmlConfigRepository>()
                #endregion

                #region 配置相关的xml相关配置，包括查询，应用服务，仓储注册

                .Register<IXmlSettingQuery, XmlSettingQuery>()
                .Register<IXmlSettingAppService, XmlSettingAppService>()
                .Register<IXmlSettingRepository, XmlSettingRepository>()
                #endregion

                #region 申请相关配置，包括查询，应用服务，仓储注册

                .Register<IRequestQuery, RequestQuery>()
                .Register<IRequestAppService, RequestAppService>()
                .Register<IRequestRepository, RequestRepository>()
                #endregion

                #region 批文相关配置，包括查询，应用服务，仓储注册

                .Register<IApprovalDocQuery, ApprovalDocQuery>()
                .Register<IApprovalDocAppService, ApprovalDocAppService>()
                .Register<IApprovalDocRepository, ApprovalDocRepository>()
                #endregion

                ;
        }

        #endregion
    }
}