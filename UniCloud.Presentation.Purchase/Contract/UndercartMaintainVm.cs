<<<<<<< HEAD
﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/16 14:18:20
// 文件名：UndercartMaintain
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
    [Export(typeof(UndercartMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UndercartMaintainVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private DocumentDTO _document = new DocumentDTO();
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
            UndercartMaintainContracts = Service.CreateCollection(_purchaseData.UndercartMaintainContracts);
            Service.RegisterCollectionView(UndercartMaintainContracts);
            UndercartMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = UndercartMaintainContracts.CurrentAddItem as UndercartMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.UndercartMaintainContractId = RandomHelper.Next();
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        newItem.DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectUndercartMaintain = !UndercartMaintainContracts.HasChanges;
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
            UndercartMaintainContracts.AutoLoad = true;
            UndercartMaintainContracts.Load(true);
            Suppliers.Load(true);
        }

        #region 起落架维修合同

        private UndercartMaintainContractDTO _undercartMaintainContract;

        /// <summary>
        ///     起落架维修合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainContractDTO> UndercartMaintainContracts { get; set; }

        /// <summary>
        ///     选中的起落架维修合同
        /// </summary>
        public UndercartMaintainContractDTO UndercartMaintainContract
        {
            get { return _undercartMaintainContract; }
            set
            {
                if (_undercartMaintainContract != value)
                {
                    _undercartMaintainContract = value;
                    if (_undercartMaintainContract != null)
                    {
                        _document.DocumentId = _undercartMaintainContract.DocumentId;
                        _document.Name = _undercartMaintainContract.DocumentName;
                        if (Suppliers != null)
                        {
                            _supplier = Suppliers.FirstOrDefault(p => p.SupplierId == _undercartMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => UndercartMaintainContract);
                }
            }
        }

        private bool _canSelectUndercartMaintain = true;
        //用户能否选择
        public bool CanSelectUndercartMaintain
        {
            get { return _canSelectUndercartMaintain; }
            set
            {
                if (_canSelectUndercartMaintain != value)
                {
                    _canSelectUndercartMaintain = value;
                    RaisePropertyChanged(() => CanSelectUndercartMaintain);
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
                    UndercartMaintainContract.Signatory = _supplier.Name;
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
                UndercartMaintainContract.DocumentId = _document.DocumentId;
                UndercartMaintainContract.DocumentName = _document.Name;
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
=======
﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/16 14:18:20
// 文件名：UndercartMaintain
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
    [Export(typeof(UndercartMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UndercartMaintainVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private DocumentDTO _document = new DocumentDTO();
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
            UndercartMaintainContracts = Service.CreateCollection(_purchaseData.UndercartMaintainContracts);
            Service.RegisterCollectionView(UndercartMaintainContracts);
            UndercartMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = UndercartMaintainContracts.CurrentAddItem as UndercartMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.UndercartMaintainContractId = RandomHelper.Next();
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        newItem.DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectUndercartMaintain = !UndercartMaintainContracts.HasChanges;
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
            UndercartMaintainContracts.AutoLoad = true;
            Suppliers.Load(true);
        }

        #region 起落架维修合同

        private UndercartMaintainContractDTO _undercartMaintainContract;

        /// <summary>
        ///     起落架维修合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainContractDTO> UndercartMaintainContracts { get; set; }

        /// <summary>
        ///     选中的起落架维修合同
        /// </summary>
        public UndercartMaintainContractDTO UndercartMaintainContract
        {
            get { return _undercartMaintainContract; }
            set
            {
                if (_undercartMaintainContract != value)
                {
                    _undercartMaintainContract = value;
                    if (_undercartMaintainContract != null)
                    {
                        _document.DocumentId = _undercartMaintainContract.DocumentId;
                        _document.Name = _undercartMaintainContract.DocumentName;
                        if (Suppliers != null)
                        {
                            _supplier = Suppliers.FirstOrDefault(p => p.SupplierId == _undercartMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => UndercartMaintainContract);
                }
            }
        }

        private bool _canSelectUndercartMaintain = true;
        //用户能否选择
        public bool CanSelectUndercartMaintain
        {
            get { return _canSelectUndercartMaintain; }
            set
            {
                if (_canSelectUndercartMaintain != value)
                {
                    _canSelectUndercartMaintain = value;
                    RaisePropertyChanged(() => CanSelectUndercartMaintain);
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
                    UndercartMaintainContract.Signatory = _supplier.Name;
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
                UndercartMaintainContract.DocumentId = _document.DocumentId;
                UndercartMaintainContract.DocumentName = _document.Name;
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
>>>>>>> b8b0c62022ad208abfc4ee01706532d59cf9eb40
}