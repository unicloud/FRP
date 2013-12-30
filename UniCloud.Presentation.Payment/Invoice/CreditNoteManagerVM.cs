#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:49:20
// 文件名：CreditNoteManagerVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(CreditNoteManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreditNoteManagerVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;
        private PaymentData _paymentData;

        [ImportingConstructor]
        public CreditNoteManagerVM(IRegionManager regionManager)
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
            CreditNotes = Service.CreateCollection<CreditNoteDTO>(_paymentData.CreditNotes);
            Service.RegisterCollectionView(CreditNotes); //注册查询集合。

            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(_paymentData, _paymentData.Currencies);

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_paymentData, _paymentData.Suppliers);

            PurchaseOrders = new QueryableDataServiceCollectionView<PurchaseOrderDTO>(_paymentData, _paymentData.PurchaseOrders);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            SubmitCommand = new DelegateCommand<object>(OnSubmit, CanSubmit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _paymentData = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(_paymentData);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 是否已提交审核

        private bool _isSubmited;

        /// <summary>
        ///  是否已提交审核
        /// </summary>
        public bool IsSubmited
        {
            get { return this._isSubmited; }
            private set
            {
                if (this._isSubmited != value)
                {
                    _isSubmited = value;
                    this.RaisePropertyChanged(() => this.IsSubmited);
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
            CreditNotes.AutoLoad = true;

            Currencies.Load(true);
            Suppliers.Load(true);
            PurchaseOrders.Load(true);
        }

        #region 业务

        #region 贷项单集合
        /// <summary>
        ///     贷项单集合
        /// </summary>
        public QueryableDataServiceCollectionView<CreditNoteDTO> CreditNotes { get; set; }

        #endregion

        #region 选择的贷项单

        private CreditNoteDTO _selCreditNote;

        /// <summary>
        ///     选择的贷项单
        /// </summary>
        public CreditNoteDTO SelCreditNote
        {
            get { return _selCreditNote; }
            set
            {
                if (_selCreditNote != value)
                {
                    _selCreditNote = value;
                    _invoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    _relatedPurchaseOrders.Clear();
                    RelatedPurchaseOrders.Add(PurchaseOrders.FirstOrDefault(p => p.Id == value.OrderId));
                    RaisePropertyChanged(() => SelCreditNote);
                }
            }
        }

        #endregion

        #region 贷项单行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     贷项单行
        /// </summary>
        public ObservableCollection<InvoiceLineDTO> InvoiceLines
        {
            get { return _invoiceLines; }
            private set
            {
                if (_invoiceLines != value)
                {
                    _invoiceLines = value;
                    RaisePropertyChanged(() => InvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的贷项单行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的贷项单行
        /// </summary>
        public InvoiceLineDTO SelInvoiceLine
        {
            get { return _selInvoiceLine; }
            set
            {
                if (_selInvoiceLine != value)
                {
                    _selInvoiceLine = value;
                    RaisePropertyChanged(() => SelInvoiceLine);
                }
            }
        }

        #endregion

        #region 币种集合
        /// <summary>
        ///     币种集合
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        #endregion

        #region 供应商集合
        /// <summary>
        ///     供应商集合
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        #endregion

        #region 关联的采购订单

        private ObservableCollection<PurchaseOrderDTO> _relatedPurchaseOrders = new ObservableCollection<PurchaseOrderDTO>();

        /// <summary>
        ///     关联的采购订单集合
        /// </summary>
        public ObservableCollection<PurchaseOrderDTO> RelatedPurchaseOrders
        {
            get { return _relatedPurchaseOrders; }
            set
            {
                if (_relatedPurchaseOrders != value)
                {
                    _relatedPurchaseOrders = value;
                    RaisePropertyChanged(() => RelatedPurchaseOrders);
                }
            }
        }

        private PurchaseOrderDTO _relatedOrder;

        /// <summary>
        ///     关联的订单行
        /// </summary>
        public PurchaseOrderDTO RelatedOrder
        {
            get { return _relatedOrder; }
            set
            {
                if (_relatedOrder != value)
                {
                    _relatedOrder = value;
                    RaisePropertyChanged(() => RelatedOrder);
                }
            }
        }

        #endregion


        #endregion

        #endregion

        #endregion

        #region 操作

        #region 新建贷项单

        /// <summary>
        ///     新建贷项单
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            PurchaseOrderChildView.ShowDialog();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除贷项单

        /// <summary>
        ///     删除贷项单
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            CreditNotes.Remove(SelCreditNote);
            var currentCreditNote = CreditNotes.FirstOrDefault();
            if (currentCreditNote == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                InvoiceLines.Clear();
                RelatedPurchaseOrders.Clear();
            }
        }

        private bool CanDelete(object obj)
        {
            return true;
        }
        #endregion

        #region 新增贷项单行
        /// <summary>
        ///     新增贷项单行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            var invoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelCreditNote.CreditNoteId,
            };
            SelCreditNote.InvoiceLines.Add(invoiceLine);
            InvoiceLines.Add(invoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }
        #endregion

        #region 删除贷项单行

        /// <summary>
        ///     删除贷项单行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            SelCreditNote.InvoiceLines.Remove(SelInvoiceLine);
            InvoiceLines.Remove(SelInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            return true;
        }
        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> SubmitCommand { get; private set; }

        private void OnSubmit(object obj)
        {
            IsSubmited = true;
        }

        private bool CanSubmit(object obj)
        {
            return true;
        }
        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelCreditNote.Reviewer = "HQB";
            SelCreditNote.ReviewDate = DateTime.Now;
        }

        private bool CanCheck(object obj)
        {
            return true;
        }
        #endregion

        #region GridView单元格变更处理
        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        /// GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        public void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "TotalLine"))
                {
                    decimal totalCount = SelCreditNote.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelCreditNote.InvoiceValue = totalCount;
                }
            }
        }


        #endregion

        #endregion

        #region 子窗体相关操作
        [Import]
        public PurchaseOrderChildView PurchaseOrderChildView; //初始化子窗体

        #region 采购订单集合

        /// <summary>
        ///    采购订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseOrderDTO> PurchaseOrders { get; set; }
        #endregion

        #region 选择的采购订单

        private PurchaseOrderDTO _selPurchaseOrder;

        /// <summary>
        ///     选择的采购订单
        /// </summary>
        public PurchaseOrderDTO SelPurchaseOrder
        {
            get { return _selPurchaseOrder; }
            set
            {
                if (_selPurchaseOrder != value)
                {
                    _selPurchaseOrder = value;
                    RaisePropertyChanged(() => SelPurchaseOrder);
                }
            }
        }
        #endregion

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            PurchaseOrderChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            if (SelPurchaseOrder == null)
            {
                MessageAlert("请先选择一个采购订单！");
            }
            else
            {
                var creditNote = new CreditNoteDTO()
                {
                    CreditNoteId = RandomHelper.Next(),
                    CreateDate = DateTime.Now,
                    InvoiceDate = DateTime.Now,
                    OrderId = SelPurchaseOrder.Id,
                    CurrencyId = SelPurchaseOrder.CurrencyId,
                    SupplierId = SelPurchaseOrder.SupplierId,
                    SupplierName = SelPurchaseOrder.SupplierName,
                };
                CreditNotes.AddNew(creditNote);
                PurchaseOrderChildView.Close();
            }

        }


        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion

        #endregion
    }
}
