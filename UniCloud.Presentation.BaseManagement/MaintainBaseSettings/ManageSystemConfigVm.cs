#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:52:15
// 文件名：ManageSystemConfigVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:52:15
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof(ManageSystemConfigVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageSystemConfigVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageSystemConfigVm(IRegionManager regionManager, IBaseManagementService service)
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
            AircraftCabinTypes = _service.CreateCollection(_context.AircraftCabinTypes);
            _service.RegisterCollectionView(AircraftCabinTypes);
            AircraftCabinTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "HasChanges")
                {
                    CanSelectAircraftCabinType = !AircraftCabinTypes.HasChanges;
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
            // 将CollectionView的AutoLoad属性设为True
            if (!AircraftCabinTypes.AutoLoad)
                AircraftCabinTypes.AutoLoad = true;
            AircraftCabinTypes.Load(true);
        }

        #region 飞机舱位类型
        /// <summary>
        ///     飞机舱位类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCabinTypeDTO> AircraftCabinTypes { get; set; }

        /// <summary>
        ///     选中的飞机舱位类型
        /// </summary>
        private AircraftCabinTypeDTO _aircraftCabinType;
        public AircraftCabinTypeDTO AircraftCabinType
        {
            get { return _aircraftCabinType; }
            set
            {
                if (_aircraftCabinType != value)
                {
                    _aircraftCabinType = value;
                    RaisePropertyChanged(() => AircraftCabinType);
                }
            }
        }

        //用户能否选择
        private bool _canSelectAircraftCabinType = true;
        public bool CanSelectAircraftCabinType
        {
            get { return _canSelectAircraftCabinType; }
            set
            {
                if (_canSelectAircraftCabinType != value)
                {
                    _canSelectAircraftCabinType = value;
                    RaisePropertyChanged(() => CanSelectAircraftCabinType);
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
