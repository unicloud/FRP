#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 16:14:05
// 文件名：ManagerAircraftLicense
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 16:14:05
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
    [Export(typeof(ManagerAircraftLicenseVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerAircraftLicenseVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly AircraftConfigData _context;
        private readonly IRegionManager _regionManager;
        private readonly IAircraftConfigService _service;

        [ImportingConstructor]
        public ManagerAircraftLicenseVm(IRegionManager regionManager, IAircraftConfigService service)
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
            Aircrafts = _service.CreateCollection(_context.Aircrafts);
            _service.RegisterCollectionView(Aircrafts);
            LicenseTypes = new QueryableDataServiceCollectionView<LicenseTypeDTO>(_context, _context.LicenseTypes);
        }

        #endregion

        #region 数据

        #region 公共属性
        public QueryableDataServiceCollectionView<LicenseTypeDTO> LicenseTypes { get; set; }
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
            if (!Aircrafts.AutoLoad)
                Aircrafts.AutoLoad = true;
            if (!LicenseTypes.AutoLoad)
                LicenseTypes.AutoLoad = true;
        }

        #region 飞机

        /// <summary>
        ///     飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

        /// <summary>
        ///     选中的飞机
        /// </summary>
        private AircraftDTO _aircraft;
        public AircraftDTO Aircraft
        {
            get { return _aircraft; }
            set
            {
                if (_aircraft != value)
                {
                    _aircraft = value;
                    RaisePropertyChanged(() => Aircraft);
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
