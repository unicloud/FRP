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
            #region 发票
            Invoices = new QueryableDataServiceCollectionView<BaseInvoiceDTO>(service.Context, service.Context.Invoices);
            Invoices.FilterDescriptors.Add(_supplierFilter);
            Invoices.LoadedData += (e, o) =>
            {
                InvoiceList = new ObservableCollection<BaseInvoiceDTO>();
                Invoices.ToList().ForEach(InvoiceList.Add);
                SelectInvoices = new ObservableCollection<BaseInvoiceDTO>();
                if (!InvoiceList.Any()) return;
                _paymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (InvoiceList.Any(t => t.InvoiceId == p.InvoiceId))
                        SelectInvoices.Add(InvoiceList.FirstOrDefault(t => t.InvoiceId == p.InvoiceId));
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
            Invoices.Load(true);
        }
        #endregion

        #region 公共属性
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        #region 发票
        public QueryableDataServiceCollectionView<BaseInvoiceDTO> Invoices { get; set; }
        public ObservableCollection<BaseInvoiceDTO> InvoiceList { get; set; }
        public ObservableCollection<BaseInvoiceDTO> SelectInvoices { get; set; }
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
            #region 发票
            SelectInvoices.ToList().ForEach(p =>
            {
                if (_paymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != p.InvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = p.InvoiceType,
                        InvoiceId = p.InvoiceId,
                        InvoiceNumber = p.InvoiceNumber,
                    };

                    _paymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            #endregion
            for (int i = _paymentNotice.PaymentNoticeLines.Count - 1; i >= 0; i--)
            {
                var temp = _paymentNotice.PaymentNoticeLines[i];
                if (SelectInvoices.Count > 0 && SelectInvoices.All(p => p.InvoiceId != temp.InvoiceId))
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
