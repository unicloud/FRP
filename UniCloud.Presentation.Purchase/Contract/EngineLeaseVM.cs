#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/07，12:45
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
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (EngineLeaseVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineLeaseVM : EditViewModelBase
    {
        #region 声明、初始化

        private const string TradeType = "租赁发动机";
        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;
        private DocumentDTO _document = new DocumentDTO();
        private bool _isAttach;
        private FilterDescriptor _orderDescriptor;
        private FilterDescriptor _tradeDescriptor1;
        private FilterDescriptor _tradeDescriptor2;

        [ImportingConstructor]
        public EngineLeaseVM(IRegionManager regionManager, IPurchaseService service) : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;

            AddTradeCommand = new DelegateCommand<object>(OnAddTrade, CanAddTrade);
            RemoveTradeCommand = new DelegateCommand<object>(OnRemoveTrade, CanRemoveTrade);
            AddOrderCommand = new DelegateCommand<object>(OnAddOrder, CanAddOrder);
            RemoveOrderCommand = new DelegateCommand<object>(OnRemoveOrder, CanRemoveOrder);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            AddOrderLineCommand = new DelegateCommand<object>(OnAddOrderLine, CanAddOrderLine);
            RemoveOrderLineCommand = new DelegateCommand<object>(OnRemoveOrderLine, CanRemoveOrderLine);
            AddContentCommand = new DelegateCommand<object>(OnAddContent, CanAddContent);
            RemoveContentCommand = new DelegateCommand<object>(OnRemoveContent, CanRemoveContent);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);

            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            InitializeViewTradeDTO();
            InitializeViewEngineLeaseOrderDTO();
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
        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> EngineMaterials { get; set; }

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

            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers), true);
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies), true);
            Linkmen = _service.GetLinkman(() => RaisePropertyChanged(() => Linkmen), true);
            EngineMaterials = _service.GetEngineMaterial(() => RaisePropertyChanged(() => EngineMaterials), true);
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
                    if (_selTradeDTO != value)
                    {
                        _selTradeDTO = value;
                        if (_selTradeDTO != null)
                        {
                            _orderDescriptor.Value = _selTradeDTO.Id;
                            RaisePropertyChanged(() => EngineMaterials);
                        }
                        else
                        {
                            _orderDescriptor.Value = -1;
                        }
                        if (!ViewEngineLeaseOrderDTO.AutoLoad)
                        {
                            ViewEngineLeaseOrderDTO.AutoLoad = true;
                        }
                        RaisePropertyChanged(() => SelTradeDTO);
                        // 刷新按钮状态
                        RefreshCommandState();
                    }
                }
            }
        }

        /// <summary>
        ///     初始化交易集合
        /// </summary>
        private void InitializeViewTradeDTO()
        {
            ViewTradeDTO = _service.CreateCollection(_context.Trades);
            _tradeDescriptor1 = new FilterDescriptor("IsClosed", FilterOperator.IsEqualTo, false);
            _tradeDescriptor2 = new FilterDescriptor("TradeType", FilterOperator.IsEqualTo, TradeType);
            ViewTradeDTO.FilterDescriptors.Add(_tradeDescriptor1);
            ViewTradeDTO.FilterDescriptors.Add(_tradeDescriptor2);
            _service.RegisterCollectionView(ViewTradeDTO);
        }

        #endregion

        #region 租赁发动机订单

        private EngineLeaseOrderDTO _selEngineLeaseOrderDTO;

        /// <summary>
        ///     租赁发动机订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineLeaseOrderDTO> ViewEngineLeaseOrderDTO { get; set; }

        /// <summary>
        ///     选中的租赁发动机订单
        /// </summary>
        public EngineLeaseOrderDTO SelEngineLeaseOrderDTO
        {
            get { return _selEngineLeaseOrderDTO; }
            private set
            {
                if (_selEngineLeaseOrderDTO != value)
                {
                    _selEngineLeaseOrderDTO = value;
                    RaisePropertyChanged(() => SelEngineLeaseOrderDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     初始化租赁发动机订单集合
        /// </summary>
        private void InitializeViewEngineLeaseOrderDTO()
        {
            ViewEngineLeaseOrderDTO = _service.CreateCollection(
                _context.EngineLeaseOrders.Expand(p => p.RelatedDocs),
                o => o.EngineLeaseOrderLines, o => o.RelatedDocs, o => o.ContractContents);
            _orderDescriptor = new FilterDescriptor("TradeId", FilterOperator.IsEqualTo, -1);
            ViewEngineLeaseOrderDTO.FilterDescriptors.Add(_orderDescriptor);
            _service.RegisterCollectionView(ViewEngineLeaseOrderDTO);
        }

        #endregion

        #region 选中的租赁发动机订单行

        private EngineLeaseOrderLineDTO _selEngineLeaseOrderLineDTO;

        /// <summary>
        ///     选中的租赁发动机订单行
        /// </summary>
        public EngineLeaseOrderLineDTO SelEngineLeaseOrderLineDTO
        {
            get { return _selEngineLeaseOrderLineDTO; }
            private set
            {
                if (_selEngineLeaseOrderLineDTO != value)
                {
                    _selEngineLeaseOrderLineDTO = value;
                    RaisePropertyChanged(() => SelEngineLeaseOrderLineDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选中的租赁发动机合同内容行

        private ContractContentDTO _selContractContentDTO;

        /// <summary>
        ///     选中的租赁发动机合同内容行
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
                    RefreshCommandState();
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
                    SelEngineLeaseOrderDTO.ContractName = _document.Name;
                    SelEngineLeaseOrderDTO.ContractDocGuid = _document.DocumentId;
                }
                else
                {
                    _document = documentView.Tag as DocumentDTO;
                    var doc = new RelatedDocDTO
                    {
                        Id = RandomHelper.Next(),
                        DocumentId = _document.DocumentId,
                        DocumentName = _document.Name,
                        SourceId = SelEngineLeaseOrderDTO.SourceGuid
                    };
                    SelEngineLeaseOrderDTO.RelatedDocs.Add(doc);
                }
            }
        }

        protected override bool CanAddAttach(object obj)
        {
            return _selEngineLeaseOrderDTO != null;
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

        #region 保存成功后执行

        protected override void OnSaveSuccess(object sender)
        {
            SelEngineLeaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        #region 撤销成功后执行

        protected override void OnAbortExecuted(object sender)
        {
            SelEngineLeaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            AddAttachCommand.RaiseCanExecuteChanged();
            AddTradeCommand.RaiseCanExecuteChanged();
            RemoveTradeCommand.RaiseCanExecuteChanged();
            AddOrderCommand.RaiseCanExecuteChanged();
            RemoveOrderCommand.RaiseCanExecuteChanged();
            RemoveDocCommand.RaiseCanExecuteChanged();
            AddOrderLineCommand.RaiseCanExecuteChanged();
            RemoveOrderLineCommand.RaiseCanExecuteChanged();
            AddContentCommand.RaiseCanExecuteChanged();
            RemoveContentCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
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
                TradeType = TradeType,
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
            if (_selEngineLeaseOrderDTO == null)
            {
                var order = new EngineLeaseOrderDTO
                {
                    Id = RandomHelper.Next(),
                    OrderDate = DateTime.Now,
                    TradeId = _selTradeDTO.Id,
                    SourceGuid = Guid.NewGuid(),
                    SupplierId = _selTradeDTO.SupplierId
                };
                ViewEngineLeaseOrderDTO.AddNew(order);
                SelTradeDTO.Status = (int) TradeStatus.进行中;
            }
            else
            {
                var order =
                    ViewEngineLeaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
                        .OrderBy(o => o.Version)
                        .LastOrDefault();
                if (order == null) return;
                var newOrder = new EngineLeaseOrderDTO
                {
                    Id = RandomHelper.Next(),
                    OrderDate = DateTime.Now,
                    TradeId = order.TradeId,
                    Name = order.Name,
                    CurrencyId = order.CurrencyId,
                    LinkmanId = order.LinkmanId,
                    SourceGuid = Guid.NewGuid(),
                    SupplierId = order.SupplierId
                };
                ViewEngineLeaseOrderDTO.AddNew(newOrder);
                order.EngineLeaseOrderLines.ToList().ForEach(line =>
                {
                    var newLine = new EngineLeaseOrderLineDTO
                    {
                        Id = RandomHelper.Next(),
                        UnitPrice = line.UnitPrice,
                        Amount = line.Amount,
                        Discount = line.Discount,
                        EstimateDeliveryDate = line.EstimateDeliveryDate,
                        Note = line.Note,
                        ContractEngineId = line.ContractEngineId,
                        EngineMaterialId = line.EngineMaterialId,
                        RankNumber = line.RankNumber,
                        SerialNumber = line.SerialNumber,
                        Status = line.Status
                    };
                    newOrder.EngineLeaseOrderLines.Add(newLine);
                });
            }
        }

        private bool CanAddOrder(object obj)
        {
            if (_selTradeDTO == null) return false;
            var order =
                ViewEngineLeaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
                    .OrderBy(o => o.Version)
                    .LastOrDefault();
            if (order != null && order.OrderStatus == OrderStatus.已审核)
                return true;
            return order == null;
        }

        #endregion

        #region 删除订单

        /// <summary>
        ///     删除订单
        /// </summary>
        public DelegateCommand<object> RemoveOrderCommand { get; private set; }

        private void OnRemoveOrder(object obj)
        {
            if (_selEngineLeaseOrderDTO != null)
            {
                ViewEngineLeaseOrderDTO.Remove(_selEngineLeaseOrderDTO);
            }
        }

        private bool CanRemoveOrder(object obj)
        {
            return _selEngineLeaseOrderDTO != null && _selEngineLeaseOrderDTO.OrderStatus < OrderStatus.已审核;
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
                        SelEngineLeaseOrderDTO.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selEngineLeaseOrderDTO != null;
        }

        #endregion

        #region 增加订单行

        /// <summary>
        ///     增加订单行
        /// </summary>
        public DelegateCommand<object> AddOrderLineCommand { get; private set; }

        private void OnAddOrderLine(object obj)
        {
            var orderLine = new EngineLeaseOrderLineDTO
            {
                Id = RandomHelper.Next(),
                Amount = 1,
                EstimateDeliveryDate = DateTime.Now,
                ContractEngineId = RandomHelper.Next()
            };

            SelEngineLeaseOrderDTO.EngineLeaseOrderLines.Add(orderLine);
        }

        private bool CanAddOrderLine(object obj)
        {
            return _selEngineLeaseOrderDTO != null;
        }

        #endregion

        #region 移除订单行

        /// <summary>
        ///     移除订单行
        /// </summary>
        public DelegateCommand<object> RemoveOrderLineCommand { get; private set; }

        private void OnRemoveOrderLine(object obj)
        {
            if (_selEngineLeaseOrderLineDTO != null)
            {
                SelEngineLeaseOrderDTO.EngineLeaseOrderLines.Remove(_selEngineLeaseOrderLineDTO);
                RemoveOrderCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveOrderLine(object obj)
        {
            return _selEngineLeaseOrderLineDTO != null;
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
            SelEngineLeaseOrderDTO.ContractContents.Add(content);
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
                SelEngineLeaseOrderDTO.ContractContents.Remove(_selContractContentDTO);
                SelContractContentDTO = null;
            }
        }

        private bool CanRemoveContent(object obj)
        {
            return _selContractContentDTO != null;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            SelEngineLeaseOrderDTO.Status = (int) OrderStatus.待审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            return _selEngineLeaseOrderDTO != null && _selEngineLeaseOrderDTO.OrderStatus == OrderStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelEngineLeaseOrderDTO.Status = (int) OrderStatus.已审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            return _selEngineLeaseOrderDTO != null && _selEngineLeaseOrderDTO.OrderStatus == OrderStatus.待审核;
        }

        #endregion

        #endregion
    }
}