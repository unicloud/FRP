﻿#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;


#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public class AircraftPurchaseReceptionManagerVM : EditMasterSlaveViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        //private PurchaseData _purchaseData;

        [ImportingConstructor]
        public AircraftPurchaseReceptionManagerVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            //Forwarders = Service.CreateCollection<ForwarderDTO>(_purchaseData.Forwarders);
            //Service.RegisterCollectionView(Forwarders);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            //_purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            //return new PurchaseService(_purchaseData);
            return null;
        }

        #endregion

        #region 数据

        #region 公共属性

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            //Forwarders.AutoLoad = true;
        }

        #region 业务

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #region 新建

        #endregion

        #endregion

        #endregion
    }
}