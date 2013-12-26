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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Payment.Invoice;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(PaymentNoticeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PaymentNoticeVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        //[Import]
        //public PaymentNoticeEdit CurrenPaymentNoticeEdit;
        [ImportingConstructor]
        public PaymentNoticeVm(IRegionManager regionManager)
        {
            _regionManager = regionManager;

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
            // 创建并注册CollectionView
            PaymentNotices = Service.CreateCollection(PaymentDataService.PaymentNotices);
            Service.RegisterCollectionView(PaymentNotices);
            PaymentNotices.PropertyChanged += OnViewPropertyChanged;
            ViewReportCommand = new DelegateCommand<object>(OnViewReport);
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
            PaymentNotices.AutoLoad = true;
            PaymentNotices.Load(true);
            Currencies.Load(true);
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
             var currenPaymentNoticeEdit=new PaymentNoticeEdit();
            currenPaymentNoticeEdit.ViewModel.InitData(0, PaymentNoticeEditClosed);
            currenPaymentNoticeEdit.ShowDialog();
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
        }
        private void PaymentNoticeEditClosed(object sender, WindowClosedEventArgs e)
        {
           PaymentNotices.Load(true);
        }
        #endregion

        #region 修改付款通知
        protected override void OnEditInvoice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
             var currenPaymentNoticeEdit=new PaymentNoticeEdit();
            currenPaymentNoticeEdit.ViewModel.InitData(PaymentNotice.PaymentNoticeId, PaymentNoticeEditClosed);
            currenPaymentNoticeEdit.ShowDialog();
        }

        protected override bool CanEditInvoice(object obj)
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

        #region 提交付款通知
        protected override void OnSubmitInvoice(object obj)
        {
            if (PaymentNotice == null)
            {
                MessageAlert("请选择一条付款通知记录！");
                return;
            }
            PaymentNotice.Status = (int)PaymentNoticeStatus.待审核;
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
            PaymentNotice.Status = (int)PaymentNoticeStatus.已审核;
            PaymentNotice.Reviewer = "admin";
            PaymentNotice.ReviewDate = DateTime.Now;
        }

        protected override bool CanReviewInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 预览报表
        /// <summary>
        ///  预览报表
        /// </summary>
        public DelegateCommand<object> ViewReportCommand { get; set; }

        protected virtual void OnViewReport(object obj)
        {
            var noticeReport = new PaymentNoticeReport();
            noticeReport.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            noticeReport.ShowDialog();
        }
        #endregion

        #region Combobox SelectedChanged

        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is SupplierDTO)
            {
                PaymentNotice.SupplierName = (comboboxSelectedItem as SupplierDTO).Name;
            }
        }
        #endregion
        #endregion
    }
}
