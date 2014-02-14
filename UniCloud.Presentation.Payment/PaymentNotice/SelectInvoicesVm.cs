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
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(SelectInvoicesVm))]
    public class SelectInvoicesVm : ViewModelBase
    {
        #region 初始化
        public SelectInvoices SelectInvoicesWindow;
        public PaymentNoticeDTO PaymentNotice;

        public SelectInvoicesVm(SelectInvoices selectInvoicesWindow, IPaymentService service)
            : base(service)
        {
            SelectInvoicesWindow = selectInvoicesWindow;
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(service.Context, service.Context.Suppliers);
            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(service.Context, service.Context.Currencies);
            #region 采购发票
            PurchaseInvoices = new QueryableDataServiceCollectionView<PurchaseInvoiceDTO>(service.Context, service.Context.PurchaseInvoices);
            PurchaseInvoices.LoadedData += (e, o) =>
            {
                PurchaseInvoiceList = new ObservableCollection<PurchaseInvoiceDTO>();
                PurchaseInvoices.ToList().ForEach(PurchaseInvoiceList.Add);
                SelectPurchaseInvoices = new ObservableCollection<PurchaseInvoiceDTO>();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 0)
                    {
                        SelectPurchaseInvoices.Add(PurchaseInvoiceList.FirstOrDefault(t => t.PurchaseInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 预付款发票
            PrepaymentInvoices = new QueryableDataServiceCollectionView<PrepaymentInvoiceDTO>(service.Context, service.Context.PrepaymentInvoices);
            PrepaymentInvoices.LoadedData += (e, o) =>
            {
                PrepaymentInvoiceList = new ObservableCollection<PrepaymentInvoiceDTO>();
                PrepaymentInvoices.ToList().ForEach(PrepaymentInvoiceList.Add);
                SelectPrepaymentInvoices = new ObservableCollection<PrepaymentInvoiceDTO>();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 1)
                    {
                        SelectPrepaymentInvoices.Add(PrepaymentInvoiceList.FirstOrDefault(t => t.PrepaymentInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 租赁发票
            LeaseInvoices = new QueryableDataServiceCollectionView<LeaseInvoiceDTO>(service.Context, service.Context.LeaseInvoices);
            LeaseInvoices.LoadedData += (e, o) =>
            {
                LeaseInvoiceList = new ObservableCollection<LeaseInvoiceDTO>();
                LeaseInvoices.ToList().ForEach(LeaseInvoiceList.Add);
                SelectLeaseInvoices = new ObservableCollection<LeaseInvoiceDTO>();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 2)
                    {
                        SelectLeaseInvoices.Add(LeaseInvoiceList.FirstOrDefault(t => t.LeaseInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 维修发票
            MaintainInvoices = new QueryableDataServiceCollectionView<BaseMaintainInvoiceDTO>(service.Context, service.Context.MaintainInvoices);
            MaintainInvoices.LoadedData += (e, o) =>
            {
                MaintainInvoiceList = new ObservableCollection<BaseMaintainInvoiceDTO>();
                MaintainInvoices.ToList().ForEach(MaintainInvoiceList.Add);
                SelectMaintainInvoices = new ObservableCollection<BaseMaintainInvoiceDTO>();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 3)
                    {
                        SelectMaintainInvoices.Add(MaintainInvoiceList.FirstOrDefault(t => t.MaintainInvoiceId == p.InvoiceId));
                    }
                });
            };
            #endregion
            #region 贷项单
            CreditNotes = new QueryableDataServiceCollectionView<CreditNoteDTO>(service.Context, service.Context.CreditNotes);
            CreditNotes.LoadedData += (e, o) =>
            {
                CreditNoteList = new ObservableCollection<CreditNoteDTO>();
                CreditNotes.ToList().ForEach(CreditNoteList.Add);
                SelectCreditNotes = new ObservableCollection<CreditNoteDTO>();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 4)
                    {
                        SelectCreditNotes.Add(CreditNoteList.FirstOrDefault(t => t.CreditNoteId == p.InvoiceId));
                    }
                });
            };
            #endregion
        }

        public void InitData(PaymentNoticeDTO paymentNotice)
        {
            PaymentNotice = paymentNotice;
            Currencies.Load(true);
            Suppliers.Load(true);

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
        public QueryableDataServiceCollectionView<PrepaymentInvoiceDTO> PrepaymentInvoices { get; set; }
        public ObservableCollection<PrepaymentInvoiceDTO> PrepaymentInvoiceList { get; set; }
        public ObservableCollection<PrepaymentInvoiceDTO> SelectPrepaymentInvoices { get; set; }
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
        public QueryableDataServiceCollectionView<CreditNoteDTO> CreditNotes { get; set; }
        public ObservableCollection<CreditNoteDTO> CreditNoteList { get; set; }
        public ObservableCollection<CreditNoteDTO> SelectCreditNotes { get; set; }
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
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.PurchaseInvoiceId))
                {
                    var puchaseInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 0,
                        InvoiceTypeString = InvoiceType.采购发票.ToString(),
                        InvoiceId = p.PurchaseInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(puchaseInvoiceLine);
                }
            });
            #endregion
            #region 预付款发票
            SelectPrepaymentInvoices.ToList().ForEach(p =>
            {
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.PrepaymentInvoiceId))
                {
                    var prepayInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 1,
                        InvoiceTypeString = InvoiceType.预付款发票.ToString(),
                        InvoiceId = p.PrepaymentInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(prepayInvoiceLine);
                }
            });
            #endregion
            #region 租赁发票
            SelectLeaseInvoices.ToList().ForEach(p =>
            {
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.LeaseInvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 2,
                        InvoiceTypeString = InvoiceType.租赁发票.ToString(),
                        InvoiceId = p.LeaseInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            #region 维修发票
            SelectMaintainInvoices.ToList().ForEach(p =>
            {
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.MaintainInvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 3,
                        InvoiceTypeString = InvoiceType.维修发票.ToString(),
                        InvoiceId = p.MaintainInvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            #region 贷项单
            SelectCreditNotes.ToList().ForEach(p =>
            {
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.CreditNoteId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 4,
                        InvoiceTypeString = InvoiceType.贷项单.ToString(),
                        InvoiceId = p.CreditNoteId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            for (int i = PaymentNotice.PaymentNoticeLines.Count - 1; i >= 0; i--)
            {
                var temp = PaymentNotice.PaymentNoticeLines[i];
                if (SelectPurchaseInvoices.Count > 0 && SelectPurchaseInvoices.All(p => p.PurchaseInvoiceId != temp.InvoiceId))
                {
                    PaymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectPrepaymentInvoices.Count > 0 && SelectPrepaymentInvoices.All(p => p.PrepaymentInvoiceId != temp.InvoiceId))
                {
                    PaymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectLeaseInvoices.Count > 0 && SelectLeaseInvoices.All(p => p.LeaseInvoiceId != temp.InvoiceId))
                {
                    PaymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectMaintainInvoices.Count > 0 && SelectMaintainInvoices.All(p => p.MaintainInvoiceId != temp.InvoiceId))
                {
                    PaymentNotice.PaymentNoticeLines.Remove(temp);
                }
                else if (SelectCreditNotes.Count > 0 && SelectCreditNotes.All(p => p.CreditNoteId != temp.InvoiceId))
                {
                    PaymentNotice.PaymentNoticeLines.Remove(temp);
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
