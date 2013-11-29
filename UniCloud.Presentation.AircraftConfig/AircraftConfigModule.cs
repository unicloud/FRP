﻿//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UniCloud.Presentation.AircraftConfig
{
    [ModuleExport(typeof (AircraftConfigModule))]
    public class AircraftConfigModule : IModule
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