#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/09，21:11
// 方案：FRP
// 项目：Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Portal;
using UniCloud.Presentation.Service.Project;
using UniCloud.Presentation.Service.Purchase;

#endregion

namespace UniCloud.Presentation.Shell
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (DocViewer).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (ICommonService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IBaseManagementService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IAircraftConfigService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IFleetPlanService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IPurchaseService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IPaymentService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IProjectService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IPortalService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (IPartService).Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.RootVisual = (UIElement) Shell;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<ShellView>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ModuleCatalog();

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "CommonService.xap",
                ModuleName = ModuleNames.CommonService,
                ModuleType =
                    "UniCloud.Presentation.CommonService.CommonServiceModule, UniCloud.Presentation.CommonService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "BaseManagement.xap",
                ModuleName = ModuleNames.BaseManagement,
                ModuleType =
                    "UniCloud.Presentation.BaseManagement.BaseManagementModule, UniCloud.Presentation.BaseManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "AircraftConfig.xap",
                ModuleName = ModuleNames.AircraftConfig,
                ModuleType =
                    "UniCloud.Presentation.AircraftConfig.AircraftConfigModule, UniCloud.Presentation.AircraftConfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "FlightLog.xap",
                ModuleName = ModuleNames.FlightLog,
                ModuleType =
                    "UniCloud.Presentation.FlightLog.FlightLogModule, UniCloud.Presentation.FlightLog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "FleetPlan.xap",
                ModuleName = ModuleNames.FleetPlan,
                ModuleType =
                    "UniCloud.Presentation.FleetPlan.FleetPlanModule, UniCloud.Presentation.FleetPlan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "Purchase.xap",
                ModuleName = ModuleNames.Purchase,
                ModuleType =
                    "UniCloud.Presentation.Purchase.PurchaseModule, UniCloud.Presentation.Purchase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "Payment.xap",
                ModuleName = ModuleNames.Payment,
                ModuleType =
                    "UniCloud.Presentation.Payment.PaymentModule, UniCloud.Presentation.Payment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "Project.xap",
                ModuleName = ModuleNames.Project,
                ModuleType =
                    "UniCloud.Presentation.Project.ProjectModule, UniCloud.Presentation.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "Portal.xap",
                ModuleName = ModuleNames.Portal,
                ModuleType =
                    "UniCloud.Presentation.Portal.PortalModule, UniCloud.Presentation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            moduleCatalog.AddModule(new ModuleInfo
            {
                InitializationMode = InitializationMode.OnDemand,
                Ref = "Part.xap",
                ModuleName = ModuleNames.Part,
                ModuleType =
                    "UniCloud.Presentation.Part.PartModule, UniCloud.Presentation.Part, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            });

            return moduleCatalog;
        }
    }
}