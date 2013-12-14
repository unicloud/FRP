﻿#region 版本信息

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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
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
        private DocumentDTO _document = new DocumentDTO();
        [Import] public DocumentViewer documentView;

        [ImportingConstructor]
        public AircraftPurchaseVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            AddTradeCommand = new DelegateCommand<object>(OnAddTrade, CanAddTrade);
            RemoveTradeCommand = new DelegateCommand<object>(OnRemoveTrade, CanRemoveTrade);
            AddOrderCommand = new DelegateCommand<object>(OnAddOrder, CanAddOrder);
            RemoveOrderCommand = new DelegateCommand<object>(OnRemoveOrder, CanRemoveOrder);
            AddOrderLineCommand = new DelegateCommand<object>(OnAddOrderLine, CanAddOrderLine);
            RemoveOrderLineCommand = new DelegateCommand<object>(OnRemoveOrderLine, CanRemoveOrderLine);

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
            ViewTradeDTO = Service.CreateCollection<TradeDTO>(_context.Trades);
            var fd1 = new FilterDescriptor("IsClosed", FilterOperator.IsEqualTo, false);
            ViewTradeDTO.FilterDescriptors.Add(fd1);
            Service.RegisterCollectionView(ViewTradeDTO);
            ViewTradeDTO.PropertyChanged += OnViewPropertyChanged;

            ViewAircraftPurchaseOrderDTO =
                Service.CreateCollection<AircraftPurchaseOrderDTO>(_context.AircraftPurchaseOrders);
            //var fd2 = new FilterDescriptor("TradeId", FilterOperator.IsEqualTo, null);
            //ViewAircraftPurchaseOrderDTO.FilterDescriptors.Add(fd2);
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

        /// <summary>
        ///     供应商
        /// </summary>
        public IEnumerable<SupplierDTO> Suppliers
        {
            get { return GlobalServiceHelper.Suppliers; }
        }

        /// <summary>
        ///     币种
        /// </summary>
        public IEnumerable<CurrencyDTO> Currencies
        {
            get { return GlobalServiceHelper.Currencies; }
        }

        /// <summary>
        ///     联系人
        /// </summary>
        public IEnumerable<LinkmanDTO> Linkmen
        {
            get { return GlobalServiceHelper.Linkmen; }
        }

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
            if (!ViewTradeDTO.Any())
            {
                ViewTradeDTO.AutoLoad = true;
            }
            if (!ViewAircraftPurchaseOrderDTO.Any())
            {
                ViewAircraftPurchaseOrderDTO.AutoLoad = true;
            }
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

        #region 添加合同文档

        protected override void OnAddAttach(object sender)
        {
            documentView.ViewModel.InitData(false, _selAircraftPurchaseOrderDTO.ContractDocGuid, DocumentViewerClosed);
            documentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (documentView.Tag is DocumentDTO)
            {
                _document = documentView.Tag as DocumentDTO;
                SelAircraftPurchaseOrderDTO.ContractName = _document.Name;
                SelAircraftPurchaseOrderDTO.ContractDocGuid = _document.DocumentId;
            }
        }

        #endregion

        #region 查看合同文档

        protected override void OnViewAttach(object sender)
        {
            documentView.ViewModel.InitData(true, _selAircraftPurchaseOrderDTO.ContractDocGuid, null);
            documentView.ShowDialog();
        }

        #endregion

        #endregion

        #region 创建新交易

        /// <summary>
        ///     创建新交易
        /// </summary>
        public DelegateCommand<object> AddTradeCommand { get; private set; }

        private void OnAddTrade(object obj)
        {
            var trade = new TradeDTO
            {
                Id = RandomHelper.Next(),
                StartDate = DateTime.Now,
            };
            ViewTradeDTO.AddNew(trade);
        }

        private bool CanAddTrade(object obj)
        {
            return true;
        }

        #endregion

        #region 删除交易

        /// <summary>
        ///     删除交易
        /// </summary>
        public DelegateCommand<object> RemoveTradeCommand { get; private set; }

        private void OnRemoveTrade(object obj)
        {
            if (_selTradeDTO != null)
            {
                ViewTradeDTO.Remove(_selTradeDTO);
            }
        }

        private bool CanRemoveTrade(object obj)
        {
            return true;
        }

        #endregion

        #region 创建新订单

        /// <summary>
        ///     创建新订单
        /// </summary>
        public DelegateCommand<object> AddOrderCommand { get; private set; }

        private void OnAddOrder(object obj)
        {
            var order = new AircraftPurchaseOrderDTO
            {
                OrderDate = DateTime.Now,
                TradeId = _selTradeDTO.Id,
            };
            ViewAircraftPurchaseOrderDTO.AddNew(order);
        }

        private bool CanAddOrder(object obj)
        {
            return true;
        }

        #endregion

        #region 删除订单

        /// <summary>
        ///     删除订单
        /// </summary>
        public DelegateCommand<object> RemoveOrderCommand { get; private set; }

        private void OnRemoveOrder(object obj)
        {
            if (_selAircraftPurchaseOrderDTO != null)
            {
                ViewAircraftPurchaseOrderDTO.Remove(_selAircraftPurchaseOrderDTO);
            }
        }

        private bool CanRemoveOrder(object obj)
        {
            return true;
        }

        #endregion

        #region 增加订单行

        /// <summary>
        ///     增加订单行
        /// </summary>
        public DelegateCommand<object> AddOrderLineCommand { get; private set; }

        private void OnAddOrderLine(object obj)
        {
            var orderLine = new AircraftPurchaseOrderLineDTO
            {
                Amount = 1,
                EstimateDeliveryDate = DateTime.Now
            };

            SelAircraftPurchaseOrderDTO.AircraftPurchaseOrderLines.Add(orderLine);
        }

        private bool CanAddOrderLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除订单行

        /// <summary>
        ///     移除订单行
        /// </summary>
        public DelegateCommand<object> RemoveOrderLineCommand { get; private set; }

        private void OnRemoveOrderLine(object obj)
        {
        }

        private bool CanRemoveOrderLine(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}