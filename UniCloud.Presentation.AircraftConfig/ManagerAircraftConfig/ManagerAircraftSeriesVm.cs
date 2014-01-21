#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 14:35:47
// 文件名：AircraftSeriesVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 14:35:47
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export(typeof(ManagerAircraftSeriesVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerAircraftSeriesVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly AircraftConfigData _context;
        private readonly IRegionManager _regionManager;
        private readonly IAircraftConfigService _service;

        [ImportingConstructor]
        public ManagerAircraftSeriesVm(IRegionManager regionManager, IAircraftConfigService service)
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
            AircraftSerieses = _service.CreateCollection(_context.AircraftSeries);
            _service.RegisterCollectionView(AircraftSerieses);
            AircraftSerieses.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = AircraftSerieses.CurrentAddItem as AircraftSeriesDTO;
                    if (newItem != null)
                    {
                        newItem.Id = Guid.NewGuid();
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectAircraftSeries = !AircraftSerieses.HasChanges;
                }
            };
        }

        #endregion

        #region 数据

        #region 公共属性
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
            if (!AircraftSerieses.AutoLoad)
                AircraftSerieses.AutoLoad = true;
            else AircraftSerieses.Load(true);
            Manufacturers = _service.GetManufacturers(() => RaisePropertyChanged(() => Manufacturers));
        }

        #region 系列

        private AircraftSeriesDTO _aircraftSeries;

        private bool _canSelectAircraftSeries = true;

        /// <summary>
        ///     系列集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftSeriesDTO> AircraftSerieses { get; set; }

        /// <summary>
        ///     选中的系列
        /// </summary>
        public AircraftSeriesDTO AircraftSeries
        {
            get { return _aircraftSeries; }
            set
            {
                if (_aircraftSeries != value)
                {
                    _aircraftSeries = value;
                    RaisePropertyChanged(() => AircraftSeries);
                }
            }
        }

        //用户能否选择
        public bool CanSelectAircraftSeries
        {
            get { return _canSelectAircraftSeries; }
            set
            {
                if (_canSelectAircraftSeries != value)
                {
                    _canSelectAircraftSeries = value;
                    RaisePropertyChanged(() => CanSelectAircraftSeries);
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
