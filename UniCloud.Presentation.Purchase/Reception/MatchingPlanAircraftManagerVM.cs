using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public class MatchingPlanAircraftManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;

        [ImportingConstructor]
        public MatchingPlanAircraftManagerVM(IRegionManager regionManager)
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
            ContractAircrafts = Service.CreateCollection<ContractAircraftDTO>(_purchaseData.ContractAircrafts);
            Service.RegisterCollectionView(ContractAircrafts); //注册查询集合。
            ContractAircrafts.PropertyChanged += OnViewPropertyChanged;
            PlanAircrafts = Service.CreateCollection<PlanAircraftDTO>(_purchaseData.PlanAircrafts);
            Service.RegisterCollectionView(PlanAircrafts); //注册查询集合。
            PlanAircrafts.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
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
            ContractAircrafts.AutoLoad = true;
            PlanAircrafts.AutoLoad = true;
        }

        #region 业务

        #region 合同飞机集合
        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        #endregion

        #region 计划飞机集合
        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> PlanAircrafts { get; set; }

        #endregion

        #region 选择的合同飞机

        private ContractAircraftDTO _selContractAircraft;

        /// <summary>
        /// 合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return this._selContractAircraft; }
            private set
            {
                if (this._selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    this.RaisePropertyChanged(() => this.SelContractAircraft);
                }
            }
        }

        #endregion

        #region 选择的合同飞机

        private PlanAircraftDTO _selPlanAircraft;

        /// <summary>
        /// 合同飞机
        /// </summary>
        public PlanAircraftDTO SelPlanAircraft
        {
            get { return this._selPlanAircraft; }
            private set
            {
                if (this._selPlanAircraft != value)
                {
                    _selPlanAircraft = value;
                    this.RaisePropertyChanged(() => this.SelPlanAircraft);
                }
            }
        }

        #endregion

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
