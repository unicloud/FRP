#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/10 9:13:44
// 文件名：SelectInvoicesVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/10 9:13:44
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(SelectInvoicesVm))]
    public class SelectInvoicesVm : ViewModelBase
    {
        #region 初始化
        public SelectInvoices SelectInvoicesWindow;
        private PaymentNoticeDTO _paymentNotice;
        private readonly FilterDescriptor _supplierFilter;

        public SelectInvoicesVm(SelectInvoices selectInvoicesWindow, IPaymentService service)
            : base(service)
        {
            SelectInvoicesWindow = selectInvoicesWindow;
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(service.Context, service.Context.Suppliers);
            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(service.Context, service.Context.Currencies);
            _supplierFilter = new FilterDescriptor("SupplierId", FilterOperator.IsEqualTo, 0);
            #region 采购发票
            PurchaseInvoices = new QueryableDataServiceCollectionView<PurchaseInvoiceDTO>(service.Context, service.Context.PurchaseInvoices);
            PurchaseInvoices.FilterDescriptors.Add(_supplierFilter);
            PurchaseInvoices.LoadedData += (e, o) =>
            {
                PurchaseInvoiceList = new ObservableCollection<PurchaseInvoiceDTO>();
                PurchaseInvoices.ToList().ForEach(PurchaseInvoiceList.Add);
                SelectPurchaseInvoices = new ObservableCollection<PurchaseInvoiceDTO>();
                if (!PurchaseInvoiceList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 0)
                    {
                        if (PurchaseInvoiceList.Any(t => t.PurchaseInvoiceId == p.InvoiceId))
                            SelectPurchaseInvoices.Add(PurchaseInvoiceList.FirstOrDefault(t => t.PurchaseInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 预付款发票
            PrepaymentInvoices = new QueryableDataServiceCollectionView<PurchasePrepaymentInvoiceDTO>(service.Context, service.Context.PurchasePrepaymentInvoices);
            PrepaymentInvoices.FilterDescriptors.Add(_supplierFilter);
            PrepaymentInvoices.LoadedData += (e, o) =>
            {
                PrepaymentInvoiceList = new ObservableCollection<PurchasePrepaymentInvoiceDTO>();
                PrepaymentInvoices.ToList().ForEach(PrepaymentInvoiceList.Add);
                SelectPrepaymentInvoices = new ObservableCollection<PurchasePrepaymentInvoiceDTO>();
                if (!PrepaymentInvoiceList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 1)
                    {
                        if (PrepaymentInvoiceList.Any(t => t.PrepaymentInvoiceId == p.InvoiceId))
                            SelectPrepaymentInvoices.Add(PrepaymentInvoiceList.FirstOrDefault(t => t.PrepaymentInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 租赁发票
            LeaseInvoices = new QueryableDataServiceCollectionView<LeaseInvoiceDTO>(service.Context, service.Context.LeaseInvoices);
            LeaseInvoices.FilterDescriptors.Add(_supplierFilter);
            LeaseInvoices.LoadedData += (e, o) =>
            {
                LeaseInvoiceList = new ObservableCollection<LeaseInvoiceDTO>();
                LeaseInvoices.ToList().ForEach(LeaseInvoiceList.Add);
                SelectLeaseInvoices = new ObservableCollection<LeaseInvoiceDTO>();
                if (!LeaseInvoiceList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 2)
                    {
                        if (LeaseInvoiceList.Any(t => t.LeaseInvoiceId == p.InvoiceId))
                            SelectLeaseInvoices.Add(LeaseInvoiceList.FirstOrDefault(t => t.LeaseInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 维修发票
            MaintainInvoices = new QueryableDataServiceCollectionView<BaseMaintainInvoiceDTO>(service.Context, service.Context.MaintainInvoices);
            MaintainInvoices.FilterDescriptors.Add(_supplierFilter);
            MaintainInvoices.LoadedData += (e, o) =>
            {
                MaintainInvoiceList = new ObservableCollection<BaseMaintainInvoiceDTO>();
                MaintainInvoices.ToList().ForEach(MaintainInvoiceList.Add);
                SelectMaintainInvoices = new ObservableCollection<BaseMaintainInvoiceDTO>();
                if (!MaintainInvoiceList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 3)
                    {
                        if (MaintainInvoiceList.Any(t => t.MaintainInvoiceId == p.InvoiceId))
                            SelectMaintainInvoices.Add(MaintainInvoiceList.FirstOrDefault(t => t.MaintainInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 贷项单
            CreditNotes = new QueryableDataServiceCollectionView<PurchaseCreditNoteDTO>(service.Context, service.Context.PurchaseCreditNotes);
            CreditNotes.FilterDescriptors.Add(_supplierFilter);
            CreditNotes.LoadedData += (e, o) =>
            {
                CreditNoteList = new ObservableCollection<PurchaseCreditNoteDTO>();
                CreditNotes.ToList().ForEach(CreditNoteList.Add);
                SelectCreditNotes = new ObservableCollection<PurchaseCreditNoteDTO>();
                if (!CreditNoteList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 4)
                    {
                        if (CreditNoteList.Any(t => t.CreditNoteId == p.InvoiceId))
                            SelectCreditNotes.Add(CreditNoteList.FirstOrDefault(t => t.CreditNoteId == p.InvoiceId));
                    }
                });
            };
            #endregion
        }

        public void InitData(PaymentNoticeDTO paymentNotice)
        {
            _paymentNotice = paymentNotice;
            Currencies.Load(true);
            Suppliers.Load(true);
            _supplierFilter.Value = paymentNotice.SupplierId;
            PurchaseInvoices.Load(true);
            PrepaymentInvoices.Load(true);
            LeaseInvoices.Load(true);
            MaintainInvoices.Load(true);
            CreditNotes.Load(true);
        }
        #endregion

        #region 公共属性
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }
        #region 采购发票
        public QueryableDataServiceCollectionView<PurchaseInvoiceDTO> PurchaseInvoices { get; set; }
        public ObservableCollection<PurchaseInvoiceDTO> PurchaseInvoiceList { get; set; }
        public ObservableCollection<PurchaseInvoiceDTO> SelectPurchaseInvoices { get; set; }
        #endregion

        #region 预付款发票
        public QueryableDataServiceCollectionView<PurchasePrepaymentInvoiceDTO> PrepaymentInvoices { get; set; }
        public ObservableCollection<PurchasePrepaymentInvoiceDTO> PrepaymentInvoiceList { get; set; }
        public ObservableCollection<PurchasePrepaymentInvoiceDTO> SelectPrepaymentInvoices { get; set; }
        #endregion

        #region 预付款发票
        public QueryableDataServiceCollectionView<LeaseInvoiceDTO> LeaseInvoices { get; set; }
        public ObservableCollection<LeaseInvoiceDTO> LeaseInvoiceList { get; set; }
        public ObservableCollection<LeaseInvoiceDTO> SelectLeaseInvoices { get; set; }
        #endregion

        #region 维修发票
        public QueryableDataServiceCollectionView<BaseMaintainInvoiceDTO> MaintainInvoices { get; set; }
        public ObservableCollection<BaseMaintainInvoiceDTO> MaintainInvoiceList { get; set; }
        public ObservableCollection<BaseMaintainInvoiceDTO> SelectMaintainInvoices { get; set; }
        #endregion

        #region 贷项单
        public QueryableDataServiceCollectionView<PurchaseCreditNoteDTO> CreditNotes { get; set; }
        public ObservableCollection<PurchaseCreditNoteDTO> CreditNoteList { get; set; }
        public ObservableCollection<PurchaseCreditNoteDTO> SelectCreditNotes { get; set; }
        #endregion

        #endregion

        #region 操作
        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            SelectInvoicesWindow.Close();
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

        public DelegateCommand<object> CommitCommand { get; set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            #region 采购发票
            SelectPurchaseInvoices.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.PurchaseInvoiceId))
                {
                    var puchaseInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 0,
                        InvoiceId = p.PurchaseInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(puchaseInvoiceLine);
                }
            });
            #endregion
            #region 预付款发票
            SelectPrepaymentInvoices.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.PrepaymentInvoiceId))
                {
                    var prepayInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 1,
                        InvoiceId = p.PrepaymentInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(prepayInvoiceLine);
                }
            });
            #endregion
            #region 租赁发票
            SelectLeaseInvoices.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.LeaseInvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 2,
                        InvoiceId = p.LeaseInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            #region 维修发票
            SelectMaintainInvoices.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.MaintainInvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 3,
                        InvoiceId = p.MaintainInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            #region 贷项单
            SelectCreditNotes.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.CreditNoteId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 4,
                        InvoiceId = p.CreditNoteId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            for (int i = _paymentNotice.PaymentNoticeLines.Count - 1; i >= 0; i--)
            {
                var temp = _paymentNotice.PaymentNoticeLines[i];
                if (SelectPurchaseInvoices.Count > 0 && SelectPurchaseInvoices.All(p => p.PurchaseInvoiceId != temp.InvoiceId))
                {
                    _paymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectPrepaymentInvoices.Count > 0 && SelectPrepaymentInvoices.All(p => p.PrepaymentInvoiceId != temp.InvoiceId))
                {
                    _paymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectLeaseInvoices.Count > 0 && SelectLeaseInvoices.All(p => p.LeaseInvoiceId != temp.InvoiceId))
                {
                    _paymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectMaintainInvoices.Count > 0 && SelectMaintainInvoices.All(p => p.MaintainInvoiceId != temp.InvoiceId))
                {
                    _paymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectCreditNotes.Count > 0 && SelectCreditNotes.All(p => p.CreditNoteId != temp.InvoiceId))
                {
                    _paymentNotice.PaymentNoticeLines.Remove(temp);
                }
            }
            SelectInvoicesWindow.Close();
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
        public override void LoadData()
        {
        }
        #endregion
    }
}
