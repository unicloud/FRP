//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.Portal.Manager;
using UniCloud.Presentation.Portal.Plan;

namespace UniCloud.Presentation.Portal
{
    [ModuleExport(typeof (PortalModule))]
    public class PortalModule : IModule
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
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (ManagerPortal));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (PlanPortal));
        }
    }
}