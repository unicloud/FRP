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
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export(typeof (EngineOilVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineOilVM : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private bool _endChanged;
        private FilterDescriptor _endDateDescriptor;
        private bool _snChanged;
        private bool _startChanged;
        private FilterDescriptor _startDateDescriptor;

        [ImportingConstructor]
        public EngineOilVM(IPartService service) : base(service)
        {
            _service = service;
            _context = service.Context;

            FilterCommand = new DelegateCommand<object>(OnFilter, CanFilter);

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
            CurrentOil = new EngineOilDTO
            {
                //Date = DateTime.Now,
                //IntervalRate = 0,
                //DeltaIntervalRate = 0
            };

            InitializeViewEngineOilDTO();
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

        private EngineOilDTO _currentOil;

        /// <summary>
        ///     当前滑油耗率数据
        /// </summary>
        public EngineOilDTO CurrentOil
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

        #region 可选开始日期

        private DateTime _displayStart;

        /// <summary>
        ///     可选开始日期
        /// </summary>
        public DateTime DisplayStart
        {
            get { return _displayStart; }
            private set
            {
                if (_displayStart != value)
                {
                    _displayStart = value;
                    RaisePropertyChanged(() => DisplayStart);
                }
            }
        }

        #endregion

        #region 可选结束日期

        private DateTime _displayEnd;

        /// <summary>
        ///     可选结束日期
        /// </summary>
        public DateTime DisplayEnd
        {
            get { return _displayEnd; }
            private set
            {
                if (_displayEnd != value)
                {
                    _displayEnd = value;
                    RaisePropertyChanged(() => DisplayEnd);
                }
            }
        }

        #endregion

        #region 开始日期

        private DateTime _start;

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime Start
        {
            get { return _start; }
            private set
            {
                if (_start != value)
                {
                    if (_start != DateTime.MinValue)
                    {
                        _startChanged = true;
                        FilterCommand.RaiseCanExecuteChanged();
                    }
                    _start = value;
                    RaisePropertyChanged(() => Start);
                }
            }
        }

        #endregion

        #region 结束日期

        private DateTime _end;

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime End
        {
            get { return _end; }
            private set
            {
                if (_end != value)
                {
                    if (_end != DateTime.MinValue)
                    {
                        _endChanged = true;
                        FilterCommand.RaiseCanExecuteChanged();
                    }
                    _end = value;
                    RaisePropertyChanged(() => End);
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
        }

        #region 发动机滑油

        private EngineOilDTO _selEngineOilDTO;

        /// <summary>
        ///     发动机滑油集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineOilDTO> ViewEngineOilDTO { get; set; }

        /// <summary>
        ///     选中的发动机滑油
        /// </summary>
        public EngineOilDTO SelEngineOilDTO
        {
            get { return _selEngineOilDTO; }
            private set
            {
                if (_selEngineOilDTO != value)
                {
                    _selEngineOilDTO = value;
                    RaisePropertyChanged(() => SelEngineOilDTO);
                }
            }
        }

        /// <summary>
        ///     初始化发动机滑油集合
        /// </summary>
        private void InitializeViewEngineOilDTO()
        {
            ViewEngineOilDTO = _service.CreateCollection(_context.EngineOils);
            _service.RegisterCollectionView(ViewEngineOilDTO);
            ViewEngineOilDTO.LoadedData += (o, e) =>
            {
                var q = 1;
            };
            // 添加过滤条件
            //_start = new DateTime(2014, 1, 1);
            //_end = DateTime.Now;
            //_displayStart = new DateTime(2013, 1, 1);
            //_displayEnd = DateTime.Now;
            //var cfd = new CompositeFilterDescriptor {LogicalOperator = FilterCompositionLogicalOperator.And};
            //_startDateDescriptor = new FilterDescriptor("Date", FilterOperator.IsGreaterThanOrEqualTo, _start);
            //cfd.FilterDescriptors.Add(_startDateDescriptor);
            //_endDateDescriptor = new FilterDescriptor("Date", FilterOperator.IsLessThanOrEqualTo, _end);
            //cfd.FilterDescriptors.Add(_endDateDescriptor);
            //ViewEngineOilDTO.FilterDescriptors.Add(cfd);
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
                CurrentOil = closestDataPoint.DataPoint.DataItem as EngineOilDTO;
            }
        }

        #region 筛选发动机滑油数据

        /// <summary>
        ///     筛选发动机滑油数据
        /// </summary>
        public DelegateCommand<object> FilterCommand { get; private set; }

        private void OnFilter(object obj)
        {
            if (_startChanged)
            {
                _startDateDescriptor.Value = _start;
                _startChanged = false;
            }
            if (_endChanged)
            {
                _endDateDescriptor.Value = _end;
                _endChanged = false;
            }
            FilterCommand.RaiseCanExecuteChanged();
        }

        private bool CanFilter(object obj)
        {
            return _startChanged || _endChanged || _snChanged;
        }

        #endregion

        #endregion
    }
}