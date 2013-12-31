#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:30:01
// 文件名：EngineImportTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:30:01
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
using System.Windows.Media;
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
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion


namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(EngineImportTypeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineImportTypeVm : ViewModelBase
    {
        #region 声明、初始化
        private FleetPlanData _fleetPlanDataService;
        public EngineImportType CurrentEngineImportType
        {
            get { return ServiceLocator.Current.GetInstance<EngineImportType>(); }
        }
        private int _i; //导出数据源格式判断
        private Grid _lineGrid, _barGrid, _importTypePieGrid;//趋势折线图区域，趋势柱状图区域， 发动机引进方式饼图区域
        private RadDateTimePicker _startDateTimePicker, _endDateTimePicker;//开始时间控件， 结束时间控件
        private RadGridView _exportRadgridview, _engineDetail; //初始化RadGridView
        private RadWindow _importTypeWindow = new RadWindow(); //用于单击发动机引进方式饼状图的用户提示
        private static readonly CommonMethod Commonmethod = new CommonMethod();
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        public EngineImportTypeVm()
        {
            ExportCommand = new DelegateCommand<object>(OnExport, CanExport);//导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView, CanExportGridView);
            ViewModelInitializer();
            InitalizerRadWindows(_importTypeWindow, "ImportType", 220);
            AddRadMenu(_importTypeWindow);
            InitializeVm();
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
            XmlConfigs = new QueryableDataServiceCollectionView<XmlConfigDTO>(_fleetPlanDataService, _fleetPlanDataService.XmlConfigs);
            XmlConfigs.LoadedData += (o, e) =>
            {
                _loadXmlConfig = true;
                InitializeData();
            };
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(_fleetPlanDataService, _fleetPlanDataService.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                _loadXmlSetting = true;
                InitializeData();
            };
            Engines = new QueryableDataServiceCollectionView<EngineDTO>(_fleetPlanDataService, _fleetPlanDataService.Engines);
        }

        /// <summary>
        /// 以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            _lineGrid = CurrentEngineImportType.LineGrid;
            _barGrid = CurrentEngineImportType.BarGrid;
            _importTypePieGrid = CurrentEngineImportType.ImportTypePieGrid;
            _engineDetail = CurrentEngineImportType.EngineDetail;

            //控制界面起止时间控件的字符串格式化
            _startDateTimePicker = CurrentEngineImportType.StartDateTimePicker;
            _endDateTimePicker = CurrentEngineImportType.EndDateTimePicker;
            _startDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
            _endDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
        }
        #endregion

        #region 数据
        #region 公共数据
        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; }
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; }
        public QueryableDataServiceCollectionView<EngineDTO> Engines { get; set; }
        #region Property


        #region 属性 EngineDTO
        private EngineDTO _engine;//发动机数据
        public EngineDTO Engine
        {
            get
            {
                return _engine;
            }
            set
            {
                _engine = value;
                RaisePropertyChanged(() => Engine);
            }
        }
        #endregion

        #region ViewModel 属性 SelectedTime --所选的时间点
        private string _selectedTime = "所选时间";
        /// <summary>
        /// 所选的时间点
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
                        SelectedTimeImportType = "所选时间的发动机引进方式分布图";
                    }
                    else
                    {
                        SelectedTimeImportType = SelectedTime + "末的发动机引进方式分布图";
                    }
                }
            }
        }
        #endregion

        #region ViewModel 属性 SelectedTimeImportType--发动机引进方式饼图的标识提示
        private string _selectedTimeImportType = "所选时间的发动机引进方式分布图";
        /// <summary>
        /// 发动机引进方式饼图的标识提示
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

        #region ViewModel 属性 EngineCount --发动机详细列表的标识栏提示
        private string _engineCount = "发动机明细";
        /// <summary>
        /// 发动机详细列表的标识栏提示
        /// </summary>
        public string EngineCount
        {
            get { return _engineCount; }
            set
            {
                if (EngineCount != value)
                {
                    _engineCount = value;
                    RaisePropertyChanged(() => EngineCount);
                }
            }
        }
        #endregion

        #region ViewModel 属性 EngineColor --期末发动机数的颜色
        private string _engineColor = Commonmethod.GetRandomColor();
        /// <summary>
        /// 期末发动机数的颜色
        /// </summary>
        public string EngineColor
        {
            get { return _engineColor; }
            set
            {
                if (EngineColor != value)
                {
                    _engineColor = value;
                    RaisePropertyChanged(() => EngineColor);
                }
            }
        }
        #endregion

        #region ViewModel 属性 FleetImportTypeTrendCollection --发动机引进方式趋势图的数据源集合
        private List<FleetImportTypeTrend> _fleetImportTypeTrendCollection;
        /// <summary>
        /// 发动机引进方式趋势图的数据源集合
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

        #region ViewModel 属性 EngineAmountCollection --发动机引进方式趋势图的发动机总数集合
        private List<FleetImportTypeTrend> _engineAmountCollection;
        /// <summary>
        /// 发动机引进方式趋势图的发动机总数集合
        /// </summary>
        public List<FleetImportTypeTrend> EngineAmountCollection
        {
            get { return _engineAmountCollection; }
            set
            {
                if (EngineAmountCollection != value)
                {
                    _engineAmountCollection = value;
                    if (EngineAmountCollection != null)
                    {
                        //控制趋势图的滚动条
                        ControlTrendScroll(EngineAmountCollection);
                    }
                    RaisePropertyChanged(() => EngineAmountCollection);
                }
            }
        }
        #endregion

        #region ViewModel 属性 FleetImportTypeCollection--发动机引进方式饼图的数据集合（指定时间点）
        private IEnumerable<FleetImportTypeComposition> _fleetImportTypeCollection;
        /// <summary>
        /// 发动机引进方式饼图的数据集合（指定时间点）
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
                    SetPieMark(FleetImportTypeCollection, "ImportTypePieGrid");
                }
            }
        }

        #endregion

        #region ViewModel 属性 EngineCollection --发动机引进方式饼图所对应的所有发动机数据（指定时间点）
        private List<EngineDTO> _engineDataObjectList;//发动机实体数据集合
        /// <summary>
        /// 发动机引进方式饼图所对应的所有发动机数据（指定时间点）
        /// </summary>
        public List<EngineDTO> EngineDataObjectList
        {
            get { return _engineDataObjectList; }
            set
            {
                _engineDataObjectList = value;
                RaisePropertyChanged(() => EngineDataObjectList);
                if (SelectedTime != "所选时间")
                {
                    if (_engineDataObjectList == null || !_engineDataObjectList.Any())
                    {
                        EngineCount = "发动机明细（0架）";
                    }
                    else
                    {
                        EngineCount = "发动机明细（" + _engineDataObjectList.Count() + "架）";
                    }
                }
                else
                {
                    EngineCount = "发动机明细";
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedIndex --时间的统计方式
        private int _selectedIndex;
        /// <summary>
        /// 时间的统计方式
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
        private DateTime? _startDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);
        /// <summary>
        /// 开始时间
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
                        SelectedStartValueChange(_startDate);
                        return;
                    }
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
        /// 结束时间
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
                        SelectedEndValueChange(_endDate);
                        return;
                    }
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
        /// 控制右键菜单的打开
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
        protected override IService CreateService()
        {
            _fleetPlanDataService = new FleetPlanData(AgentHelper.FleetPlanServiceUri);
            return new FleetPlanService(_fleetPlanDataService);
        }

        public override void LoadData()
        {
            IsBusy = true;
            XmlConfigs.Load(true);
            XmlSettings.Load(true);
            //Engines.Load(true);
        }


        #region ViewModel 属性 Zoom --滚动条的对应
        private Size _zoom = new Size(1, 1);
        /// <summary>
        /// 滚动条的对应
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
        /// 滚动条的滑动
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

        #region 操作
        /// <summary>
        /// 关闭发动机引进方式子窗口
        /// </summary>
        public void CloseImportTypeWindow()
        {
            _importTypeWindow.Close();
        }

        /// <summary>
        /// 发动机引进方式趋势图的数据绑定界面
        /// </summary>
        public void ImportTypeTrendInitialize(List<FleetImportTypeTrend> fleetImportTypeTrendCollection)
        {
            var radCartesianChartRoot = _lineGrid.Children[0] as RadCartesianChart;
            var scrollViewer = _lineGrid.Children[1] as ScrollViewer;
            if (scrollViewer != null)
            {
                var stackPanel = scrollViewer.Content as StackPanel;
                if (radCartesianChartRoot != null) radCartesianChartRoot.Series.Clear();
                if (stackPanel != null)
                {
                    stackPanel.Children.Clear();
                    if (FleetImportTypeTrendCollection != null)
                    {
                        foreach (var groupItem in FleetImportTypeTrendCollection.GroupBy(p => p.ImportType).ToList())
                        {
                            var line = new LineSeries { StrokeThickness = 3, DisplayName = groupItem.Key };
                            var fleetImportTypeTrend = groupItem.FirstOrDefault();
                            if (fleetImportTypeTrend != null)
                                line.Stroke = new SolidColorBrush(Commonmethod.GetColor(fleetImportTypeTrend.Color));
                            line.CategoryBinding = Commonmethod.CreateBinding("DateTime");
                            line.ValueBinding = Commonmethod.CreateBinding("AirNum");
                            line.ItemsSource = groupItem.ToList();
                            var radCartesianChart = _lineGrid.Children[0] as RadCartesianChart;
                            if (radCartesianChart != null)
                                line.PointTemplate = radCartesianChart.Resources["LinePointTemplate"] as DataTemplate;
                            var cartesianChart = _lineGrid.Children[0] as RadCartesianChart;
                            if (cartesianChart != null)
                                line.TrackBallInfoTemplate = cartesianChart.Resources["LineTrackBallInfoTemplate"] as DataTemplate;
                            if (radCartesianChartRoot != null) radCartesianChartRoot.Series.Add(line);

                            var panel = new StackPanel
                            {
                                Margin = new Thickness(5, 5, 5, 5),
                                Orientation = Orientation.Horizontal
                            };
                            var importTypeTrend = groupItem.FirstOrDefault();
                            if (importTypeTrend != null)
                                panel.Background = new SolidColorBrush(Commonmethod.GetColor(importTypeTrend.Color));
                            var checkBox = new CheckBox { IsChecked = true };
                            checkBox.Checked += CheckboxChecked;
                            checkBox.Unchecked += CheckboxUnchecked;
                            checkBox.Content = groupItem.Key;
                            checkBox.Foreground = new SolidColorBrush(Colors.White);
                            checkBox.VerticalAlignment = VerticalAlignment.Center;
                            checkBox.Style = CurrentEngineImportType.Resources["LegengCheckBoxStyle"] as Style;
                            panel.Children.Add(checkBox);
                            stackPanel.Children.Add(panel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控制趋势图的滚动条
        /// </summary>
        /// <param name="engineAmountCollection"></param>
        public void ControlTrendScroll(List<FleetImportTypeTrend> engineAmountCollection)
        {
            //控制趋势图的滚动条
            if (engineAmountCollection != null && engineAmountCollection.Count() >= 12)
            {
                CurrentEngineImportType.LineCategoricalAxis.MajorTickInterval = engineAmountCollection.Count() / 6;
                CurrentEngineImportType.BarCategoricalAxis.MajorTickInterval = engineAmountCollection.Count() / 6;
            }
            else
            {
                CurrentEngineImportType.LineCategoricalAxis.MajorTickInterval = 1;
                CurrentEngineImportType.BarCategoricalAxis.MajorTickInterval = 1;
            }
        }

        /// <summary>
        /// 根据相应的饼图数据生成饼图标签
        /// </summary>
        /// <param name="fleetImportTypeCollection">饼图数据集合</param>
        public void SetPieMark(IEnumerable<FleetImportTypeComposition> fleetImportTypeCollection, string regionGrid)
        {
            if (regionGrid != "ImportTypePieGrid")
            {
                return;
            }

            var radPieChart = _importTypePieGrid.Children[0] as RadPieChart;
            var scrollViewer = _importTypePieGrid.Children[1] as ScrollViewer;
            if (scrollViewer != null)
            {
                var stackPanel = scrollViewer.Content as StackPanel;
                if (radPieChart != null)
                {
                    radPieChart.Series[0].SliceStyles.Clear();
                    if (stackPanel != null)
                    {
                        stackPanel.Children.Clear();
                        if (fleetImportTypeCollection == null)
                        {
                            return;
                        }
                        foreach (var item in fleetImportTypeCollection)
                        {
                            var setter = new Setter { Property = System.Windows.Shapes.Shape.FillProperty, Value = item.Color };
                            var style = new Style { TargetType = typeof(System.Windows.Shapes.Path) };
                            style.Setters.Add(setter);
                            radPieChart.Series[0].SliceStyles.Add(style);

                            var barPanel = new StackPanel();
                            barPanel.MouseLeftButtonDown += PiePanelMouseLeftButtonDown;
                            barPanel.Orientation = Orientation.Horizontal;
                            var rectangle = new System.Windows.Shapes.Rectangle();
                            rectangle.Width = 15;
                            rectangle.Height = 15;
                            rectangle.Fill = new SolidColorBrush(Commonmethod.GetColor(item.Color));
                            var textBlock = new TextBlock
                                            {
                                                Text = item.ImportType,
                                                Style =
                                                    CurrentEngineImportType.Resources.FirstOrDefault(p => p.Key.ToString().Equals("legendItemStyle", StringComparison.OrdinalIgnoreCase)).Value as Style
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
        /// 选择开始时间时
        /// </summary>
        /// <param name="dataTimeStart"></param>
        public void SelectedStartValueChange(DateTime? dataTimeStart)
        {
            _startDateTimePicker.SelectedValue = dataTimeStart;
        }

        /// <summary>
        /// 选择结束时间时
        /// </summary>
        /// <param name="dataTimeEnd"></param>
        public void SelectedEndValueChange(DateTime? dataTimeEnd)
        {
            _endDateTimePicker.SelectedValue = dataTimeEnd;
        }

        /// <summary>
        /// 滚动条初始化
        /// </summary>
        public void ZoomInitialize()
        {
            Zoom = new Size(1, 1);
        }

        /// <summary>
        /// 弹出窗体关闭时，取消相应饼图的弹出项
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
            var scrollViewer = grid.Children[1] as ScrollViewer;
            if (scrollViewer != null)
            {
                var stackPanel = scrollViewer.Content as StackPanel;
                if (stackPanel != null)
                    foreach (var item in stackPanel.Children)
                    {
                        var panel = item as StackPanel;
                        if (panel != null)
                        {
                            var rectangle = panel.Children[0] as System.Windows.Shapes.Rectangle;
                            if (rectangle != null)
                            {
                                rectangle.Width = 15;
                                rectangle.Height = 15;
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// 初始化子窗体
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
            radwindow.Content = Commonmethod.CreatOperationGridView();
            radwindow.Closed += RadwindowClosed;
        }

        /// <summary>
        /// 饼状图标签的选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PiePanelMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //选中航空公司的名称
            var stackPanelRoot = sender as StackPanel;
            if (stackPanelRoot != null)
            {
                var textBlock = stackPanelRoot.Children[1] as TextBlock;
                if (textBlock != null)
                {
                    string shortName = textBlock.Text;

                    //修改饼图标签中的突出显示
                    var stackPanel = stackPanelRoot.Parent as StackPanel;
                    if (stackPanel != null)
                        foreach (var item in stackPanel.Children)
                        {
                            var childStackPanel = item as StackPanel;
                            if (childStackPanel != null)
                            {
                                var itemRectangle = childStackPanel.Children[0] as System.Windows.Shapes.Rectangle;
                                var block = childStackPanel.Children[1] as TextBlock;
                                if (block != null)
                                {
                                    string itemShortName = block.Text;
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
                            }
                        }

                    //修改对应饼图块状的突出显示
                    var radPieChart = (((stackPanelRoot.Parent as StackPanel).Parent as ScrollViewer).Parent as Grid).Children[0] as RadPieChart;
                    foreach (var item in radPieChart.Series[0].DataPoints)
                    {
                        var pieDataPoint = item;
                        if ((pieDataPoint.DataItem as FleetImportTypeComposition).ImportType.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                        {
                            pieDataPoint.IsSelected = !pieDataPoint.IsSelected;
                            if (pieDataPoint.IsSelected)
                            {
                                if (radPieChart.EmptyContent.ToString().Equals("发动机引进方式分布", StringComparison.OrdinalIgnoreCase))
                                {
                                    GetGridViewDataSource(pieDataPoint, _importTypeWindow, "发动机引进方式");
                                }
                            }
                            else
                            {
                                if (radPieChart.EmptyContent.ToString().Equals("发动机引进方式分布", StringComparison.OrdinalIgnoreCase))
                                {
                                    _importTypeWindow.Close();
                                }
                            }
                        }
                        else
                        {
                            pieDataPoint.IsSelected = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            var chartSelectionBehavior = sender as ChartSelectionBehavior;
            if (chartSelectionBehavior != null)
            {
                var selectedPoint = chartSelectionBehavior.Chart.SelectedPoints.FirstOrDefault(p =>
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
        /// 发动机饼状图的选择点事件
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

                var stackPanel = new StackPanel();
                if (radChartBase.EmptyContent.ToString().Equals("发动机引进方式分布", StringComparison.OrdinalIgnoreCase))
                {
                    var scrollViewer = _importTypePieGrid.Children[1] as ScrollViewer;
                    if (scrollViewer != null)
                        stackPanel = scrollViewer.Content as StackPanel;
                }

                if (stackPanel != null)
                {
                    foreach (var item in stackPanel.Children)
                    {
                        var itemrectangle = (item as StackPanel).Children[0] as System.Windows.Shapes.Rectangle;
                        itemrectangle.Width = 15;
                        itemrectangle.Height = 15;
                    }

                    if (selectedPoint != null)
                    {
                        var childStackPanel = stackPanel.Children
                            .FirstOrDefault(p => ((p as StackPanel).Children[1] as TextBlock).Text.Equals((selectedPoint.DataItem as FleetImportTypeComposition).ImportType, StringComparison.OrdinalIgnoreCase)) as StackPanel;
                        var rectangle = childStackPanel.Children[0] as System.Windows.Shapes.Rectangle;
                        rectangle.Width = 12;
                        rectangle.Height = 12;

                        if (radChartBase.EmptyContent.ToString().Equals("发动机引进方式分布", StringComparison.OrdinalIgnoreCase))
                        {
                            GetGridViewDataSource(selectedPoint, _importTypeWindow, "发动机引进方式");
                        }
                    }
                    else
                    {
                        if (radChartBase.EmptyContent.ToString().Equals("发动机引进方式分布", StringComparison.OrdinalIgnoreCase))
                        {
                            _importTypeWindow.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控制趋势图中折线（饼状）的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var grid = (((checkBox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_lineGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Visible;
                }
                else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_barGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// 控制趋势图中折线（饼状）的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var stackPanel = checkBox.Parent as StackPanel;
                if (stackPanel != null)
                {
                    var panel = stackPanel.Parent as StackPanel;
                    if (panel != null)
                    {
                        var grid = (panel.Parent as ScrollViewer).Parent as Grid;
                        if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                        {
                            (_lineGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                        {
                            (_barGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkBox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
        }

        #endregion

        #endregion
        #endregion

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
                    //创建RadGridViewS
                    var columnsList = new Dictionary<string, string>
                                      {
                                          {"DateTime", "时间点"},
                                          {"ImportType", "发动机引进方式"},
                                          {"AirNum", "发动机数"},
                                          {"Amount", "期末发动机数"}
                                      };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetImportTypeTrendCollection, "LineImportType");

                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
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
                    var columnsList = new Dictionary<string, string> { { "ImportType", "发动机引进方式" }, { "AirNum", "发动机数（架）" } };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetImportTypeCollection, "PieImportType");

                    _i = 1;
                    _exportRadgridview.ElementExporting -= ElementExporting;
                    _exportRadgridview.ElementExporting += ElementExporting;
                    using (var stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
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
        /// 设置导出样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            e.Width = 120;
            if (e.Element == ExportElement.Cell && e.Value != null)
            {
                if (_i % 5 == 3 && _i >= 8 && (sender as RadGridView).Name.Equals("LineImportType", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }

        private bool _canExport = true;
        bool CanExport(object sender)
        {
            return _canExport;
        }

        #endregion

        #region ViewModel 命令 --导出数据EngineDetail
        public DelegateCommand<object> ExportGridViewCommand { get; set; }

        private void OnExportGridView(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出数据", StringComparison.OrdinalIgnoreCase) && _engineDetail != null)
            {
                _engineDetail.ElementExporting -= ElementExporting;
                _engineDetail.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                {
                    if (stream != null)
                    {
                        _engineDetail.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
        }
        private bool _canExportGridView = true;
        bool CanExportGridView(object sender)
        {
            return _canExportGridView;
        }

        #endregion

        #region  增加子窗体的右键导出功能

        public void AddRadMenu(RadWindow rwindow)
        {
            var radcm = new RadContextMenu();//新建右键菜单
            radcm.Opened += Radcm_Opened;
            var rmi = new RadMenuItem { Header = "导出表格" };//新建右键菜单项
            rmi.Click += MenuItemClick;//为菜单项注册事件
            rmi.DataContext = rwindow.Name;
            radcm.Items.Add(rmi);
            RadContextMenu.SetContextMenu(rwindow, radcm);//为控件绑定右键菜单
        }
        void Radcm_Opened(object sender, RoutedEventArgs e)
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
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
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
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            if (_loadXmlSetting && _loadXmlConfig)
            {
                _loadXmlConfig = false;
                _loadXmlSetting = false;
                IsBusy = false;
                CreateFleetImportTypeTrendCollection();
            }
        }

        /// <summary>
        /// 获取发动机引进方式趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreateFleetImportTypeTrendCollection()
        {
            var importTypeCollection = new List<FleetImportTypeTrend>();
            var amountCollection = new List<FleetImportTypeTrend>();

            #region 发动机引进方式XML文件的读写
            var xmlConfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("发动机引进方式", StringComparison.OrdinalIgnoreCase));

            var colorSetting = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorSetting != null && XElement.Parse(colorSetting.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                XElement engineColor = XElement.Parse(colorSetting.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));

                if (engineColor != null)
                {
                    var firstOrDefault = engineColor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals("发动机数", StringComparison.OrdinalIgnoreCase));
                    if (firstOrDefault != null)
                        EngineColor = firstOrDefault.Attribute("Color").Value;
                }
                else
                {
                    EngineColor = Commonmethod.GetRandomColor();
                }
            }

            XElement importColor = null;
            if (colorSetting != null && XElement.Parse(colorSetting.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase)))
            {
                importColor = XElement.Parse(colorSetting.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (XElement datetime in xelement.Descendants("DateTime"))
                    {
                        string currentTime = Convert.ToDateTime(datetime.Attribute("EndOfMonth").Value).ToString("yyyy/M");

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
                        if (SelectedIndex == 1)//按半年统计
                        {
                            if (Convert.ToDateTime(currentTime).Month != 6 && Convert.ToDateTime(currentTime).Month != 12)
                            {
                                continue;
                            }
                        }
                        else if (SelectedIndex == 2)//按年份统计
                        {
                            if (Convert.ToDateTime(currentTime).Month != 12)
                            {
                                continue;
                            }
                        }

                        foreach (XElement type in datetime.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("发动机引进方式", StringComparison.OrdinalIgnoreCase))
                            {
                                string currentAmount = type.Attribute("Amount").Value;
                                //发动机总数柱状集合
                                var engineAmount = new FleetImportTypeTrend
                                {
                                    DateTime = currentTime,
                                    Amount = Convert.ToInt32(currentAmount),
                                    Color = EngineColor
                                };
                                amountCollection.Add(engineAmount);

                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    //发动机引进方式折线集合
                                    var fleetImportTypeTrend = new FleetImportTypeTrend
                                    {
                                        ImportType = item.Attribute("Name").Value,
                                        DateTime = currentTime,
                                        AirNum = Convert.ToInt32(item.Value),
                                        Amount = Convert.ToInt32(currentAmount)
                                    };
                                    if (importColor != null)
                                    {
                                        var firstOrDefault = importColor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetImportTypeTrend.ImportType, StringComparison.OrdinalIgnoreCase));
                                        if (firstOrDefault !=
                                            null)
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
                EngineAmountCollection = amountCollection;
            }
        }

        /// <summary>
        /// 是否相同的发动机引进方式
        /// </summary>
        /// <param name="importType1"></param>
        /// <param name="importType2"></param>
        /// <returns></returns>
        protected bool SameImportType(string importType1, string importType2)
        {
            int index1 = importType1.IndexOf("续租", StringComparison.OrdinalIgnoreCase);
            if (index1 >= -1)
            {
                return importType1.Contains(importType2);
            }
            else
            {
                int index2 = importType2.IndexOf("续租", StringComparison.OrdinalIgnoreCase);
                if (index2 >= -1)
                {
                    return importType2.Contains(importType1);
                }
                else
                {
                    return importType1.Equals(importType2, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        /// <summary>
        /// 趋势图的选择点事件数据源
        /// </summary>
        public void ChartSelectionBehaviorSelection(DateTime time)
        {
            //            var Engine = this.ViewEngine
            //                        .Where(o => o.OperationHistories.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)))
            //                        .Where(o => o.OperationHistories.FirstOrDefault(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)).ImportCategory.ActionType .Equals( "引进");

            #region 发动机引进方式XML文件的读写
            var xmlConfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("发动机引进方式", StringComparison.OrdinalIgnoreCase));

            XElement importTypeColor = null;
            XmlSettingDTO colorConfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase)))
            {
                importTypeColor = XElement.Parse(colorConfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                var importTypeList = new List<FleetImportTypeComposition>();//发动机引进方式饼图集合

                XElement xelement = XElement.Parse(xmlConfig.ConfigContent).Descendants("DateTime").FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                if (xelement != null)
                {
                    foreach (XElement type in xelement.Descendants("Type"))
                    {
                        if (type.Attribute("TypeName").Value.Equals("发动机引进方式", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XElement item in type.Descendants("Item"))
                            {
                                var fleetImportTypeComposition = new FleetImportTypeComposition
                                                                 {
                                                                     ImportType = item.Attribute("Name").Value,
                                                                     AirNum = Convert.ToInt32(item.Value),
                                                                     AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                                                 };
                                if (importTypeColor != null)
                                {
                                    var firstOrDefault = importTypeColor.Descendants("Item")
                                        .FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetImportTypeComposition.ImportType, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetImportTypeComposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetImportTypeComposition.AirNum > 0)
                                {
                                    importTypeList.Add(fleetImportTypeComposition);
                                }
                            }
                        }
                    }
                }
                FleetImportTypeCollection = importTypeList;
            }
            #endregion

            //发动机详细数据
        }

        /// <summary>
        /// 根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="header">窗体标示</param>
        protected void GetGridViewDataSource(PieDataPoint selectedItem, RadWindow radwindow, string header)
        {
            if (selectedItem != null && radwindow != null)
            {
                //var fleetImporttTypeComposition = selectedItem.DataItem as FleetImportTypeComposition;
                //DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
                //var engine = this.Engines.Where(o => o.OperationHistories.Any(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)))
                //    .Where(o => o.OperationHistories.FirstOrDefault(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)).ImportCategory.ActionType .Equals( "引进");
                //List<EngineDTO> engines = engine.Where(p => this.SameImportType(p.OperationHistories.
                //    FirstOrDefault(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)).ImportCategory.ActionName, fleetImporttTypeComposition.ImportType)).ToList();
                ////找到子窗体的RadGridView，并为其赋值
                //RadGridView rgv = radwindow.Content as RadGridView;
                //rgv.ItemsSource = Commonmethod.GetEngineByTime(engines, time);
                //radwindow.Header = header + " " + fleetImporttTypeComposition.ImportType + "：" + fleetImporttTypeComposition.AirTt;
                //if (!radwindow.IsOpen)
                //{
                //    Commonmethod.ShowRadWindow(radwindow);
                //}
            }
        }

        #endregion

        #region Class
        /// <summary>
        /// 发动机引进方式趋势对象
        /// </summary>
        public class FleetImportTypeTrend
        {
            public FleetImportTypeTrend()
            {
                Color = Commonmethod.GetRandomColor();
            }
            public string ImportType { get; set; }//引进名称
            public int AirNum { get; set; }//发动机引进方式的发动机数
            public string DateTime { get; set; }//时间点
            public int Amount { get; set; }//发动机总数
            public string Color { get; set; }//引进的颜色
        }

        /// <summary>
        ///饼图的分布对象
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
        #endregion
    }
}

