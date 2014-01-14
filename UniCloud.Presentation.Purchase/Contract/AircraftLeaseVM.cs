﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/07，11:29
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
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (AircraftLeaseVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftLeaseVM : EditViewModelBase
    {
        #region 声明、初始化

        private const string TradeType = "租赁飞机";
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;
        private FilterDescriptor _orderDescriptor;
        private FilterDescriptor _tradeDescriptor1;
        private FilterDescriptor _tradeDescriptor2;

        [ImportingConstructor]
        public AircraftLeaseVM(IPurchaseService service) : base(service)
        {
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
            InitializeViewAircraftLeaseOrderDTO();
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
            if (!ViewTradeDTO.AutoLoad) ViewTradeDTO.AutoLoad = true;
            else ViewTradeDTO.Load(true);

            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers), true);
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies), true);
            Linkmen = _service.GetLinkman(() => RaisePropertyChanged(() => Linkmen), true);
            AircraftMaterials = _service.GetAircraftMaterial(() => RaisePropertyChanged(() => AircraftMaterials), true);
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
                            RaisePropertyChanged(() => AircraftMaterials);
                        }
                        else
                        {
                            _orderDescriptor.Value = -1;
                        }
                        if (!ViewAircraftLeaseOrderDTO.AutoLoad)
                        {
                            ViewAircraftLeaseOrderDTO.AutoLoad = true;
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

        #region 租赁飞机订单

        private AircraftLeaseOrderDTO _selAircraftLeaseOrderDTO;

        /// <summary>
        ///     租赁飞机订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseOrderDTO> ViewAircraftLeaseOrderDTO { get; set; }

        /// <summary>
        ///     选中的租赁飞机订单
        /// </summary>
        public AircraftLeaseOrderDTO SelAircraftLeaseOrderDTO
        {
            get { return _selAircraftLeaseOrderDTO; }
            private set
            {
                if (_selAircraftLeaseOrderDTO != value)
                {
                    _selAircraftLeaseOrderDTO = value;
                    RaisePropertyChanged(() => SelAircraftLeaseOrderDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     初始化租赁飞机订单集合
        /// </summary>
        private void InitializeViewAircraftLeaseOrderDTO()
        {
            ViewAircraftLeaseOrderDTO = _service.CreateCollection(
                _context.AircraftLeaseOrders.Expand(p => p.RelatedDocs),
                o => o.AircraftLeaseOrderLines, o => o.RelatedDocs, o => o.ContractContents);
            _orderDescriptor = new FilterDescriptor("TradeId", FilterOperator.IsEqualTo, -1);
            ViewAircraftLeaseOrderDTO.FilterDescriptors.Add(_orderDescriptor);
            _service.RegisterCollectionView(ViewAircraftLeaseOrderDTO);
        }

        #endregion

        #region 选中的租赁飞机订单行

        private AircraftLeaseOrderLineDTO _selAircraftLeaseOrderLineDTO;

        /// <summary>
        ///     选中的租赁飞机订单行
        /// </summary>
        public AircraftLeaseOrderLineDTO SelAircraftLeaseOrderLineDTO
        {
            get { return _selAircraftLeaseOrderLineDTO; }
            private set
            {
                if (_selAircraftLeaseOrderLineDTO != value)
                {
                    _selAircraftLeaseOrderLineDTO = value;
                    RaisePropertyChanged(() => SelAircraftLeaseOrderLineDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选中的租赁飞机合同内容行

        private ContractContentDTO _selContractContentDTO;

        /// <summary>
        ///     选中的租赁飞机合同内容行
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

        #region 保存成功后执行

        protected override void OnSaveSuccess(object sender)
        {
            SelAircraftLeaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        #region 撤销成功后执行

        protected override void OnAbortExecuted(object sender)
        {
            SelAircraftLeaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        protected override bool CanAddAttach(object obj)
        {
            return _selAircraftLeaseOrderDTO != null;
        }

        #region 添加附件成功后执行的操作

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);
            if (sender is Guid)
            {
                SelAircraftLeaseOrderDTO.ContractDocGuid = doc.DocumentId;
                SelAircraftLeaseOrderDTO.ContractName = doc.Name;
            }
            else
            {
                var relatedDoc = new RelatedDocDTO
                {
                    Id = RandomHelper.Next(),
                    DocumentId = doc.DocumentId,
                    DocumentName = doc.Name,
                    SourceId = SelAircraftLeaseOrderDTO.SourceGuid
                };
                SelAircraftLeaseOrderDTO.RelatedDocs.Add(relatedDoc);
            }
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
            if (_selAircraftLeaseOrderDTO == null)
            {
                var order = new AircraftLeaseOrderDTO
                {
                    Id = RandomHelper.Next(),
                    OrderDate = DateTime.Now,
                    TradeId = _selTradeDTO.Id,
                    SourceGuid = Guid.NewGuid(),
                    SupplierId = _selTradeDTO.SupplierId
                };
                ViewAircraftLeaseOrderDTO.AddNew(order);
                SelTradeDTO.Status = (int) TradeStatus.进行中;
            }
            else
            {
                var order =
                    ViewAircraftLeaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
                        .OrderBy(o => o.Version)
                        .LastOrDefault();
                if (order == null) return;
                var newOrder = new AircraftLeaseOrderDTO
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
                ViewAircraftLeaseOrderDTO.AddNew(newOrder);
                order.AircraftLeaseOrderLines.ToList().ForEach(line =>
                {
                    var newLine = new AircraftLeaseOrderLineDTO
                    {
                        Id = RandomHelper.Next(),
                        UnitPrice = line.UnitPrice,
                        Amount = line.Amount,
                        Discount = line.Discount,
                        EstimateDeliveryDate = line.EstimateDeliveryDate,
                        Note = line.Note,
                        ContractAircraftId = line.ContractAircraftId,
                        AircraftMaterialId = line.AircraftMaterialId,
                        RankNumber = line.RankNumber,
                        CSCNumber = line.CSCNumber,
                        SerialNumber = line.SerialNumber,
                        Status = line.Status
                    };
                    newOrder.AircraftLeaseOrderLines.Add(newLine);
                });
            }
        }

        private bool CanAddOrder(object obj)
        {
            if (_selTradeDTO == null) return false;
            var order =
                ViewAircraftLeaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
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
            if (_selAircraftLeaseOrderDTO != null)
            {
                ViewAircraftLeaseOrderDTO.Remove(_selAircraftLeaseOrderDTO);
            }
        }

        private bool CanRemoveOrder(object obj)
        {
            return _selAircraftLeaseOrderDTO != null && _selAircraftLeaseOrderDTO.OrderStatus < OrderStatus.已审核;
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
                        SelAircraftLeaseOrderDTO.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selAircraftLeaseOrderDTO != null;
        }

        #endregion

        #region 增加订单行

        /// <summary>
        ///     增加订单行
        /// </summary>
        public DelegateCommand<object> AddOrderLineCommand { get; private set; }

        private void OnAddOrderLine(object obj)
        {
            var orderLine = new AircraftLeaseOrderLineDTO
            {
                Id = RandomHelper.Next(),
                Amount = 1,
                EstimateDeliveryDate = DateTime.Now,
                ContractAircraftId = RandomHelper.Next()
            };

            SelAircraftLeaseOrderDTO.AircraftLeaseOrderLines.Add(orderLine);
        }

        private bool CanAddOrderLine(object obj)
        {
            return _selAircraftLeaseOrderDTO != null;
        }

        #endregion

        #region 移除订单行

        /// <summary>
        ///     移除订单行
        /// </summary>
        public DelegateCommand<object> RemoveOrderLineCommand { get; private set; }

        private void OnRemoveOrderLine(object obj)
        {
            if (_selAircraftLeaseOrderLineDTO != null)
            {
                SelAircraftLeaseOrderDTO.AircraftLeaseOrderLines.Remove(_selAircraftLeaseOrderLineDTO);
                RemoveOrderCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveOrderLine(object obj)
        {
            return _selAircraftLeaseOrderLineDTO != null;
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
            SelAircraftLeaseOrderDTO.ContractContents.Add(content);
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
                SelAircraftLeaseOrderDTO.ContractContents.Remove(_selContractContentDTO);
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
            SelAircraftLeaseOrderDTO.Status = (int) OrderStatus.待审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            return _selAircraftLeaseOrderDTO != null && _selAircraftLeaseOrderDTO.OrderStatus == OrderStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelAircraftLeaseOrderDTO.Status = (int) OrderStatus.已审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            return _selAircraftLeaseOrderDTO != null && _selAircraftLeaseOrderDTO.OrderStatus == OrderStatus.待审核;
        }

        #endregion

        #endregion
    }
}