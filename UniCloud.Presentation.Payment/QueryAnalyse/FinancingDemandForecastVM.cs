#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:55:35
// 文件名：FinancingDemandForecastVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof (FinancingDemandForecastVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FinancingDemandForecastVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public FinancingDemandForecastVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            //RelatedDocs = Service.CreateCollection<RelatedDocDTO>(_purchaseData.RelatedDocs);
            //Service.RegisterCollectionView(RelatedDocs); //注册查询集合。
            //RelatedDocs.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            //NewCommand = new DelegateCommand<object>(OnNew, CanNew);
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
            //RelatedDocs.AutoLoad = true;
        }

        #region 业务

        #endregion

        #endregion

        #endregion

        #region 重载操作

        #endregion
    }
}
