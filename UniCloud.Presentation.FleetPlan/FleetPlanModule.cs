//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.FleetPlan.PerformFleetPlan;
using UniCloud.Presentation.FleetPlan.PrepareFleetPlan;

namespace UniCloud.Presentation.FleetPlan
{
    [ModuleExport(typeof (FleetPlanModule))]
    public class FleetPlanModule : IModule
    {
        [Import] public IRegionManager regionManager;

        #region IModule 成员

        public void Initialize()
        {
            RegisterView();
        }

        #endregion

        private void RegisterView()
        {
            //编制运力规划
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(AirProgramming));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(CaacProgramming));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FleetPlanPrepare));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FleetPlanLay));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FleetPlanPublish));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FleetPlanSend));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(SpareEnginePlanLay));

            //执行运力规划
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FleetPlanDeliver));
        }
    }
}