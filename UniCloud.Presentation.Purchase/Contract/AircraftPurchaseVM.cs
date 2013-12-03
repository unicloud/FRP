#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/27，12:49
// 方案：FRP
// 项目：Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (AircraftPurchaseVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftPurchaseVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _context;

        [ImportingConstructor]
        public AircraftPurchaseVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);

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
            // 创建并注册CollectionView，例如：
            // Orders = Service.CreateCollection<Order>(context.Orders);
            // Service.RegisterCollectionView(Orders);

            ViewTradeDTO = Service.CreateCollection<TradeDTO>(_context.Trades);
            //var fd = new FilterDescriptor("IsClosed", FilterOperator.IsEqualTo, false);
            //ViewTradeDTO.FilterDescriptors.Add(fd);
            Service.RegisterCollectionView(ViewTradeDTO);
            ViewTradeDTO.PropertyChanged += OnViewPropertyChanged;

            ViewAircraftPurchaseOrderDTO =
                Service.CreateCollection<AircraftPurchaseOrderDTO>(_context.AircraftPurchaseOrders);
            Service.RegisterCollectionView(ViewAircraftPurchaseOrderDTO);
            ViewAircraftPurchaseOrderDTO.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _context = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_context);
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
            // 将CollectionView的AutoLoad属性设为True，例如：
            // Orders.AutoLoad = true;
            ViewTradeDTO.AutoLoad = true;
            ViewAircraftPurchaseOrderDTO.AutoLoad = true;
        }

        #region 交易

        private TradeDTO _selTradeDTO;

        /// <summary>
        ///     交易集合
        /// </summary>
        public QueryableDataServiceCollectionView<TradeDTO> ViewTradeDTO { get; set; }

        /// <summary>
        ///     选中的交易
        /// </summary>
        public TradeDTO SelTradeDTO
        {
            get { return _selTradeDTO; }
            set
            {
                if (_selTradeDTO != value)
                {
                    _selTradeDTO = value;
                    RaisePropertyChanged(() => SelTradeDTO);
                }
            }
        }

        #endregion

        #region 购买飞机订单

        private AircraftPurchaseOrderDTO _selAircraftPurchaseOrderDTO;

        /// <summary>
        ///     购买飞机订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseOrderDTO> ViewAircraftPurchaseOrderDTO { get; set; }

        /// <summary>
        ///     选中的购买飞机订单
        /// </summary>
        public AircraftPurchaseOrderDTO SelAircraftPurchaseOrderDTO
        {
            get { return _selAircraftPurchaseOrderDTO; }
            set
            {
                if (_selAircraftPurchaseOrderDTO != value)
                {
                    _selAircraftPurchaseOrderDTO = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseOrderDTO);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #region 新建交易

        /// <summary>
        ///     新建交易
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var trade = new TradeDTO
            {
                Id = RandomHelper.Next(),
                StartDate = DateTime.Now,
            };
            ViewTradeDTO.AddNew(trade);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 创建新版本订单

        /// <summary>
        ///     创建新版本订单
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除当前版本订单

        /// <summary>
        ///     删除当前版本订单
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
        }

        private bool CanRemove(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}