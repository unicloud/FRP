#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Purchase
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
using UniCloud.Presentation.Purchase.Contract;
using UniCloud.Presentation.Purchase.Forwarder;
using UniCloud.Presentation.Purchase.Reception;
using UniCloud.Presentation.Purchase.Supplier;
using UniCloud.Presentation.Service.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase
{
    [ModuleExport(typeof (PurchaseModule))]
    public class PurchaseModule : IModule
    {
        [Import] public IRegionManager regionManager;

        #region IModule 成员

        public void Initialize()
        {
            LoadStaticData();
            RegisterView();
        }

        #endregion

        private static void LoadStaticData()
        {
            GlobalServiceHelper.LoadSupplier();
            GlobalServiceHelper.LoadAircraftType();
        }

        private void RegisterView()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (ForwarderManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (AircraftPurchase));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (MatchingPlanAircraftManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (AircraftPurchaseReceptionManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (AircraftLeaseReceptionManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (EnginePurchaseReceptionManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (EngineLeaseReceptionManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (SupplierRoleManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (SupplierMaterialManager));
            //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ApuMaintain));
            //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(EngineMaintain));
            //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UndercartMaintain));
            //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(AnalyseAircraftPrice));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(QuerySupplier));

        }
    }
}