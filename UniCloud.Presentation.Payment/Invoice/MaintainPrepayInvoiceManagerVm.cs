#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/4 10:37:51
// 文件名：MaintainPrepayInvoiceManagerVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/4 10:37:51
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof (MaintainPrepayInvoiceManagerVm))]
    public class MaintainPrepayInvoiceManagerVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public MaintainPrepayInvoiceManagerVm(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
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
            PrepaymentInvoices = _service.CreateCollection(_context.MaintainPrepaymentInvoices, o => o.InvoiceLines);
            PrepaymentInvoices.LoadedData += (o, e) =>
            {
                if (SelPrepaymentInvoice == null)
                    SelPrepaymentInvoice = PrepaymentInvoices.FirstOrDefault();
            };
            _service.RegisterCollectionView(PrepaymentInvoices); //注册查询集合。

            MaintainPaymentSchedules = new QueryableDataServiceCollectionView<MaintainPaymentScheduleDTO>(_context,
                _context.MaintainPaymentSchedules);

            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(_context,
                _context.PaymentSchedules);
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

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     项名称
        /// </summary>
        public Dictionary<int, ItemNameType> ItemNameTypes
        {
            get
            {
                return Enum.GetValues(typeof (ItemNameType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (ItemNameType) value);
            }
        }

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

        #region 是否已提交审核

        private bool _isSubmited;

        /// <summary>
        ///     是否已提交审核
        /// </summary>
        public bool IsSubmited
        {
            get { return _isSubmited; }
            private set
            {
                if (_isSubmited != value)
                {
                    _isSubmited = value;
                    RaisePropertyChanged(() => IsSubmited);
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
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies));
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
            PrepaymentInvoices.Load(true);
            MaintainPaymentSchedules.Load(true);
            PaymentSchedules.Load(true);
        }

        #region 业务

        #region 预付款发票集合

        /// <summary>
        ///     预付款发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainPrepaymentInvoiceDTO> PrepaymentInvoices { get; set; }

        #endregion

        #region 选择的预付款发票

        private MaintainPrepaymentInvoiceDTO _selPrepaymentInvoice;

        /// <summary>
        ///     选择的预付款发票
        /// </summary>
        public MaintainPrepaymentInvoiceDTO SelPrepaymentInvoice
        {
            get { return _selPrepaymentInvoice; }
            set
            {
                _selPrepaymentInvoice = value;
                _invoiceLines.Clear();
                if (value != null)
                {
                    SelInvoiceLine = value.InvoiceLines.FirstOrDefault();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    SelInvoiceLine = InvoiceLines.FirstOrDefault();
                    _relatedPaymentSchedule.Clear();
                    RelatedPaymentSchedule.Add(
                        PaymentSchedules.FirstOrDefault(p =>
                        {
                            var paymentScheduleLine =
                                p.PaymentScheduleLines.FirstOrDefault(
                                    l => l.PaymentScheduleLineId == value.PaymentScheduleLineId);
                            return paymentScheduleLine != null &&
                                   paymentScheduleLine.PaymentScheduleLineId == value.PaymentScheduleLineId;
                        }));
                    SelPaymentSchedule = RelatedPaymentSchedule.FirstOrDefault();
                    if (SelPaymentSchedule != null)
                        RelatedPaymentScheduleLine =
                            SelPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                                l => l.InvoiceId == value.PrepaymentInvoiceId);
                }
                RaisePropertyChanged(() => SelPrepaymentInvoice);
            }
        }

        #endregion

        #region 预付款发票行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     预付款发票行
        /// </summary>
        public ObservableCollection<InvoiceLineDTO> InvoiceLines
        {
            get { return _invoiceLines; }
            set
            {
                if (_invoiceLines != value)
                {
                    _invoiceLines = value;
                    RaisePropertyChanged(() => InvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的预付款发票行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的预付款发票行
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

        #region 关联的付款计划及付款计划行

        private ObservableCollection<PaymentScheduleDTO> _relatedPaymentSchedule =
            new ObservableCollection<PaymentScheduleDTO>();

        private PaymentScheduleLineDTO _relatedPaymentScheduleLine;

        private PaymentScheduleDTO _selPaymentSchedule;

        /// <summary>
        ///     关联的付款计划
        /// </summary>
        public ObservableCollection<PaymentScheduleDTO> RelatedPaymentSchedule
        {
            get { return _relatedPaymentSchedule; }
            set
            {
                if (_relatedPaymentSchedule != value)
                {
                    _relatedPaymentSchedule = value;
                    RaisePropertyChanged(() => RelatedPaymentSchedule);
                }
            }
        }

        /// <summary>
        ///     关联的付款计划
        /// </summary>
        public PaymentScheduleDTO SelPaymentSchedule
        {
            get { return _selPaymentSchedule; }
            set
            {
                if (_selPaymentSchedule != value)
                {
                    _selPaymentSchedule = value;
                    RaisePropertyChanged(() => SelPaymentSchedule);
                }
            }
        }

        /// <summary>
        ///     选择的付款计划行
        /// </summary>
        public PaymentScheduleLineDTO RelatedPaymentScheduleLine
        {
            get { return _relatedPaymentScheduleLine; }
            set
            {
                if (_relatedPaymentScheduleLine != value)
                {
                    _relatedPaymentScheduleLine = value;
                    RaisePropertyChanged(() => RelatedPaymentScheduleLine);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 新建预付款发票

        /// <summary>
        ///     新建预付款发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            PrepayPayscheduleChildView.ShowDialog();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除预付款发票

        /// <summary>
        ///     删除预付款发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                PrepaymentInvoices.Remove(SelPrepaymentInvoice);
                SelPrepaymentInvoice = PrepaymentInvoices.FirstOrDefault();
                if (SelPrepaymentInvoice == null)
                {
                    //删除完，若没有记录了，则也要删除界面明细
                    InvoiceLines.Clear();
                    RelatedPaymentSchedule.Clear();
                }
            });
        }

        private bool CanDelete(object obj)
        {
            bool canRemove;
            if (SelPrepaymentInvoice != null)
                canRemove = true;
            else if (PrepaymentInvoices != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }

        #endregion

        #region 新增预付款发票行

        /// <summary>
        ///     新增预付款发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            if (SelPrepaymentInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }

            SelInvoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelPrepaymentInvoice.PrepaymentInvoiceId
            };
            SelPrepaymentInvoice.InvoiceLines.Add(SelInvoiceLine);
            InvoiceLines.Add(SelInvoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除预付款发票行

        /// <summary>
        ///     删除预付款发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (SelInvoiceLine == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                SelPrepaymentInvoice.InvoiceLines.Remove(SelInvoiceLine);
                InvoiceLines.Remove(SelInvoiceLine);
                SelInvoiceLine = SelPrepaymentInvoice.InvoiceLines.FirstOrDefault();
            });
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
            SelPrepaymentInvoice.Reviewer = "HQB";
            SelPrepaymentInvoice.ReviewDate = DateTime.Now;
        }

        private bool CanCheck(object obj)
        {
            return true;
        }

        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
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
                    var totalCount = SelPrepaymentInvoice.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelPrepaymentInvoice.InvoiceValue = totalCount;
                }
            }
        }

        #endregion

        #endregion

        #region 子窗体相关操作

        [Import] public MaintainPrepayPayscheduleChildView PrepayPayscheduleChildView; //初始化子窗体

        #region 付款计划集合

        private MaintainPaymentScheduleDTO _selectMaintainPaymentSchedule;
        private PaymentScheduleLineDTO _selectPaymentScheduleLine;

        /// <summary>
        ///     所有付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentScheduleDTO> PaymentSchedules { get; set; }

        /// <summary>
        ///     维修付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainPaymentScheduleDTO> MaintainPaymentSchedules { get; set; }

        /// <summary>
        ///     选择的维修付款计划
        /// </summary>
        public MaintainPaymentScheduleDTO SelectMaintainPaymentSchedule
        {
            get { return _selectMaintainPaymentSchedule; }
            set
            {
                if (_selectMaintainPaymentSchedule != value)
                {
                    _selectMaintainPaymentSchedule = value;
                    RaisePropertyChanged(() => SelectMaintainPaymentSchedule);
                }
            }
        }

        public PaymentScheduleLineDTO SelectPaymentScheduleLine
        {
            get { return _selectPaymentScheduleLine; }
            set
            {
                _selectPaymentScheduleLine = value;
                RaisePropertyChanged(() => SelectPaymentScheduleLine);
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
            PrepayPayscheduleChildView.Close();
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
            var invoice = new MaintainPrepaymentInvoiceDTO
            {
                PrepaymentInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
                OperatorName = StatusData.curUser
            };
            if (SelectMaintainPaymentSchedule != null)
            {
                if (SelectPaymentScheduleLine == null)
                {
                    MessageAlert("请选择一条付款计划行！");
                }
                else
                {
                    invoice.SupplierId = SelectMaintainPaymentSchedule.SupplierId;
                    invoice.CurrencyId = SelectMaintainPaymentSchedule.CurrencyId;
                    invoice.SupplierName = SelectMaintainPaymentSchedule.SupplierName;
                    invoice.PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId;
                    invoice.InvoiceValue = SelectPaymentScheduleLine.Amount;
                    var invoiceLine = new InvoiceLineDTO
                    {
                        Amount = SelectPaymentScheduleLine.Amount,
                    };
                    invoice.InvoiceLines.Add(invoiceLine);
                    PrepaymentInvoices.AddNew(invoice);
                    PrepayPayscheduleChildView.Close();
                }
            }
            else
            {
                MessageAlert("未选中维修付款计划！");
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