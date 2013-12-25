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
    [Export(typeof(UndercartMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UndercartMaintainVm: InvoiceVm
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public UndercartMaintainVm(IRegionManager regionManager)
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
            UndercartMaintainInvoices = Service.CreateCollection(PaymentDataService.UndercartMaintainInvoices);
            Service.RegisterCollectionView(UndercartMaintainInvoices);
            UndercartMaintainInvoices.PropertyChanged += OnViewPropertyChanged;
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
            UndercartMaintainInvoices.AutoLoad = true;
            UndercartMaintainInvoices.Load(true);
            Suppliers.Load(true);
            Currencies.Load(true);
        }

        #region 起落架维修发票
        /// <summary>
        /// 起落架维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO> UndercartMaintainInvoices { get; set; }

        private UndercartMaintainInvoiceDTO _undercartMaintainInvoice;
        /// <summary>
        /// 选中的起落架维修发票
        /// </summary>
        public UndercartMaintainInvoiceDTO UndercartMaintainInvoice
        {
            get { return _undercartMaintainInvoice; }
            set
            {
                if (_undercartMaintainInvoice != value)
                {
                    _undercartMaintainInvoice = value;
                    RaisePropertyChanged(() => UndercartMaintainInvoice);
                }
            }
        }

        private MaintainInvoiceLineDTO _undercartMaintainInvoiceLine;
        /// <summary>
        /// 选中的APU维修发票
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
            var maintainInvoice = new UndercartMaintainInvoiceDTO
                                  {
                                      UndercartMaintainInvoiceId = RandomHelper.Next(),
                                      CreateDate = DateTime.Now,
                                      InvoiceDate = DateTime.Now
                                  };
            UndercartMaintainInvoices.AddNew(maintainInvoice);
        }

        protected override bool CanAddInvoice(object obj)
        {
            return true;
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
                                                UndercartMaintainInvoices.Remove(_undercartMaintainInvoice);
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
            var maintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
            };

            UndercartMaintainInvoice.MaintainInvoiceLines.Add(maintainInvoiceLine);
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
            UndercartMaintainInvoice.Status = (int)InvoiceStatus.待审核;
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
            UndercartMaintainInvoice.Status = (int)InvoiceStatus.已审核;
            UndercartMaintainInvoice.Reviewer = "admin";
            UndercartMaintainInvoice.ReviewDate = DateTime.Now;
            UndercartMaintainInvoice.IsValid = true;
        }

        protected override bool CanReviewInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(false, UndercartMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var document = DocumentView.Tag as DocumentDTO;
                UndercartMaintainInvoice.DocumentId = document.DocumentId;
                UndercartMaintainInvoice.DocumentName = document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (UndercartMaintainInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, UndercartMaintainInvoice.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }
        #endregion

        #region Combobox SelectedChanged

        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is SupplierDTO)
            {
                UndercartMaintainInvoice.SupplierName = (comboboxSelectedItem as SupplierDTO).Name;
            }
        }
        #endregion
        #endregion
    }
}
