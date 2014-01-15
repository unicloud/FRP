#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：AircraftConfig
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig;

#endregion

namespace UniCloud.Presentation.AircraftConfig
{
    [ModuleExport(typeof (AircraftConfigModule))]
    public class AircraftConfigModule : IModule
    {
        [Import] public IRegionManager regionManager;

        #region IModule 成员

        public void Initialize()
        {
            //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ManagerAircraftSeries));
        }

        #endregion
    }
}