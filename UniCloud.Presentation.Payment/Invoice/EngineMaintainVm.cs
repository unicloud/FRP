#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:44:59
// 文件名：EngineMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:44:59
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
    [Export(typeof(EngineMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineMaintainVm: InvoiceVm
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public EngineMaintainVm(IRegionManager regionManager)
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
            EngineMaintainInvoices = Service.CreateCollection(PaymentDataService.EngineMaintainInvoices);
            Service.RegisterCollectionView(EngineMaintainInvoices);
            EngineMaintainInvoices.PropertyChanged += OnViewPropertyChanged;
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
            EngineMaintainInvoices.AutoLoad = true;
            EngineMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
        }

        #region 发动机维修发票
        /// <summary>
        /// 发动机维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO> EngineMaintainInvoices { get; set; }

        private EngineMaintainInvoiceDTO _engineMaintainInvoice;
        /// <summary>
        /// 选中的发动机维修发票
        /// </summary>
        public EngineMaintainInvoiceDTO EngineMaintainInvoice
        {
            get { return _engineMaintainInvoice; }
            set
            {
                if (_engineMaintainInvoice != value)
                {
                    _engineMaintainInvoice = value;
                    RaisePropertyChanged(() => EngineMaintainInvoice);
                }
            }
        }

        private MaintainInvoiceLineDTO _engineMaintainInvoiceLine;
        /// <summary>
        /// 选中的APU维修发票
        /// </summary>
        public MaintainInvoiceLineDTO EngineMaintainInvoiceLine
        {
            get { return _engineMaintainInvoiceLine; }
            set
            {
                if (_engineMaintainInvoiceLine != value)
                {
                    _engineMaintainInvoiceLine = value;
                    RaisePropertyChanged(() => EngineMaintainInvoiceLine);
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
                    EngineMaintainInvoice.SupplierName = _supplier.Name;
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
            var maintainInvoice = new EngineMaintainInvoiceDTO
                                  {
                                      EngineMaintainInvoiceId = RandomHelper.Next(),
                                      CreateDate = DateTime.Now,
                                      InvoiceDate = DateTime.Now
                                  };
            EngineMaintainInvoices.AddNew(maintainInvoice);
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 删除维修发票
        protected override void OnRemoveInvoice(object obj)
        {
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                EngineMaintainInvoices.Remove(_engineMaintainInvoice);
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
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            var maintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            EngineMaintainInvoice.MaintainInvoiceLines.Add(maintainInvoiceLine);
        }

        protected override bool CanAddInvoiceLine(object obj)
        {
            return true;
        }
        #endregion

        #region 移除维修发票行
        protected override void OnRemoveInvoiceLine(object obj)
        {
            if (EngineMaintainInvoiceLine == null)
            {
                MessageAlert("请选择一条维修发票明细！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                EngineMaintainInvoice.MaintainInvoiceLines.Remove(EngineMaintainInvoiceLine);
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
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            EngineMaintainInvoice.Status = (int)InvoiceStatus.待审核;
        }

        protected override bool CanSubmitInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 审核发票
        protected override void OnReviewInvoice(object obj)
        {
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条维修发票记录！");
                return;
            }
            EngineMaintainInvoice.Status = (int)InvoiceStatus.已审核;
            EngineMaintainInvoice.Reviewer = "admin";
            EngineMaintainInvoice.ReviewDate = DateTime.Now;
            EngineMaintainInvoice.IsValid = true;
        }

        protected override bool CanReviewInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(false, EngineMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var document = DocumentView.Tag as DocumentDTO;
                EngineMaintainInvoice.DocumentId = document.DocumentId;
                EngineMaintainInvoice.DocumentName = document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (EngineMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, EngineMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }
        #endregion
        #endregion
    }
}
