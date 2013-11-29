//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UniCloud.Presentation.FleetPlan
{
    [ModuleExport(typeof (FleetPlanModule))]
    public class FleetPlanModule : IModule
    {
        [Import] public IRegionManager regionManager;

        #region IModule 成员

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void RegisterView()
        {
        }
    }
}