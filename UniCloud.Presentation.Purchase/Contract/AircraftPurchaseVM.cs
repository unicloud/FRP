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
using System.Data.Services.Client;
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
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

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
        private bool _isAttach;
        private FilterDescriptor _orderDescriptor;
        private FilterDescriptor _tradeDescriptor;

        [ImportingConstructor]
        public AircraftPurchaseVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            AddTradeCommand = new DelegateCommand<object>(OnAddTrade, CanAddTrade);
            RemoveTradeCommand = new DelegateCommand<object>(OnRemoveTrade, CanRemoveTrade);
            AddOrderCommand = new DelegateCommand<object>(OnAddOrder, CanAddOrder);
            RemoveOrderCommand = new DelegateCommand<object>(OnRemoveOrder, CanRemoveOrder);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            AddOrderLineCommand = new DelegateCommand<object>(OnAddOrderLine, CanAddOrderLine);
            RemoveOrderLineCommand = new DelegateCommand<object>(OnRemoveOrderLine, CanRemoveOrderLine);
            AddContentCommand = new DelegateCommand<object>(OnAddContent, CanAddContent);
            RemoveContentCommand = new DelegateCommand<object>(OnRemoveContent, CanRemoveContent);

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
            _tradeDescriptor = new FilterDescriptor("IsClosed", FilterOperator.IsEqualTo, false);
            ViewTradeDTO.FilterDescriptors.Add(_tradeDescriptor);
            Service.RegisterCollectionView(ViewTradeDTO);
            ViewTradeDTO.PropertyChanged += OnViewPropertyChanged;

            ViewAircraftPurchaseOrderDTO =
                Service.CreateCollection<AircraftPurchaseOrderDTO>(
                    _context.AircraftPurchaseOrders.Expand(p => p.RelatedDocs));
            _orderDescriptor = new FilterDescriptor("TradeId", FilterOperator.IsEqualTo, -1);
            ViewAircraftPurchaseOrderDTO.FilterDescriptors.Add(_orderDescriptor);
            Service.RegisterCollectionView(ViewAircraftPurchaseOrderDTO);
            ViewAircraftPurchaseOrderDTO.PropertyChanged += OnViewPropertyChanged;

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_context, _context.Suppliers);
            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(_context, _context.Currencies);
            Linkmen = new QueryableDataServiceCollectionView<LinkmanDTO>(_context, _context.Linkmans);
            AircraftMaterials = new QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO>(_context,
                _context.SupplierCompanyAcMaterials);
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
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        /// <summary>
        ///     币种
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public QueryableDataServiceCollectionView<LinkmanDTO> Linkmen { get; set; }

        /// <summary>
        ///     飞机物料
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> AircraftMaterials { get; set; }

        #region 是否可以编辑合同分解

        private bool _contentReadOnly = true;

        /// <summary>
        ///     是否可以编辑合同分解
        /// </summary>
        public bool ContentReadOnly
        {
            get { return _contentReadOnly; }
            private set
            {
                if (_contentReadOnly != value)
                {
                    _contentReadOnly = value;
                    RaisePropertyChanged(() => ContentReadOnly);
                }
            }
        }

        #endregion

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
            ViewTradeDTO.AutoLoad = true;

            Suppliers.Load();
            Currencies.Load();
            Linkmen.Load();
            AircraftMaterials.Load();
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
            private set
            {
                if (_selTradeDTO != value)
                {
                    _selTradeDTO = value;
                    if (_selTradeDTO != null)
                    {
                        _orderDescriptor.Value = _selTradeDTO.Id;
                        RaisePropertyChanged(() => AircraftMaterials);
                    }
                    else
                    {
                        _orderDescriptor.Value = -1;
                    }
                    if (!ViewAircraftPurchaseOrderDTO.AutoLoad)
                    {
                        ViewAircraftPurchaseOrderDTO.AutoLoad = true;
                    }
                    // 刷新按钮状态
                    RemoveTradeCommand.RaiseCanExecuteChanged();

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
            private set
            {
                if (_selAircraftPurchaseOrderDTO != value)
                {
                    _selAircraftPurchaseOrderDTO = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseOrderDTO);
                    // 刷新按钮状态
                    RemoveOrderCommand.RaiseCanExecuteChanged();
                    AddAttachCommand.RaiseCanExecuteChanged();
                    AddOrderLineCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 选中的购买飞机订单行

        private AircraftPurchaseOrderLineDTO _selAircraftPurchaseOrderLineDTO;

        /// <summary>
        ///     选中的购买飞机订单行
        /// </summary>
        public AircraftPurchaseOrderLineDTO SelAircraftPurchaseOrderLineDTO
        {
            get { return _selAircraftPurchaseOrderLineDTO; }
            private set
            {
                if (_selAircraftPurchaseOrderLineDTO != value)
                {
                    _selAircraftPurchaseOrderLineDTO = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseOrderLineDTO);
                    // 刷新按钮状态
                    RemoveOrderLineCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 选中的购买飞机合同内容行

        private ContractContentDTO _selContractContentDTO;

        /// <summary>
        ///     选中的购买飞机合同内容行
        /// </summary>
        public ContractContentDTO SelContractContentDTO
        {
            get { return _selContractContentDTO; }
            private set
            {
                if (_selContractContentDTO != value)
                {
                    _selContractContentDTO = value;
                    RaisePropertyChanged(() => SelContractContentDTO);
                    ContentReadOnly = _selContractContentDTO == null;
                    // 刷新按钮状态
                    RemoveContentCommand.RaiseCanExecuteChanged();
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
            if (sender is Guid)
            {
                _isAttach = true;
                var docId = (Guid) sender;
                var documentView = new DocumentViewer();
                documentView.ViewModel.InitData(false, docId, DocumentViewerClosed);
                documentView.ShowDialog();
            }
            else
            {
                _isAttach = false;
                var docId = Guid.Empty;
                var documentView = new DocumentViewer();
                documentView.ViewModel.InitData(false, docId, DocumentViewerClosed);
                documentView.ShowDialog();
            }
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            var documentView = sender as DocumentViewer;
            if (documentView != null && documentView.Tag is DocumentDTO)
            {
                if (_isAttach)
                {
                    _document = documentView.Tag as DocumentDTO;
                    SelAircraftPurchaseOrderDTO.ContractName = _document.Name;
                    SelAircraftPurchaseOrderDTO.ContractDocGuid = _document.DocumentId;
                }
                else
                {
                    _document = documentView.Tag as DocumentDTO;
                    var doc = new RelatedDocDTO
                    {
                        Id = RandomHelper.Next(),
                        DocumentId = _document.DocumentId,
                        DocumentName = _document.Name,
                        SourceId = SelAircraftPurchaseOrderDTO.SourceGuid
                    };
                    SelAircraftPurchaseOrderDTO.RelatedDocs.Add(doc);
                }
            }
        }

        protected override bool CanAddAttach(object obj)
        {
            return _selAircraftPurchaseOrderDTO != null;
        }

        #endregion

        #region 查看合同文档

        protected override void OnViewAttach(object sender)
        {
            if (sender is Guid)
            {
                var docId = (Guid) sender;
                var documentView = new DocumentViewer();
                documentView.ViewModel.InitData(true, docId, DocumentViewerClosed);
                documentView.Show();
            }
            else if (sender is RelatedDocDTO)
            {
                var doc = sender as RelatedDocDTO;
                var documentView = new DocumentViewer();
                documentView.ViewModel.InitData(true, doc.DocumentId, DocumentViewerClosed);
                documentView.Show();
            }
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
            return _selTradeDTO != null && _selTradeDTO.TradeStatus == TradeStatus.开始;
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
                Id = RandomHelper.Next(),
                OrderDate = DateTime.Now,
                TradeId = _selTradeDTO.Id,
                SourceGuid = Guid.NewGuid(),
                SupplierId=_selTradeDTO.SupplierId
            };
            ViewAircraftPurchaseOrderDTO.AddNew(order);
            SelTradeDTO.Status = (int) TradeStatus.进行中;
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
            return _selAircraftPurchaseOrderDTO != null && _selAircraftPurchaseOrderDTO.OrderStatus < OrderStatus.已审核;
        }

        #endregion

        #region 删除关联文档

        /// <summary>
        ///     删除关联文档
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {
            var doc = obj as RelatedDocDTO;
            if (doc != null)
            {
                MessageConfirm("确认移除", "是否移除关联文档：" + doc.DocumentName + "？", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        SelAircraftPurchaseOrderDTO.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selAircraftPurchaseOrderDTO != null;
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
                Id = RandomHelper.Next(),
                Amount = 1,
                EstimateDeliveryDate = DateTime.Now
            };

            SelAircraftPurchaseOrderDTO.AircraftPurchaseOrderLines.Add(orderLine);
        }

        private bool CanAddOrderLine(object obj)
        {
            return _selAircraftPurchaseOrderDTO != null;
        }

        #endregion

        #region 移除订单行

        /// <summary>
        ///     移除订单行
        /// </summary>
        public DelegateCommand<object> RemoveOrderLineCommand { get; private set; }

        private void OnRemoveOrderLine(object obj)
        {
            if (_selAircraftPurchaseOrderLineDTO != null)
            {
                SelAircraftPurchaseOrderDTO.AircraftPurchaseOrderLines.Remove(_selAircraftPurchaseOrderLineDTO);
                RemoveOrderCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveOrderLine(object obj)
        {
            return _selAircraftPurchaseOrderLineDTO != null;
        }

        #endregion

        #region 增加合同分解内容

        /// <summary>
        ///     增加合同分解内容
        /// </summary>
        public DelegateCommand<object> AddContentCommand { get; private set; }

        private void OnAddContent(object obj)
        {
            var content = new ContractContentDTO
            {
                Id = RandomHelper.Next(),
            };
            SelAircraftPurchaseOrderDTO.ContractContents.Add(content);
            SelContractContentDTO = content;
        }

        private bool CanAddContent(object obj)
        {
            return true;
        }

        #endregion

        #region 移除合同分解内容

        /// <summary>
        ///     移除合同分解内容
        /// </summary>
        public DelegateCommand<object> RemoveContentCommand { get; private set; }

        private void OnRemoveContent(object obj)
        {
            if (_selContractContentDTO != null)
            {
                SelAircraftPurchaseOrderDTO.ContractContents.Remove(_selContractContentDTO);
                SelContractContentDTO = null;
            }
        }

        private bool CanRemoveContent(object obj)
        {
            return _selContractContentDTO != null;
        }

        #endregion

        #endregion
    }
}