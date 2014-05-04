#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:45:49
// 文件名：APUMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:45:49
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof(APUMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class APUMaintainVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public APUMaintainVm(IRegionManager regionManager, IPaymentService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            // 创建并注册CollectionView
            ApuMaintainInvoices = _service.CreateCollection(_context.APUMaintainInvoices, o => o.MaintainInvoiceLines);
            ApuMaintainInvoices.PageSize = 6;
            var supplierFilter = new FilterDescriptor("MaintainSupplier", FilterOperator.IsEqualTo, true);
            Suppliers.FilterDescriptors.Add(supplierFilter);
            _service.RegisterCollectionView(ApuMaintainInvoices);
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
            if (!ApuMaintainInvoices.AutoLoad)
                ApuMaintainInvoices.AutoLoad = true;
            ApuMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
            PaymentSchedules.Load(true);
        }

        #region APU维修发票

        private APUMaintainInvoiceDTO _apuMaintainInvoice;

        private MaintainInvoiceLineDTO _apuMaintainInvoiceLine;

        /// <summary>
        ///     APU维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUMaintainInvoiceDTO> ApuMaintainInvoices { get; set; }

        /// <summary>
        ///     选中的APU维修发票
        /// </summary>
        public APUMaintainInvoiceDTO ApuMaintainInvoice
        {
            get { return _apuMaintainInvoice; }
            set
            {
                _apuMaintainInvoice = value;
                RelatedPaymentSchedule.Clear();
                SelPaymentSchedule = null;
                if (value != null)
                {
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
                                l => l.InvoiceId == value.APUMaintainInvoiceId);
                }
                RaisePropertyChanged(() => ApuMaintainInvoice);
            }
        }

        /// <summary>
        ///     选中的APU维修发票
        /// </summary>
        public MaintainInvoiceLineDTO ApuMaintainInvoiceLine
        {
            get { return _apuMaintainInvoiceLine; }
            set
            {
                if (_apuMaintainInvoiceLine != value)
                {
                    _apuMaintainInvoiceLine = value;
                    RaisePropertyChanged(() => ApuMaintainInvoiceLine);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 创建新维修发票

        protected override void OnAddInvoice(object obj)
        {
            PrepayPayscheduleChildView.ViewModel.InitData(typeof(APUMaintainInvoiceDTO), PrepayPayscheduleChildViewClosed);
            PrepayPayscheduleChildView.ShowDialog();
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
        }

        private void PrepayPayscheduleChildViewClosed(object sender, WindowClosedEventArgs e)
        {
            if (PrepayPayscheduleChildView.Tag != null)
            {
                var maintainInvoice = PrepayPayscheduleChildView.Tag as APUMaintainInvoiceDTO;
                ApuMaintainInvoices.AddNew(maintainInvoice);
            }
        }
        #endregion

        #region 删除维修发票

        protected override void OnRemoveInvoice(object obj)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                ApuMaintainInvoices.Remove(_apuMaintainInvoice);
                ApuMaintainInvoice = ApuMaintainInvoices.FirstOrDefault();
            });
        }

        protected override bool CanRemoveInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 增加维修发票行

        protected override void OnAddInvoiceLine(object obj)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            var maintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            ApuMaintainInvoice.MaintainInvoiceLines.Add(maintainInvoiceLine);
        }

        protected override bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除维修发票行

        protected override void OnRemoveInvoiceLine(object obj)
        {
            if (ApuMaintainInvoiceLine == null)
            {
                MessageAlert("请选择一条维修发票明细！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                ApuMaintainInvoice.MaintainInvoiceLines.Remove(ApuMaintainInvoiceLine);
            });
        }

        protected override bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 提交发票

        protected override void OnSubmitInvoice(object obj)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            ApuMaintainInvoice.Status = (int)InvoiceStatus.待审核;
        }

        protected override bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核发票

        protected override void OnReviewInvoice(object obj)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            ApuMaintainInvoice.Status = (int)InvoiceStatus.已审核;
            ApuMaintainInvoice.Reviewer = "admin";
            ApuMaintainInvoice.ReviewDate = DateTime.Now;
            ApuMaintainInvoice.IsValid = true;
        }

        protected override bool CanReviewInvoice(object obj)
        {
            return true;
        }

        #endregion

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
                ApuMaintainInvoice.DocumentId = doc.DocumentId;
                ApuMaintainInvoice.DocumentName = doc.Name;
            }
        }

        #endregion

        #region Combobox SelectedChanged

        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is SupplierDTO)
            {
                ApuMaintainInvoice.SupplierName = (comboboxSelectedItem as SupplierDTO).Name;
            }
        }

        #endregion

        #endregion
    }
}