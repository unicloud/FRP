#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/25 15:07:05
// 文件名：PaymentNoticeEditVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/25 15:07:05
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
using UniCloud.Presentation.Payment.Invoice;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{

    public class PaymentNoticeEditVm : InvoiceVm
    {
        #region 声明、初始化

        public PaymentNoticeEdit CurrenPaymentNoticeEdit;
        private int _currentPaymentNoticeId;
        [ImportingConstructor]
        public PaymentNoticeEditVm(PaymentNoticeEdit payeNoticeEdit)
        {
            CurrenPaymentNoticeEdit = payeNoticeEdit;
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
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            // 创建并注册CollectionView
            PaymentNotices = new QueryableDataServiceCollectionView<PaymentNoticeDTO>(PaymentDataService, PaymentDataService.PaymentNotices);
            PaymentNotices.LoadedData += (o, e) =>
            {
                try
                {
                    var result = (o as QueryableDataServiceCollectionView<PaymentNoticeDTO>).FirstOrDefault(p => p.PaymentNoticeId == _currentPaymentNoticeId);
                    if (result != null)
                    {
                        PaymentNotice = result;
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert(ex.Message);
                }
            };
            PaymentNotices.SubmittedChanges += (o, e) =>
                                               {
                                                   if (e.Error == null)
                                                       CurrenPaymentNoticeEdit.Close();
                                               };
        }

        #endregion

        #region 数据

        #region 公共属性
        /// <summary>
        /// 发票类型
        /// </summary>
        public Array InvoiceTypes
        {
            get { return Enum.GetValues(typeof(InvoiceType)); }
        }

        /// <summary>
        /// 选中供应商
        /// </summary>
        private SupplierDTO _supplier;
        public SupplierDTO Supplier
        {
            get { return _supplier; }
            set
            {
                _supplier = value;
                if (_supplier != null)
                {
                    PaymentNotice.SupplierId = _supplier.SupplierId;
                    PaymentNotice.SupplierName = _supplier.Name;
                }
                RaisePropertyChanged("Supplier");
            }
        }

        /// <summary>
        /// 选中银行账号
        /// </summary>
        private BankAccountDTO _bankAccount;
        public BankAccountDTO BankAccount
        {
            get { return _bankAccount; }
            set
            {
                _bankAccount = value;
                if (_bankAccount != null)
                {
                    PaymentNotice.BankAccountId = _bankAccount.BankAccountId;
                    PaymentNotice.BankAccountName = _bankAccount.Account + "/" + _bankAccount.Bank + _bankAccount.Branch;
                }
                RaisePropertyChanged("BankAccount");
            }
        }

        #endregion

        #region 加载数据

        public void InitData(int paymentNoticeId, EventHandler<WindowClosedEventArgs> closed)
        {
            CurrenPaymentNoticeEdit.Closed -= closed;
            CurrenPaymentNoticeEdit.Closed += closed;
            CurrenPaymentNoticeEdit.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Suppliers.Load(true);
            Currencies.Load(true);

            if (paymentNoticeId == 0)
            {
                PaymentNotice = new PaymentNoticeDTO
                                {
                                    PaymentNoticeId = RandomHelper.Next(),
                                    CreateDate = DateTime.Now,
                                    DeadLine = DateTime.Now,
                                    Status = 0,
                                    StatusString = PaymentNoticeStatus.草稿.ToString()
                                };
                PaymentNotices.AddNew(PaymentNotice);
            }
            else
            {
                _currentPaymentNoticeId = paymentNoticeId;
                PaymentNotices.Load(true);
            }
        }

        #region 付款通知
        /// <summary>
        /// 付款通知集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentNoticeDTO> PaymentNotices { get; set; }

        private PaymentNoticeDTO _paymentNotice;
        /// <summary>
        /// 选中的付款通知
        /// </summary>
        public PaymentNoticeDTO PaymentNotice
        {
            get { return _paymentNotice; }
            set
            {
                if (_paymentNotice != value)
                {
                    _paymentNotice = value;
                    RaisePropertyChanged(() => PaymentNotice);
                }
            }
        }

        private PaymentNoticeLineDTO _paymentNoticeLine;
        /// <summary>
        /// 选中的付款通知行
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
            var paymentNotice = new PaymentNoticeDTO
                                  {
                                      PaymentNoticeId = RandomHelper.Next(),
                                      CreateDate = DateTime.Now,
                                      DeadLine = DateTime.Now
                                  };
            PaymentNotices.AddNew(paymentNotice);
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
                                                PaymentNotices.Remove(_paymentNotice);
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
            var maintainInvoiceLine = new PaymentNoticeLineDTO
            {
                PaymentNoticeLineId = RandomHelper.Next(),
            };

            PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
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
                                            });
        }

        protected override bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }
        #endregion

        #region
        public DelegateCommand<object> CellEditEndCommand { get; set; }
        public void CellEditEnd(object sender)
        {
            
        }
        #endregion
        #endregion
    }
}
