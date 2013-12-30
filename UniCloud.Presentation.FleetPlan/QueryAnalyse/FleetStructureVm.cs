#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:36:22
// 文件名：FleetStructureVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:36:22
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
    [Export(typeof(FleetStructureVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetStructureVm : ViewModelBase
    {
        #region 声明、初始化
        private FleetPlanData _fleetPlanDataService;

        public FleetStructure CurrrentFleetStructure
        {
            get { return ServiceLocator.Current.GetInstance<FleetStructure>(); }
        }
        private static readonly CommonMethod Commonmethod = new CommonMethod();
        private int _i; //导出数据源格式判断
        private Grid _lineGrid, _barGrid, _regionalPieGrid, _typePieGrid;//趋势折线图区域，趋势柱状图区域， 座级饼图区域，机型饼图区域
        private RadDateTimePicker _startDateTimePicker, _endDateTimePicker;//开始时间控件， 结束时间控件
        private RadGridView _exportRadgridview, _aircraftDetail; //初始化RadGridView
        private readonly RadWindow _regionalWindow = new RadWindow(); //用于单击座级饼状图的用户提示
        private readonly RadWindow _typeWindow = new RadWindow(); //用于单击机型饼状图的用户提示
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        public FleetStructureVm()
        {
            ExportCommand = new DelegateCommand<object>(OnExport, CanExport);//导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView, CanExportGridView);
            ViewModelInitializer();
            InitalizerRadWindows(_regionalWindow, "Regional", 220);
            InitalizerRadWindows(_typeWindow, "Type", 240);
            AddRadMenu(_regionalWindow);
            AddRadMenu(_typeWindow);
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
            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_fleetPlanDataService, _fleetPlanDataService.Aircrafts);
        }

        /// <summary>
        /// 以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            _lineGrid = CurrrentFleetStructure.LineGrid;
            _barGrid = CurrrentFleetStructure.BarGrid;
            _regionalPieGrid = CurrrentFleetStructure.RegionalPieGrid;
            _typePieGrid = CurrrentFleetStructure.TypePieGrid;

            _aircraftDetail = CurrrentFleetStructure.AircraftDetail;
            //控制界面起止时间控件的字符串格式化
            _startDateTimePicker = CurrrentFleetStructure.StartDateTimePicker;
            _endDateTimePicker = CurrrentFleetStructure.EndDateTimePicker;
            _startDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
            _endDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";


            _lineGrid = CurrrentFleetStructure.LineGrid;
            _barGrid = CurrrentFleetStructure.BarGrid;
        }
        #endregion

        #region 数据
        #region 公共数据
        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; }//XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; } //飞机集合 

        #region 属性 AircraftDTO
        private AircraftDTO _aircraftDataObject;//飞机实体数据
        public AircraftDTO AircraftDataObject
        {
            get
            {
                return _aircraftDataObject;
            }
            set
            {
                _aircraftDataObject = value;
                RaisePropertyChanged(() => AircraftDataObject);
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
                    CloseRegionAndTypeWindows();
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeRegional = "所选时间的座级分布图";
                        SelectedTimeType = "所选时间的机型分布图";
                    }
                    else
                    {
                        SelectedTimeRegional = SelectedTime + "末的座级分布图";
                        SelectedTimeType = SelectedTime + "末的机型分布图";
                    }
                }
            }
        }
        #endregion

        #region ViewModel 属性 FleetAircraftTypeTrendCollection --座级趋势图的数据源集合
        private List<FleetAircraftRegionalTrend> _fleetAircraftTypeTrendCollection;
        /// <summary>
        /// 座级趋势图的数据源集合
        /// </summary>
        public List<FleetAircraftRegionalTrend> FleetAircraftTypeTrendCollection
        {
            get { return _fleetAircraftTypeTrendCollection; }
            set
            {
                if (FleetAircraftTypeTrendCollection != value)
                {
                    _fleetAircraftTypeTrendCollection = value;
                    AircraftTypeTrendInit(FleetAircraftTypeTrendCollection);
                    RaisePropertyChanged(() => FleetAircraftTypeTrendCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftAmountCollection --柱状趋势图的飞机总数集合
        private List<FleetAircraftRegionalTrend> _aircraftAmountCollection;
        /// <summary>
        /// 柱状趋势图的数据源集合
        /// </summary>
        public List<FleetAircraftRegionalTrend> AircraftAmountCollection
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

        #region ViewModel 属性 FleetAircraftRegionalCollection--座级饼图的数据集合（指定时间点）
        private IEnumerable<FleetAircraftTypeComposition> _fleetAircraftRegionalCollection;
        /// <summary>
        /// 座级饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftRegionalCollection
        {
            get { return _fleetAircraftRegionalCollection; }
            set
            {
                if (_fleetAircraftRegionalCollection != null && !Equals(_fleetAircraftRegionalCollection, value))
                {
                    _fleetAircraftRegionalCollection = value;
                    RaisePropertyChanged(() => FleetAircraftRegionalCollection);
                    SetPieMark(FleetAircraftRegionalCollection, "RegionalPieGrid");
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftTypeCollection--机型饼图的数据集合（指定时间点）
        private IEnumerable<FleetAircraftTypeComposition> _fleetAircraftTypeCollection;
        /// <summary>
        /// 机型饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftTypeCollection
        {
            get { return _fleetAircraftTypeCollection; }
            set
            {
                if (Equals(_fleetAircraftTypeCollection, value)) return;
                _fleetAircraftTypeCollection = value;
                RaisePropertyChanged(() => FleetAircraftTypeCollection);
                SetPieMark(FleetAircraftTypeCollection, "TypePieGrid");
            }
        }

        #endregion

        #region ViewModel 属性 AircraftDataObjectList --座级（机型)饼图所对应的所有飞机数据（指定时间点）

        private List<AircraftDTO> _aircraftCollection;
        /// <summary>
        /// 座级（机型)饼图所对应的所有飞机数据（指定时间点）
        /// </summary>
        public List<AircraftDTO> AircraftCollection
        {
            get { return _aircraftCollection; }
            set
            {
                _aircraftCollection = value;
                RaisePropertyChanged(() => AircraftCollection);
                if (SelectedTime != "所选时间")
                {
                    if (_aircraftCollection == null || !_aircraftCollection.Any())
                    {
                        AircraftCount = "飞机明细（0架）";
                    }
                    else
                    {
                        AircraftCount = "飞机明细（" + _aircraftCollection.Count() + "架）";
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
        private string _aircraftCount = "飞机明细";
        /// <summary>
        /// 飞机详细列表的标识栏提示
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

        #region ViewModel 属性 SelectedTimeRegional --座级饼图的标识提示
        private string _selectedTimeRegional = "所选时间的座级分布图";
        /// <summary>
        /// 座级饼图的标识提示
        /// </summary>
        public string SelectedTimeRegional
        {
            get { return _selectedTimeRegional; }
            set
            {
                if (SelectedTimeRegional != value)
                {
                    _selectedTimeRegional = value;
                    RaisePropertyChanged(() => SelectedTimeRegional);

                }
            }
        }
        #endregion

        #region ViewModel 属性 SelectedTimeType --机型饼图的标识提示
        private string _selectedTimeType = "所选时间的机型分布图";
        /// <summary>
        /// 机型饼图的标识提示
        /// </summary>
        public string SelectedTimeType
        {
            get { return _selectedTimeType; }
            set
            {
                if (SelectedTimeType != value)
                {
                    _selectedTimeType = value;
                    RaisePropertyChanged(() => SelectedTimeType);
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
                    CreateFleetAircraftTypeTrendCollection();
                    RaisePropertyChanged(() => SelectedIndex);
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
                    CreateFleetAircraftTypeTrendCollection();
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
                    CreateFleetAircraftTypeTrendCollection();
                }
            }
        }
        #endregion

        #region ViewModel 属性 AircraftColor --期末飞机数的颜色
        private string _aircraftColor = Commonmethod.GetRandomColor();
        /// <summary>
        /// 期末飞机数的颜色
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
            Aircrafts.Load(true);
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
        #region Method

        /// <summary>
        /// 选择开始时间
        /// </summary>
        /// <param name="dataTimeStart"></param>
        public void SelectedStartValueChange(DateTime? dataTimeStart)
        {
            _startDateTimePicker.SelectedValue = dataTimeStart;
        }

        /// <summary>
        /// 根据相应的饼图数据生成饼图标签
        /// </summary>
        public void SetPieMark(IEnumerable<FleetAircraftTypeComposition> ienumerable, string regionGrid)
        {
            RadPieChart radpiechart;
            StackPanel stackpanel;
            if (regionGrid.Equals("TypePieGrid", StringComparison.OrdinalIgnoreCase))
            {
                radpiechart = _typePieGrid.Children[0] as RadPieChart;
                stackpanel = ((ScrollViewer)_typePieGrid.Children[1]).Content as StackPanel;
            }
            else
            {
                radpiechart = _regionalPieGrid.Children[0] as RadPieChart;
                stackpanel = ((ScrollViewer)_regionalPieGrid.Children[1]).Content as StackPanel;
            }

            if (radpiechart != null) radpiechart.Series[0].SliceStyles.Clear();
            if (stackpanel != null) stackpanel.Children.Clear();
            if (ienumerable == null)
            {
                return;
            }
            foreach (var item in ienumerable)
            {
                var setter = new Setter { Property = System.Windows.Shapes.Shape.FillProperty, Value = item.Color };
                var style = new Style { TargetType = typeof(System.Windows.Shapes.Path) };
                style.Setters.Add(setter);
                if (radpiechart != null) radpiechart.Series[0].SliceStyles.Add(style);

                var barpanel = new StackPanel();
                barpanel.MouseLeftButtonDown += PiePanelMouseLeftButtonDown;
                barpanel.Orientation = Orientation.Horizontal;
                var rectangle = new System.Windows.Shapes.Rectangle
                {
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(Commonmethod.GetColor(item.Color))
                };
                var textblock = new TextBlock
                {
                    Text = item.AircraftRegional,
                    Style = CurrrentFleetStructure.Resources.FirstOrDefault(p => p.Key.ToString().Equals("legendItemStyle", StringComparison.OrdinalIgnoreCase)).Value as Style
                };
                barpanel.Children.Add(rectangle);
                barpanel.Children.Add(textblock);
                if (stackpanel != null) stackpanel.Children.Add(barpanel);
            }
        }

        /// <summary>
        /// 控制趋势图滚动条
        /// </summary>
        /// <param name="aircraftAmountCollection"></param>
        public void ControlTrendScroll(List<FleetAircraftRegionalTrend> aircraftAmountCollection)
        {
            if (aircraftAmountCollection != null && aircraftAmountCollection.Count() >= 12)
            {
                CurrrentFleetStructure.LineCategoricalAxis.MajorTickInterval = aircraftAmountCollection.Count() / 6;
                CurrrentFleetStructure.BarCategoricalAxis.MajorTickInterval = aircraftAmountCollection.Count() / 6;
            }
            else
            {
                CurrrentFleetStructure.LineCategoricalAxis.MajorTickInterval = 1;
                CurrrentFleetStructure.BarCategoricalAxis.MajorTickInterval = 1;
            }
        }

        /// <summary>
        /// 座级数据源绑定界面
        /// </summary>
        /// <param name="fleetAircraftTypeTrendCollection"></param>
        public void AircraftTypeTrendInit(List<FleetAircraftRegionalTrend> fleetAircraftTypeTrendCollection)
        {
            var radcartesianchart = _lineGrid.Children[0] as RadCartesianChart;
            var stackpanel = ((ScrollViewer)_lineGrid.Children[1]).Content as StackPanel;
            if (radcartesianchart != null) radcartesianchart.Series.Clear();
            if (stackpanel != null)
            {
                stackpanel.Children.Clear();

                //StringBuilder CellETemp = new StringBuilder();
                //CellETemp.Append("<DataTemplate ");
                //CellETemp.Append("xmlns = 'http://schemas.microsoft.com/client/2007' ");
                //CellETemp.Append("xmlns:x = 'http://schemas.microsoft.com/winfx/2006/xaml' ");
                //CellETemp.Append("xmlns:local = 'clr-namespace:CAAC.CAFM.Views");
                //CellETemp.Append(";assembly=YourAssembly'>");
                //CellETemp.Append("<Ellipse Height='8' Width='8' Fill='{Binding Path=DataItem.Color}'/>");
                //CellETemp.Append("</DataTemplate>");
                //DataTemplate datatemplate=(DataTemplate)XamlReader.Load(CellETemp.ToString());
                if (fleetAircraftTypeTrendCollection != null)
                {
                    foreach (var groupItem in fleetAircraftTypeTrendCollection.GroupBy(p => p.AircraftRegional).ToList())
                    {
                        var fleetAircraftRegionalTrend = groupItem.FirstOrDefault();
                        if (fleetAircraftRegionalTrend != null)
                        {
                            var line =
                                new LineSeries
                                {
                                    StrokeThickness = 3,
                                    DisplayName = groupItem.Key,
                                    Stroke = new SolidColorBrush(Commonmethod.GetColor(fleetAircraftRegionalTrend.Color)),
                                    CategoryBinding = Commonmethod.CreateBinding("DateTime"),
                                    ValueBinding = Commonmethod.CreateBinding("AirNum"),
                                    ItemsSource = groupItem.ToList(),
                                    PointTemplate = ((RadCartesianChart)_lineGrid.Children[0]).Resources["LinePointTemplate"] as DataTemplate,
                                    TrackBallInfoTemplate = ((RadCartesianChart)_lineGrid.Children[0]).Resources["LineTrackBallInfoTemplate"] as DataTemplate
                                };
                            if (radcartesianchart != null) radcartesianchart.Series.Add(line);
                        }

                        var aircraftRegionalTrend = groupItem.FirstOrDefault();
                        if (aircraftRegionalTrend != null)
                        {
                            var panel = new StackPanel
                            {
                                Margin = new Thickness(5, 5, 5, 5),
                                Orientation = Orientation.Horizontal,
                                Background = new SolidColorBrush(Commonmethod.GetColor(aircraftRegionalTrend.Color))
                            };
                            var checkbox = new CheckBox { IsChecked = true };
                            checkbox.Checked += CheckboxChecked;
                            checkbox.Unchecked += CheckboxUnchecked;
                            checkbox.Content = groupItem.Key;
                            checkbox.Foreground = new SolidColorBrush(Colors.White);
                            checkbox.VerticalAlignment = VerticalAlignment.Center;
                            checkbox.Style = CurrrentFleetStructure.Resources["LegengCheckBoxStyle"] as Style;
                            panel.Children.Add(checkbox);
                            stackpanel.Children.Add(panel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 关闭座级机型子窗口
        /// </summary>
        public void CloseRegionAndTypeWindows()
        {
            _regionalWindow.Close();
            _typeWindow.Close();
        }

        /// <summary>
        /// 初始化时初始化滚动条
        /// </summary>
        public void ZoomInit()
        {
            Zoom = new Size(1, 1);
        }

        /// <summary>
        /// 选择结束时间
        /// </summary>
        /// <param name="dataTimeEnd"></param>
        public void SelectedEndValueChange(DateTime? dataTimeEnd)
        {
            _endDateTimePicker.SelectedValue = dataTimeEnd;
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
        /// 弹出窗体关闭时，取消相应饼图的弹出项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadwindowClosed(object sender, WindowClosedEventArgs e)
        {
            var radwindow = sender as RadWindow;
            var grid = new Grid();
            if (radwindow != null && radwindow.Name.Equals("Regional", StringComparison.OrdinalIgnoreCase))
            {
                grid = _regionalPieGrid;
            }
            else if (radwindow != null && radwindow.Name.Equals("Type", StringComparison.OrdinalIgnoreCase))
            {
                grid = _typePieGrid;
            }

            //更改对应饼图的突出显示
            foreach (var item in ((RadPieChart)grid.Children[0]).Series[0].DataPoints)
            {
                item.IsSelected = false;
            }
            //更改对应饼图的标签大小
            foreach (var item in ((StackPanel)((ScrollViewer)grid.Children[1]).Content).Children)
            {
                var rectangle = ((StackPanel)item).Children[0] as System.Windows.Shapes.Rectangle;
                if (rectangle != null)
                {
                    rectangle.Width = 15;
                    rectangle.Height = 15;
                }
            }
        }

        /// <summary>
        /// 饼状图标签的选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PiePanelMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //选中航空公司的名称
            var stackpanel = sender as StackPanel;
            if (stackpanel != null)
            {
                var textBlock = stackpanel.Children[1] as TextBlock;
                if (textBlock != null)
                {
                    string shortname = textBlock.Text;

                    //修改饼图标签中的突出显示
                    foreach (var item in ((StackPanel)stackpanel.Parent).Children)
                    {
                        var childstackpanel = item as StackPanel;
                        if (childstackpanel != null)
                        {
                            var itemrectangle = childstackpanel.Children[0] as System.Windows.Shapes.Rectangle;
                            string itemshortname = ((TextBlock)childstackpanel.Children[1]).Text;
                            if (itemshortname.Equals(shortname, StringComparison.OrdinalIgnoreCase))
                            {
                                if (itemrectangle != null && itemrectangle.Width == 12)
                                {
                                    itemrectangle.Width = 15;
                                    itemrectangle.Height = 15;
                                }
                                else
                                {
                                    itemrectangle.Width = 12;
                                    itemrectangle.Height = 12;
                                }
                            }
                            else
                            {
                                itemrectangle.Width = 15;
                                itemrectangle.Height = 15;
                            }
                        }
                    }
                    //修改对应饼图块状的突出显示
                    var radpiechart = ((Grid)((ScrollViewer)((StackPanel)stackpanel.Parent).Parent).Parent).Children[0] as RadPieChart;
                    if (radpiechart != null)
                        foreach (var item in radpiechart.Series[0].DataPoints)
                        {
                            var piedatapoint = item;
                            if (((FleetAircraftTypeComposition)piedatapoint.DataItem).AircraftRegional.Equals(shortname, StringComparison.OrdinalIgnoreCase))
                            {

                                piedatapoint.IsSelected = !piedatapoint.IsSelected;
                                if (piedatapoint.IsSelected)
                                {
                                    if (radpiechart.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                                    {
                                        //GetGridViewDataSourse(piedatapoint, RegionalWindow, "座级");
                                    }
                                    else if (radpiechart.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                                    {
                                        //  GetGridViewDataSourse(piedatapoint, TypeWindow, "机型");
                                    }
                                }
                                else
                                {
                                    if (radpiechart.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                                    {
                                        _regionalWindow.Close();
                                    }
                                    else if (radpiechart.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                                    {
                                        _typeWindow.Close();
                                    }
                                }
                            }
                            else
                            {
                                piedatapoint.IsSelected = false;
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
            DataPoint selectedPoint = ((ChartSelectionBehavior)sender).Chart.SelectedPoints.FirstOrDefault(p => ((CategoricalSeries)p.Presenter).Visibility == Visibility.Visible);
            if (selectedPoint != null)
            {
                var fleetaircraftregionaltrend = selectedPoint.DataItem as FleetAircraftRegionalTrend;
                if (fleetaircraftregionaltrend != null && SelectedTime != fleetaircraftregionaltrend.DateTime)
                {
                    //选中时间点
                    SelectedTime = fleetaircraftregionaltrend.DateTime;

                    DateTime time = Convert.ToDateTime(fleetaircraftregionaltrend.DateTime).AddMonths(1).AddDays(-1);
                    ChartSelectionBehaviorSelection(time);
                }
            }
        }

        /// <summary>
        /// 飞机饼状图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RadPieChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            RadChartBase radchartbase = ((ChartSelectionBehavior)sender).Chart;
            var selectedPoint = radchartbase.SelectedPoints.FirstOrDefault() as PieDataPoint;

            var stackpanel = new StackPanel();
            if (radchartbase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
            {
                stackpanel = ((ScrollViewer)_regionalPieGrid.Children[1]).Content as StackPanel;
            }
            else if (radchartbase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
            {
                stackpanel = ((ScrollViewer)_typePieGrid.Children[1]).Content as StackPanel;
            }

            foreach (var item in stackpanel.Children)
            {
                var itemrectangle = ((StackPanel)item).Children[0] as System.Windows.Shapes.Rectangle;
                if (itemrectangle != null)
                {
                    itemrectangle.Width = 15;
                    itemrectangle.Height = 15;
                }
            }

            if (selectedPoint != null)
            {
                var childstackpanel = stackpanel.Children
                    .FirstOrDefault(p => ((TextBlock)((StackPanel)p).Children[1]).Text.Equals(((FleetAircraftTypeComposition)selectedPoint.DataItem).AircraftRegional, StringComparison.OrdinalIgnoreCase)) as StackPanel;
                if (childstackpanel != null)
                {
                    var rectangle = childstackpanel.Children[0] as System.Windows.Shapes.Rectangle;
                    if (rectangle != null)
                    {
                        rectangle.Width = 12;
                        rectangle.Height = 12;
                    }
                }

                if (radchartbase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                {
                    ShowGridViewData(selectedPoint, _regionalWindow, "座级");
                }
                else if (radchartbase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                {
                    ShowGridViewData(selectedPoint, _typeWindow, "机型");
                }
            }
            else
            {
                if (radchartbase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                {
                    _regionalWindow.Close();
                }
                else if (radchartbase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                {
                    _typeWindow.Close();
                }
            }
        }

        /// <summary>
        /// 根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="radwindow"></param>
        /// <param name="header"></param>
        public void ShowGridViewData(PieDataPoint selectedItem, RadWindow radwindow, string header)
        {
            if (selectedItem != null && radwindow != null)
            {
                var fleetaircrafttypecomposition = selectedItem.DataItem as FleetAircraftTypeComposition;
                GetGridViewDataSourse(header);
                //找到子窗体的RadGridView，并为其赋值
                var rgv = radwindow.Content as RadGridView;
                // rgv.ItemsSource = commonmethod.GetAircraftByTime(airlineAircrafts, time);
                radwindow.Header = header + " " + fleetaircrafttypecomposition.AircraftRegional + "：" + fleetaircrafttypecomposition.AirTt;
                if (!radwindow.IsOpen)
                {
                    Commonmethod.ShowRadWindow(radwindow);
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
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var grid = (((checkbox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_lineGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Visible;
                }
                else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                {
                    (_barGrid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase)).Visibility = System.Windows.Visibility.Visible;
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
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var stackPanel = ((StackPanel)checkbox.Parent).Parent as StackPanel;
                if (stackPanel != null)
                {
                    var grid = ((ScrollViewer)stackPanel.Parent).Parent as Grid;
                    if (grid.Name.Equals("LineGrid", StringComparison.OrdinalIgnoreCase))
                    {
                        var firstOrDefault = ((RadCartesianChart)_lineGrid.Children[0]).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                        if (
                            firstOrDefault != null)
                            firstOrDefault.Visibility = Visibility.Collapsed;
                    }
                    else if (grid.Name.Equals("BarGrid", StringComparison.OrdinalIgnoreCase))
                    {
                        var firstOrDefault = ((RadCartesianChart)_barGrid.Children[0]).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                        if (
                            firstOrDefault != null)
                            firstOrDefault.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
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
                if (menu.Name.Equals("TrendGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string>();
                    columnsList.Add("DateTime", "时间点");
                    columnsList.Add("AircraftType", "机型");
                    columnsList.Add("AirNum", "飞机数");
                    columnsList.Add("Amount", "期末飞机数");
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTypeTrendCollection, "FleetStructure");

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
                else if (menu.Name.Equals("RegionalPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftRegionalCollection == null || !FleetAircraftRegionalCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "AircraftType", "座级" }, { "AirNum", "飞机数" } };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftRegionalCollection, "RegionalPieStructure");

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
                else if (menu.Name.Equals("TypePieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftTypeCollection == null || !FleetAircraftTypeCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "AircraftType", "机型" }, { "AirNum", "飞机数(架)" } };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTypeCollection, "TypePieStructure");

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
                else if (menu.Name.Equals("RegionalPieGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_regionalPieGrid != null)
                    {
                        Commonmethod.ExportToImage(_regionalPieGrid);
                    }
                }
                else if (menu.Name.Equals("TypePieGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_typePieGrid != null)
                    {
                        Commonmethod.ExportToImage(_typePieGrid);
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
            if (e.Element == ExportElement.Cell &&
                e.Value != null)
            {
                if (_i % 5 == 3 && _i >= 8 && ((RadGridView)sender).Name.Equals("FleetStructure", StringComparison.OrdinalIgnoreCase))
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

        #region ViewModel 命令 --导出数据AircraftDetail

        public DelegateCommand<object> ExportGridViewCommand { get; set; }

        private void OnExportGridView(object sender)
        {
            var menu = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (menu != null && menu.Header.ToString().Equals("导出数据", StringComparison.OrdinalIgnoreCase) && _aircraftDetail != null)
            {
                _aircraftDetail.ElementExporting -= ElementExporting;
                _aircraftDetail.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                {
                    if (stream != null)
                    {
                        _aircraftDetail.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
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
            radcm.Opened += radcm_Opened;
            var rmi = new RadMenuItem { Header = "导出表格" };//新建右键菜单项
            rmi.Click += MenuItemClick;//为菜单项注册事件
            rmi.DataContext = rwindow.Name;
            radcm.Items.Add(rmi);
            RadContextMenu.SetContextMenu(rwindow, radcm);//为控件绑定右键菜单
        }

        void radcm_Opened(object sender, RoutedEventArgs e)
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
            if (rmi != null && rmi.DataContext.ToString().Equals("Regional", StringComparison.OrdinalIgnoreCase))
            {
                rgview = _regionalWindow.Content as RadGridView;

            }
            else if (rmi != null && rmi.DataContext.ToString().Equals("Type", StringComparison.OrdinalIgnoreCase))
            {
                rgview = _typeWindow.Content as RadGridView;
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

        #region Methods

        /// <summary>
        /// 根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem">选中点</param>
        /// <param name="radwindow">弹出窗体</param>
        /// <param name="header">窗体标示</param>
        protected void GetGridViewDataSourse(string header)
        {
            //if (selectedItem != null && radwindow != null)
            //{
            //    FleetAircraftTypeComposition fleetaircrafttypecomposition = selectedItem.DataItem as FleetAircraftTypeComposition;
            //    DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
            //    var aircraft = this.AircraftDataObjectList.Where(o => o.OperationHistories.Any(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))
            //        && o.AircraftBusinesses.Any(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)));
            //    List<Aircraft> airlineAircrafts = new List<Aircraft>();
            //    if (header .Equals(  "座级")
            //    {
            //        airlineAircrafts = aircraft.Where(p => p.AircraftBusinesses.FirstOrDefault(pp => pp.StartDate <= time
            //            && !(pp.EndDate != null && pp.EndDate < time)).AircraftType.AircraftCategory.Regional .Equals(  fleetaircrafttypecomposition.AircraftRegional).ToList();
            //    }
            //    else if (header .Equals(  "机型")
            //    {
            //        airlineAircrafts =
            //            aircraft.Where(p => p.AircraftBusinesses.FirstOrDefault(pp => pp.StartDate <= time
            //                                                                          &&
            //                                                                          !(pp.EndDate != null &&
            //                                                                            pp.EndDate < time))
            //                                 .AircraftType.Name .Equals(  fleetaircrafttypecomposition.AircraftRegional)
            //                    .ToList();
            //    }
            //}
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            if (_loadXmlSetting && _loadXmlConfig)
            {
                IsBusy = false;
                _loadXmlSetting = false;
                _loadXmlConfig = false;
                CreateFleetAircraftTypeTrendCollection();
            }
        }

        /// <summary>
        /// 获取座级趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreateFleetAircraftTypeTrendCollection()
        {
            var regionalCollection = new List<FleetAircraftRegionalTrend>();
            var amountCollection = new List<FleetAircraftRegionalTrend>();

            #region 座级机型XML文件的读写
            var xmlconfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("座级机型", StringComparison.OrdinalIgnoreCase));

            XElement aircraftcolor = null;
            XmlSettingDTO colorconfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                aircraftcolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
            }

            if (aircraftcolor != null)
            {
                var firstOrDefault = aircraftcolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                    AircraftColor = firstOrDefault.Attribute("Color").Value;
            }

            XElement regionalcolor = null;
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase)))
            {
                regionalcolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlconfig != null)
            {
                XElement xelement = XElement.Parse(xmlconfig.ConfigContent);
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
                            if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                            {
                                string currentAmount = type.Attribute("Amount").Value;

                                //飞机总数柱状集合
                                var aircraftamount = new FleetAircraftRegionalTrend
                                {
                                    DateTime = currentTime,
                                    Amount = Convert.ToInt32(currentAmount),
                                    Color = AircraftColor
                                };
                                amountCollection.Add(aircraftamount);

                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    //座级折线集合
                                    var fleetaircraftregionaltrend = new FleetAircraftRegionalTrend
                                    {
                                        AircraftRegional = item.Attribute("Name").Value,
                                        DateTime = currentTime,
                                        AirNum = Convert.ToInt32(item.Value),
                                        Amount = Convert.ToInt32(currentAmount)
                                    };
                                    if (regionalcolor != null)
                                    {
                                        var firstOrDefault = regionalcolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetaircraftregionaltrend.AircraftRegional, StringComparison.OrdinalIgnoreCase));
                                        if (firstOrDefault != null)
                                            fleetaircraftregionaltrend.Color = firstOrDefault.Attribute("Color").Value;
                                    }
                                    regionalCollection.Add(fleetaircraftregionaltrend);
                                }
                            }
                        }
                    }
                }
            }
            #endregion


            FleetAircraftTypeTrendCollection = regionalCollection;
            AircraftAmountCollection = amountCollection;


            //对界面数据集合进行重新初始化
            SelectedTime = "所选时间";
            FleetAircraftRegionalCollection = null;
            FleetAircraftTypeCollection = null;
            AircraftCollection = null;
            ZoomInit();
        }

        /// <summary>
        /// 趋势图的选择点事件数据源
        /// </summary>
        public void ChartSelectionBehaviorSelection(DateTime time)
        {
            var aircraft = Aircrafts.
                        Where(o => o.OperationHistories.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time))
                        && o.AircraftBusinesses.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)));

            #region 座级机型XML文件的读写
            var xmlconfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("座级机型", StringComparison.OrdinalIgnoreCase));

            XElement regionalcolor = null;
            var colorconfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase)))
            {
                regionalcolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase));
            }
            XElement typecolor = null;
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typecolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlconfig != null)
            {
                var aircraftRegionalList = new List<FleetAircraftTypeComposition>();//座级饼图集合
                var aircraftTypeList = new List<FleetAircraftTypeComposition>();//机型饼图集合

                XElement xelement = XElement.Parse(xmlconfig.ConfigContent).Descendants("DateTime").FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                if (xelement != null)
                {
                    foreach (XElement type in xelement.Descendants("Type"))
                    {
                        if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XElement item in type.Descendants("Item"))
                            {
                                var fleetaircrafttypecomposition = new FleetAircraftTypeComposition
                                {
                                    AircraftRegional = item.Attribute("Name").Value,
                                    AirNum = Convert.ToInt32(item.Value),
                                    AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                };
                                if (regionalcolor != null)
                                {
                                    var firstOrDefault = regionalcolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetaircrafttypecomposition.AircraftRegional, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetaircrafttypecomposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetaircrafttypecomposition.AirNum > 0)
                                {
                                    aircraftRegionalList.Add(fleetaircrafttypecomposition);
                                }
                            }
                        }
                        else if (type.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XElement item in type.Descendants("Item"))
                            {
                                var fleetaircrafttypecomposition = new FleetAircraftTypeComposition
                                {
                                    AircraftRegional = item.Attribute("Name").Value,
                                    AirNum = Convert.ToInt32(item.Value),
                                    AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                };
                                if (typecolor != null)
                                {
                                    var firstOrDefault = typecolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetaircrafttypecomposition.AircraftRegional, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetaircrafttypecomposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetaircrafttypecomposition.AirNum > 0)
                                {
                                    aircraftTypeList.Add(fleetaircrafttypecomposition);
                                }
                            }
                        }
                    }
                }

                FleetAircraftRegionalCollection = aircraftRegionalList;
                FleetAircraftTypeCollection = aircraftTypeList;
            }
            #endregion

            //飞机详细数据
            //AircraftCollection = Commonmethod.GetAircraftByTime(aircraft.ToList(), time);
        }

        #endregion
        #endregion

        #region Class

        /// <summary>
        ///饼图的分布对象
        /// </summary>
        public class FleetAircraftTypeComposition
        {
            public FleetAircraftTypeComposition()
            {
                Color = Commonmethod.GetRandomColor();
            }
            public string AircraftRegional { get; set; }
            public decimal AirNum { get; set; }
            public string AirTt { get; set; }
            public string Color { get; set; }
        }

        /// <summary>
        /// 座级趋势对象
        /// </summary>
        public class FleetAircraftRegionalTrend
        {
            public FleetAircraftRegionalTrend()
    {
                Color = Commonmethod.GetRandomColor();
            }
            public string AircraftRegional { get; set; }//座级名称
            public int AirNum { get; set; }//座级的飞机数
            public string DateTime { get; set; }//时间点
            public int Amount { get; set; }//飞机数
            public string Color { get; set; }//颜色
        }

        #endregion
    }
}
