#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:47:38
// 文件名：UnderCartMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:47:38
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof (UndercartMaintainVm))]
    public class UndercartMaintainVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;
        [Import] public DocumentViewer documentView;

        [ImportingConstructor]
        public UndercartMaintainVm(IPaymentService service)
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
            // 创建并注册CollectionView
            UndercartMaintainInvoices = _service.CreateCollection(_context.UndercartMaintainInvoices,
                o => o.MaintainInvoiceLines);
            UndercartMaintainInvoices.PageSize = 6;
            UndercartMaintainInvoices.LoadedData += (o, e) =>
            {
                if (UndercartMaintainInvoice == null)
                    UndercartMaintainInvoice = UndercartMaintainInvoices.FirstOrDefault();
            };
            var supplierFilter = new FilterDescriptor("MaintainSupplier", FilterOperator.IsEqualTo, true);
            Suppliers.FilterDescriptors.Add(supplierFilter);
            _service.RegisterCollectionView(UndercartMaintainInvoices);
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
            if (!UndercartMaintainInvoices.AutoLoad)
                UndercartMaintainInvoices.AutoLoad = true;
            UndercartMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
            PaymentSchedules.Load(true);
        }

        #region 起落架维修发票

        private UndercartMaintainInvoiceDTO _undercartMaintainInvoice;

        private MaintainInvoiceLineDTO _undercartMaintainInvoiceLine;

        /// <summary>
        ///     起落架维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO> UndercartMaintainInvoices { get; set; }

        /// <summary>
        ///     选中的起落架维修发票
        /// </summary>
        public UndercartMaintainInvoiceDTO UndercartMaintainInvoice
        {
            get { return _undercartMaintainInvoice; }
            set
            {
                _undercartMaintainInvoice = value;
                RelatedPaymentSchedule.Clear();
                SelPaymentSchedule = null;
                if (value != null)
                {
                    UndercartMaintainInvoiceLine = value.MaintainInvoiceLines.FirstOrDefault();
                    var relate = PaymentSchedules.FirstOrDefault(p =>
                    {
                        var paymentScheduleLine =
                            p.PaymentScheduleLines.FirstOrDefault(
                                l => l.PaymentScheduleLineId == value.PaymentScheduleLineId);
                        return paymentScheduleLine != null &&
                               paymentScheduleLine.PaymentScheduleLineId == value.PaymentScheduleLineId;
                    });
                    if (relate != null)
                        RelatedPaymentSchedule.Add(relate);
                    SelPaymentSchedule = RelatedPaymentSchedule.FirstOrDefault();
                    if (SelPaymentSchedule != null)
                        RelatedPaymentScheduleLine =
                            SelPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                                l => l.InvoiceId == value.UndercartMaintainInvoiceId);
                }
                RaisePropertyChanged(() => UndercartMaintainInvoice);
            }
        }

        /// <summary>
        ///     选中的APU维修发票
        /// </summary>
        public MaintainInvoiceLineDTO UndercartMaintainInvoiceLine
        {
            get { return _undercartMaintainInvoiceLine; }
            set
            {
                if (_undercartMaintainInvoiceLine != value)
                {
                    _undercartMaintainInvoiceLine = value;
                    RaisePropertyChanged(() => UndercartMaintainInvoiceLine);
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
            MessageConfirm("是否根据付款计划创建?", (s, arg) =>
            {
                if (arg.DialogResult != true)
                {
                    UndercartMaintainInvoice = new UndercartMaintainInvoiceDTO
                    {
                        UndercartMaintainInvoiceId =
                            RandomHelper.Next(),
                        CreateDate = DateTime.Now,
                        InvoiceDate = DateTime.Now,
                        InMaintainTime = DateTime.Now,
                        OutMaintainTime = DateTime.Now,
                        OperatorName = StatusData.curUser
                    };
                    var currency = Currencies.FirstOrDefault();
                    if (currency != null)
                        UndercartMaintainInvoice.CurrencyId = currency.Id;
                    var supplier = Suppliers.FirstOrDefault();
                    if (supplier != null)
                    {
                        UndercartMaintainInvoice.SupplierId = supplier.SupplierId;
                        UndercartMaintainInvoice.SupplierName = supplier.Name;
                    }
                    UndercartMaintainInvoices.AddNew(UndercartMaintainInvoice);
                    return;
                }
                PrepayPayscheduleChildView.ViewModel.InitData(
                    typeof (UndercartMaintainInvoiceDTO),
                    PrepayPayscheduleChildViewClosed);
                PrepayPayscheduleChildView.ShowDialog();
            });
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
        }

        private void PrepayPayscheduleChildViewClosed(object sender, WindowClosedEventArgs e)
        {
            if (PrepayPayscheduleChildView.Tag != null)
            {
                UndercartMaintainInvoice = PrepayPayscheduleChildView.Tag as UndercartMaintainInvoiceDTO;
                UndercartMaintainInvoices.AddNew(UndercartMaintainInvoice);
            }
        }

        #endregion

        #region 删除维修发票

        protected override void OnRemoveInvoice(object obj)
        {
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                UndercartMaintainInvoices.Remove(UndercartMaintainInvoice);
                UndercartMaintainInvoice = UndercartMaintainInvoices.FirstOrDefault();
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
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            UndercartMaintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            UndercartMaintainInvoice.MaintainInvoiceLines.Add(UndercartMaintainInvoiceLine);
        }

        protected override bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除维修发票行

        protected override void OnRemoveInvoiceLine(object obj)
        {
            if (UndercartMaintainInvoiceLine == null)
            {
                MessageAlert("请选择一条维修发票明细！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                UndercartMaintainInvoice.MaintainInvoiceLines.Remove(UndercartMaintainInvoiceLine);
                UndercartMaintainInvoiceLine = UndercartMaintainInvoice.MaintainInvoiceLines.FirstOrDefault();
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
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            UndercartMaintainInvoice.Status = (int) InvoiceStatus.待审核;
        }

        protected override bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核发票

        protected override void OnReviewInvoice(object obj)
        {
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            UndercartMaintainInvoice.Status = (int) InvoiceStatus.已审核;
            UndercartMaintainInvoice.Reviewer = "admin";
            UndercartMaintainInvoice.ReviewDate = DateTime.Now;
            UndercartMaintainInvoice.IsValid = true;
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
                UndercartMaintainInvoice.DocumentId = doc.DocumentId;
                UndercartMaintainInvoice.DocumentName = doc.Name;
            }
        }

        #endregion

        #region GridView单元格变更处理

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnCellEditEnd(object sender)
        {
            UndercartMaintainInvoice.InvoiceValue =
                UndercartMaintainInvoice.MaintainInvoiceLines.Sum(
                    invoiceLine => invoiceLine.Amount*invoiceLine.UnitPrice);
        }

        #endregion

        #endregion
    }
}