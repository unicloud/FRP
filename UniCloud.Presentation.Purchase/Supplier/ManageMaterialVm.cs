#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/10 14:48:44
// 文件名：ManageMaterialVm
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/10 14:48:44
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    /// <summary>
    /// 基础配置，包括维修工作单元、BFEMaterial的维护
    /// </summary>
    [Export(typeof(ManageMaterialVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageMaterialVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;

        [ImportingConstructor]
        public ManageMaterialVm(IRegionManager regionManager, IPurchaseService service)
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
            //创建并注册CollectionView
            EngineMaterials = _service.CreateCollection(_context.EngineMaterials);
            _service.RegisterCollectionView(EngineMaterials);
            EngineMaterials.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = EngineMaterials.CurrentAddItem as EngineMaterialDTO;
                    if (newItem != null)
                    {
                        newItem.EngineMaterialId = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectEngineMaterial = !EngineMaterials.HasChanges;
                }
            };

            //创建并注册CollectionView
            BFEMaterials = _service.CreateCollection(_context.BFEMaterials);
            _service.RegisterCollectionView(BFEMaterials);
            BFEMaterials.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = BFEMaterials.CurrentAddItem as BFEMaterialDTO;
                    if (newItem != null)
                    {
                        newItem.BFEMaterialId = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectBFEMaterial = !BFEMaterials.HasChanges;
                }
            };

            Manufacturers=new QueryableDataServiceCollectionView<ManufacturerDTO>(_context,_context.Manufacturers);
            Manufacturers.FilterDescriptors.Add(new FilterDescriptor("Type",FilterOperator.IsEqualTo,2));
        }

        #endregion

        #region 数据

        #region 公共属性
        /// <summary>
        ///     制造商集合
        /// </summary>
        public QueryableDataServiceCollectionView<ManufacturerDTO> Manufacturers { get; set; }
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!EngineMaterials.AutoLoad)
                EngineMaterials.AutoLoad = true;
            EngineMaterials.Load(true);

            //// 将CollectionView的AutoLoad属性设为True
            if (!BFEMaterials.AutoLoad)
                BFEMaterials.AutoLoad = true;
            BFEMaterials.Load(true);
        }

        #region EngineMaterial

        private bool _canSelectEngineMaterial = true;
        private EngineMaterialDTO _EngineMaterial;

        /// <summary>
        ///     EngineMaterial集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineMaterialDTO> EngineMaterials { get; set; }

        /// <summary>
        ///     选中的EngineMaterial
        /// </summary>
        public EngineMaterialDTO EngineMaterial
        {
            get { return _EngineMaterial; }
            set
            {
                if (value != null && _EngineMaterial != value)
                {
                    _EngineMaterial = value;
                    RaisePropertyChanged(() => EngineMaterial);
                }
            }
        }

        //用户能否选择
        public bool CanSelectEngineMaterial
        {
            get { return _canSelectEngineMaterial; }
            set
            {
                if (_canSelectEngineMaterial != value)
                {
                    _canSelectEngineMaterial = value;
                    RaisePropertyChanged(() => CanSelectEngineMaterial);
                }
            }
        }

        #endregion

        #region BFEMaterial

        private bool _canSelectBFEMaterial = true;
        private BFEMaterialDTO _BFEMaterial;

        /// <summary>
        ///     BFEMaterial集合
        /// </summary>
        public QueryableDataServiceCollectionView<BFEMaterialDTO> BFEMaterials { get; set; }

        /// <summary>
        ///     选中的BFEMaterial
        /// </summary>
        public BFEMaterialDTO BFEMaterial
        {
            get { return _BFEMaterial; }
            set
            {
                if (value != null && _BFEMaterial != value)
                {
                    _BFEMaterial = value;
                    RaisePropertyChanged(() => BFEMaterial);
                }
            }
        }

        //用户能否选择
        public bool CanSelectBFEMaterial
        {
            get { return _canSelectBFEMaterial; }
            set
            {
                if (_canSelectBFEMaterial != value)
                {
                    _canSelectBFEMaterial = value;
                    RaisePropertyChanged(() => CanSelectBFEMaterial);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion
        #endregion
    }
}
