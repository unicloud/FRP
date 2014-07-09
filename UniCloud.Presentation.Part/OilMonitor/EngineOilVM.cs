#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，15:04
// 方案：FRP
// 项目：Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export(typeof (EngineOilVM))]
    public class EngineOilVM : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private FilterDescriptor _oilUserDescriptor;
        private FilterDescriptor _startDateDescriptor;

        [ImportingConstructor]
        public EngineOilVM(IPartService service) : base(service)
        {
            _service = service;
            _context = service.Context;

            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            Zoom = new Size(1, 1);
            PanOffset = new Point(-10000, 0);
            CurrentOil = new OilMonitorDTO
            {
                Date = DateTime.Now,
                IntervalRate = 0,
                DeltaIntervalRate = 0
            };

            InitializeViewEngineOilDTO();
            InitializeViewOilMonitorDTO();

            // 设置选中的期间
            SelPeriod = OilMonitorPeriod.最近90天;
            _startDateDescriptor.Value = DateTime.Now.AddDays(-90);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region Zoom

        private Size _zoom;

        /// <summary>
        ///     Zoom
        /// </summary>
        public Size Zoom
        {
            get { return _zoom; }
            private set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged(() => Zoom);
                }
            }
        }

        #endregion

        #region PanOffset

        private Point _panOffset;

        /// <summary>
        ///     PanOffset
        /// </summary>
        public Point PanOffset
        {
            get { return _panOffset; }
            private set
            {
                if (_panOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged(() => PanOffset);
                }
            }
        }

        #endregion

        #region 当前滑油耗率数据

        private OilMonitorDTO _currentOil;

        /// <summary>
        ///     当前滑油耗率数据
        /// </summary>
        public OilMonitorDTO CurrentOil
        {
            get { return _currentOil; }
            private set
            {
                if (_currentOil != value)
                {
                    _currentOil = value;
                    RaisePropertyChanged(() => CurrentOil);
                }
            }
        }

        #endregion

        #region 时间跨度

        private Array _period;

        /// <summary>
        ///     时间跨度
        /// </summary>
        public Array Period
        {
            get { return _period ?? (_period = Enum.GetValues(typeof (OilMonitorPeriod))); }
        }

        #endregion

        #region 选中的期间

        private OilMonitorPeriod _selPeriod;

        /// <summary>
        ///     选中的期间
        /// </summary>
        public OilMonitorPeriod SelPeriod
        {
            get { return _selPeriod; }
            private set
            {
                if (_selPeriod == value) return;
                _selPeriod = value;
                RaisePropertyChanged(() => SelPeriod);
                switch (_selPeriod)
                {
                    case OilMonitorPeriod.最近90天:
                        _startDateDescriptor.Value = DateTime.Now.AddDays(-90);
                        break;
                    case OilMonitorPeriod.最近30天:
                        _startDateDescriptor.Value = DateTime.Now.AddDays(-30);
                        break;
                    case OilMonitorPeriod.最近一周:
                        _startDateDescriptor.Value = DateTime.Now.AddDays(-7);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

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
            if (!ViewEngineOilDTO.AutoLoad) ViewEngineOilDTO.AutoLoad = true;
            else ViewEngineOilDTO.Load(true);
            if (!ViewOilMonitorDTO.AutoLoad) ViewOilMonitorDTO.AutoLoad = true;
            else ViewOilMonitorDTO.Load(true);
        }

        #region 滑油监控发动机

        private EngineOilDTO _selEngineOilDTO;

        /// <summary>
        ///     滑油监控发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineOilDTO> ViewEngineOilDTO { get; set; }

        /// <summary>
        ///     选中的滑油监控发动机
        /// </summary>
        public EngineOilDTO SelEngineOilDTO
        {
            get { return _selEngineOilDTO; }
            set
            {
                if (_selEngineOilDTO != value)
                {
                    _selEngineOilDTO = value;
                    RaisePropertyChanged(() => SelEngineOilDTO);
                    _oilUserDescriptor.Value = _selEngineOilDTO.Id;
                }
            }
        }

        /// <summary>
        ///     初始化滑油监控发动机集合
        /// </summary>
        private void InitializeViewEngineOilDTO()
        {
            ViewEngineOilDTO = _service.CreateCollection(_context.EngineOils);
            _service.RegisterCollectionView(ViewEngineOilDTO);
            // 添加排序条件
            var sort = new SortDescriptor
            {
                Member = "Status",
                SortDirection = ListSortDirection.Descending
            };
            ViewEngineOilDTO.SortDescriptors.Add(sort);
        }

        #endregion

        #region 滑油消耗数据

        private OilMonitorDTO _selOilMonitorDTO;

        /// <summary>
        ///     滑油消耗数据集合
        /// </summary>
        public QueryableDataServiceCollectionView<OilMonitorDTO> ViewOilMonitorDTO { get; set; }

        /// <summary>
        ///     选中的滑油消耗数据
        /// </summary>
        public OilMonitorDTO SelOilMonitorDTO
        {
            get { return _selOilMonitorDTO; }
            set
            {
                if (_selOilMonitorDTO != value)
                {
                    _selOilMonitorDTO = value;
                    RaisePropertyChanged(() => SelOilMonitorDTO);
                }
            }
        }

        /// <summary>
        ///     初始化滑油消耗数据集合
        /// </summary>
        private void InitializeViewOilMonitorDTO()
        {
            ViewOilMonitorDTO = _service.CreateCollection(_context.OilMonitors);
            _service.RegisterCollectionView(ViewOilMonitorDTO);

            // 添加过滤条件
            var cfd = new CompositeFilterDescriptor {LogicalOperator = FilterCompositionLogicalOperator.And};
            _oilUserDescriptor = new FilterDescriptor("OilUserID", FilterOperator.IsEqualTo, -1);
            cfd.FilterDescriptors.Add(_oilUserDescriptor);
            _startDateDescriptor = new FilterDescriptor("Date", FilterOperator.IsGreaterThanOrEqualTo, DateTime.Now);
            cfd.FilterDescriptors.Add(_startDateDescriptor);
            ViewOilMonitorDTO.FilterDescriptors.Add(cfd);
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        public void TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            var closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                CurrentOil = closestDataPoint.DataPoint.DataItem as OilMonitorDTO;
            }
        }

        #endregion
    }
}