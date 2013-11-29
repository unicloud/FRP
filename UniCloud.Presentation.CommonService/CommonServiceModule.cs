//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UniCloud.Presentation.CommonService
{
    [ModuleExport(typeof (CommonServiceModule))]
    public class CommonServiceModule : IModule
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