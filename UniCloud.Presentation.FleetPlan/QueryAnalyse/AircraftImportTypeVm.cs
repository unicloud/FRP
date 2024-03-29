﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:25:03
// 文件名：AircraftImportTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:25:03
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof (AircraftImportTypeVm))]
    public class AircraftImportTypeVm : ViewModelBase
    {
        #region 声明、初始化

        private static readonly CommonMethod Commonmethod = new CommonMethod();
        private readonly FleetPlanData _fleetPlanContext;
        private readonly RadWindow _importTypeWindow = new RadWindow(); //用于单击飞机引进方式饼状图的用户提示
        private RadGridView _aircraftDetail; //初始化RadGridView

        private RadGridView _exportRadgridview; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _importTypePieGrid; //趋势折线图区域，趋势柱状图区域， 飞机引进方式饼图区域
        private Grid _lineGrid; //趋势折线图区域，趋势柱状图区域， 飞机引进方式饼图区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        [ImportingConstructor]
        public AircraftImportTypeVm(IFleetPlanService service)
            : base(service)
        {
            _fleetPlanContext = service.Context;

            ViewModelInitializer();
            InitalizerRadWindows(_importTypeWindow, "ImportType", 220);
            AddRadMenu(_importTypeWindow);
            InitializeVm();
        }

        public AircraftImportType CurrentAircraftImportType
        {
            get { return ServiceLocator.Current.GetInstance<AircraftImportType>(); }
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
            var baseManagementService = ServiceLocator.Current.GetInstance<IBaseManagementService>();
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(baseManagementService.Context,
                baseManagementService.Context.XmlSettings);
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
            ExportCommand = new DelegateCommand<object>(OnExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView);
            ToggleButtonCommand = new DelegateCommand<object>(ToggleButtonCheck);

            _lineGrid = CurrentAircraftImportType.LineGrid;
            _importTypePieGrid = CurrentAircraftImportType.ImportTypePieGrid;
            _aircraftDetail = CurrentAircraftImportType.AircraftDetail;
        }

        #endregion

        #region 数据

        #region 公共数据

        private ObservableCollection<FleetData> _fleetDatas;
        private ObservableCollection<FleetImportTypeTrend> _importTypes;
        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; } //XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

        public ObservableCollection<FleetData> FleetDatas
        {
            get { return _fleetDatas; }
            set
            {
                _fleetDatas = value;
                RaisePropertyChanged("FleetDatas");
            }
        }

        private ObservableCollection<FleetData> StaticFleetDatas { get; set; }

        public ObservableCollection<FleetImportTypeTrend> ImportTypes
        {
            get { return _importTypes; }
            set
            {
                _importTypes = value;
                RaisePropertyChanged("ImportTypes");
            }
        }

        #region ViewModel 属性 SelectedTime --所选的时间点

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
                    CloseImportTypeWindow();
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeImportType = "所选时间的飞机引进方式分布图";
                    }
                    else
                    {
                        SelectedTimeImportType = SelectedTime + "末的飞机引进方式分布图";
                    }
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeImportType--飞机引进方式饼图的标识提示

        private string _selectedTimeImportType = "所选时间的飞机引进方式分布图";

        /// <summary>
        ///     飞机引进方式饼图的标识提示
        /// </summary>
        public string SelectedTimeImportType
        {
            get { return _selectedTimeImportType; }
            set
            {
                if (SelectedTimeImportType != value)
                {
                    _selectedTimeImportType = value;
                    RaisePropertyChanged(() => SelectedTimeImportType);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftCount --飞机详细列表的标识栏提示

        private string _aircraftCount = "飞机明细";

        /// <summary>
        ///     飞机详细列表的标识栏提示
        /// </summary>
        public string AircraftCount
        {
            get { return _aircraftCount; }
            set
            {
                if (AircraftCount != value)
                {
                    _aircraftCount = value;
                    RaisePropertyChanged(() => AircraftCount);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftCollection --机龄饼图所对应的所有飞机数据（指定时间点）

        private List<AircraftDTO> _aircraftCollection; //机龄饼图所对应的所有飞机数据（指定时间点）

        /// <summary>
        ///     机龄饼图所对应的所有飞机数据（指定时间点）
        /// </summary>
        public List<AircraftDTO> AircraftCollection
        {
            get { return _aircraftCollection; }
            set
            {
                _aircraftCollection = value;
                RaisePropertyChanged("AircraftCollection");

                if (!SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                {
                    if (AircraftCollection == null || !AircraftCollection.Any())
                    {
                        AircraftCount = "飞机明细（0架）";
                    }
                    else
                    {
                        AircraftCount = "飞机明细（" + AircraftCollection.Count + "架）";
                    }
                }
                else
                {
                    AircraftCount = "飞机明细";
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftColor --期末飞机数的颜色

        private string _aircraftColor = Commonmethod.GetRandomColor();

        /// <summary>
        ///     期末飞机数的颜色
        /// </summary>
        public string AircraftColor
        {
            get { return _aircraftColor; }
            set
            {
                if (AircraftColor != value)
                {
                    _aircraftColor = value;
                    RaisePropertyChanged(() => AircraftColor);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetImportTypeTrendCollection --飞机引进方式趋势图的数据源集合

        private List<FleetImportTypeTrend> _fleetImportTypeTrendCollection;

        /// <summary>
        ///     飞机引进方式趋势图的数据源集合
        /// </summary>
        public List<FleetImportTypeTrend> FleetImportTypeTrendCollection
        {
            get { return _fleetImportTypeTrendCollection; }
            set
            {
                if (FleetImportTypeTrendCollection != value)
                {
                    _fleetImportTypeTrendCollection = value;
                    ImportTypeTrendInitialize(FleetImportTypeTrendCollection);
                    RaisePropertyChanged(() => FleetImportTypeTrendCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftAmountCollection --飞机引进方式趋势图的飞机总数集合

        private List<FleetImportTypeTrend> _aircraftAmountCollection;

        /// <summary>
        ///     飞机引进方式趋势图的飞机总数集合
        /// </summary>
        public List<FleetImportTypeTrend> AircraftAmountCollection
        {
            get { return _aircraftAmountCollection; }
            set
            {
                if (AircraftAmountCollection != value)
                {
                    _aircraftAmountCollection = value;
                    if (AircraftAmountCollection != null)
                    {
                        //控制趋势图的滚动条
                        ControlTrendScroll(AircraftAmountCollection);
                    }
                    RaisePropertyChanged(() => AircraftAmountCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetImportTypeCollection--飞机引进方式饼图的数据集合（指定时间点）

        private IEnumerable<FleetImportTypeComposition> _fleetImportTypeCollection;

        /// <summary>
        ///     飞机引进方式饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetImportTypeComposition> FleetImportTypeCollection
        {
            get { return _fleetImportTypeCollection; }
            set
            {
                if (!Equals(_fleetImportTypeCollection, value))
                {
                    _fleetImportTypeCollection = value;
                    RaisePropertyChanged(() => FleetImportTypeCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftCollection --飞机引进方式饼图所对应的所有飞机数据（指定时间点）

        private List<AircraftDTO> _aircraftList; //飞机实体数据集合

        public List<AircraftDTO> AircraftList
        {
            get { return _aircraftList; }
            set
            {
                _aircraftList = value;
                RaisePropertyChanged(() => AircraftList);
                if (SelectedTime != "所选时间")
                {
                    if (_aircraftList == null || !_aircraftList.Any())
                    {
                        AircraftCount = "飞机明细（0架）";
                    }
                    else
                    {
                        AircraftCount = "飞机明细（" + _aircraftList.Count() + "架）";
                    }
                }
                else
                {
                    AircraftCount = "飞机明细";
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
                    CreateFleetImportTypeTrendCollection();
                    RaisePropertyChanged(() => SelectedIndex);
                }
            }
        }

        #endregion

        #region ViewModel 属性 StartDate --开始时间

        private DateTime? _startDate = new DateTime(2000, 1, 1);

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
                    _startDate = value;
                    RaisePropertyChanged(() => StartDate);
                    CreateFleetImportTypeTrendCollection();
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
                    _endDate = value;
                    RaisePropertyChanged(() => EndDate);
                    CreateFleetImportTypeTrendCollection();
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

        #endregion

        #region 加载数据

        public override void LoadData()
        {
            if (!XmlConfigs.AutoLoad)
                XmlConfigs.AutoLoad = true;
            if (!XmlSettings.AutoLoad)
                XmlSettings.AutoLoad = true;
            if (!Aircrafts.AutoLoad)
                Aircrafts.AutoLoad = true;
            IsBusy = XmlConfigs.IsBusy && XmlSettings.IsBusy && Aircrafts.IsBusy;
        }

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

        #endregion

        #endregion

        #region 操作

        public DelegateCommand<object> ToggleButtonCommand { get; set; }

        /// <summary>
        ///     关闭飞机引进方式子窗口
        /// </summary>
        public void CloseImportTypeWindow()
        {
            _importTypeWindow.Close();
        }

        /// <summary>
        ///     飞机引进方式趋势图的数据绑定界面
        /// </summary>
        public void ImportTypeTrendInitialize(List<FleetImportTypeTrend> fleetImportTypeTrendCollection)
        {
            FleetDatas = new ObservableCollection<FleetData>();
            StaticFleetDatas = new ObservableCollection<FleetData>();
            var result = new ObservableCollection<FleetData>();
            var importTypeResult = new ObservableCollection<FleetImportTypeTrend>();

            if (FleetImportTypeTrendCollection != null)
            {
                foreach (var groupItem in FleetImportTypeTrendCollection.GroupBy(p => p.ImportType).ToList())
                {
                    var fleetImportTypeTrend = groupItem.FirstOrDefault();
                    if (fleetImportTypeTrend != null)
                    {
                        var tempData = new FleetData
                        {
                            ImportTypeName = groupItem.Key,
                            Data = new ObservableCollection<FleetImportTypeTrend>()
                        };
                        groupItem.ToList().ForEach(tempData.Data.Add);
                        result.Add(tempData);
                        importTypeResult.Add(fleetImportTypeTrend);
                    }
                }
            }
            result.ToList().ForEach(FleetDatas.Add);
            result.ToList().ForEach(StaticFleetDatas.Add);
            ImportTypes = importTypeResult;
        }

        /// <summary>
        ///     控制趋势图的滚动条
        /// </summary>
        /// <param name="aircraftAmountCollection"></param>
        public void ControlTrendScroll(List<FleetImportTypeTrend> aircraftAmountCollection)
        {
            //控制趋势图的滚动条
            if (AircraftAmountCollection != null && AircraftAmountCollection.Count() >= 12)
            {
                CurrentAircraftImportType.LineCategoricalAxis.MajorTickInterval = AircraftAmountCollection.Count()/6;
                CurrentAircraftImportType.BarCategoricalAxis.MajorTickInterval = AircraftAmountCollection.Count()/6;
            }
            else
            {
                CurrentAircraftImportType.LineCategoricalAxis.MajorTickInterval = 1;
                CurrentAircraftImportType.BarCategoricalAxis.MajorTickInterval = 1;
            }
        }

        /// <summary>
        ///     滚动条初始化
        /// </summary>
        public void ZoomInitialize()
        {
            Zoom = new Size(1, 1);
        }

        /// <summary>
        ///     弹出窗体关闭时，取消相应饼图的弹出项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadwindowClosed(object sender, WindowClosedEventArgs e)
        {
            var radWindow = sender as RadWindow;
            var grid = new Grid();
            if (radWindow != null && radWindow.Name.Equals("ImportType", StringComparison.OrdinalIgnoreCase))
            {
                grid = _importTypePieGrid;
            }

            //更改对应饼图的突出显示
            var radPieChart = grid.Children[0] as RadPieChart;
            if (radPieChart != null)
                foreach (var item in radPieChart.Series[0].DataPoints)
                {
                    item.IsSelected = false;
                }
            //更改对应饼图的标签大小
            ((RadLegend) grid.Children[1]).Items.ToList().ForEach(p => p.IsHovered = false);
        }

        /// <summary>
        ///     初始化子窗体
        /// </summary>
        public void InitalizerRadWindows(RadWindow radWindow, string windowsName, int length)
        {
            //运营计划子窗体的设置
            radWindow.Name = windowsName;
            radWindow.Top = length;
            radWindow.Left = length;
            radWindow.Height = 300;
            radWindow.Width = 600;
            radWindow.ResizeMode = ResizeMode.CanResize;
            radWindow.Content = Commonmethod.CreatOperationGridView();
            radWindow.Closed += RadwindowClosed;
        }

        /// <summary>
        ///     趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            var chartSelectionBehavior = sender as ChartSelectionBehavior;
            if (chartSelectionBehavior != null)
            {
                var selectedPoint =
                    chartSelectionBehavior.Chart.SelectedPoints.FirstOrDefault(
                        p =>
                        {
                            var categoricalSeries = p.Presenter as CategoricalSeries;
                            return categoricalSeries != null && categoricalSeries.Visibility == Visibility.Visible;
                        });
                if (selectedPoint != null)
                {
                    var fleetImportTypeTrend = selectedPoint.DataItem as FleetImportTypeTrend;
                    if (fleetImportTypeTrend != null && SelectedTime != fleetImportTypeTrend.DateTime)
                    {
                        //选中时间点
                        SelectedTime = fleetImportTypeTrend.DateTime;
                        var time = Convert.ToDateTime(fleetImportTypeTrend.DateTime).AddMonths(1).AddDays(-1);
                        ChartSelectionBehaviorSelection(time);
                    }
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
                var radChartBase = chartSelectionBehavior.Chart;
                var selectedPoint = radChartBase.SelectedPoints.FirstOrDefault() as PieDataPoint;

                if (selectedPoint != null)
                {
                    var items = ((RadLegend) _importTypePieGrid.Children[1]).Items;
                    items.ToList().ForEach(p => p.IsHovered = false);
                    foreach (var item in items)
                    {
                        if (item.Title.Equals(((FleetImportTypeComposition) selectedPoint.DataItem).ImportType,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            item.IsHovered = true;
                            break;
                        }
                    }
                    if (radChartBase.EmptyContent.ToString().Equals("飞机引进方式分布", StringComparison.OrdinalIgnoreCase))
                    {
                        GetGridViewDataSource(selectedPoint, _importTypeWindow, "飞机引进方式");
                    }
                }
                else
                {
                    if (radChartBase.EmptyContent.ToString().Equals("飞机引进方式分布", StringComparison.OrdinalIgnoreCase))
                    {
                        _importTypeWindow.Close();
                    }
                }
            }
        }

        /// <summary>
        ///     控制趋势图中折线（饼状）的显示/隐藏
        /// </summary>
        /// <param name="sender"></param>
        private void ToggleButtonCheck(object sender)
        {
            var button = sender as RadToggleButton;
            if (button != null)
            {
                if (button.IsChecked != null && (bool) button.IsChecked)
                {
                    var temp =
                        StaticFleetDatas.FirstOrDefault(
                            p => p.ImportTypeName.Equals((string) button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (temp != null &&
                        !FleetDatas.Any(
                            p => p.ImportTypeName.Equals(temp.ImportTypeName, StringComparison.OrdinalIgnoreCase)))
                    {
                        FleetDatas.Add(temp);
                    }
                }
                else
                {
                    for (var i = FleetDatas.Count - 1; i > -1; i--)
                    {
                        var temp = FleetDatas[i];
                        if (temp.ImportTypeName.Equals((string) button.Tag, StringComparison.OrdinalIgnoreCase))
                        {
                            FleetDatas.Remove(temp);
                            break;
                        }
                    }
                }
            }
        }

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
        }

        #region Command

        #region ViewModel 命令 --导出图表

        public DelegateCommand<object> ExportCommand { get; set; }

        private void OnExport(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出源数据", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("TrendGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string>
                    {
                        {"DateTime", "时间点"},
                        {"ImportType", "飞机引进方式"},
                        {"AirNum", "飞机数"},
                        {"Amount", "期末飞机数"}
                    };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetImportTypeTrendCollection, "LineImportType");

                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (
                        var stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                        )
                    {
                        if (stream != null)
                        {
                            _exportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (menu.Name.Equals("ImportTypePieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetImportTypeCollection == null || !FleetImportTypeCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> {{"ImportType", "飞机引进方式"}, {"AirNum", "飞机数（架）"}};
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetImportTypeCollection,
                        "PieImportType");

                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (
                        var stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
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
                if (menu.Name.Equals("TrendGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_lineGrid != null)
                    {
                        Commonmethod.ExportToImage(_lineGrid.Parent as Grid);
                    }
                }
                else if (menu.Name.Equals("ImportTypePieGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_importTypePieGrid != null)
                    {
                        Commonmethod.ExportToImage(_importTypePieGrid);
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
            // ReSharper disable once CSharpWarnings::CS0618
            e.Width = 120;
            if (e.Element == ExportElement.Cell && e.Value != null)
            {
                var radGridView = sender as RadGridView;
                if (radGridView != null && (_i%5 == 3 && _i >= 8 &&
                                            radGridView.Name.Equals("LineImportType", StringComparison.OrdinalIgnoreCase)))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }

        #endregion

        #region ViewModel 命令 --导出数据aircraftDetail

        public DelegateCommand<object> ExportGridViewCommand { get; set; }

        private void OnExportGridView(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出数据", StringComparison.OrdinalIgnoreCase) &&
                _aircraftDetail != null)
            {
                _aircraftDetail.ElementExporting -= ElementExporting;
                _aircraftDetail.ElementExporting += ElementExporting;
                using (var stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        _aircraftDetail.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
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
            if (rmi != null)
            {
                var radcm = rmi.Parent as RadContextMenu;
                if (radcm != null) radcm.StaysOpen = false;
            }
            RadGridView rgview = null;
            if (rmi != null && rmi.DataContext.ToString().Equals("ImportType", StringComparison.OrdinalIgnoreCase))
            {
                rgview = _importTypeWindow.Content as RadGridView;
            }
            if (rgview != null)
            {
                rgview.ElementExporting -= ElementExporting;
                rgview.ElementExporting += ElementExporting;
                using (var stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        rgview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
        }


        /// <summary>
        ///     初始化数据
        /// </summary>
        private void InitializeData()
        {
            if (_loadXmlConfig && _loadXmlSetting)
            {
                _loadXmlSetting = false;
                _loadXmlConfig = false;
                IsBusy = false;
                CreateFleetImportTypeTrendCollection();
            }
        }

        /// <summary>
        ///     获取飞机引进方式趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreateFleetImportTypeTrendCollection()
        {
            var importTypeCollection = new List<FleetImportTypeTrend>();
            var amountCollection = new List<FleetImportTypeTrend>();

            #region 飞机引进方式XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("飞机引进方式", StringComparison.OrdinalIgnoreCase));

            var colorSetting =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorSetting != null && XElement.Parse(colorSetting.SettingContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                var aircraftColor = XElement.Parse(colorSetting.SettingContent)
                    .Descendants("Type")
                    .FirstOrDefault(
                        p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));

                if (aircraftColor != null)
                {
                    var firstOrDefault = aircraftColor.Descendants("Item")
                        .FirstOrDefault(p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                    if (firstOrDefault != null)
                        AircraftColor = firstOrDefault.Attribute("Color").Value;
                }
                else
                {
                    AircraftColor = Commonmethod.GetRandomColor();
                }
            }

            XElement importColor = null;
            if (colorSetting != null && XElement.Parse(colorSetting.SettingContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase)))
            {
                importColor = XElement.Parse(colorSetting.SettingContent)
                    .Descendants("Type")
                    .FirstOrDefault(
                        p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                var xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (var datetime in xelement.Descendants("DateTime"))
                    {
                        var currentTime =
                            Convert.ToDateTime(datetime.Attribute("EndOfMonth").Value).ToString("yyyy/M");

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

                        foreach (var type in datetime.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("飞机引进方式"))
                            {
                                var currentAmount = type.Attribute("Amount").Value;

                                //飞机总数柱状集合
                                var aircraftAmount = new FleetImportTypeTrend
                                {
                                    DateTime = currentTime,
                                    Amount = Convert.ToInt32(currentAmount),
                                    Color = AircraftColor
                                };
                                amountCollection.Add(aircraftAmount);

                                foreach (var item in type.Descendants("Item"))
                                {
                                    //飞机引进方式折线集合
                                    var fleetImportTypeTrend = new FleetImportTypeTrend
                                    {
                                        ImportType = item.Attribute("Name").Value,
                                        DateTime = currentTime,
                                        AirNum = Convert.ToInt32(item.Value),
                                        Amount = Convert.ToInt32(currentAmount)
                                    };
                                    if (importColor != null)
                                    {
                                        var firstOrDefault = importColor.Descendants("Item")
                                            .FirstOrDefault(
                                                p =>
                                                    p.Attribute("Name")
                                                        .Value.Equals(fleetImportTypeTrend.ImportType,
                                                            StringComparison.OrdinalIgnoreCase));
                                        if (firstOrDefault != null)
                                            fleetImportTypeTrend.Color = firstOrDefault.Attribute("Color").Value;
                                    }
                                    importTypeCollection.Add(fleetImportTypeTrend);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            SelectedTime = "所选时间";
            ZoomInitialize();
            if (importTypeCollection.Any() && amountCollection.Any())
            {
                FleetImportTypeTrendCollection = importTypeCollection;
                AircraftAmountCollection = amountCollection;
            }
        }

        /// <summary>
        ///     是否相同的飞机引进方式
        /// </summary>
        /// <param name="importType1"></param>
        /// <param name="importType2"></param>
        /// <returns></returns>
        protected bool SameImportType(string importType1, string importType2)
        {
            var index1 = importType1.IndexOf("续租", StringComparison.OrdinalIgnoreCase);
            if (index1 > 0)
            {
                return importType1.Contains(importType2);
            }
            var index2 = importType2.IndexOf("续租", StringComparison.OrdinalIgnoreCase);
            if (index2 > 0)
            {
                return importType2.Contains(importType1);
            }
            return importType1.Equals(importType2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     趋势图的选择点事件数据源
        /// </summary>
        public void ChartSelectionBehaviorSelection(DateTime time)
        {
            var aircraft = Aircrafts.Where(
                o => o.OperationHistories.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)))
                .Where(o =>
                {
                    var operationHistoryDto =
                        o.OperationHistories.FirstOrDefault(
                            p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time));
                    return operationHistoryDto != null &&
                           operationHistoryDto.ImportActionType.Equals("引进", StringComparison.OrdinalIgnoreCase);
                });

            #region 飞机引进方式XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("飞机引进方式", StringComparison.OrdinalIgnoreCase));

            XElement importTypeColor = null;
            var colorConfig =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase)))
            {
                importTypeColor = XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .FirstOrDefault(
                        p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                var importTypeList = new List<FleetImportTypeComposition>(); //飞机引进方式饼图集合

                var xelement = XElement.Parse(xmlConfig.ConfigContent)
                    .Descendants("DateTime")
                    .FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                if (xelement != null)
                {
                    foreach (var type in xelement.Descendants("Type"))
                    {
                        if (type.Attribute("TypeName").Value.Equals("飞机引进方式", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (var item in type.Descendants("Item"))
                            {
                                var fleetimporttypecomposition = new FleetImportTypeComposition
                                {
                                    ImportType = item.Attribute("Name").Value,
                                    AirNum = Convert.ToInt32(item.Value),
                                    AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                };
                                if (importTypeColor != null)
                                {
                                    var firstOrDefault = importTypeColor.Descendants("Item")
                                        .FirstOrDefault(
                                            p =>
                                                p.Attribute("Name")
                                                    .Value.Equals(fleetimporttypecomposition.ImportType,
                                                        StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetimporttypecomposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetimporttypecomposition.AirNum > 0)
                                {
                                    importTypeList.Add(fleetimporttypecomposition);
                                }
                            }
                        }
                    }
                }
                FleetImportTypeCollection = importTypeList;
            }

            #endregion

            if (aircraft != null)
            {
                //飞机详细数据集合
                AircraftCollection = Commonmethod.GetAircraftByTime(aircraft.ToList(), time);
            }
        }

        /// <summary>
        ///     根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="header">窗体标示</param>
        protected void GetGridViewDataSource(PieDataPoint selectedItem, RadWindow radWindow, string header)
        {
            if (selectedItem != null && radWindow != null)
            {
                var fleetImportTypeComposition = selectedItem.DataItem as FleetImportTypeComposition;
                if (fleetImportTypeComposition != null)
                {
                    var time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
                    var aircraft = Aircrafts.Where(o => o.OperationHistories.Any(
                        a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)))
                        .Where(o =>
                        {
                            var operationHistoryDto = o.OperationHistories.FirstOrDefault(
                                a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time));
                            return operationHistoryDto != null &&
                                   operationHistoryDto.ImportActionType.Equals("引进", StringComparison.OrdinalIgnoreCase);
                        });
                    var airlineAircrafts = aircraft.Where(p =>
                    {
                        var firstOrDefault = p.OperationHistories.
                            FirstOrDefault(
                                a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time));
                        return firstOrDefault != null &&
                               SameImportType(firstOrDefault.ImportActionName, fleetImportTypeComposition.ImportType);
                    }).ToList();
                    //找到子窗体的RadGridView，并为其赋值
                    var rgv = radWindow.Content as RadGridView;
                    if (rgv != null) rgv.ItemsSource = Commonmethod.GetAircraftByTime(airlineAircrafts, time);
                    radWindow.Header = header + " " + fleetImportTypeComposition.ImportType + "：" +
                                       fleetImportTypeComposition.AirTt;
                    if (!radWindow.IsOpen)
                    {
                        Commonmethod.ShowRadWindow(radWindow);
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Class

        public class FleetData
        {
            public string ImportTypeName { get; set; }
            public ObservableCollection<FleetImportTypeTrend> Data { get; set; }
        }

        /// <summary>
        ///     饼图的分布对象
        /// </summary>
        public class FleetImportTypeComposition
        {
            public FleetImportTypeComposition()
            {
                Color = Commonmethod.GetRandomColor();
            }

            public string ImportType { get; set; }
            public decimal AirNum { get; set; }
            public string AirTt { get; set; }
            public string Color { get; set; }
        }

        /// <summary>
        ///     飞机引进方式趋势对象
        /// </summary>
        public class FleetImportTypeTrend
        {
            public FleetImportTypeTrend()
            {
                Color = Commonmethod.GetRandomColor();
            }

            public string ImportType { get; set; } //引进名称
            public int AirNum { get; set; } //飞机引进方式的飞机数
            public string DateTime { get; set; } //时间点
            public int Amount { get; set; } //飞机总数
            public string Color { get; set; } //引进的颜色
        }

        #endregion
    }
}