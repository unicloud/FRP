#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 15:22:36
// 文件名：ManagerLicenseTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 15:22:36
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
    [Export(typeof (ManagerLicenseTypeVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManagerLicenseTypeVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly AircraftConfigData _context;
        private readonly IAircraftConfigService _service;

        [ImportingConstructor]
        public ManagerLicenseTypeVm(IAircraftConfigService service)
            : base(service)
        {
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
            LicenseTypes = _service.CreateCollection(_context.LicenseTypes);
            LicenseTypes.PageSize = 20;
            _service.RegisterCollectionView(LicenseTypes);
            LicenseTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = LicenseTypes.CurrentAddItem as LicenseTypeDTO;
                    if (newItem != null)
                    {
                        newItem.LicenseTypeId = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectLicenseType = !LicenseTypes.HasChanges;
                }
            };
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!LicenseTypes.AutoLoad)
                LicenseTypes.AutoLoad = true;
            LicenseTypes.Load(true);
        }

        #region 证照类型

        private bool _canSelectLicenseType = true;

        /// <summary>
        ///     选中的证照类型
        /// </summary>
        private LicenseTypeDTO _licenseType;

        /// <summary>
        ///     证照类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<LicenseTypeDTO> LicenseTypes { get; set; }

        public LicenseTypeDTO LicenseType
        {
            get { return _licenseType; }
            set
            {
                if (_licenseType != value)
                {
                    _licenseType = value;
                    RaisePropertyChanged(() => LicenseType);
                }
            }
        }

        //用户能否选择

        public bool CanSelectLicenseType
        {
            get { return _canSelectLicenseType; }
            set
            {
                if (_canSelectLicenseType != value)
                {
                    _canSelectLicenseType = value;
                    RaisePropertyChanged(() => CanSelectLicenseType);
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