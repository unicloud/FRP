//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.Payment.Invoice;
using UniCloud.Presentation.Payment.QueryAnalyse;

namespace UniCloud.Presentation.Payment
{
    [ModuleExport(typeof (PaymentModule))]
    public class PaymentModule : IModule
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
        }

        private void RegisterView()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(CreditNoteManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(LeaseInvoiceManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(PrePayInvoiceManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(PurchaseInvoiceManager));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(EngineMaintain));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(APUMaintain));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UnderCartMaintain));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FuselageMaintain));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(AnalyseMaintenanceCosts));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FinancingDemandForecast));
        }
    }
}