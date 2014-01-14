#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/14 15:38:56
// 文件名：AirframeMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/14 15:38:56
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
    [Export(typeof(AirframeMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AirframeMaintainVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;
        private readonly DocumentDTO _document = new DocumentDTO();

        [ImportingConstructor]
        public AirframeMaintainVm(IRegionManager regionManager, IPurchaseService service)
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
            AirframeMaintainContracts = _service.CreateCollection(_context.AirframeMaintainContracts);
            _service.RegisterCollectionView(AirframeMaintainContracts);
            AirframeMaintainContracts.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = AirframeMaintainContracts.CurrentAddItem as AirframeMaintainContractDTO;
                    if (newItem != null)
                    {
                        newItem.AirframeMaintainContractId = RandomHelper.Next();
                        newItem.SignDate = DateTime.Now;
                        newItem.CreateDate = DateTime.Now;
                        DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectAirframeMaintain = !AirframeMaintainContracts.HasChanges;
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
            if (AirframeMaintainContracts.AutoLoad)
                AirframeMaintainContracts.AutoLoad = true;
            AirframeMaintainContracts.Load(true);
            Suppliers.Load(true);
        }

        #region APU维修合同

        private AirframeMaintainContractDTO _airframeMaintainContract;

        private bool _canSelectAirframeMaintain = true;

        /// <summary>
        ///     APU维修合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirframeMaintainContractDTO> AirframeMaintainContracts { get; set; }

        /// <summary>
        ///     选中的APU维修合同
        /// </summary>
        public AirframeMaintainContractDTO AirframeMaintainContract
        {
            get { return _airframeMaintainContract; }
            set
            {
                if (_airframeMaintainContract != value)
                {
                    _airframeMaintainContract = value;
                    if (_airframeMaintainContract != null)
                    {
                        _document.DocumentId = _airframeMaintainContract.DocumentId;
                        _document.Name = _airframeMaintainContract.DocumentName;
                        DocumentName = _airframeMaintainContract.DocumentName;
                        if (string.IsNullOrEmpty(DocumentName))
                        {
                            DocumentName = "添加附件";
                        }
                        if (Suppliers != null)
                        {
                            _supplier = Suppliers.FirstOrDefault(p => p.SupplierId == _airframeMaintainContract.SignatoryId);
                        }
                    }
                    RaisePropertyChanged(() => AirframeMaintainContract);
                }
            }
        }

        //用户能否选择
        public bool CanSelectAirframeMaintain
        {
            get { return _canSelectAirframeMaintain; }
            set
            {
                if (_canSelectAirframeMaintain != value)
                {
                    _canSelectAirframeMaintain = value;
                    RaisePropertyChanged(() => CanSelectAirframeMaintain);
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
                    AirframeMaintainContract.Signatory = _supplier.Name;
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
                AirframeMaintainContract.DocumentId = doc.DocumentId;
                AirframeMaintainContract.DocumentName = doc.Name;
                DocumentName = doc.Name;
            }
        }

        #endregion

        #region 重载操作

        #endregion

        #endregion
    }
}
