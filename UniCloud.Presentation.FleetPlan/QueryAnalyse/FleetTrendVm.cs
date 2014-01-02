#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/31 18:52:37
// 文件名：FleetTrend
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/31 18:52:37
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Telerik.Charting;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Export;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof (FleetTrendVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetTrendVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _fleetPlanContext;
        private readonly RadWindow _aircraftWindow = new RadWindow(); //用于单击飞机数饼状图的用户提示
        private readonly CommonMethod _commonMethod = new CommonMethod();
        private readonly IFleetPlanService _service;
        private Grid _aircraftPieGrid; //折线趋势图区域，柱状趋势图区域， 飞机数饼图区域
        private Grid _barGrid; //折线趋势图区域，柱状趋势图区域， 飞机数饼图区域
        private RadDateTimePicker _endDateTimePicker; //开始时间控件， 结束时间控件
        private RadGridView _exportRadgridview; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _lineGrid; //折线趋势图区域，柱状趋势图区域， 飞机数饼图区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;
        private RadGridView _planDetailGridview; //初始化RadGridView
        private RadDateTimePicker _startDateTimePicker; //开始时间控件， 结束时间控件

        [ImportingConstructor]
        public FleetTrendVm(IFleetPlanService service)
        {
            _service = service;
            _fleetPlanContext = _service.Context;
            ExportCommand = new DelegateCommand<object>(OnExport, CanExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView, CanExportGridView); //导出数据表数据
            ViewModelInitializer();
            InitalizerRadWindows(_aircraftWindow, "Aircraft", 200);
            AddRadMenu(_aircraftWindow);
            InitializeVm();
        }

        public FleetTrend CurrentFleetTrend
        {
            get { return ServiceLocator.Current.GetInstance<FleetTrend>(); }
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        public void InitializeVm()
        {
            // 创建并注册CollectionView
            XmlConfigs = new QueryableDataServiceCollectionView<XmlConfigDTO>(_fleetPlanContext,
                _fleetPlanContext.XmlConfigs);
            XmlConfigs.LoadedData += (o, e) =>
            {
                _loadXmlConfig = true;
                InitializeData();
            };
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(_fleetPlanContext,
                _fleetPlanContext.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                _loadXmlSetting = true;
                InitializeData();
            };
            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_fleetPlanContext,
                _fleetPlanContext.Aircrafts);
        }

        /// <summary>
        ///     以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            _lineGrid = CurrentFleetTrend.LineGrid;
            _barGrid = CurrentFleetTrend.BarGrid;
            _aircraftPieGrid = CurrentFleetTrend.AircraftPieGrid;
            //控制界面起止时间控件的字符串格式化
            _planDetailGridview = CurrentFleetTrend.PlanDetailGridview;
            _startDateTimePicker = CurrentFleetTrend.StartDateTimePicker;
            _endDateTimePicker = CurrentFleetTrend.EndDateTimePicker;
            _startDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
            _endDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
        }

        #endregion

        #region 数据

        #region 公共属性

        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; } //XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; } //飞机集合 
        public AirlinesDTO CurrentAirlines { get; set; }
        #region  属性 SelectedTime --所选的时间点

        private string _selectedTime = "所选时间";

        /// <summary>
        ///     所选的时间点
        /// </summary>
        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (SelectedTime != value)
                {
                    _selectedTime = value;
                    _aircraftWindow.Close();
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeAircraft = "所选时间的飞机分布图";
                    }
                    else
                    {
                        SelectedTimeAircraft = SelectedTime + "末的飞机分布图";
                    }
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftTrendLineCollection --折线图的统计总数集合

        private List<FleetAircraftTrend> _fleetAircraftTrendLineCollection;

        /// <summary>
        ///     折线图的统计总数集合
        /// </summary>
        public List<FleetAircraftTrend> FleetAircraftTrendLineCollection
        {
            get { return _fleetAircraftTrendLineCollection; }
            set
            {
                if (FleetAircraftTrendLineCollection != value)
                {
                    _fleetAircraftTrendLineCollection = value;

                    if (FleetAircraftTrendLineCollection != null && FleetAircraftTrendLineCollection.Count() >= 12)
                    {
                        CurrentFleetTrend.LineCategoricalAxis.MajorTickInterval =
                            FleetAircraftTrendLineCollection.Count()/6;
                        CurrentFleetTrend.BarCategoricalAxis.MajorTickInterval =
                            FleetAircraftTrendLineCollection.Count()/6;
                    }
                    else
                    {
                        CurrentFleetTrend.LineCategoricalAxis.MajorTickInterval = 1;
                        CurrentFleetTrend.BarCategoricalAxis.MajorTickInterval = 1;
                    }
                    RaisePropertyChanged(() => FleetAircraftTrendLineCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftTrendBarCollection --柱状图的净增数集合

        private List<FleetAircraftTrend> _fleetAircraftTrendBarCollection;

        /// <summary>
        ///     柱状图的净增数集合
        /// </summary>
        public List<FleetAircraftTrend> FleetAircraftTrendBarCollection
        {
            get { return _fleetAircraftTrendBarCollection; }
            set
            {
                if (FleetAircraftTrendBarCollection != value)
                {
                    _fleetAircraftTrendBarCollection = value;
                    RaisePropertyChanged(() => FleetAircraftTrendBarCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftCollection --选中时间点的飞机数分布集合

        private IEnumerable<FleetAircraft> _fleetAircraftCollection;

        /// <summary>
        ///     选中时间点的飞机数分布集合
        /// </summary>
        public IEnumerable<FleetAircraft> FleetAircraftCollection
        {
            get { return _fleetAircraftCollection; }
            set
            {
                if (!Equals(FleetAircraftCollection, value))
                {
                    _fleetAircraftCollection = value;
                    RaisePropertyChanged(() => FleetAircraftCollection);
                    SetPieMark(_fleetAircraftCollection, _aircraftPieGrid);
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeAircraft --飞机数饼图的标识提示

        private string _selectedTimeAircraft = "所选时间的飞机分布图";

        /// <summary>
        ///     飞机数饼图的标识提示
        /// </summary>
        public string SelectedTimeAircraft
        {
            get { return _selectedTimeAircraft; }
            set
            {
                if (SelectedTimeAircraft != value)
                {
                    _selectedTimeAircraft = value;
                    RaisePropertyChanged(() => SelectedTimeAircraft);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftCollection --选中时间点的飞机数据集合

        private List<AircraftDTO> _aircraftCollection;

        /// <summary>
        ///     选中时间点的飞机数分布集合
        /// </summary>
        public List<AircraftDTO> AircraftCollection
        {
            get { return _aircraftCollection; }
            set
            {
                if (AircraftCollection != value)
                {
                    _aircraftCollection = value;
                    RaisePropertyChanged(() => AircraftCollection);
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeAircraftList = "所选时间的飞机详细";
                    }
                    else
                    {
                        if (AircraftCollection == null || !AircraftCollection.Any())
                        {
                            SelectedTimeAircraftList = SelectedTime + "末的飞机详细（0架）";
                        }
                        else
                        {
                            SelectedTimeAircraftList = SelectedTime + "末的飞机详细（" + AircraftCollection.Count() + "架）";
                        }
                    }
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeAircraftList --飞机列表的标识提示

        private string _selectedTimeAircraftList = "所选时间的飞机详细";

        /// <summary>
        ///     飞机列表的标识提示
        /// </summary>
        public string SelectedTimeAircraftList
        {
            get { return _selectedTimeAircraftList; }
            set
            {
                if (SelectedTimeAircraftList != value)
                {
                    _selectedTimeAircraftList = value;
                    RaisePropertyChanged(() => SelectedTimeAircraftList);
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedIndex --时间的统计方式

        private int _selectedIndex;

        /// <summary>
        ///     时间的统计方式
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (SelectedIndex != value)
                {
                    _selectedIndex = value;
                    CreatFleetAircraftTrendCollection();
                    RaisePropertyChanged(() => SelectedIndex);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftMaxValue --柱状图中飞机数轴的最大值

        private int _aircraftMaxValue = 100;

        /// <summary>
        ///     柱状图中飞机数轴的最大值
        /// </summary>
        public int AircraftMaxValue
        {
            get { return _aircraftMaxValue; }
            set
            {
                if (AircraftMaxValue != value)
                {
                    _aircraftMaxValue = value;
                    RaisePropertyChanged(() => AircraftMaxValue);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftMinValue --柱状图中飞机数轴的最小值

        private int _aircraftMinValue = -100;

        /// <summary>
        ///     柱状图中飞机数轴的最小值
        /// </summary>
        public int AircraftMinValue
        {
            get { return _aircraftMinValue; }
            set
            {
                if (AircraftMinValue != value)
                {
                    _aircraftMinValue = value;
                    RaisePropertyChanged(() => AircraftMinValue);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftStep --柱状图中飞机数轴的节点距离

        private int _aircraftStep = 20;

        /// <summary>
        ///     柱状图中飞机数轴的节点距离
        /// </summary>
        public int AircraftStep
        {
            get { return _aircraftStep; }
            set
            {
                if (AircraftStep != value)
                {
                    _aircraftStep = value;
                    RaisePropertyChanged(() => AircraftStep);
                }
            }
        }

        #endregion

        #region ViewModel 属性 EndDate --结束时间

        private DateTime? _endDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/M"));

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (EndDate != value)
                {
                    if (value == null)
                    {
                        _endDateTimePicker.SelectedValue = _endDate;
                        return;
                    }
                    _endDate = value;
                    RaisePropertyChanged(() => EndDate);
                    CreatFleetAircraftTrendCollection();
                }
            }
        }

        #endregion

        #region ViewModel 属性 StartDate --开始时间

        private DateTime? _startDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (StartDate != value)
                {
                    if (value == null)
                    {
                        _startDateTimePicker.SelectedValue = _startDate;
                        return;
                    }
                    _startDate = value;
                    RaisePropertyChanged(() => StartDate);
                    CreatFleetAircraftTrendCollection();
                }
            }
        }

        #endregion

        #region ViewModel 属性 IsHidden --控制是否含分公司的饼图区域显示

        private bool _isHidden = true;

        /// <summary>
        ///     控制是否含分公司的饼图区域显示
        /// </summary>
        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                if (IsHidden != value)
                {
                    _isHidden = value;
                    RaisePropertyChanged(() => IsHidden);
                }
            }
        }

        #endregion

        #region ViewModel 属性 Zoom --滚动条的对应

        private Size _zoom = new Size(1, 1);

        /// <summary>
        ///     滚动条的对应
        /// </summary>
        public Size Zoom
        {
            get { return _zoom; }
            set
            {
                if (Zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged(() => Zoom);
                }
            }
        }

        #endregion

        #region ViewModel 属性 PanOffset --滚动条的滑动

        private Point _panOffset;

        /// <summary>
        ///     滚动条的滑动
        /// </summary>
        public Point PanOffset
        {
            get { return _panOffset; }
            set
            {
                if (PanOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged(() => PanOffset);
                }
            }
        }

        #endregion

        #region ViewModel 属性 Visibility --控制是否含分公司的趋势图数据显示

        private Visibility _visibility = Visibility.Collapsed;

        /// <summary>
        ///     控制是否含分公司的饼图区域显示
        /// </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (Visibility != value)
                {
                    _visibility = value;
                    RaisePropertyChanged(() => Visibility);
                }
            }
        }

        #endregion

        #region ViewModel 属性 IsContextMenuOpen --控制右键菜单的打开

        private bool _isContextMenuOpen = true;

        /// <summary>
        ///     控制右键菜单的打开
        /// </summary>
        public bool IsContextMenuOpen
        {
            get { return _isContextMenuOpen; }
            set
            {
                if (_isContextMenuOpen != value)
                {
                    _isContextMenuOpen = value;
                    RaisePropertyChanged(() => IsContextMenuOpen);
                }
            }
        }

        #endregion

        //飞机集合 

        #endregion

        #region 加载数据

        public override void LoadData()
        {
            IsBusy = true;
            XmlConfigs.Load(true);
            XmlSettings.Load(true);
            Aircrafts.Load(true);

            //if (CurrentAirlines != null && CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType == 0 || p.SubType == 2))
            //{
            //    IsHidden = false;
            //}
            //else
            //{
            //    IsHidden = true;
            //}


            //if (CurrentAirlines!= null && CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType ==1))
            //{
            //    Visibility = Visibility.Visible;
            //    (BarGrid.Children[0] as RadCartesianChart).TooltipTemplate = CurrentFleetTrend.Resources["TooltipTemplateChild"] as DataTemplate;
            //}
            //else
            //{
            //    Visibility = Visibility.Collapsed;
            //    (BarGrid.Children[0] as RadCartesianChart).TooltipTemplate = CurrentFleetTrend.Resources["TooltipTemplate"] as DataTemplate;
            //}
        }

        #endregion

        #endregion

        #region 操作

        /// <summary>
        ///     根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem">选中点</param>
        /// <param name="radwindow">弹出窗体</param>
        /// <param name="header">窗体标示</param>
        private void GetGridViewDataSourse(PieDataPoint selectedItem, RadWindow radwindow, string header)
        {
            //if (selectedItem != null && radwindow != null)
            //{
            //    var fleetAircraft = selectedItem.DataItem as FleetAircraft;
            //    DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
            //    var aircraft = Aircrafts.Where(o => o.OperationHistories.Any(a =>
            //        (a.Airlines.ShortName .Equals( CurrentAirlines.ShortName,StringComparison.OrdinalIgnoreCase) || a.Airlines.SubType ==2)
            //        && a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))
            //        && o.AircraftBusinesses.Any(t => t.StartDate <= time && !(t.EndDate != null && t.EndDate < time)));

            //    var airlineAircrafts = new List<AircraftDTO>();
            //    if (fleetAircraft.Aircraft .Equals( CurrentAirlines.ShortName,StringComparison.OrdinalIgnoreCase))
            //    {
            //        airlineAircrafts = aircraft.Where(p =>
            //        {
            //            var operationHistory = p.OperationHistories.FirstOrDefault(pp => pp.Airlines.ShortName .Equals( CurrentAirlines.ShortName,StringComparison.OrdinalIgnoreCase) && pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time));
            //            if (operationHistory ==null)
            //            {
            //                return false;
            //            }
            //            else if (operationHistory.SubOperationHistories.Count <= 0)
            //            {
            //                return true;
            //            }
            //            else
            //            {
            //                var suboperationCategory = operationHistory.SubOperationHistories.Where(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time));
            //                if (suboperationCategory == null || suboperationCategory.Count() == 0) return true;
            //                return suboperationCategory.Any(a => a.Airlines.ShortName .Equals( CurrentAirlines.ShortName,StringComparison.OrdinalIgnoreCase));

            //            }
            //        }).ToList();

            //    }
            //    else
            //    {
            //        //分子公司的筛选
            //        var aircraftSubCompany = aircraft.Where(p =>
            //                                                p.OperationHistories.Any(
            //                                                    pp =>
            //                                                    pp.Airlines.ShortName .Equals( fleetAircraft.Aircraft &&
            //                                                    pp.Airlines.SubType== 2
            //                                                    && pp.StartDate <= time &&
            //                                           !(pp.EndDate != null && pp.EndDate < time))).ToList();
            //        //分公司的筛选     
            //        var aircraftFiliale = aircraft.Where(p =>
            //        {
            //            var operationHistory = p.OperationHistories.FirstOrDefault(pp => pp.Airlines.ShortName .Equals( CurrentAirlines.ShortName,StringComparison.OrdinalIgnoreCase) && pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time));
            //            if (operationHistory ==null ||
            //                operationHistory.SubOperationHistories.Count <= 0)
            //            {
            //                return false;
            //            }
            //            else
            //            {
            //                return
            //                    operationHistory.SubOperationHistories.Any(a =>
            //                        a.Airlines.ShortName .Equals(fleetAircraft.Aircraft,StringComparison.OrdinalIgnoreCase) &&
            //                        a.StartDate <= time &&!(a.EndDate != null && a.EndDate < time));

            //            }
            //        }).ToList();
            //        airlineAircrafts = aircraftSubCompany.Union(aircraftFiliale).ToList();
            //    }
            //    //找到子窗体的RadGridView，并为其赋值
            //    var rgv = radwindow.Content as RadGridView;
            //    rgv.ItemsSource = _commonMethod.GetAircraftByTime(airlineAircrafts, time);
            //    radwindow.Header = fleetAircraft.Aircraft + header + "：" + fleetAircraft.ToolTip;
            //    if (!radwindow.IsOpen)
            //    {
            //        _commonMethod.ShowRadWindow(radwindow);
            //    }
            //}
        }

        /// <summary>
        ///     初始化子窗体
        /// </summary>
        public void InitalizerRadWindows(RadWindow radwindow, string windowsName, int length)
        {
            //运营计划子窗体的设置
            radwindow.Name = windowsName;
            radwindow.Top = length;
            radwindow.Left = length;
            radwindow.Height = 250;
            radwindow.Width = 500;
            radwindow.ResizeMode = ResizeMode.CanResize;
            radwindow.Content = _commonMethod.CreatOperationGridView();
            radwindow.Closed += RadwindowClosed;
        }

        /// <summary>
        ///     弹出窗体关闭时，取消相应饼图的弹出项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadwindowClosed(object sender, WindowClosedEventArgs e)
        {
            var radWindow = sender as RadWindow;
            var grid = new Grid();
            if (radWindow != null && radWindow.Name.Equals("Aircraft", StringComparison.OrdinalIgnoreCase))
            {
                grid = _aircraftPieGrid;
            }

            //更改对应饼图的突出显示
            foreach (var item in (grid.Children[0] as RadPieChart).Series[0].DataPoints)
            {
                item.IsSelected = false;
            }
            //更改对应饼图的标签大小
            foreach (var item in ((grid.Children[1] as ScrollViewer).Content as StackPanel).Children)
            {
                var rectangle = (item as StackPanel).Children[0] as Rectangle;
                if (rectangle != null)
                {
                    rectangle.Width = 15;
                    rectangle.Height = 15;
                }
            }
        }

        /// <summary>
        ///     初始化数据
        /// </summary>
        private void InitializeData()
        {
            if (_loadXmlSetting && _loadXmlConfig)
            {
                _loadXmlConfig = false;
                _loadXmlSetting = false;
                IsBusy = false;
                CreatFleetAircraftTrendCollection();
                SetRadCartesianChartColor();
            }
        }

        #endregion

        #region Command

        #region ViewModel 命令 --导出图表

        public DelegateCommand<object> ExportCommand { get; private set; }

        private void OnExport(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出源数据", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("LineGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //if (CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType == 1))
                    //{
                    //    //当包含子公司时
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount", "期末飞机数(子)"},
                    //                          {"AircraftAmount1", "期末飞机数"}
                    //                      };
                    //    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendLineCollection, "SubFleetTrendAll");
                    //}
                    //else
                    //{
                    //    //创建RadGridView
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount1", "期末飞机数"}
                    //                      };
                    //    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendLineCollection, "FleetTrendAll");
                    //}
                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (
                        Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                        )
                    {
                        if (stream != null)
                        {
                            _exportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (menu.Name.Equals("BarGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //if (CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType == 1))
                    //{
                    //    //当包含子公司时
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount", "飞机净增数(子)"},
                    //                          {"AircraftAmount1", "飞机净增数"}
                    //                      };
                    //    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendBarCollection, "SubFleetTrendAll");
                    //}
                    //else
                    //{
                    //    //创建RadGridView
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount1", "飞机净增数"}
                    //                      };
                    //    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendBarCollection, "FleetTrendAll");
                    //}
                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (
                        Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                        )
                    {
                        if (stream != null)
                        {
                            _exportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (menu.Name.Equals("AircraftPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftCollection == null || !FleetAircraftCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> {{"Aircraft", "航空公司"}, {"Amount", "飞机数（架）"}};
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftCollection,
                        "PieFleetTrend");

                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (
                        Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                        )
                    {
                        if (stream != null)
                        {
                            _exportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
            }
            else if (menu != null && menu.Header.ToString().Equals("导出图片", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("LineGridImage", StringComparison.OrdinalIgnoreCase) ||
                    menu.Name.Equals("BarGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_lineGrid != null)
                    {
                        _commonMethod.ExportToImage(_lineGrid.Parent as Grid);
                    }
                }
                else if (menu.Name.Equals("AircraftPieGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_aircraftPieGrid != null)
                    {
                        _commonMethod.ExportToImage(_aircraftPieGrid);
                    }
                }
            }
        }

        /// <summary>
        ///     设置导出样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            e.Width = 120;
            if (e.Element == ExportElement.Cell && e.Value != null)
            {
                if (_i%3 == 0 && _i >= 6 &&
                    (sender as RadGridView).Name.Equals("FleetTrendAll", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
                else if (_i%4 == 3 && _i >= 7 &&
                         (sender as RadGridView).Name.Equals("SubFleetTrendAll", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }

        private bool CanExport(object sender)
        {
            return true;
        }

        #endregion

        #region ViewModel 命令 --导出数据planDetail

        public DelegateCommand<object> ExportGridViewCommand { get; private set; }

        private void OnExportGridView(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出数据", StringComparison.OrdinalIgnoreCase) &&
                _planDetailGridview != null)
            {
                _planDetailGridview.ElementExporting -= ElementExporting;
                _planDetailGridview.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        _planDetailGridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
        }

        private bool CanExportGridView(object sender)
        {
            return true;
        }

        #endregion

        #region  增加子窗体的右键导出功能

        public void AddRadMenu(RadWindow rwindow)
        {
            var radcm = new RadContextMenu(); //新建右键菜单
            radcm.Opened += radcm_Opened;
            var rmi = new RadMenuItem {Header = "导出表格"}; //新建右键菜单项
            rmi.Click += MenuItemClick; //为菜单项注册事件
            rmi.DataContext = rwindow.Name;
            radcm.Items.Add(rmi);
            RadContextMenu.SetContextMenu(rwindow, radcm); //为控件绑定右键菜单
        }

        private void radcm_Opened(object sender, RoutedEventArgs e)
        {
            var radcm = sender as RadContextMenu;
            if (radcm != null) radcm.StaysOpen = true;
        }

        public void MenuItemClick(object sender, RadRoutedEventArgs e)
        {
            var rmi = sender as RadMenuItem;
            var radcm = rmi.Parent as RadContextMenu;
            if (radcm != null) radcm.StaysOpen = false;
            RadGridView rgview = null;
            if (rmi.DataContext.ToString().Equals("Aircraft", StringComparison.OrdinalIgnoreCase))
            {
                rgview = _aircraftWindow.Content as RadGridView;
            }
            if (rgview != null)
            {
                rgview.ElementExporting -= ElementExporting;
                rgview.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        rgview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        ///     获取趋势图的颜色配置
        /// </summary>
        private Dictionary<string, string> GetColorDictionary()
        {
            var colorDictionary = new Dictionary<string, string>();
            XmlConfigDTO colorConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null &&
                XElement.Parse(colorConfig.XmlContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                XElement capacityColor =
                    XElement.Parse(colorConfig.XmlContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                foreach (var item in capacityColor.Descendants("Item"))
                {
                    colorDictionary.Add(item.Attribute("Name").Value, item.Attribute("Color").Value);
                }
            }
            else
            {
                colorDictionary.Add("飞机数（子）", _commonMethod.GetRandomColor());
                colorDictionary.Add("座位数（子）", _commonMethod.GetRandomColor());
                colorDictionary.Add("商载量（子）", _commonMethod.GetRandomColor());
                colorDictionary.Add("飞机数", _commonMethod.GetRandomColor());
                colorDictionary.Add("座位数", _commonMethod.GetRandomColor());
                colorDictionary.Add("商载量", _commonMethod.GetRandomColor());
            }
            return colorDictionary;
        }

        /// <summary>               
        ///     获取总数和净增数趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreatFleetAircraftTrendCollection()
        {
            var fleetAircraftTrendLineList = new List<FleetAircraftTrend>(); //折线图统计总数的集合
            var fleetAircraftTrendBarList = new List<FleetAircraftTrend>(); //柱状图统计净增的集合

            #region 飞机运力XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("飞机运力", StringComparison.OrdinalIgnoreCase));
            Dictionary<string, string> colordictionary = GetColorDictionary();
            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.XmlContent);
                if (xelement != null)
                {
                    //记录上一个时间点的总数，便于统计净增数据
                    int lastAircraftAmount = 0;
                    int lastAircraftAmount1 = 0;

                    foreach (XElement datetime in xelement.Descendants("DateTime"))
                    {
                        string currentTime =
                            Convert.ToDateTime(datetime.Attribute("EndOfMonth").Value).ToString("yyyy/M");
                        if (SelectedIndex == 1) //按半年统计
                        {
                            if (Convert.ToDateTime(currentTime).Month != 6 &&
                                Convert.ToDateTime(currentTime).Month != 12)
                            {
                                continue;
                            }
                        }
                        else if (SelectedIndex == 2) //按年份统计
                        {
                            if (Convert.ToDateTime(currentTime).Month != 12)
                            {
                                continue;
                            }
                        }
                        var fleetAircraftTrenLine = new FleetAircraftTrend {DateTime = currentTime}; //折线图的总数对象
                        var fleetAircraftTrenBar = new FleetAircraftTrend {DateTime = currentTime}; //柱状图的净增数对象
                        foreach (XElement type in datetime.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("飞机数（子）", StringComparison.OrdinalIgnoreCase))
                            {
                                fleetAircraftTrenLine.AircraftAmount = Convert.ToInt32(type.Attribute("Amount").Value);
                                //飞机净增数
                                fleetAircraftTrenBar.AircraftAmount = fleetAircraftTrenLine.AircraftAmount -
                                                                      lastAircraftAmount;

                                fleetAircraftTrenLine.AircraftColor =
                                    fleetAircraftTrenBar.AircraftColor = colordictionary["飞机数（子）"];
                            }
                            else if (type.Attribute("TypeName").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase))
                            {
                                fleetAircraftTrenLine.AircraftAmount1 = Convert.ToInt32(type.Attribute("Amount").Value);
                                //飞机净增数
                                fleetAircraftTrenBar.AircraftAmount1 = fleetAircraftTrenLine.AircraftAmount1 -
                                                                       lastAircraftAmount1;

                                fleetAircraftTrenLine.AircraftColor1 =
                                    fleetAircraftTrenBar.AircraftColor1 = colordictionary["飞机数"];
                            }
                        }

                        //将当前总数赋值做为下一次计算净增量。
                        lastAircraftAmount = fleetAircraftTrenLine.AircraftAmount;
                        lastAircraftAmount1 = fleetAircraftTrenLine.AircraftAmount1;

                        //注：放于此为了正确统计净增量
                        //早于开始时间时执行下一个
                        if (Convert.ToDateTime(currentTime) < StartDate)
                        {
                            continue;
                        }
                        //晚于结束时间时跳出循环
                        if (Convert.ToDateTime(currentTime) > EndDate)
                        {
                            break;
                        }

                        //添加进相应的数据源集合
                        fleetAircraftTrendLineList.Add(fleetAircraftTrenLine);
                        fleetAircraftTrendBarList.Add(fleetAircraftTrenBar);
                    }

                    if (fleetAircraftTrendBarList.Any())
                    {
                        AircraftMaxValue = fleetAircraftTrendBarList.Max(p => p.AircraftAmount);
                        AircraftMinValue = fleetAircraftTrendBarList.Min(p => p.AircraftAmount);
                        if (AircraftMinValue >= 0)
                        {
                            AircraftMinValue = 0;
                            if (AircraftMaxValue == 0)
                            {
                                AircraftMaxValue = 10;
                            }
                        }
                        AircraftStep = Convert.ToInt32(AircraftMaxValue/2);
                    }
                }
            }

            #endregion

            FleetAircraftTrendLineCollection = fleetAircraftTrendLineList;
            FleetAircraftTrendBarCollection = fleetAircraftTrendBarList;

            SelectedTime = "所选时间";
            FleetAircraftCollection = null;
            AircraftCollection = null;
            Zoom = new Size(1, 1);
        }


        /// <summary>
        ///     趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            DataPoint selectedPoint =
                (sender as ChartSelectionBehavior).Chart.SelectedPoints.FirstOrDefault(
                    p => (p.Presenter as CategoricalSeries).Visibility == Visibility.Visible);
            if (selectedPoint != null)
            {
                var fleetAircraftTrend = selectedPoint.DataItem as FleetAircraftTrend;
                if (SelectedTime != fleetAircraftTrend.DateTime)
                {
                    //选中时间点
                    SelectedTime = fleetAircraftTrend.DateTime;

                    DateTime time = Convert.ToDateTime(fleetAircraftTrend.DateTime).AddMonths(1).AddDays(-1);
                    //var aircraftListRoot = Aircrafts.Where(o => o.OperationHistories.Any(a => (a.Airlines.ShortName.Equals(CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase) || a.Airlines.SubType == 2)
                    //        && a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))
                    //        && o.AircraftBusinesses.Any(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))).ToList();
                    //AircraftCollection = _commonMethod.GetAircraftByTime(aircraftListRoot, time);

                    #region 飞机运力XML文件的读写

                    var xmlConfig =
                        XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("飞机运力", StringComparison.OrdinalIgnoreCase));

                    XElement airlineColor = null;
                    XmlConfigDTO colorConfig =
                        XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                    if (colorConfig != null &&
                        XElement.Parse(colorConfig.XmlContent)
                            .Descendants("Type")
                            .Any(p => p.Attribute("TypeName").Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase)))
                    {
                        var firstOrDefault =
                            XmlConfigs.FirstOrDefault(
                                p => p.ConfigType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                        if (firstOrDefault != null)
                            airlineColor =
                                XElement.Parse(firstOrDefault.XmlContent)
                                    .Descendants("Type")
                                    .FirstOrDefault(
                                        p =>
                                            p.Attribute("TypeName")
                                                .Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase));
                    }
                    if (xmlConfig != null)
                    {
                        var aircraftList = new List<FleetAircraft>(); //飞机数饼图集合

                        XElement xelement =
                            XElement.Parse(xmlConfig.XmlContent)
                                .Descendants("DateTime")
                                .FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                        if (xelement != null)
                        {
                            foreach (XElement type in xelement.Descendants("Type"))
                            {
                                if (type.Attribute("TypeName").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase))
                                {
                                    foreach (XElement item in type.Descendants("Item"))
                                    {
                                        var fleetAircraft = new FleetAircraft
                                                            {
                                                                Aircraft = item.Attribute("Name").Value,
                                                                Amount = Convert.ToDecimal(item.Value),
                                                                ToolTip = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                                            };
                                        if (airlineColor != null)
                                        {
                                            var firstOrDefault = airlineColor.Descendants("Item")
                                                .FirstOrDefault(
                                                    p =>
                                                        p.Attribute("Name")
                                                            .Value.Equals(fleetAircraft.Aircraft,
                                                                StringComparison.OrdinalIgnoreCase));
                                            if (firstOrDefault != null)
                                                fleetAircraft.Color = firstOrDefault.Attribute("Color").Value;
                                        }
                                        if (fleetAircraft.Amount > 0)
                                        {
                                            aircraftList.Add(fleetAircraft);
                                        }
                                    }
                                }
                            }
                        }
                        FleetAircraftCollection = aircraftList;
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        ///     飞机饼状图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadPieChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            var chartSelectionBehavior = sender as ChartSelectionBehavior;
            if (chartSelectionBehavior != null)
            {
                RadChartBase radChartBase = chartSelectionBehavior.Chart;
                var selectedPoint = radChartBase.SelectedPoints.FirstOrDefault() as PieDataPoint;

                var stackPanelRoot = new StackPanel();
                if (radChartBase.EmptyContent.ToString().Equals("飞机数分布", StringComparison.OrdinalIgnoreCase))
                {
                    var scrollViewer = _aircraftPieGrid.Children[1] as ScrollViewer;
                    if (scrollViewer != null)
                        stackPanelRoot = scrollViewer.Content as StackPanel;
                }

                if (stackPanelRoot != null)
                {
                    foreach (var item in stackPanelRoot.Children)
                    {
                        var stackPanel = item as StackPanel;
                        if (stackPanel != null)
                        {
                            var itemrectangle = stackPanel.Children[0] as Rectangle;
                            if (itemrectangle != null)
                            {
                                itemrectangle.Width = 15;
                                itemrectangle.Height = 15;
                            }
                        }
                    }

                    if (selectedPoint != null)
                    {
                        var childStackPanel =
                            stackPanelRoot.Children.FirstOrDefault(
                                p =>
                                    ((p as StackPanel).Children[1] as TextBlock).Text.Equals(
                                        (selectedPoint.DataItem as FleetAircraft).Aircraft,
                                        StringComparison.OrdinalIgnoreCase)) as StackPanel;
                        var rectangle = childStackPanel.Children[0] as Rectangle;
                        if (rectangle != null)
                        {
                            rectangle.Width = 12;
                            rectangle.Height = 12;
                        }

                        if (radChartBase.EmptyContent.ToString().Equals("飞机数分布", StringComparison.OrdinalIgnoreCase))
                        {
                            GetGridViewDataSourse(selectedPoint, _aircraftWindow, "飞机数");
                        }
                    }
                    else
                    {
                        if (radChartBase.EmptyContent.ToString().Equals("飞机数分布", StringComparison.OrdinalIgnoreCase))
                        {
                            _aircraftWindow.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     根据相应的饼图数据生成饼图标签
        /// </summary>
        /// <param name="ienumerable">饼图数据集合</param>
        private void SetPieMark(IEnumerable<FleetAircraft> ienumerable, Grid grid)
        {
            var radPieChart = grid.Children[0] as RadPieChart;
            var scrollViewer = grid.Children[1] as ScrollViewer;
            if (scrollViewer != null)
            {
                var stackPanel = scrollViewer.Content as StackPanel;

                if (radPieChart != null)
                {
                    radPieChart.Series[0].SliceStyles.Clear();
                    if (stackPanel != null)
                    {
                        stackPanel.Children.Clear();
                        if (ienumerable == null)
                        {
                            return;
                        }
                        foreach (var item in ienumerable)
                        {
                            var setter = new Setter
                                         {
                                Property = Shape.FillProperty,
                                             Value = item.Color
                                         };
                            var style = new Style {TargetType = typeof (System.Windows.Shapes.Path)};
                            style.Setters.Add(setter);
                            radPieChart.Series[0].SliceStyles.Add(style);

                            var barPanel = new StackPanel();
                            barPanel.MouseLeftButtonDown += PiePanelMouseLeftButtonDown;
                            barPanel.Orientation = Orientation.Horizontal;
                            var rectangle = new Rectangle
                                            {
                                                Width = 15,
                                                Height = 15,
                                                Fill = new SolidColorBrush(_commonMethod.GetColor(item.Color))
                                            };
                            var textBlock = new TextBlock
                                            {
                                                Text = item.Aircraft,
                                                Style = CurrentFleetTrend.Resources.FirstOrDefault(
                                    p => p.Key.ToString().Equals("legendItemStyle", StringComparison.OrdinalIgnoreCase))
                                    .Value as Style
                                            };
                            barPanel.Children.Add(rectangle);
                            barPanel.Children.Add(textBlock);
                            stackPanel.Children.Add(barPanel);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     控制趋势图的Y轴和折线及标签颜色
        /// </summary>
        private void SetRadCartesianChartColor()
        {
            Dictionary<string, string> colorDictionary = GetColorDictionary();
            //控制折线趋势图的Y轴颜色
            foreach (
                var item in
                    ((_lineGrid.Children[0] as RadCartesianChart).Resources["AdditionalVerticalAxis"] as AxisCollection)
                )
            {
                var linearAxis = item as LinearAxis;
                if (linearAxis != null &&
                    linearAxis.Title.ToString().Equals("飞机数（架）", StringComparison.OrdinalIgnoreCase))
                {
                    linearAxis.ElementBrush = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                }
            }
            //控制折线趋势图的线条颜色
            foreach (var item in ((_lineGrid.Children[0] as RadCartesianChart).Series))
            {
                var linearSeries = item as LineSeries;
                if (linearSeries != null)
                {
                    if (linearSeries.DisplayName.Equals("期末飞机数（子）", StringComparison.OrdinalIgnoreCase))
                    {
                        linearSeries.Stroke = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数（子）"]));
                    }
                    if (linearSeries.DisplayName.Equals("期末飞机数", StringComparison.OrdinalIgnoreCase))
                    {
                        linearSeries.Stroke = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                    }
                }
            }

            //控制折线趋势图的标签颜色
            foreach (var item in ((_lineGrid.Children[1] as ScrollViewer).Content as StackPanel).Children)
            {
                var stackPanel = item as StackPanel;
                if (stackPanel != null)
                {
                    var checkBox = stackPanel.Children[0] as CheckBox;
                    if (checkBox != null)
                    {
                        if (checkBox.Content.ToString().Equals("期末飞机数（子）", StringComparison.OrdinalIgnoreCase))
                        {
                            stackPanel.Background =
                                new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数（子）"]));
                        }
                        if (checkBox.Content.ToString().Equals("期末飞机数", StringComparison.OrdinalIgnoreCase))
                        {
                            stackPanel.Background = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                        }
                    }
                }
            }


            //控制柱状趋势图的Y轴颜色
            foreach (
                var item in
                    ((_barGrid.Children[0] as RadCartesianChart).Resources["AdditionalVerticalAxis"] as AxisCollection))
            {
                var linearAxis = item as LinearAxis;
                if (linearAxis != null &&
                    linearAxis.Title.ToString().Equals("飞机净增（架）", StringComparison.OrdinalIgnoreCase))
                {
                    linearAxis.ElementBrush = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                }
            }

            //控制柱状趋势图的标签颜色
            foreach (var item in ((_barGrid.Children[1] as ScrollViewer).Content as StackPanel).Children)
            {
                var stackPanel = item as StackPanel;
                var checkBox = stackPanel.Children[0] as CheckBox;
                if (checkBox != null)
                {
                    if (checkBox.Content.ToString().Equals("飞机净增数（子）", StringComparison.OrdinalIgnoreCase))
                    {
                        stackPanel.Background = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数（子）"]));
                    }
                    if (checkBox.Content.ToString().Equals("飞机净增数", StringComparison.OrdinalIgnoreCase))
                    {
                        stackPanel.Background = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                    }
                }
            }
        }

        /// <summary>
        ///     饼状图标签的选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PiePanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选中航空公司的名称
            var stackPanel = sender as StackPanel;
            string shortName = (stackPanel.Children[1] as TextBlock).Text;

            //修改饼图标签中的突出显示
            foreach (var item in (stackPanel.Parent as StackPanel).Children)
            {
                var childStackPanel = item as StackPanel;
                var itemRectangle = childStackPanel.Children[0] as Rectangle;
                string itemShortName = (childStackPanel.Children[1] as TextBlock).Text;
                if (itemShortName.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                {
                    if (itemRectangle.Width == 12)
                    {
                        itemRectangle.Width = 15;
                        itemRectangle.Height = 15;
                    }
                    else
                    {
                        itemRectangle.Width = 12;
                        itemRectangle.Height = 12;
                    }
                }
                else
                {
                    itemRectangle.Width = 15;
                    itemRectangle.Height = 15;
                }
            }

            //修改对应饼图块状的突出显示
            var radPieChart =
                (((stackPanel.Parent as StackPanel).Parent as ScrollViewer).Parent as Grid).Children[0] as RadPieChart;
            foreach (var item in radPieChart.Series[0].DataPoints)
            {
                var pieDataPoint = item;
                if ((pieDataPoint.DataItem as FleetAircraft).Aircraft.Equals(shortName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    pieDataPoint.IsSelected = !pieDataPoint.IsSelected;
                    if (pieDataPoint.IsSelected)
                    {
                        if (radPieChart.EmptyContent.ToString().Equals("飞机数分布", StringComparison.OrdinalIgnoreCase))
                        {
                            GetGridViewDataSourse(pieDataPoint, _aircraftWindow, "飞机数");
                        }
                    }
                    else
                    {
                        if (radPieChart.EmptyContent.ToString().Equals("飞机数分布", StringComparison.OrdinalIgnoreCase))
                        {
                            _aircraftWindow.Close();
                        }
                    }
                }
                else
                {
                    pieDataPoint.IsSelected = false; 
                }
            }
        }

        /// <summary>
        ///     控制趋势图中折线（饼状）的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var grid =
                    (((checkBox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_lineGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                        p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase))
                        .Visibility = Visibility.Visible;
                }
                else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_barGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                        p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase))
                        .Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        ///     控制趋势图中折线（饼状）的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var grid =
                    (((checkBox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_lineGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                        p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase))
                        .Visibility = Visibility.Collapsed;
                }
                else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_barGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                        p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase))
                        .Visibility = Visibility.Collapsed;
                }
            }
        }

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
        }

        #endregion

        #region Class

        /// <summary>
        ///     饼图的分布对象
        /// </summary>
        public class FleetAircraft
        {
            public FleetAircraft()
        {
                Color = new CommonMethod().GetRandomColor();
            }

            public string Aircraft { get; set; } //飞机相关的名称
            public decimal Amount { get; set; } //分布的计数
            public string ToolTip { get; set; } //显示的提示
            public string Color { get; set; } //航空公司颜色
        }

        /// <summary>
        ///     趋势图的对象
        /// </summary>
        public class FleetAircraftTrend
            {
            public string Aircraft { get; set; } //飞机相关的名称
            public string DateTime { get; set; } //时间点
            public int AircraftAmount { get; set; } //飞机数的总数（子）
            public int AircraftAmount1 { get; set; } //飞机数的总数
            public string AircraftColor { get; set; } //飞机数的颜色（子）
            public string AircraftColor1 { get; set; } //飞机数的颜色
        }

        #endregion
    }
}
