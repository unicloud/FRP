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
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(APUMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class APUMaintainVm : InvoiceVm
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public APUMaintainVm(IRegionManager regionManager)
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
            ApuMaintainInvoices = Service.CreateCollection(PaymentDataService.APUMaintainInvoices);
            Service.RegisterCollectionView(ApuMaintainInvoices);
            ApuMaintainInvoices.PropertyChanged += OnViewPropertyChanged;
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
            ApuMaintainInvoices.AutoLoad = true;
            ApuMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
        }

        #region APU维修发票
        /// <summary>
        /// APU维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUMaintainInvoiceDTO> ApuMaintainInvoices { get; set; }

        private APUMaintainInvoiceDTO _apuMaintainInvoice;
        /// <summary>
        /// 选中的APU维修发票
        /// </summary>
        public APUMaintainInvoiceDTO ApuMaintainInvoice
        {
            get { return _apuMaintainInvoice; }
            set
            {
                if (_apuMaintainInvoice != value)
                {
                    _apuMaintainInvoice = value;
                    RaisePropertyChanged(() => ApuMaintainInvoice);
                }
            }
        }

        private MaintainInvoiceLineDTO _apuMaintainInvoiceLine;
        /// <summary>
        /// 选中的APU维修发票
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

        #region 签约对象
        private SupplierDTO _supplier;
        /// <summary>
        /// 选中的签约对象
        /// </summary>
        public SupplierDTO Supplier
        {
            get { return _supplier; }
            set
            {
                if (value != null && _supplier != value)
                {
                    _supplier = value;
                    ApuMaintainInvoice.SupplierName = _supplier.Name;
                    RaisePropertyChanged(() => Supplier);
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
            var maintainInvoice = new APUMaintainInvoiceDTO
                                  {
                                      APUMaintainInvoiceId = RandomHelper.Next(),
                                      CreateDate = DateTime.Now,
                                      InvoiceDate = DateTime.Now
                                  };
            ApuMaintainInvoices.AddNew(maintainInvoice);
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
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

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(false, ApuMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var document = DocumentView.Tag as DocumentDTO;
                ApuMaintainInvoice.DocumentId = document.DocumentId;
                ApuMaintainInvoice.DocumentName = document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (ApuMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, ApuMaintainInvoice.DocumentId, null);
            DocumentView.ShowDialog();
        }
        #endregion
        #endregion
    }
}
