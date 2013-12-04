#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/13 16:48:40
// 文件名：ApuMaintainVm
// 版本：V1.0.0
//
// 修改者： 时间： 
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
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof(ApuMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApuMaintainVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private Document.Document _document = new Document.Document();
        [Import]
        public WordViewer WordView;
        [Import]
        public PDFViewer PdfView;

        [ImportingConstructor]
        public ApuMaintainVm(IRegionManager regionManager)
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
            ApuMaintainContracts = Service.CreateCollection(_purchaseData.APUMaintainContracts);
            Service.RegisterCollectionView(ApuMaintainContracts);
            ApuMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = ApuMaintainContracts.CurrentAddItem as APUMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        _document.Id = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectApuMaintain = !ApuMaintainContracts.HasChanges;
                }
            };
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
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
            ApuMaintainContracts.AutoLoad = true;
        }


        #region APU维修合同
        /// <summary>
        /// APU维修合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUMaintainContractDTO> ApuMaintainContracts { get; set; }

        private APUMaintainContractDTO _apuMaintainContract;
        /// <summary>
        /// 选中的APU维修合同
        /// </summary>
        public APUMaintainContractDTO ApuMaintainContract
        {
            get { return _apuMaintainContract; }
            set
            {
                if (_apuMaintainContract != value)
                {
                    _apuMaintainContract = value;
                    if (_apuMaintainContract != null)
                    {
                        _document.Id = _apuMaintainContract.DocumentId;
                        _document.Name = _apuMaintainContract.DocumentName;
                        if (value.Suppliers != null)
                        {
                            _supplier = value.Suppliers.FirstOrDefault(p => p.SupplierId == _apuMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => ApuMaintainContract);
                }
            }
        }

        private bool _canSelectApuMaintain = true;
        //用户能否选择
        public bool CanSelectApuMaintain
        {
            get { return _canSelectApuMaintain; }
            set
            {
                if (_canSelectApuMaintain != value)
                {
                    _canSelectApuMaintain = value;
                    RaisePropertyChanged(() => CanSelectApuMaintain);
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
                    ApuMaintainContract.Signatory = _supplier.Name;
                    RaisePropertyChanged(() => Supplier);
                }
            }
        }
        #endregion

        #endregion

        #endregion

        #region 操作

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            var radRadioButton = sender as RadRadioButton;
            if ((bool)radRadioButton.IsChecked)
            {
                WordView.Tag = null;
                WordView.ViewModel.InitData(false, _document, WordViewerClosed);
                WordView.ShowDialog();
            }
            else
            {
                PdfView.Tag = null;
                PdfView.ViewModel.InitData(false, _document, PdfViewerClosed);
                PdfView.ShowDialog();
            }
        }

        private void WordViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (WordView.Tag != null && WordView.Tag is Document.Document)
            {
                var document = WordView.Tag as Document.Document;
                ApuMaintainContract.DocumentId = document.Id;
                ApuMaintainContract.DocumentName = document.Name;
            }
        }

        private void PdfViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (PdfView.Tag != null && PdfView.Tag is Document.Document)
            {
                var document = PdfView.Tag as Document.Document;
                ApuMaintainContract.DocumentId = document.Id;
                ApuMaintainContract.DocumentName = document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (string.IsNullOrEmpty(_document.Name))
            {
                return;
            }
            if (_document.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                PdfView.Tag = null;
                PdfView.ViewModel.InitData(true, _document, PdfViewerClosed);
                PdfView.ShowDialog();
            }
            else
            {
                WordView.Tag = null;
                WordView.ViewModel.InitData(true, _document, WordViewerClosed);
                WordView.ShowDialog();
            }
        }
        #endregion

        #region 重载操作


        #endregion

        #endregion
    } 
}
