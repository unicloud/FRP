#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10，13:11
// 方案：FRP
// 项目：DistributedServices.Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Microsoft.Practices.Unity;
using UniCloud.Application.PartBC.AcConfigServices;
using UniCloud.Application.PartBC.AcDailyUtilizationServices;
using UniCloud.Application.PartBC.AdSbServices;
using UniCloud.Application.PartBC.AircraftServices;
using UniCloud.Application.PartBC.AircraftTypeServices;
using UniCloud.Application.PartBC.AirStructureDamageServices;
using UniCloud.Application.PartBC.AnnualMaintainPlanServices;
using UniCloud.Application.PartBC.BasicConfigGroupServices;
using UniCloud.Application.PartBC.BasicConfigHistoryServices;
using UniCloud.Application.PartBC.BasicConfigServices;
using UniCloud.Application.PartBC.ContractAircraftServices;
using UniCloud.Application.PartBC.CtrlUnitServices;
using UniCloud.Application.PartBC.InstallControllerServices;
using UniCloud.Application.PartBC.ItemServices;
using UniCloud.Application.PartBC.MaintainCtrlServices;
using UniCloud.Application.PartBC.MaintainWorkServices;
using UniCloud.Application.PartBC.ModServices;
using UniCloud.Application.PartBC.OilMonitorServices;
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Application.PartBC.Query.AcConfigQueries;
using UniCloud.Application.PartBC.Query.AcDailyUtilizationQueries;
using UniCloud.Application.PartBC.Query.AdSbQueries;
using UniCloud.Application.PartBC.Query.AircraftQueries;
using UniCloud.Application.PartBC.Query.AircraftTypeQueries;
using UniCloud.Application.PartBC.Query.AirStructureDamageQueries;
using UniCloud.Application.PartBC.Query.AnnualMaintainPlanQueries;
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Application.PartBC.Query.BasicConfigHistoryQueries;
using UniCloud.Application.PartBC.Query.BasicConfigQueries;
using UniCloud.Application.PartBC.Query.ContractAircraftQueries;
using UniCloud.Application.PartBC.Query.CtrlUnitQueries;
using UniCloud.Application.PartBC.Query.InstallControllerQueries;
using UniCloud.Application.PartBC.Query.ItemQueries;
using UniCloud.Application.PartBC.Query.MaintainCtrlQueries;
using UniCloud.Application.PartBC.Query.MaintainWorkQueries;
using UniCloud.Application.PartBC.Query.ModQueries;
using UniCloud.Application.PartBC.Query.OilMonitorQueries;
using UniCloud.Application.PartBC.Query.PnRegQueries;
using UniCloud.Application.PartBC.Query.ScnQueries;
using UniCloud.Application.PartBC.Query.SnHistoryQueries;
using UniCloud.Application.PartBC.Query.SnRegQueries;
using UniCloud.Application.PartBC.Query.SnRemInstRecordQueries;
using UniCloud.Application.PartBC.Query.SpecialConfigQueries;
using UniCloud.Application.PartBC.ScnServices;
using UniCloud.Application.PartBC.SnHistoryServices;
using UniCloud.Application.PartBC.SnRegServices;
using UniCloud.Application.PartBC.SnRemInstRecordServices;
using UniCloud.Application.PartBC.SpecialConfigServices;
using UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.ModAgg;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Part.InstanceProviders
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
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                //.RegisterType<IEventAggregator, EventAggregator>(new WcfPerRequestLifetimeManager())

            #region 领域事件相关配置

                //.RegisterType<IPartEvent, PartEvent>(new WcfPerRequestLifetimeManager())

            #endregion

            #region 飞机日利用率相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IAcDailyUtilizationQuery, AcDailyUtilizationQuery>()
                .RegisterType<IAcDailyUtilizationAppService, AcDailyUtilizationAppService>()
                .RegisterType<IAcDailyUtilizationRepository, AcDailyUtilizationRepository>()

            #endregion

            #region 运营飞机相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftQuery, AircraftQuery>()
                .RegisterType<IAircraftAppService, AircraftAppService>()
                .RegisterType<IAircraftRepository, AircraftRepository>()
            #endregion

            #region 机型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<IAircraftTypeAppService, AircraftTypeAppService>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<IAircraftSeriesRepository, AircraftSeriesRepository>()
            #endregion

            #region 功能构型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAcConfigQuery, AcConfigQuery>()
                .RegisterType<IAcConfigAppService, AcConfigAppService>()
            #endregion

            #region 基本构型组相关配置，包括查询，应用服务，仓储注册

.RegisterType<IBasicConfigGroupQuery, BasicConfigGroupQuery>()
                .RegisterType<IBasicConfigGroupAppService, BasicConfigGroupAppService>()
                .RegisterType<IBasicConfigGroupRepository, BasicConfigGroupRepository>()
            #endregion

            #region 基本构型相关配置，包括查询，应用服务，仓储注册

.RegisterType<IBasicConfigQuery, BasicConfigQuery>()
                .RegisterType<IBasicConfigAppService, BasicConfigAppService>()
                .RegisterType<IBasicConfigRepository, BasicConfigRepository>()
            #endregion

            #region 基本构型历史相关配置，包括查询，应用服务，仓储注册

.RegisterType<IBasicConfigHistoryQuery, BasicConfigHistoryQuery>()
                .RegisterType<IBasicConfigHistoryAppService, BasicConfigHistoryAppService>()
                .RegisterType<IBasicConfigHistoryRepository, BasicConfigHistoryRepository>()
            #endregion

            #region 合同飞机相关配置，包括查询，应用服务，仓储注册

.RegisterType<IContractAircraftQuery, ContractAircraftQuery>()
                .RegisterType<IContractAircraftAppService, ContractAircraftAppService>()
                .RegisterType<IContractAircraftRepository, ContractAircraftRepository>()
            #endregion

            #region 控制单位相关配置，包括查询，应用服务，仓储注册

.RegisterType<ICtrlUnitQuery, CtrlUnitQuery>()
                .RegisterType<ICtrlUnitAppService, CtrlUnitAppService>()
                .RegisterType<ICtrlUnitRepository, CtrlUnitRepository>()
            #endregion

            #region 附件项相关配置，包括查询，应用服务，仓储注册

.RegisterType<IItemQuery, ItemQuery>()
                .RegisterType<IItemAppService, ItemAppService>()
                .RegisterType<IItemRepository, ItemRepository>()
            #endregion

            #region 装机控制相关配置，包括查询，应用服务，仓储注册

.RegisterType<IInstallControllerQuery, InstallControllerQuery>()
                .RegisterType<IInstallControllerAppService, InstallControllerAppService>()
                .RegisterType<IInstallControllerRepository, InstallControllerRepository>()
            #endregion

            #region 维修控制组相关配置，包括查询，应用服务，仓储注册

.RegisterType<IMaintainCtrlQuery, MaintainCtrlQuery>()
                .RegisterType<IMaintainCtrlAppService, MaintainCtrlAppService>()
                .RegisterType<IMaintainCtrlRepository, MaintainCtrlRepository>()
            #endregion

            #region 维修工作相关配置，包括查询，应用服务，仓储注册

.RegisterType<IMaintainWorkQuery, MaintainWorkQuery>()
                .RegisterType<IMaintainWorkAppService, MaintainWorkAppService>()
                .RegisterType<IMaintainWorkRepository, MaintainWorkRepository>()
            #endregion

            #region Mod号相关配置，包括查询，应用服务，仓储注册

.RegisterType<IModQuery, ModQuery>()
                .RegisterType<IModAppService, ModAppService>()
                .RegisterType<IModRepository, ModRepository>()
            #endregion

            #region 附件相关配置，包括查询，应用服务，仓储注册

.RegisterType<IPnRegQuery, PnRegQuery>()
                .RegisterType<IPnRegAppService, PnRegAppService>()
                .RegisterType<IPnRegRepository, PnRegRepository>()
            #endregion

            #region SCN相关配置，包括查询，应用服务，仓储注册

.RegisterType<IScnQuery, ScnQuery>()
                .RegisterType<IScnAppService, ScnAppService>()
                .RegisterType<IScnRepository, ScnRepository>()
                .RegisterType<IAirBusScnRepository, AirBusScnRepository>()
            #endregion

            #region 序号件相关配置，包括查询，应用服务，仓储注册

.RegisterType<ISnRegQuery, SnRegQuery>()
                .RegisterType<ISnRegAppService, SnRegAppService>()
                .RegisterType<ISnRegRepository, SnRegRepository>()
            #endregion

            #region 序号件装机历史相关配置，包括查询，应用服务，仓储注册

.RegisterType<ISnHistoryQuery, SnHistoryQuery>()
                .RegisterType<ISnHistoryAppService, SnHistoryAppService>()
                .RegisterType<ISnHistoryRepository, SnHistoryRepository>()
            #endregion

            #region 序号件拆换记录相关配置，包括查询，应用服务，仓储注册

.RegisterType<ISnRemInstRecordQuery, SnRemInstRecordQuery>()
                .RegisterType<ISnRemInstRecordAppService, SnRemInstRecordAppService>()
                .RegisterType<ISnRemInstRecordRepository, SnRemInstRecordRepository>()
            #endregion

            #region 特殊选型相关配置，包括查询，应用服务，仓储注册

.RegisterType<ISpecialConfigQuery, SpecialConfigQuery>()
                .RegisterType<ISpecialConfigAppService, SpecialConfigAppService>()
                .RegisterType<ISpecialConfigRepository, SpecialConfigRepository>()
            #endregion

            #region 滑油监控相关配置，包括查询、应用服务、仓储注册

.RegisterType<IOilMonitorQuery, OilMonitorQuery>()
                .RegisterType<IOilMonitorAppService, OilMonitorAppService>()
                .RegisterType<IOilMonitorRepository, OilMonitorRepository>()

            #endregion

            #region AirStructureDamage相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAirStructureDamageQuery, AirStructureDamageQuery>()
                .RegisterType<IAirStructureDamageAppService, AirStructureDamageAppService>()
                .RegisterType<IAirStructureDamageRepository, AirStructureDamageRepository>()
            #endregion

            #region AdSb相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAdSbQuery, AdSbQuery>()
                .RegisterType<IAdSbAppService, AdSbAppService>()
                .RegisterType<IAdSbRepository, AdSbRepository>()
            #endregion

            #region 年度送修计划相关配置，包括查询，应用服务，仓储注册

.RegisterType<IAnnualMaintainPlanQuery, AnnualMaintainPlanQuery>()
                .RegisterType<IAnnualMaintainPlanAppService, AnnualMaintainPlanAppService>()
                .RegisterType<IAnnualMaintainPlanRepository, AnnualMaintainPlanRepository>()
            #endregion
;
        }

        #endregion
    }
}