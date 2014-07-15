#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:21
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
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export(typeof (APUOilVM))]
    public class APUOilVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private FilterDescriptor _oilUserDescriptor;
        private FilterDescriptor _startDateDescriptor;

        [ImportingConstructor]
        public APUOilVM(IPartService service)
            : base(service)
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

            InitializeViewAPUOilDTO();
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
                if (_zoom == value) return;
                _zoom = value;
                RaisePropertyChanged(() => Zoom);
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
                if (_panOffset == value) return;
                _panOffset = value;
                RaisePropertyChanged(() => PanOffset);
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
                if (_currentOil == value) return;
                _currentOil = value;
                RaisePropertyChanged(() => CurrentOil);
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
            if (!ViewAPUOilDTO.AutoLoad) ViewAPUOilDTO.AutoLoad = true;
            else ViewAPUOilDTO.Load(true);
        }

        #region 滑油监控发动机

        private APUOilDTO _selAPUOilDTO;

        /// <summary>
        ///     滑油监控APU集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUOilDTO> ViewAPUOilDTO { get; set; }

        /// <summary>
        ///     选中的滑油监控APU
        /// </summary>
        public APUOilDTO SelAPUOilDTO
        {
            get { return _selAPUOilDTO; }
            set
            {
                if (_selAPUOilDTO == value) return;
                _selAPUOilDTO = value;
                if (_selAPUOilDTO != null)
                {
                    _oilUserDescriptor.Value = _selAPUOilDTO.Id;
                    if (!ViewOilMonitorDTO.AutoLoad) ViewOilMonitorDTO.AutoLoad = true;
                    else ViewOilMonitorDTO.Load(true);
                }
                RaisePropertyChanged(() => SelAPUOilDTO);
            }
        }

        /// <summary>
        ///     初始化滑油监控APU集合
        /// </summary>
        private void InitializeViewAPUOilDTO()
        {
            ViewAPUOilDTO = _service.CreateCollection(_context.APUOils);
            _service.RegisterCollectionView(ViewAPUOilDTO);
            // 添加排序条件
            var sort = new SortDescriptor
            {
                Member = "Status",
                SortDirection = ListSortDirection.Descending
            };
            ViewAPUOilDTO.SortDescriptors.Add(sort);
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
                if (_selOilMonitorDTO == value) return;
                _selOilMonitorDTO = value;
                RaisePropertyChanged(() => SelOilMonitorDTO);
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

        #region 重载操作

        #region 保存成功后执行

        protected override void OnSaveSuccess(object sender)
        {
        }

        #endregion

        #region 撤销成功后执行

        protected override void OnAbortExecuted(object sender)
        {
        }

        #endregion

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
        }

        #endregion

        #endregion

        #endregion
    }
}