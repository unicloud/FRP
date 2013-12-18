#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/16 13:56:08
// 文件名：EngineMaintainVm
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
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof(EngineMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineMaintainVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private DocumentDTO _document = new DocumentDTO();
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
            EngineMaintainContracts = Service.CreateCollection(_purchaseData.EngineMaintainContracts);
            Service.RegisterCollectionView(EngineMaintainContracts);
            EngineMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = EngineMaintainContracts.CurrentAddItem as EngineMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.EngineMaintainContractId = RandomHelper.Next();
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        newItem.DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectEngineMaintain = !EngineMaintainContracts.HasChanges;
                }
            };

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_purchaseData, _purchaseData.Suppliers);
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
        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
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
            EngineMaintainContracts.AutoLoad = true;
            EngineMaintainContracts.Load(true);
            Suppliers.Load(true);
        }

        #region 发动机维修合同

        private EngineMaintainContractDTO _engineMaintainContract;

        /// <summary>
        ///     发动机维修合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineMaintainContractDTO> EngineMaintainContracts { get; set; }

        /// <summary>
        ///     选中的发动机维修合同
        /// </summary>
        public EngineMaintainContractDTO EngineMaintainContract
        {
            get { return _engineMaintainContract; }
            set
            {
                if (_engineMaintainContract != value)
                {
                    _engineMaintainContract = value;
                    if (_engineMaintainContract != null)
                    {
                        _document.DocumentId = _engineMaintainContract.DocumentId;
                        _document.Name = _engineMaintainContract.DocumentName;
                        if (Suppliers != null)
                        {
                            _supplier = Suppliers.FirstOrDefault(p => p.SupplierId == _engineMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => EngineMaintainContract);
                }
            }
        }

        private bool _canSelectEngineMaintain = true;
        //用户能否选择
        public bool CanSelectEngineMaintain
        {
            get { return _canSelectEngineMaintain; }
            set
            {
                if (_canSelectEngineMaintain != value)
                {
                    _canSelectEngineMaintain = value;
                    RaisePropertyChanged(() => CanSelectEngineMaintain);
                }
            }
        }
        #endregion

        #region 签约对象
        private SupplierDTO _supplier;
        /// <summary>
        ///     选中的签约对象
        /// </summary>
        public SupplierDTO Supplier
        {
            get { return _supplier; }
            set
            {
                if (value != null && _supplier != value)
                {
                    _supplier = value;
                    EngineMaintainContract.Signatory = _supplier.Name;
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
            DocumentView.ViewModel.InitData(false, _document.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
            }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                _document = DocumentView.Tag as DocumentDTO;
                EngineMaintainContract.DocumentId = _document.DocumentId;
                EngineMaintainContract.DocumentName = _document.Name;
            }
        }

        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            DocumentView.ViewModel.InitData(true, _document.DocumentId, null);
            DocumentView.ShowDialog();
        }
        #endregion

        #region 重载操作


        #endregion

        #endregion
    }
}