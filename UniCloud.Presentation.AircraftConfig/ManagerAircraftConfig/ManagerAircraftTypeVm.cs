#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 17:32:57
// 文件名：ManagerAircraftTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 17:32:57
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export(typeof(ManagerAircraftTypeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerAircraftTypeVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly AircraftConfigData _context;
        private readonly IRegionManager _regionManager;
        private readonly IAircraftConfigService _service;

        [ImportingConstructor]
        public ManagerAircraftTypeVm(IRegionManager regionManager, IAircraftConfigService service)
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
            AircraftTypes = _service.CreateCollection(_context.AircraftTypes);
            _service.RegisterCollectionView(AircraftTypes);
            AircraftTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = AircraftTypes.CurrentAddItem as AircraftTypeDTO;
                    if (newItem != null)
                    {
                        newItem.AircraftTypeId = Guid.NewGuid();
                       var  caacAircraftType = CaacAircraftTypes.FirstOrDefault();
                        if (caacAircraftType != null) 
                            newItem.CaacAircraftTypeId = caacAircraftType.CAACAircraftTypeId;
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectAircraftType = !AircraftTypes.HasChanges;
                }
            };
            AircraftSerieses = _service.CreateCollection(_context.AircraftSeries);
        }

        #endregion

        #region 数据

        #region 公共属性
        /// <summary>
        /// 座级
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> AircraftCategories { get; set; }
        /// <summary>
        /// 制造商
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
            if (!AircraftTypes.AutoLoad)
                AircraftTypes.AutoLoad = true;
            AircraftTypes.Load(true);
            AircraftSerieses.AutoLoad = true;
            CaacAircraftTypes = _service.GetCAACAircraftTypes(() => RaisePropertyChanged(() => CaacAircraftTypes));
            Manufacturers = _service.GetManufacturers(() => RaisePropertyChanged(() => Manufacturers));
            AircraftCategories = _service.GetAircraftCategories(() => RaisePropertyChanged(() => AircraftCategories));
        }

        #region 机型

        private AircraftTypeDTO _aircraftType;

        private bool _canSelectAircraftType = true;

        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        /// <summary>
        ///     选中的机型
        /// </summary>
        public AircraftTypeDTO AircraftType
        {
            get { return _aircraftType; }
            set
            {
                if (value != null && _aircraftType != value)
                {
                    _aircraftType = value;
                    if (AircraftSerieses != null)
                    {
                        _aircraftSeries = AircraftSerieses.FirstOrDefault(p => p.ManufacturerId == _aircraftType.ManufacturerId);
                    }
                    RaisePropertyChanged(() => AircraftType);
                }
            }
        }

        //用户能否选择
        public bool CanSelectAircraftType
        {
            get { return _canSelectAircraftType; }
            set
            {
                if (_canSelectAircraftType != value)
                {
                    _canSelectAircraftType = value;
                    RaisePropertyChanged(() => CanSelectAircraftType);
                }
            }
        }

        #endregion

        #region 民航机型
        /// <summary>
        /// 民航机型
        /// </summary>
        public QueryableDataServiceCollectionView<CAACAircraftTypeDTO> CaacAircraftTypes { get; set; }

        private CAACAircraftTypeDTO _caacAircraftType;
        public CAACAircraftTypeDTO CaacAircraftType
        {
            get { return _caacAircraftType; }
            set
            {
                _caacAircraftType = value;
                if (_caacAircraftType != null)
                {
                    AircraftType.AircraftSeriesId = _caacAircraftType.AircraftSeriesId;
                    AircraftType.AircraftCategoryId = _caacAircraftType.AircraftCategoryId;
                    AircraftType.ManufacturerId = _caacAircraftType.ManufacturerId;
                }
                RaisePropertyChanged("CaacAircraftType");
            }
        }
        #endregion

        #region 飞机系列
        /// <summary>
        /// 飞机系列
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftSeriesDTO> AircraftSerieses { get; set; }

        private AircraftSeriesDTO _aircraftSeries;
        public AircraftSeriesDTO AircraftSeries
        {
            get { return _aircraftSeries; }
            set
            {
                _aircraftSeries = value;
                RaisePropertyChanged("AircraftSeries");
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
