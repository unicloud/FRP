﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:50:09
// 文件名：FuselageMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:50:09
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
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof (AirframeMaintainVm))]
    public class AirframeMaintainVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public AirframeMaintainVm(IPaymentService service)
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
            AirframeMaintainInvoices = _service.CreateCollection(_context.AirframeMaintainInvoices,
                o => o.MaintainInvoiceLines);
            AirframeMaintainInvoices.PageSize = 6;
            AirframeMaintainInvoices.LoadedData += (o, e) =>
            {
                if (AirframeMaintainInvoice == null)
                    AirframeMaintainInvoice = AirframeMaintainInvoices.FirstOrDefault();
            };
            var supplierFilter = new FilterDescriptor("MaintainSupplier", FilterOperator.IsEqualTo, true);
            Suppliers.FilterDescriptors.Add(supplierFilter);
            _service.RegisterCollectionView(AirframeMaintainInvoices);
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
            if (!AirframeMaintainInvoices.AutoLoad)
                AirframeMaintainInvoices.AutoLoad = true;
            AirframeMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
            PaymentSchedules.Load(true);
        }

        #region 机身维修发票

        private AirframeMaintainInvoiceDTO _airframeMaintainInvoice;

        private MaintainInvoiceLineDTO _airframeMaintainInvoiceLine;

        /// <summary>
        ///     机身维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirframeMaintainInvoiceDTO> AirframeMaintainInvoices { get; set; }

        /// <summary>
        ///     选中的机身维修发票
        /// </summary>
        public AirframeMaintainInvoiceDTO AirframeMaintainInvoice
        {
            get { return _airframeMaintainInvoice; }
            set
            {
                _airframeMaintainInvoice = value;
                RelatedPaymentSchedule.Clear();
                SelPaymentSchedule = null;
                if (value != null)
                {
                    AirframeMaintainInvoiceLine = value.MaintainInvoiceLines.FirstOrDefault();
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
                                l => l.InvoiceId == value.AirframeMaintainInvoiceId);
                }
                RaisePropertyChanged(() => AirframeMaintainInvoice);
            }
        }

        /// <summary>
        ///     选中的APU维修发票
        /// </summary>
        public MaintainInvoiceLineDTO AirframeMaintainInvoiceLine
        {
            get { return _airframeMaintainInvoiceLine; }
            set
            {
                if (_airframeMaintainInvoiceLine != value)
                {
                    _airframeMaintainInvoiceLine = value;
                    RaisePropertyChanged(() => AirframeMaintainInvoiceLine);
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
                    AirframeMaintainInvoice = new AirframeMaintainInvoiceDTO
                    {
                        AirframeMaintainInvoiceId = RandomHelper.Next(),
                        CreateDate = DateTime.Now,
                        InvoiceDate = DateTime.Now,
                        InMaintainTime = DateTime.Now,
                        OutMaintainTime = DateTime.Now,
                        OperatorName = StatusData.curUser
                    };
                    var currency = Currencies.FirstOrDefault();
                    if (currency != null)
                        AirframeMaintainInvoice.CurrencyId = currency.Id;
                    var supplier = Suppliers.FirstOrDefault();
                    if (supplier != null)
                    {
                        AirframeMaintainInvoice.SupplierId = supplier.SupplierId;
                        AirframeMaintainInvoice.SupplierName = supplier.Name;
                    }
                    AirframeMaintainInvoices.AddNew(AirframeMaintainInvoice);
                    return;
                }
                PrepayPayscheduleChildView.ViewModel.InitData(
                    typeof (AirframeMaintainInvoiceDTO),
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
                AirframeMaintainInvoice = PrepayPayscheduleChildView.Tag as AirframeMaintainInvoiceDTO;
                AirframeMaintainInvoices.AddNew(AirframeMaintainInvoice);
            }
        }

        #endregion

        #region 删除维修发票

        protected override void OnRemoveInvoice(object obj)
        {
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AirframeMaintainInvoices.Remove(_airframeMaintainInvoice);
                AirframeMaintainInvoice = AirframeMaintainInvoices.FirstOrDefault();
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
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            AirframeMaintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            AirframeMaintainInvoice.MaintainInvoiceLines.Add(AirframeMaintainInvoiceLine);
        }

        protected override bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除维修发票行

        protected override void OnRemoveInvoiceLine(object obj)
        {
            if (AirframeMaintainInvoiceLine == null)
            {
                MessageAlert("请选择一条维修发票明细！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AirframeMaintainInvoice.MaintainInvoiceLines.Remove(AirframeMaintainInvoiceLine);
                AirframeMaintainInvoiceLine = AirframeMaintainInvoice.MaintainInvoiceLines.FirstOrDefault();
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
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            AirframeMaintainInvoice.Status = (int) InvoiceStatus.待审核;
        }

        protected override bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核发票

        protected override void OnReviewInvoice(object obj)
        {
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            AirframeMaintainInvoice.Status = (int) InvoiceStatus.已审核;
            AirframeMaintainInvoice.Reviewer = "admin";
            AirframeMaintainInvoice.ReviewDate = DateTime.Now;
            AirframeMaintainInvoice.IsValid = true;
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
                AirframeMaintainInvoice.DocumentId = doc.DocumentId;
                AirframeMaintainInvoice.DocumentName = doc.Name;
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
            AirframeMaintainInvoice.InvoiceValue =
                AirframeMaintainInvoice.MaintainInvoiceLines.Sum(invoiceLine => invoiceLine.Amount*invoiceLine.UnitPrice);
        }

        #endregion

        #endregion
    }
}