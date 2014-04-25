//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using Microsoft.Practices.Unity;
using UniCloud.Application.FleetPlanBC.ActionCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftCategoryServices;
using UniCloud.Application.FleetPlanBC.AircraftConfigurationServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanHistoryServices;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.AircraftSeriesServices;
using UniCloud.Application.FleetPlanBC.AircraftServices;
using UniCloud.Application.FleetPlanBC.AircraftTypeServices;
using UniCloud.Application.FleetPlanBC.AirlinesServices;
using UniCloud.Application.FleetPlanBC.AirProgrammingServices;
using UniCloud.Application.FleetPlanBC.AnnualServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.CAACAircraftTypeServices;
using UniCloud.Application.FleetPlanBC.CaacProgrammingServices;
using UniCloud.Application.FleetPlanBC.EnginePlanServices;
using UniCloud.Application.FleetPlanBC.EngineServices;
using UniCloud.Application.FleetPlanBC.EngineTypeServices;
using UniCloud.Application.FleetPlanBC.IssuedUnitServices;
using UniCloud.Application.FleetPlanBC.MailAddressServices;
using UniCloud.Application.FleetPlanBC.ManagerServices;
using UniCloud.Application.FleetPlanBC.ManufacturerServices;
using UniCloud.Application.FleetPlanBC.PlanAircraftServices;
using UniCloud.Application.FleetPlanBC.PlanEngineServices;
using UniCloud.Application.FleetPlanBC.ProgrammingFileServices;
using UniCloud.Application.FleetPlanBC.ProgrammingServices;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftConfigurationQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftSeriesQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.AirProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Application.FleetPlanBC.Query.CAACAircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.CaacProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.EnginePlanQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.IssuedUnitQueries;
using UniCloud.Application.FleetPlanBC.Query.MailAddressQueries;
using UniCloud.Application.FleetPlanBC.Query.ManagerQueries;
using UniCloud.Application.FleetPlanBC.Query.ManufacturerQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Application.FleetPlanBC.Query.PlanHistoryQueries;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingFileQueries;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.RelatedDocQueries;
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Application.FleetPlanBC.Query.SupplierQueries;
using UniCloud.Application.FleetPlanBC.Query.XmlConfigQueries;
using UniCloud.Application.FleetPlanBC.RelatedDocServices;
using UniCloud.Application.FleetPlanBC.RequestServices;
using UniCloud.Application.FleetPlanBC.SupplierServices;
using UniCloud.Application.FleetPlanBC.XmlConfigServices;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.FleetPlanBC.Repositories;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

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
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, FleetPlanBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 活动类型相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IActionCategoryQuery, ActionCategoryQuery>()
                .RegisterType<IActionCategoryAppService, ActionCategoryAppService>()
                .RegisterType<IActionCategoryRepository, ActionCategoryRepository>()
                #endregion

                #region 飞机配置相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftConfigurationQuery, AircraftConfigurationQuery>()
                .RegisterType<IAircraftConfigurationAppService, AircraftConfigurationAppService>()
                .RegisterType<IAircraftConfigurationRepository, AircraftConfigurationRepository>()
                #endregion

                #region 飞机系列相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftSeriesQuery, AircraftSeriesQuery>()
                .RegisterType<IAircraftSeriesAppService, AircraftSeriesAppService>()
                .RegisterType<IAircraftSeriesRepository, AircraftSeriesRepository>()
                #endregion

                #region 实际飞机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftQuery, AircraftQuery>()
                .RegisterType<IAircraftAppService, AircraftAppService>()
                .RegisterType<IAircraftRepository, AircraftRepository>()
                #endregion

                #region 座级相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftCategoryQuery, AircraftCategoryQuery>()
                .RegisterType<IAircraftCategoryAppService, AircraftCategoryAppService>()
                .RegisterType<IAircraftCategoryRepository, AircraftCategoryRepository>()
                #endregion

                #region 飞机计划相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPlanQuery, PlanQuery>()
                .RegisterType<IPlanAppService, PlanAppService>()
                .RegisterType<IPlanRepository, PlanRepository>()
                .RegisterType<IPlanHistoryQuery, PlanHistoryQuery>()
                .RegisterType<IPlanHistoryAppService, PlanHistoryAppService>()
                .RegisterType<IPlanHistoryRepository, PlanHistoryRepository>()
                #endregion

                #region 机型相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<ICAACAircraftTypeQuery, CAACAircraftTypeQuery>()
                .RegisterType<ICAACAircraftTypeAppService, CAACAircraftTypeAppService>()
                .RegisterType<ICAACAircraftTypeRepository, CAACAircraftTypeRepository>()
                #endregion

                #region 航空公司相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAirlinesQuery, AirlinesQuery>()
                .RegisterType<IAirlinesAppService, AirlinesAppService>()
                .RegisterType<IAirlinesRepository, AirlinesRepository>()
                #endregion

                #region 航空公司五年规划相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAirProgrammingQuery, AirProgrammingQuery>()
                .RegisterType<IAirProgrammingAppService, AirProgrammingAppService>()
                .RegisterType<IAirProgrammingRepository, AirProgrammingRepository>()
                #endregion

                #region 计划年度相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAnnualQuery, AnnualQuery>()
                .RegisterType<IAnnualAppService, AnnualAppService>()
                .RegisterType<IAnnualRepository, AnnualRepository>()
                #endregion
                
                #region 批文相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IApprovalDocQuery, ApprovalDocQuery>()
                .RegisterType<IApprovalDocAppService, ApprovalDocAppService>()
                .RegisterType<IApprovalDocRepository, ApprovalDocRepository>()
                #endregion

                #region 民航局五年规划相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ICaacProgrammingQuery, CaacProgrammingQuery>()
                .RegisterType<ICaacProgrammingAppService, CaacProgrammingAppService>()
                .RegisterType<ICaacProgrammingRepository, CaacProgrammingRepository>()
                #endregion

                #region 备发计划相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IEnginePlanQuery, EnginePlanQuery>()
                .RegisterType<IEnginePlanAppService, EnginePlanAppService>()
                .RegisterType<IEnginePlanRepository, EnginePlanRepository>()
                #endregion

                #region 发动机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IEngineQuery, EngineQuery>()
                .RegisterType<IEngineAppService, EngineAppService>()
                .RegisterType<IEngineRepository, EngineRepository>()
                #endregion

                #region 发动机类型相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IEngineTypeQuery, EngineTypeQuery>()
                .RegisterType<IEngineTypeAppService, EngineTypeAppService>()
                .RegisterType<IEngineTypeRepository, EngineTypeRepository>()
                #endregion

                #region 邮箱账号相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IMailAddressQuery, MailAddressQuery>()
                .RegisterType<IMailAddressAppService, MailAddressAppService>()
                .RegisterType<IMailAddressRepository, MailAddressRepository>()
                #endregion

                #region 发文单位相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IIssuedUnitQuery, IssuedUnitQuery>()
                .RegisterType<IIssuedUnitAppService, IssuedUnitAppService>()
                .RegisterType<IIssuedUnitRepository, IssuedUnitRepository>()
                #endregion

                #region 管理者相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IManagerQuery, ManagerQuery>()
                .RegisterType<IManagerAppService, ManagerAppService>()
                .RegisterType<IManagerRepository, ManagerRepository>()
                #endregion

                #region 制造商相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IManufacturerQuery, ManufacturerQuery>()
                .RegisterType<IManufacturerAppService, ManufacturerAppService>()
                .RegisterType<IManufacturerRepository, ManufacturerRepository>()
                #endregion

                #region 计划飞相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPlanAircraftQuery, PlanAircraftQuery>()
                .RegisterType<IPlanAircraftAppService, PlanAircraftAppService>()
                .RegisterType<IPlanAircraftRepository, PlanAircraftRepository>()
                #endregion

                #region 计划发动机相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPlanEngineQuery, PlanEngineQuery>()
                .RegisterType<IPlanEngineAppService, PlanEngineAppService>()
                .RegisterType<IPlanEngineRepository, PlanEngineRepository>()
                #endregion

                #region 规划期间相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IProgrammingQuery, ProgrammingQuery>()
                .RegisterType<IProgrammingAppService, ProgrammingAppService>()
                .RegisterType<IProgrammingRepository, ProgrammingRepository>()
                #endregion

                #region 规划文档相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IProgrammingFileQuery, ProgrammingFileQuery>()
                .RegisterType<IProgrammingFileAppService, ProgrammingFileAppService>()
                .RegisterType<IProgrammingFileRepository, ProgrammingFileRepository>()
                #endregion

                #region 申请相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IRequestQuery, RequestQuery>()
                .RegisterType<IRequestAppService, RequestAppService>()
                .RegisterType<IRequestRepository, RequestRepository>()
                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ISupplierQuery, SupplierQuery>()
                .RegisterType<ISupplierAppService, SupplierAppService>()
                .RegisterType<ISupplierRepository, SupplierRepository>()
                #endregion

                #region 分析数据相关的xml相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IXmlConfigQuery, XmlConfigQuery>()
                .RegisterType<IXmlConfigAppService, XmlConfigAppService>()
                .RegisterType<IXmlConfigRepository, XmlConfigRepository>()
                #endregion

                #region 申请相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IRequestQuery, RequestQuery>()
                .RegisterType<IRequestAppService, RequestAppService>()
                .RegisterType<IRequestRepository, RequestRepository>()
                #endregion

                #region 批文相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IApprovalDocQuery, ApprovalDocQuery>()
                .RegisterType<IApprovalDocAppService, ApprovalDocAppService>()
                .RegisterType<IApprovalDocRepository, ApprovalDocRepository>()
                #endregion

                #region 关联文档相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IRelatedDocQuery, RelatedDocQuery>()
                .RegisterType<IRelatedDocAppService, RelatedDocAppService>()
                .RegisterType<IRelatedDocRepository, RelatedDocRepository>()
                #endregion
                ;
        }

        #endregion
    }
}