#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:42:11
// 文件名：FleetAgeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:42:11
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(FleetAgeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetAgeVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _fleetPlanContext;
        private static readonly CommonMethod CommonMethod = new CommonMethod();
        private readonly RadWindow _ageWindow = new RadWindow(); //用于单击机龄饼状图的用户提示
        private readonly IFleetPlanService _service;

        private Grid _agePieGrid; //趋势折线图区域， 机龄饼图区域
        private RadGridView _aircraftDetail; //初始化RadGridView
        private RadGridView _exportRadgridview; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _lineGrid; //趋势折线图区域， 机龄饼图区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        [ImportingConstructor]
        public FleetAgeVm(IFleetPlanService service)
        {
            _service = service;
            _fleetPlanContext = _service.Context;
            
            ViewModelInitializer();
            InitalizerRadWindows(_ageWindow, "Age", 220);
            AddRadMenu(_ageWindow);
            InitializeVm();
        }

        public FleetAge CurrentFleetAge
        {
            get { return ServiceLocator.Current.GetInstance<FleetAge>(); }
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

        #endregion

        #region 数据

        #region 公共数据

        public Dictionary<string, List<AircraftDTO>> AircraftByAgeDic = new Dictionary<string, List<AircraftDTO>>();
        //机龄饼图的飞机数据分布字典

        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; } //XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; } //飞机集合 
        public ObservableCollection<FleetData> FleetDatas { get; set; }
        private ObservableCollection<FleetData> StaticFleetDatas { get; set; }
        public ObservableCollection<FleetAgeTrend> AircraftTypes { get; set; }
        #endregion

        #region 加载数据

        public override void LoadData()
        {
            IsBusy = true;
            XmlConfigs.Load(true);
            XmlSettings.Load(true);
            Aircrafts.Load(true);
        }

        #region 属性 AircraftDataObject

        private AircraftDTO _aircraft; //飞机实体数据

        public AircraftDTO Aircraft
        {
            get { return _aircraft; }
            set
            {
                _aircraft = value;
                RaisePropertyChanged("Aircraft");
            }
        }

        #endregion

        #region 属性 XmlConfigDataObjectAircraft

        private XmlConfigDTO _xmlConfigDataObjectAircraft; //飞机XmlConfig

        public XmlConfigDTO XmlConfigDataObjectAircraft
        {
            get { return _xmlConfigDataObjectAircraft; }
            set
            {
                _xmlConfigDataObjectAircraft = value;
                RaisePropertyChanged("XmlConfigDataObjectAircraft");
            }
        }

        #endregion

        #region 属性 SelectedTime --所选的时间点

        private string _selectedTime = "所选时间"; //所选的时间点

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
                    //   AgeWindow.Close();
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeAge = "所选时间和机型的机龄分布图";
                    }
                    else
                    {
                        SelectedTimeAge = SelectedTime + " 机型：" + SelectedType + "的机龄分布图";
                    }
                }
            }
        }

        #endregion

        #region 属性 SelectedType --所选的机型

        private string _selectedType = string.Empty; //所选的机型

        /// <summary>
        ///     所选的时间点
        /// </summary>
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (SelectedType != value)
                {
                    _selectedType = value;
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAgeCollection--机龄饼图的数据集合（指定时间点）

        private List<FleetAgeComposition> _fleetAgeCollection;

        /// <summary>
        ///     平均机龄饼图饼图的数据集合（指定时间点）
        /// </summary>
        public List<FleetAgeComposition> FleetAgeCollection
        {
            get { return _fleetAgeCollection; }
            set
            {
                if (_fleetAgeCollection != value)
                {
                    _fleetAgeCollection = value;
                    SetPieMark(FleetAgeCollection);
                    RaisePropertyChanged(() => FleetAgeCollection);
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

        #region ViewModel 属性 AircraftCount --飞机详细列表的标识栏提示

        private string _aircraftCount = "飞机明细"; //飞机详细列表的标识栏提示

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

        #region ViewModel 属性 SelectedTimeAge --机龄饼图的标识提示

        private string _selectedTimeAge = "所选时间和机型的机龄分布图"; //机龄饼图的标识提示

        /// <summary>
        ///     机龄饼图的标识提示
        /// </summary>
        public string SelectedTimeAge
        {
            get { return _selectedTimeAge; }
            set
            {
                if (SelectedTimeAge != value)
                {
                    _selectedTimeAge = value;
                    RaisePropertyChanged(() => SelectedTimeAge);
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedIndex --时间的统计方式

        private int _selectedIndex; //时间的统计方式

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
                    CreateFleetAgeTrendCollection();
                    RaisePropertyChanged(() => SelectedIndex);
                }
            }
        }

        #endregion

        #region ViewModel 属性 EndDate --结束时间

        private DateTime? _endDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/M")); //结束时间

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
                    CreateFleetAgeTrendCollection();
                }
            }
        }

        #endregion

        #region ViewModel 属性 StartDate --开始时间

        private DateTime? _startDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1); //开始时间

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
                    CreateFleetAgeTrendCollection();
                }
            }
        }

        #endregion

        #region ViewModel 属性 IsContextMenuOpen --控制右键菜单的打开

        private bool _isContextMenuOpen = true; //控制右键菜单的打开

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

        #region ViewModel 属性 FleetManufacturerTrendCollection --平均机龄趋势图的数据源集合

        private List<FleetAgeTrend> _fleetAgeTrendCollection; //平均机龄趋势图的数据源集合

        /// <summary>
        ///     平均机龄趋势图的数据源集合
        /// </summary>
        public List<FleetAgeTrend> FleetAgeTrendCollection
        {
            get { return _fleetAgeTrendCollection; }
            set
            {
                if (FleetAgeTrendCollection != value)
                {
                    _fleetAgeTrendCollection = value;
                    FleetAgeTrendCollectionChange(_fleetAgeTrendCollection);
                    RaisePropertyChanged(() => FleetAgeTrendCollection);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 根据选中饼图的航空公司弹出相应的数据列表窗体

        /// <summary>
        ///     根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem">选中点</param>
        /// <param name="radwindow">弹出窗体</param>
        /// <param name="header">窗体标示</param>
        private void GetGridViewDataSourse(PieDataPoint selectedItem, RadWindow radwindow, string header)
        {
            if (selectedItem != null && radwindow != null)
            {
                DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
                var fleetAgeComposition = selectedItem.DataItem as FleetAgeComposition;
                //找到子窗体的RadGridView，并为其赋值
                var rgv = radwindow.Content as RadGridView;
                if (fleetAgeComposition != null &&(AircraftByAgeDic != null && AircraftByAgeDic.ContainsKey(fleetAgeComposition.AgeGroup)))
                {
                    if (rgv != null)
                        rgv.ItemsSource = CommonMethod.GetAircraftByTime(AircraftByAgeDic[fleetAgeComposition.AgeGroup], time);
                }
                if (fleetAgeComposition != null)
                    _ageWindow.Header = fleetAgeComposition.AgeGroup + "的飞机数：" + fleetAgeComposition.ToolTip;
                if (!radwindow.IsOpen)
                {
                    CommonMethod.ShowRadWindow(radwindow);
                }
            }
        }

        #endregion

        #region 以View的实例初始化ViewModel相关字段、属性

        /// <summary>
        ///     以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            PieDeployCommand = new DelegateCommand<object>(OnPieDeploy); //机龄饼图的配置
            ExportCommand = new DelegateCommand<object>(OnExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView); //导出视图的数据
            ToggleButtonCommand = new DelegateCommand<object>(ToggleButtonCheck);
            _lineGrid = CurrentFleetAge.LineGrid;
            _agePieGrid = CurrentFleetAge.AgePieGrid;
            _aircraftDetail = CurrentFleetAge.AircraftDetail;
        }

        #region 初始化子窗体

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
            radwindow.Content = CommonMethod.CreatOperationGridView();
            radwindow.Closed += RadwindowClosed;
        }

        #endregion

        #region 趋势图的选择点事件

        /// <summary>
        ///     趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            DataPoint selectedPoint = ((ChartSelectionBehavior)sender).Chart.SelectedPoints.FirstOrDefault(p =>
            {
                var categoricalSeries = p.Presenter as CategoricalSeries;
                return categoricalSeries != null && categoricalSeries.Visibility == Visibility.Visible;
            });
            if (selectedPoint != null)
            {
                var fleetAgeTrend = selectedPoint.DataItem as FleetAgeTrend;
                if (fleetAgeTrend != null &&
                    (!SelectedTime.Equals(fleetAgeTrend.DateTime, StringComparison.OrdinalIgnoreCase) ||
                     !SelectedType.Equals(fleetAgeTrend.AircraftType, StringComparison.OrdinalIgnoreCase)))
                {
                    //选中机型
                    SelectedType = fleetAgeTrend.AircraftType;
                    //选中时间点
                    SelectedTime = fleetAgeTrend.DateTime;

                    DateTime time = Convert.ToDateTime(fleetAgeTrend.DateTime).AddMonths(1).AddDays(-1);
                    CreateFleetAgeCollection(SelectedType, time);
                }
            }
        }

        #endregion

        #region 飞机饼状图的选择点事件

        /// <summary>
        ///     飞机饼状图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadPieChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            var radChartBase = ((ChartSelectionBehavior)sender).Chart;
            var selectedPoint = radChartBase.SelectedPoints.FirstOrDefault() as PieDataPoint;

            var stackPanel = new StackPanel();
            if (radChartBase.EmptyContent.ToString().Equals("机龄分布", StringComparison.OrdinalIgnoreCase))
            {
                stackPanel = ((ScrollViewer)_agePieGrid.Children[1]).Content as StackPanel;
            }
            if (stackPanel != null)
            {
                foreach (var item in stackPanel.Children)
                {
                    var itemRectangle = ((StackPanel)item).Children[0] as Rectangle;
                    if (itemRectangle != null)
                    {
                        itemRectangle.Width = 15;
                        itemRectangle.Height = 15;
                    }
                }
                if (selectedPoint != null)
                {
                    var childStackPanel =
                        stackPanel.Children.FirstOrDefault(
                            p =>
                                ((TextBlock)((StackPanel)p).Children[1]).Text ==
                                ((FleetAgeComposition)selectedPoint.DataItem).AgeGroup) as StackPanel;
                    if (childStackPanel != null)
                    {
                        var rectangle = childStackPanel.Children[0] as Rectangle;
                        if (rectangle != null)
                        {
                            rectangle.Width = 12;
                            rectangle.Height = 12;
                        }
                    }
                    if (radChartBase.EmptyContent.ToString().Equals("机龄分布", StringComparison.OrdinalIgnoreCase))
                    {
                        GetGridViewDataSourse(selectedPoint, _ageWindow, "机龄");
                    }
                }
                else
                {
                    if (radChartBase.EmptyContent.ToString().Equals("机龄分布", StringComparison.OrdinalIgnoreCase))
                    {
                        _ageWindow.Close();
                    }
                }
            }
        }

        #endregion

        #region 根据相应的饼图数据生成饼图标签

        /// <summary>
        ///     根据相应的饼图数据生成饼图标签
        /// </summary>
        /// <param name="ienumerable">饼图数据集合</param>
        public void SetPieMark(IEnumerable<FleetAgeComposition> ienumerable)
        {
            var grid = _agePieGrid;
            var radPieChart = grid.Children[0] as RadPieChart;
            var stackPanel = ((ScrollViewer)grid.Children[1]).Content as StackPanel;

            if (radPieChart != null) radPieChart.Series[0].SliceStyles.Clear();
            if (stackPanel != null) stackPanel.Children.Clear();
            if (ienumerable == null)
            {
                return;
            }
            foreach (var item in ienumerable)
            {
                var setter = new Setter { Property = Shape.FillProperty, Value = item.Color };
                var style = new Style { TargetType = typeof(System.Windows.Shapes.Path) };
                style.Setters.Add(setter);
                if (radPieChart != null) radPieChart.Series[0].SliceStyles.Add(style);

                var barPanel = new StackPanel();
                barPanel.MouseLeftButtonDown += PiePanelMouseLeftButtonDown;
                barPanel.Orientation = Orientation.Horizontal;
                var rectangle = new Rectangle
                {
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(CommonMethod.GetColor(item.Color))
                };
                var textBlock = new TextBlock
                {
                    Text = item.AgeGroup,
                    Style =
                        CurrentFleetAge.Resources.FirstOrDefault(
                            p => p.Key.ToString().Equals("legendItemStyle", StringComparison.OrdinalIgnoreCase)).Value
                            as Style
                };
                barPanel.Children.Add(rectangle);
                barPanel.Children.Add(textBlock);
                if (stackPanel != null) stackPanel.Children.Add(barPanel);
            }
        }

        #endregion

        #region 平均机龄趋势图的数据源集合集合改变时

        /// <summary>
        ///     平均机龄趋势图的数据源集合集合改变时
        /// </summary>
        /// <param name="fleetAgeTrendCollection"></param>
        public void FleetAgeTrendCollectionChange(List<FleetAgeTrend> fleetAgeTrendCollection)
        {
            FleetDatas = new ObservableCollection<FleetData>();
            StaticFleetDatas = new ObservableCollection<FleetData>();
            var result = new ObservableCollection<FleetData>();
            var aircraftTypeResult = new ObservableCollection<FleetAgeTrend>();

            if (fleetAgeTrendCollection != null)
            {
                foreach (var groupItem in fleetAgeTrendCollection.GroupBy(p => p.AircraftType).ToList())
                {
                    var firstOrDefault = groupItem.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        var tempData = new FleetData
                                       {
                                           AircraftTypeName = groupItem.Key,
                                           Data = new ObservableCollection<FleetAgeTrend>()
                                       };
                        groupItem.ToList().ForEach(tempData.Data.Add);
                        result.Add(tempData);
                        aircraftTypeResult.Add(firstOrDefault);
                    }
                }
            }
            FleetDatas.Add(result.FirstOrDefault(p => p.AircraftTypeName.Equals("所有机型", StringComparison.OrdinalIgnoreCase)));
            result.ToList().ForEach(StaticFleetDatas.Add);
            AircraftTypes = aircraftTypeResult;
            //控制趋势图的滚动条
            int dateTimeCount = FleetAgeTrendCollection.Select(p => p.DateTime).Distinct().Count();
            if (FleetAgeTrendCollection != null && dateTimeCount >= 12)
            {
                CurrentFleetAge.LineCategoricalAxis.MajorTickInterval = dateTimeCount / 6;
            }
            else
            {
                CurrentFleetAge.LineCategoricalAxis.MajorTickInterval = 1;
            }
        }

        #endregion

        #region 弹出窗体关闭时，取消相应饼图的弹出项

        /// <summary>
        ///     弹出窗体关闭时，取消相应饼图的弹出项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadwindowClosed(object sender, WindowClosedEventArgs e)
        {
            var radWindow = sender as RadWindow;
            var grid = new Grid();
            if (radWindow != null && radWindow.Name.Equals("Age", StringComparison.OrdinalIgnoreCase))
            {
                grid = _agePieGrid;
            }

            //更改对应饼图的突出显示
            foreach (var item in ((RadPieChart)grid.Children[0]).Series[0].DataPoints)
            {
                item.IsSelected = false;
            }
            //更改对应饼图的标签大小
            foreach (var item in ((StackPanel)((ScrollViewer)grid.Children[1]).Content).Children)
            {
                var stackPanel = item as StackPanel;
                if (stackPanel != null)
                {
                    var rectangle = stackPanel.Children[0] as Rectangle;
                    if (rectangle != null)
                    {
                        rectangle.Width = 15;
                        rectangle.Height = 15;
                    }
                }
            }
        }

        #endregion

        public DelegateCommand<object> ToggleButtonCommand { get; set; }
        /// <summary>
        /// 控制趋势图中折线（饼状）的显示/隐藏
        /// </summary>
        /// <param name="sender"></param>
        private void ToggleButtonCheck(object sender)
        {
            var button = sender as RadToggleButton;
            if (button != null)
            {
                if ((bool)button.IsChecked)
                {
                    var temp = StaticFleetDatas.FirstOrDefault(p => p.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (!FleetDatas.Any(p => p.AircraftTypeName.Equals(temp.AircraftTypeName, StringComparison.OrdinalIgnoreCase)))
                    {
                        FleetDatas.Add(temp);
                    }
                }
                else
                {
                    for (int i = FleetDatas.Count - 1; i > -1; i--)
                    {
                        var temp = FleetDatas[i];
                        if (temp.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase))
                        {
                            FleetDatas.Remove(temp);
                            break;
                        }
                    }
                }
            }
        }

        #region 饼状图标签的选择事件

        /// <summary>
        ///     饼状图标签的选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PiePanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选中航空公司的名称
            var stackPanel = sender as StackPanel;
            if (stackPanel != null)
            {
                string shortName = ((TextBlock)stackPanel.Children[1]).Text;
                //修改饼图标签中的突出显示
                foreach (var item in ((StackPanel)stackPanel.Parent).Children)
                {
                    var childStackPanel = item as StackPanel;
                    if (childStackPanel != null)
                    {
                        var itemRectangle = childStackPanel.Children[0] as Rectangle;
                        string itemShortName = ((TextBlock)childStackPanel.Children[1]).Text;
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

                //修改对应饼图块状的突出显示
                var radPieChart =
                    ((Grid)((ScrollViewer)((StackPanel)stackPanel.Parent).Parent).Parent).Children[0] as RadPieChart;
                if (radPieChart != null)
                    foreach (var item in radPieChart.Series[0].DataPoints)
                    {
                        var pieDataPoint = item;
                        if (((FleetAgeComposition)pieDataPoint.DataItem).AgeGroup.Equals(shortName,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            pieDataPoint.IsSelected = !pieDataPoint.IsSelected;
                            if (pieDataPoint.IsSelected)
                            {
                                if (radPieChart.EmptyContent.ToString()
                                    .Equals("机龄分布", StringComparison.OrdinalIgnoreCase))
                                {
                                    GetGridViewDataSourse(pieDataPoint, _ageWindow, "机龄");
                                }
                            }
                            else
                            {
                                if (radPieChart.EmptyContent.ToString()
                                    .Equals("机龄分布", StringComparison.OrdinalIgnoreCase))
                                {
                                    _ageWindow.Close();
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

        #endregion

        #region 右键导出可用

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
        }

        #endregion

        #endregion

        #region Command

        #region ViewModel 命令 PieDeployCommand --机龄饼图的配置

        // 机龄饼图的配置
        public DelegateCommand<object> PieDeployCommand { get; set; }

        private void OnPieDeploy(object obj)
        {
            var window = ServiceLocator.Current.GetInstance<AgeDeploy>();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Closed += WindowClosed;
            window.ShowDialog();
        }

        /// <summary>
        ///     机龄配置窗体关闭后刷新饼图的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosed(object sender, WindowClosedEventArgs e)
        {
            var window = sender as RadWindow;
            if (window != null && (window.Tag != null && Convert.ToBoolean(window.Tag)))
            {
                if (!SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                {
                    _ageWindow.Close();
                    DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
                    CreateFleetAgeCollection(SelectedType, time);
                }
                window.Tag = false;
            }
        }
        #endregion

        #region ViewModel 命令 --导出图表

        public DelegateCommand<object> ExportCommand { get; set; }

        private void OnExport(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出源数据", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("LineGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAgeTrendCollection == null || !FleetAgeTrendCollection.Any())
                    {
                        return;
                    }
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string>
                        {
                            {"DateTime", "时间点"},
                            {"AircraftType", "机型"},
                            {"AverageAge", "机型平均机龄"}
                        };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAgeTrendCollection,
                        "LineAge");
                }
                else if (menu.Name.Equals("AgePieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAgeCollection == null || !FleetAgeCollection.Any())
                    {
                        return;
                    }
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "AgeGroup", "机龄范围" }, { "GroupCount", "飞机数(架)" } };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAgeCollection,
                        "PieAge");
                }

                _i = 1;
                _exportRadgridview.ElementExporting -= ElementExporting;
                _exportRadgridview.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        _exportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
            else if (menu != null && menu.Header.ToString().Equals("导出图片", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("LineGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_lineGrid != null)
                    {
                        CommonMethod.ExportToImage(_lineGrid);
                    }
                }
                else if (menu.Name.Equals("AgePieGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_agePieGrid != null)
                    {
                        CommonMethod.ExportToImage(_agePieGrid);
                    }
                }
            }
        }

        #region 设置导出样式

        /// <summary>
        ///     设置导出样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            e.Width = 120;
            if (e.Element == ExportElement.Cell &&
                e.Value != null)
            {
                if (_i % 4 == 3 && _i >= 7 &&
                    ((RadGridView)sender).Name.Equals("LineAge", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }
        #endregion

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
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
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
            var rmi = new RadMenuItem { Header = "导出表格" }; //新建右键菜单项
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
            if (rmi != null && rmi.DataContext.ToString().Equals("Age", StringComparison.OrdinalIgnoreCase))
            {
                rgview = _ageWindow.Content as RadGridView;
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

        #region 获取平均机龄趋势图的数据源集合

        /// <summary>
        ///     获取平均机龄趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreateFleetAgeTrendCollection()
        {
            var collection = new List<FleetAgeTrend>();

            #region 平均机龄XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄分析", StringComparison.OrdinalIgnoreCase));
            string aircraftColor = string.Empty;
            var colorConfig =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                var firstOrDefault =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                {
                    var orDefault =
                        firstOrDefault.Descendants("Item")
                            .FirstOrDefault(
                                p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                    if (orDefault != null)
                        aircraftColor = orDefault.Attribute("Color").Value;
                }
            }

            XElement typeColor = null;
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typeColor =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
            }


            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (XElement datetime in xelement.Descendants("DateTime"))
                    {
                        string currentTime =
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

                        foreach (XElement type in datetime.Descendants("Type"))
                        {
                            var fleetageTrend = new FleetAgeTrend
                                                {
                                                    AircraftType = type.Attribute("TypeName").Value,
                                                    AverageAge = Math.Round(Convert.ToDouble(type.Attribute("Amount").Value), 4),
                                                    DateTime = currentTime
                                                }; //折线图的总数对象
                            if (fleetageTrend.AircraftType.Equals("所有机型", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrEmpty(aircraftColor))
                                {
                                    fleetageTrend.Color = aircraftColor;
                                }
                            }
                            else
                            {
                                if (typeColor != null)
                                {
                                    var firstOrDefault =
                                        typeColor.Descendants("Item")
                                            .FirstOrDefault(
                                                p =>
                                                    p.Attribute("Name")
                                                        .Value.Equals(fleetageTrend.AircraftType,
                                                            StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault !=
                                        null)
                                        fleetageTrend.Color = firstOrDefault.Attribute("Color").Value;
                                }
                            }
                            //添加进相应的数据源集合
                            collection.Add(fleetageTrend);
                        }
                    }
                }
            }

            #endregion

            SelectedType = string.Empty;
            SelectedTime = "所选时间";
            //对界面数据集合进行重新初始化
            FleetAgeCollection = null;
            AircraftByAgeDic = new Dictionary<string, List<AircraftDTO>>();
            AircraftCollection = null;
            FleetAgeTrendCollection = collection;
        }

        #endregion

        #region 初始化数据

        /// <summary>
        ///     初始化数据
        /// </summary>
        private void InitializeData()
        {
            if (_loadXmlConfig && _loadXmlSetting)
            {
                IsBusy = false;
                _loadXmlConfig = false;
                _loadXmlSetting = false;
                CreateFleetAgeTrendCollection();
            }
        }

        #endregion

        #region 根据选中时间和机型生成相应的饼图和飞机数据

        /// <summary>
        ///     根据选中时间和机型生成相应的饼图和飞机数据
        /// </summary>
        /// <param name="aircraftType">选中机型</param>
        /// <param name="time">选中时间</param>
        public void CreateFleetAgeCollection(string aircraftType, DateTime time)
        {
            //机龄的饼图分布集合
            var ageCompositionList = new List<FleetAgeComposition>();
            //机龄饼图的飞机数据分布字典
            AircraftByAgeDic = new Dictionary<string, List<AircraftDTO>>();

            IEnumerable<AircraftDTO> aircraft = null;
            if (aircraftType.Equals("所有机型", StringComparison.OrdinalIgnoreCase))
            {
                aircraft = Aircrafts.Where(p => p.FactoryDate != null)
                    .Where(o =>/*AircraftBusinesses.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)) &&*/
                            o.FactoryDate <= time && !(o.ExportDate != null && o.ExportDate < time));
            }

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄配置", StringComparison.OrdinalIgnoreCase));

            XElement ageColor = null;
            XmlSettingDTO colorConfig =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase)))
            {
                ageColor =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null && aircraft != null)
            {
                var aircraftDtos = aircraft as AircraftDTO[] ?? aircraft.ToArray();

                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                foreach (XElement item in xelement.Descendants("Item"))
                {
                    int startYear = Convert.ToInt32(item.Attribute("Start").Value);
                    int endYear = Convert.ToInt32(item.Attribute("End").Value);
                    //设置相应机龄范围的飞机数据，用于弹出窗体的数据显示
                    var aircraftByAge = aircraftDtos.Where(p =>
                        endYear * 12 >
                        (time.Year - Convert.ToDateTime(p.FactoryDate).Year) * 12 +
                        (time.Month - Convert.ToDateTime(p.FactoryDate).Month)
                        &&
                        (time.Year - Convert.ToDateTime(p.FactoryDate).Year) * 12 +
                        (time.Month - Convert.ToDateTime(p.FactoryDate).Month) >= startYear * 12).ToList();
                    if (aircraftByAge != null && aircraftByAge.Any())
                    {
                        var ageComposition = new FleetAgeComposition
                                             {
                                                 AgeGroup = item.Value,
                                                 GroupCount = aircraftByAge.Count()
                                             };
                        ageComposition.ToolTip = ageComposition.GroupCount + " 架，占" +
                                                 (aircraftByAge.Count() * 100 / aircraftDtos.Count()).ToString("##0") + "%";
                        if (ageColor != null)
                        {
                            var firstOrDefault = ageColor.Descendants("Item")
                                .FirstOrDefault(
                                    p =>
                                        p.Attribute("Name")
                                            .Value.Equals(ageComposition.AgeGroup, StringComparison.OrdinalIgnoreCase));
                            if (firstOrDefault != null)
                                ageComposition.Color = firstOrDefault.Attribute("Color").Value;
                        }
                        ageCompositionList.Add(ageComposition);
                        AircraftByAgeDic.Add(ageComposition.AgeGroup, aircraftByAge);
                    }
                }
                FleetAgeCollection = ageCompositionList;
                //飞机详细数据集合
                AircraftCollection = CommonMethod.GetAircraftByTime(aircraftDtos.ToList(), time);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Classes

        #region 机龄饼图的分布对象

        /// <summary>
        ///     机龄饼图的分布对象
        /// </summary>
        public class FleetAgeComposition
        {
            public FleetAgeComposition()
            {
                Color = CommonMethod.GetRandomColor();
            }

            public string AgeGroup { get; set; }
            public decimal GroupCount { get; set; }
            public string ToolTip { get; set; }
            public string Color { get; set; }
        }

        #endregion

        #region 平均机龄趋势对象

        /// <summary>
        ///     平均机龄趋势对象
        /// </summary>
        public class FleetAgeTrend
        {
            public FleetAgeTrend()
            {
                Color = CommonMethod.GetRandomColor();
            }

            public string AircraftType { get; set; } //机型名称
            public double AverageAge { get; set; } //机型的平均年龄
            public string DateTime { get; set; } //时间点
            public string Color { get; set; } //机型的颜色
        }

        #endregion

        public class FleetData
        {
            public string AircraftTypeName { get; set; }
            public ObservableCollection<FleetAgeTrend> Data { get; set; }
        }
        #endregion
    }
}
