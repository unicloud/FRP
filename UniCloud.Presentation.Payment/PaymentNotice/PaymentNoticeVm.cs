#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 13:38:48
// 文件名：PaymentNoticeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 13:38:48
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Payment.MaintainInvoice;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof (PaymentNoticeVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PaymentNoticeVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;
        [Import] public BankAccountWindow bankAccountWindow;
        public SelectInvoices selectInvoicesWindow;

        [ImportingConstructor]
        public PaymentNoticeVm(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            CompleteNoticeCommand = new DelegateCommand<object>(OnCompleteNotice, CanCompleteNotice);
            // 创建并注册CollectionView
            PaymentNotices = _service.CreateCollection(_context.PaymentNotices, o => o.PaymentNoticeLines);
            PaymentNotices.PageSize = 6;
            PaymentNotices.LoadedData += (o, e) =>
            {
                if (PaymentNotice == null)
                    PaymentNotice = PaymentNotices.FirstOrDefault();
            };
            _service.RegisterCollectionView(PaymentNotices);
            ViewReportCommand = new DelegateCommand<object>(OnViewReport);
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
            // 将CollectionView的AutoLoad属性设为True
            if (!PaymentNotices.AutoLoad)
                PaymentNotices.AutoLoad = true;
            PaymentNotices.Load(true);
            Currencies.Load(true);
            Suppliers.Load(true);
        }

        #region 付款通知

        private PaymentNoticeDTO _paymentNotice;

        private PaymentNoticeLineDTO _paymentNoticeLine;

        /// <summary>
        ///     付款通知集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentNoticeDTO> PaymentNotices { get; set; }

        /// <summary>
        ///     选中的付款通知
        /// </summary>
        public PaymentNoticeDTO PaymentNotice
        {
            get { return _paymentNotice; }
            set
            {
                if (_paymentNotice != value)
                {
                    _paymentNotice = value;
                    if (_paymentNotice != null)
                    {
                        PaymentNoticeLine = _paymentNotice.PaymentNoticeLines.FirstOrDefault();
                    }
                    RaisePropertyChanged(() => PaymentNotice);
                }
            }
        }

        /// <summary>
        ///     选中的付款通知行
        /// </summary>
        public PaymentNoticeLineDTO PaymentNoticeLine
        {
            get { return _paymentNoticeLine; }
            set
            {
                if (_paymentNoticeLine != value)
                {
                    _paymentNoticeLine = value;
                    RaisePropertyChanged(() => PaymentNoticeLine);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 创建新付款通知

        protected override void OnAddInvoice(object obj)
        {
            PaymentNotice = new PaymentNoticeDTO
            {
                PaymentNoticeId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                DeadLine = DateTime.Now,
                Status = 0,
            };
            var firstOrDefault = Suppliers.FirstOrDefault();
            if (firstOrDefault != null)
            {
                PaymentNotice.SupplierId = firstOrDefault.SupplierId;
                var bankAccount = firstOrDefault.BankAccounts.FirstOrDefault();
                if (bankAccount != null)
                {
                    PaymentNotice.BankAccountName = bankAccount.Account + "/" + bankAccount.Bank + bankAccount.Branch;
                    PaymentNotice.BankAccountId = bankAccount.BankAccountId;
                }
            }
            var currencyDto = Currencies.FirstOrDefault();
            if (currencyDto != null) PaymentNotice.CurrencyId = currencyDto.Id;
            PaymentNotices.AddNew(PaymentNotice);
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 删除付款通知

        protected override void OnRemoveInvoice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                PaymentNotices.Remove(PaymentNotice);
                PaymentNotice = PaymentNotices.FirstOrDefault();
            });
        }

        protected override bool CanRemoveInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 增加付款通知行

        protected override void OnAddInvoiceLine(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条付款通知记录！");
                return;
            }

            selectInvoicesWindow = new SelectInvoices();
            selectInvoicesWindow.ViewModel.InitData(PaymentNotice);
            selectInvoicesWindow.ShowDialog();

            //var maintainInvoiceLine = new PaymentNoticeLineDTO
            //{
            //    PaymentNoticeLineId = RandomHelper.Next(),
            //};

            //PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
        }

        protected override bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除付款通知行

        protected override void OnRemoveInvoiceLine(object obj)
        {
            if (PaymentNoticeLine == null)
            {
                MessageAlert("请选择一条付款通知明细！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                PaymentNotice.PaymentNoticeLines.Remove(PaymentNoticeLine);
                PaymentNoticeLine = PaymentNotice.PaymentNoticeLines.FirstOrDefault();
            });
        }

        protected override bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 提交付款通知

        protected override void OnSubmitInvoice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条付款通知记录！");
                return;
            }
            PaymentNotice.Status = (int) PaymentNoticeStatus.待审核;
        }

        protected override bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核付款通知

        protected override void OnReviewInvoice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条付款通知记录！");
                return;
            }
            PaymentNotice.Status = (int) PaymentNoticeStatus.已审核;
            PaymentNotice.Reviewer = "admin";
            PaymentNotice.ReviewDate = DateTime.Now;
        }

        protected override bool CanReviewInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 完成付款通知

        /// <summary>
        ///     完成付款通知
        /// </summary>
        public DelegateCommand<object> CompleteNoticeCommand { get; set; }

        protected void OnCompleteNotice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条付款通知记录！");
                return;
            }
            PaymentNotice.IsComplete = true;
        }

        protected bool CanCompleteNotice(object obj)
        {
            return true;
        }

        #endregion

        #region 预览报表

        /// <summary>
        ///     预览报表
        /// </summary>
        public DelegateCommand<object> ViewReportCommand { get; set; }

        protected virtual void OnViewReport(object obj)
        {
            var noticeReport = new PaymentNoticeReport {WindowStartupLocation = WindowStartupLocation.CenterScreen};
            noticeReport.ShowDialog();
        }

        #endregion

        #region Combobox SelectedChanged

        public void PaymentNoticeListCellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if (e.Cell.Column.UniqueName.Equals("Supplier", StringComparison.OrdinalIgnoreCase))
            {
                var comboBox = e.EditingElement as RadComboBox;
                if (comboBox != null)
                {
                    var supplier = comboBox.SelectedItem as SupplierDTO;
                    if (supplier != null)
                    {
                        PaymentNotice.SupplierName = supplier.Name;
                        bankAccountWindow.ViewModel.InitBankAccounts(supplier.BankAccounts);
                        bankAccountWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        bankAccountWindow.Closed += BankAccountWindowClosed;
                        bankAccountWindow.ShowDialog();
                    }
                }
            }
        }

        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is SupplierDTO)
            {
            }
        }

        private void BankAccountWindowClosed(object sender, WindowClosedEventArgs e)
        {
            if (bankAccountWindow.Tag is BankAccountDTO)
            {
                var bankAccount = bankAccountWindow.Tag as BankAccountDTO;
                PaymentNotice.BankAccountId = bankAccount.BankAccountId;
                PaymentNotice.BankAccountName = bankAccount.Account + "/" + bankAccount.Bank + bankAccount.Branch;
            }
        }

        #endregion

        #endregion
    }
}