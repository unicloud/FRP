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
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
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

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;
        private readonly DocumentDTO _document = new DocumentDTO();

        [ImportingConstructor]
        public EngineMaintainVm(IRegionManager regionManager, IPurchaseService service)
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
            EngineMaintainContracts = _service.CreateCollection(_context.EngineMaintainContracts);
            _service.RegisterCollectionView(EngineMaintainContracts);
            EngineMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = EngineMaintainContracts.CurrentAddItem as EngineMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.EngineMaintainContractId = RandomHelper.Next();
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        var firstOrDefault = Suppliers.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            newItem.SignatoryId = firstOrDefault.SupplierId;
                            newItem.Signatory = firstOrDefault.Name;
                        }
                        DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectEngineMaintain = !EngineMaintainContracts.HasChanges;
                }
            };

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_context, _context.Suppliers);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     文档名称
        /// </summary>
        private string _documentName;

        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        public string DocumentName
        {
            get { return _documentName; }
            set
            {
                _documentName = value;
                RaisePropertyChanged("DocumentName");
            }
        }

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
            if (!EngineMaintainContracts.AutoLoad)
                EngineMaintainContracts.AutoLoad = true;
            EngineMaintainContracts.Load(true);
            Suppliers.Load(true);
        }

        #region 发动机维修合同

        private bool _canSelectEngineMaintain = true;
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
                        DocumentName = _engineMaintainContract.DocumentName;
                        if (string.IsNullOrEmpty(DocumentName))
                        {
                            DocumentName = "添加附件";
                        }
                        if (Suppliers != null)
                        {
                            _supplier =
                                Suppliers.FirstOrDefault(p => p.SupplierId == _engineMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => EngineMaintainContract);
                }
            }
        }

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
                EngineMaintainContract.DocumentId = doc.DocumentId;
                EngineMaintainContract.DocumentName = doc.Name;
                DocumentName = doc.Name;
            }
        }

        #endregion

        #region 重载操作

        #endregion

        #endregion
    }
}