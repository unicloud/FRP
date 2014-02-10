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
            EngineMaintainInvoices = new QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO>(service.Context, service.Context.EngineMaintainInvoices);
            EngineMaintainInvoices.LoadedData += (e, o) =>
            {
                EngineMaintainInvoiceList = new ObservableCollection<EngineMaintainInvoiceDTO>();
                EngineMaintainInvoices.ToList().ForEach(EngineMaintainInvoiceList.Add);
                SelectEngineMaintainInvoices.Clear();

                PaymentNotice.PaymentNoticeLines.ToList().ForEach(p =>
                {
                    if (p.InvoiceType == 3)
                    {
                        SelectEngineMaintainInvoices.Add(EngineMaintainInvoiceList.FirstOrDefault(t => t.EngineMaintainInvoiceId == p.InvoiceId));
                    }
                });
            };
        }

        public void InitData(PaymentNoticeDTO paymentNotice)
        {
            PaymentNotice = paymentNotice;
            EngineMaintainInvoices.Load(true);
        }
        #endregion

        #region 公共属性
        #region 维修发票
        public QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO> EngineMaintainInvoices { get; set; }
        public ObservableCollection<EngineMaintainInvoiceDTO> EngineMaintainInvoiceList { get; set; }
        private ObservableCollection<EngineMaintainInvoiceDTO> _selectEngineMaintainInvoices = new ObservableCollection<EngineMaintainInvoiceDTO>();
        public ObservableCollection<EngineMaintainInvoiceDTO> SelectEngineMaintainInvoices
        {
            get
            {
                return _selectEngineMaintainInvoices;
            }
            set
            {
                _selectEngineMaintainInvoices = value;
                RaisePropertyChanged(() => SelectEngineMaintainInvoices);
            }
        }
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
            SelectInvoicesWindow.MaintainInvoiceGridView.SelectedItems.ToList().ForEach(p =>
            {
                if (PaymentNotice.PaymentNoticeLines.All(t => t.InvoiceId != (p as EngineMaintainInvoiceDTO).EngineMaintainInvoiceId))
                {
                    var maintainInvoiceLine = new PaymentNoticeLineDTO
                    {
                        PaymentNoticeLineId = RandomHelper.Next(),
                        InvoiceType = 3,
                        InvoiceTypeString = InvoiceType.维修发票.ToString(),
                        InvoiceId = (p as EngineMaintainInvoiceDTO).EngineMaintainInvoiceId,
                        InvoiceNumber = (p as EngineMaintainInvoiceDTO).InvoiceNumber,
                    };

                    PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
                }
            });
            for (int i = PaymentNotice.PaymentNoticeLines.Count - 1; i > 0; i--)
            {
                var temp = PaymentNotice.PaymentNoticeLines[i];
                if (SelectInvoicesWindow.MaintainInvoiceGridView.SelectedItems.ToList().All(p => (p as EngineMaintainInvoiceDTO).EngineMaintainInvoiceId != temp.InvoiceId))
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
