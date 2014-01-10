#region Version Info

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
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof (AirframeMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AirframeMaintainVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPaymentService _service;
        [Import] public DocumentViewer DocumentView;

        [ImportingConstructor]
        public AirframeMaintainVm(IRegionManager regionManager, IPaymentService service) : base(service)
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
            AirframeMaintainInvoices = _service.CreateCollection(_context.AirframeMaintainInvoices,o=>o.MaintainInvoiceLines);
            _service.RegisterCollectionView(AirframeMaintainInvoices);
            //ApuMaintainInvoices.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == "HasChanges")
            //    {
            //        CanSelectApuMaintain = !ApuMaintainInvoices.HasChanges;
            //    }
            //};
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
            AirframeMaintainInvoices.AutoLoad = true;
            AirframeMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
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
                if (_airframeMaintainInvoice != value)
                {
                    _airframeMaintainInvoice = value;
                    RaisePropertyChanged(() => AirframeMaintainInvoice);
                }
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
            var maintainInvoice = new AirframeMaintainInvoiceDTO
            {
                AirframeMaintainInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now
            };
            AirframeMaintainInvoices.AddNew(maintainInvoice);
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
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
            var maintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            AirframeMaintainInvoice.MaintainInvoiceLines.Add(maintainInvoiceLine);
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

        #region 添加附件

        protected override void OnAddAttach(object sender)
        {
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(false, AirframeMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var document = DocumentView.Tag as DocumentDTO;
                AirframeMaintainInvoice.DocumentId = document.DocumentId;
                AirframeMaintainInvoice.DocumentName = document.Name;
            }
        }

        #endregion

        #region 查看附件

        protected override void OnViewAttach(object sender)
        {
            if (AirframeMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, AirframeMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        #endregion

        #region Combobox SelectedChanged

        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is SupplierDTO)
            {
                AirframeMaintainInvoice.SupplierName = (comboboxSelectedItem as SupplierDTO).Name;
            }
        }

        #endregion

        #endregion
    }
}