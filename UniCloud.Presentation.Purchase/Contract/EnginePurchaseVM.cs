#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/07，12:55
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
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (EnginePurchaseVM))]
    public class EnginePurchaseVM : EditViewModelBase
    {
        #region 声明、初始化

        private const string TradeType = "购买发动机";
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;
        private FilterDescriptor _orderDescriptor;
        private FilterDescriptor _tradeDescriptor1;
        private FilterDescriptor _tradeDescriptor2;

        [ImportingConstructor]
        public EnginePurchaseVM(IPurchaseService service)
            : base(service)
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
            InitializeViewEnginePurchaseOrderDTO();
            InitializeSupplierDTO();
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 供应商
        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        private void InitializeSupplierDTO()
        {
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_context, _context.Suppliers);
            var supplierFilter = new FilterDescriptor("EnginePurchaseSupplier", FilterOperator.IsEqualTo, true);
            Suppliers.FilterDescriptors.Add(supplierFilter);
        }

        #endregion

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
                if (_contentReadOnly == value) return;
                _contentReadOnly = value;
                RaisePropertyChanged(() => ContentReadOnly);
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

            Suppliers.Load(true);
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
                if (_selTradeDTO == value) return;
                _selTradeDTO = value;
                if (_selTradeDTO != null)
                {
                    _orderDescriptor.Value = _selTradeDTO.Id;
                    RaisePropertyChanged(() => EngineMaterials);
                }
                if (!ViewEnginePurchaseOrderDTO.AutoLoad)
                    ViewEnginePurchaseOrderDTO.AutoLoad = true;
                RaisePropertyChanged(() => SelTradeDTO);
                // 刷新按钮状态
                RefreshCommandState();
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

        #region 购买发动机订单

        private EnginePurchaseOrderDTO _selEnginePurchaseOrderDTO;

        /// <summary>
        ///     购买发动机订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseOrderDTO> ViewEnginePurchaseOrderDTO { get; set; }

        /// <summary>
        ///     选中的购买发动机订单
        /// </summary>
        public EnginePurchaseOrderDTO SelEnginePurchaseOrderDTO
        {
            get { return _selEnginePurchaseOrderDTO; }
            private set
            {
                if (_selEnginePurchaseOrderDTO == value) return;
                _selEnginePurchaseOrderDTO = value;
                if (_selEnginePurchaseOrderDTO != null)
                    SelEnginePurchaseOrderLineDTO = _selEnginePurchaseOrderDTO.EnginePurchaseOrderLines.FirstOrDefault();
                RaisePropertyChanged(() => SelEnginePurchaseOrderDTO);
                // 刷新按钮状态
                RefreshCommandState();
            }
        }

        /// <summary>
        ///     初始化购买发动机订单集合
        /// </summary>
        private void InitializeViewEnginePurchaseOrderDTO()
        {
            ViewEnginePurchaseOrderDTO = _service.CreateCollection(
                _context.EnginePurchaseOrders.Expand(p => p.RelatedDocs),
                o => o.EnginePurchaseOrderLines, o => o.RelatedDocs, o => o.ContractContents);
            _orderDescriptor = new FilterDescriptor("TradeId", FilterOperator.IsEqualTo, -1);
            ViewEnginePurchaseOrderDTO.FilterDescriptors.Add(_orderDescriptor);
            _service.RegisterCollectionView(ViewEnginePurchaseOrderDTO);
        }

        #endregion

        #region 选中的购买发动机订单行

        private EnginePurchaseOrderLineDTO _selEnginePurchaseOrderLineDTO;

        /// <summary>
        ///     选中的购买发动机订单行
        /// </summary>
        public EnginePurchaseOrderLineDTO SelEnginePurchaseOrderLineDTO
        {
            get { return _selEnginePurchaseOrderLineDTO; }
            private set
            {
                if (_selEnginePurchaseOrderLineDTO == value) return;
                _selEnginePurchaseOrderLineDTO = value;
                RaisePropertyChanged(() => SelEnginePurchaseOrderLineDTO);
                // 刷新按钮状态
                RefreshCommandState();
            }
        }

        #endregion

        #region 选中的购买发动机合同内容行

        private ContractContentDTO _selContractContentDTO;

        /// <summary>
        ///     选中的购买发动机合同内容行
        /// </summary>
        public ContractContentDTO SelContractContentDTO
        {
            get { return _selContractContentDTO; }
            private set
            {
                if (_selContractContentDTO == value) return;
                _selContractContentDTO = value;
                RaisePropertyChanged(() => SelContractContentDTO);
                ContentReadOnly = _selContractContentDTO == null;
                // 刷新按钮状态
                RefreshCommandState();
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
            SelEnginePurchaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        #region 撤销成功后执行

        protected override void OnAbortExecuted(object sender)
        {
            SelEnginePurchaseOrderLineDTO = null;
            SelContractContentDTO = null;
        }

        #endregion

        protected override bool CanAddAttach(object obj)
        {
            return _selEnginePurchaseOrderDTO != null;
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
                SelEnginePurchaseOrderDTO.ContractDocGuid = doc.DocumentId;
                SelEnginePurchaseOrderDTO.ContractName = doc.Name;
            }
            else
            {
                var relatedDoc = new RelatedDocDTO
                {
                    Id = RandomHelper.Next(),
                    DocumentId = doc.DocumentId,
                    DocumentName = doc.Name,
                    SourceId = SelEnginePurchaseOrderDTO.SourceGuid
                };
                SelEnginePurchaseOrderDTO.RelatedDocs.Add(relatedDoc);
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
            SelTradeDTO = new TradeDTO
            {
                Id = RandomHelper.Next(),
                TradeType = TradeType,
                StartDate = DateTime.Now,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                SelTradeDTO.SupplierId = supplier.SupplierId;
                SelTradeDTO.SuppierCompanyId = supplier.SuppierCompanyId;
            }
            ViewTradeDTO.AddNew(SelTradeDTO);
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
            if (SelTradeDTO == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                ViewTradeDTO.Remove(SelTradeDTO);
                SelTradeDTO = ViewTradeDTO.FirstOrDefault();
            });
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
            if (SelEnginePurchaseOrderDTO == null)
            {
                SelEnginePurchaseOrderDTO = new EnginePurchaseOrderDTO
                {
                    Id = RandomHelper.Next(),
                    OrderDate = DateTime.Now,
                    TradeId = _selTradeDTO.Id,
                    SourceGuid = Guid.NewGuid(),
                    SupplierId = _selTradeDTO.SupplierId,
                    OperatorName = StatusData.curUser
                };
                var currency = Currencies.FirstOrDefault();
                if (currency != null)
                    SelEnginePurchaseOrderDTO.CurrencyId = currency.Id;
                ViewEnginePurchaseOrderDTO.AddNew(SelEnginePurchaseOrderDTO);
                SelTradeDTO.Status = (int) TradeStatus.进行中;
            }
            else
            {
                var order =
                    ViewEnginePurchaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
                        .OrderBy(o => o.Version)
                        .LastOrDefault();
                if (order == null) return;
                SelEnginePurchaseOrderDTO = new EnginePurchaseOrderDTO
                {
                    Id = RandomHelper.Next(),
                    OrderDate = DateTime.Now,
                    TradeId = order.TradeId,
                    Name = order.Name,
                    CurrencyId = order.CurrencyId,
                    LinkmanId = order.LinkmanId,
                    SourceGuid = Guid.NewGuid(),
                    SupplierId = order.SupplierId,
                    OperatorName = StatusData.curUser
                };
                var currency = Currencies.FirstOrDefault();
                if (currency != null)
                    SelEnginePurchaseOrderDTO.CurrencyId = currency.Id;
                ViewEnginePurchaseOrderDTO.AddNew(SelEnginePurchaseOrderDTO);
                order.EnginePurchaseOrderLines.ToList().ForEach(line =>
                {
                    var newLine = new EnginePurchaseOrderLineDTO
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
                    SelEnginePurchaseOrderDTO.EnginePurchaseOrderLines.Add(newLine);
                });
            }
            RefreshCommandState();
        }

        private bool CanAddOrder(object obj)
        {
            if (_selTradeDTO == null) return false;
            var order =
                ViewEnginePurchaseOrderDTO.Where(o => o.TradeId == _selTradeDTO.Id)
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
            if (SelEnginePurchaseOrderDTO == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                ViewEnginePurchaseOrderDTO.Remove(SelEnginePurchaseOrderDTO);
                SelEnginePurchaseOrderDTO = ViewEnginePurchaseOrderDTO.FirstOrDefault();
            });
        }

        private bool CanRemoveOrder(object obj)
        {
            return _selEnginePurchaseOrderDTO != null && _selEnginePurchaseOrderDTO.OrderStatus < OrderStatus.已审核;
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
                        SelEnginePurchaseOrderDTO.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selEnginePurchaseOrderDTO != null;
        }

        #endregion

        #region 增加订单行

        /// <summary>
        ///     增加订单行
        /// </summary>
        public DelegateCommand<object> AddOrderLineCommand { get; private set; }

        private void OnAddOrderLine(object obj)
        {
            SelEnginePurchaseOrderLineDTO = new EnginePurchaseOrderLineDTO
            {
                Id = RandomHelper.Next(),
                Amount = 1,
                EstimateDeliveryDate = DateTime.Now,
                ContractEngineId = RandomHelper.Next()
            };

            SelEnginePurchaseOrderDTO.EnginePurchaseOrderLines.Add(SelEnginePurchaseOrderLineDTO);
        }

        private bool CanAddOrderLine(object obj)
        {
            return _selEnginePurchaseOrderDTO != null;
        }

        #endregion

        #region 移除订单行

        /// <summary>
        ///     移除订单行
        /// </summary>
        public DelegateCommand<object> RemoveOrderLineCommand { get; private set; }

        private void OnRemoveOrderLine(object obj)
        {
            if (SelEnginePurchaseOrderLineDTO == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                SelEnginePurchaseOrderDTO.EnginePurchaseOrderLines.Remove(SelEnginePurchaseOrderLineDTO);
                SelEnginePurchaseOrderLineDTO = SelEnginePurchaseOrderDTO.EnginePurchaseOrderLines.FirstOrDefault();
                RemoveOrderCommand.RaiseCanExecuteChanged();
            });
        }

        private bool CanRemoveOrderLine(object obj)
        {
            return _selEnginePurchaseOrderLineDTO != null;
        }

        #endregion

        #region 增加合同分解内容

        /// <summary>
        ///     增加合同分解内容
        /// </summary>
        public DelegateCommand<object> AddContentCommand { get; private set; }

        private void OnAddContent(object obj)
        {
            SelContractContentDTO = new ContractContentDTO
            {
                Id = RandomHelper.Next(),
            };
            SelEnginePurchaseOrderDTO.ContractContents.Add(SelContractContentDTO);
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
            if (SelContractContentDTO == null) return;
            SelEnginePurchaseOrderDTO.ContractContents.Remove(SelContractContentDTO);
            SelContractContentDTO = SelEnginePurchaseOrderDTO.ContractContents.FirstOrDefault();
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
            SelEnginePurchaseOrderDTO.Status = (int) OrderStatus.待审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            return _selEnginePurchaseOrderDTO != null && _selEnginePurchaseOrderDTO.OrderStatus == OrderStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelEnginePurchaseOrderDTO.Status = (int) OrderStatus.已审核;
            // 刷新按钮状态
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            return _selEnginePurchaseOrderDTO != null && _selEnginePurchaseOrderDTO.OrderStatus == OrderStatus.待审核;
        }

        #endregion

        #endregion
    }
}