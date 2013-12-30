#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:07:56
// 文件名：CountRegisteredFleetVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:07:56
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Data;
using UniCloud.Presentation.Export;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.CommonExtension;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion


namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(CountRegisteredFleetVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CountRegisteredFleetVm : ViewModelBase
    {
        #region 声明、初始化
        private FleetPlanData _fleetPlanDataService;
        public CountRegisteredFleet CurrentCountRegisteredFleet
        {
            get { return ServiceLocator.Current.GetInstance<CountRegisteredFleet>(); }
        }
        private static readonly CommonMethod CommonMethod = new CommonMethod();


        private int _i; //导出数据源格式判断
        private Grid _monthGrid, _yearGrid;
        private RadDateTimePicker _startDateTimePicker, _endDateTimePicker;//开始时间控件， 结束时间控件
        private RadGridView _monthExportRadgridview, _yearExportRadgridview; //初始化RadGridView

        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        public CountRegisteredFleetVm()
        {
            ExportCommand = new DelegateCommand<object>(OnExport, CanExport);//导出图表源数据（Source data）
            ViewModelInitializer();
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
            _monthGrid = CurrentCountRegisteredFleet.MonthGrid;
            _yearGrid = CurrentCountRegisteredFleet.YearGrid;
            //控制界面起止时间控件的字符串格式化
            _startDateTimePicker = CurrentCountRegisteredFleet.StartDateTimePicker;
            _endDateTimePicker = CurrentCountRegisteredFleet.EndDateTimePicker;
            _startDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
            _endDateTimePicker.Culture.DateTimeFormat.ShortDatePattern = "yyyy/M";
        }
        #endregion

        #region 数据
        #region 公共数据
        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; }//XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; } //飞机集合 

        #region ViewModel 属性  CanExportData
        private bool _canExport = true;//数据是否可导出
        public bool CanExportData
        {
            get { return _canExport; }
            set
            {
                _canExport = value;
                RaisePropertyChanged(() => CanExportData);
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
                    CreatFleetRegisteredTrendMonthCollection();
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
                    CreatFleetRegisteredTrendMonthCollection();
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

        #region ViewModel 属性 FleetAircraftTrendMonthCollection --月平均在册的机型飞机数集合
        private List<FleetRegisteredTrend> _fleetRegisteredTrendMonthCollection;
        /// <summary>
        /// 月平均在册的机型飞机数集合
        /// </summary>
        public List<FleetRegisteredTrend> FleetRegisteredTrendMonthCollection
        {
            get { return _fleetRegisteredTrendMonthCollection; }
            set
            {
                if (FleetRegisteredTrendMonthCollection != value)
                {
                    _fleetRegisteredTrendMonthCollection = value;
                    FleetRegisteredTrendMonthCollectionChange(_fleetRegisteredTrendMonthCollection);
                    RaisePropertyChanged(() => FleetRegisteredTrendMonthCollection);
                }
            }
        }
        #endregion

        #region ViewModel 属性 FleetRegisteredTrendYearCollection --年平均在册的机型飞机数集合
        private List<FleetRegisteredTrend> _fleetRegisteredTrendYearCollection;
        /// <summary>
        /// 年平均在册的机型飞机数集合
        /// </summary>
        public List<FleetRegisteredTrend> FleetRegisteredTrendYearCollection
        {
            get { return _fleetRegisteredTrendYearCollection; }
            set
            {
                if (!Equals(FleetRegisteredTrendYearCollection, value))
                {
                    _fleetRegisteredTrendYearCollection = value;
                    FleetRegisteredTrendYearCollectionChange(_fleetRegisteredTrendYearCollection);
                    RaisePropertyChanged(() => FleetRegisteredTrendYearCollection);
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
            //Aircrafts.Load(true);
        }

        #endregion
        #endregion

        #region 操作

        /// <summary>
        /// 创建GradDataView
        /// </summary>
        /// <param name="structs"></param>
        /// <param name="itemsSource"></param>
        /// <param name="headername"></param>
        public void CreateDataGridView(Dictionary<string, string> structs, List<FleetRegisteredTrend> itemsSource, string headername)
        {
            if (headername.Equals("FleetTrendPnrMonth", StringComparison.OrdinalIgnoreCase))
            {
                _monthExportRadgridview = ImageAndGridOperation.CreatDataGridView(structs, itemsSource, headername);
            }
            else
            {
                _yearExportRadgridview = ImageAndGridOperation.CreatDataGridView(structs, itemsSource, headername);
            }
        }

        /// <summary>
        /// 选择开始时间
        /// </summary>
        /// <param name="dataTimeStart"></param>
        public void SelectedStartValueChange(DateTime? dataTimeStart)
        {
            _startDateTimePicker.SelectedValue = dataTimeStart;
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
        /// 年平均在册的机型飞机数集合变化时
        /// </summary>
        /// <param name="fleetRegisteredTrendYearCollection"></param>
        public void FleetRegisteredTrendYearCollectionChange(List<FleetRegisteredTrend> fleetRegisteredTrendYearCollection)
        {
            var radCartesianChart = _yearGrid.Children[0] as RadCartesianChart;
            var stackpanel = ((ScrollViewer)_yearGrid.Children[1]).Content as StackPanel;

            if (radCartesianChart != null) radCartesianChart.Series.Clear();
            if (stackpanel != null)
            {
                stackpanel.Children.Clear();

                if (FleetRegisteredTrendYearCollection != null)
                {
                    foreach (var groupItem in FleetRegisteredTrendYearCollection.GroupBy(p => p.AircraftType).ToList())
                    {
                        var fleetRegisteredTrend = groupItem.FirstOrDefault();
                        if (fleetRegisteredTrend != null)
                        {
                            var line = new LineSeries
                                {
                                    StrokeThickness = 3,
                                    DisplayName = groupItem.Key,
                                    Stroke = new SolidColorBrush(CommonMethod.GetColor(fleetRegisteredTrend.Color)),
                                    CategoryBinding = CommonMethod.CreateBinding("DateTime"),
                                    ValueBinding = CommonMethod.CreateBinding("RegisteredCount"),
                                    ItemsSource = groupItem.ToList()
                                };
                            if (line.DisplayName != "所有机型")
                            {
                                line.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                CurrentCountRegisteredFleet.YearLinearAxis.ElementBrush = line.Stroke;
                            }
                            line.PointTemplate = CurrentCountRegisteredFleet.Resources["PointTemplate"] as DataTemplate;
                            line.TrackBallInfoTemplate = CurrentCountRegisteredFleet.Resources["TrackBallInfoTemplate"] as DataTemplate;
                            if (radCartesianChart != null) radCartesianChart.Series.Add(line);

                            var panel = new StackPanel
                                {
                                    Margin = new Thickness(5, 5, 5, 5),
                                    Orientation = Orientation.Horizontal,
                                    Background = new SolidColorBrush(CommonMethod.GetColor(fleetRegisteredTrend.Color))
                                };
                            var checkbox = new CheckBox { IsChecked = line.DisplayName.Equals("所有机型", StringComparison.OrdinalIgnoreCase) };
                            checkbox.Checked += CheckboxChecked;
                            checkbox.Unchecked += CheckboxUnchecked;
                            checkbox.Content = groupItem.Key;
                            checkbox.Foreground = new SolidColorBrush(Colors.White);
                            checkbox.VerticalAlignment = VerticalAlignment.Center;
                            checkbox.Style = CurrentCountRegisteredFleet.Resources["LegengCheckBoxStyle"] as Style;
                            panel.Children.Add(checkbox);
                            stackpanel.Children.Add(panel);
                        }
                    }
                }
            }

            //控制趋势图的滚动条
            int datetimecount = FleetRegisteredTrendYearCollection.Select(p => p.DateTime).Distinct().Count();
            if (FleetRegisteredTrendYearCollection != null && datetimecount >= 12)
            {
                CurrentCountRegisteredFleet.YearCategoricalAxis.MajorTickInterval = datetimecount / 6;
            }
            else
            {
                CurrentCountRegisteredFleet.YearCategoricalAxis.MajorTickInterval = 1;
            }
        }

        /// <summary>
        /// 月平均在册的机型飞机数集合变化时
        /// </summary>
        /// <param name="fleetRegisteredTrendMonthCollection"></param>
        public void FleetRegisteredTrendMonthCollectionChange(List<FleetRegisteredTrend> fleetRegisteredTrendMonthCollection)
        {
            var radCartesianChart = _monthGrid.Children[0] as RadCartesianChart;
            var stackpanel = ((ScrollViewer)_monthGrid.Children[1]).Content as StackPanel;

            if (radCartesianChart != null) radCartesianChart.Series.Clear();
            if (stackpanel != null)
            {
                stackpanel.Children.Clear();

                if (FleetRegisteredTrendMonthCollection != null)
                {
                    foreach (var groupItem in FleetRegisteredTrendMonthCollection.GroupBy(p => p.AircraftType).ToList())
                    {
                        var fleetRegisteredTrend = groupItem.FirstOrDefault();
                        if (fleetRegisteredTrend != null)
                        {
                            var line = new LineSeries
                                {
                                    StrokeThickness = 3,
                                    DisplayName = groupItem.Key,
                                    Stroke = new SolidColorBrush(CommonMethod.GetColor(fleetRegisteredTrend.Color)),
                                    CategoryBinding = CommonMethod.CreateBinding("DateTime"),
                                    ValueBinding = CommonMethod.CreateBinding("RegisteredCount"),
                                    ItemsSource = groupItem.ToList()
                                };
                            if (line.DisplayName != "所有机型")
                            {
                                line.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                CurrentCountRegisteredFleet.MonthLinearAxis.ElementBrush = line.Stroke;
                            }
                            line.PointTemplate = CurrentCountRegisteredFleet.Resources["PointTemplate"] as DataTemplate;
                            line.TrackBallInfoTemplate = CurrentCountRegisteredFleet.Resources["TrackBallInfoTemplate"] as DataTemplate;
                            if (radCartesianChart != null) radCartesianChart.Series.Add(line);

                            var panel = new StackPanel
                                {
                                    Margin = new Thickness(5, 5, 5, 5),
                                    Orientation = Orientation.Horizontal,
                                    Background = new SolidColorBrush(CommonMethod.GetColor(fleetRegisteredTrend.Color))
                                };
                            var checkbox = new CheckBox { IsChecked = line.DisplayName.Equals("所有机型", StringComparison.OrdinalIgnoreCase) };
                            checkbox.Checked += CheckboxChecked;
                            checkbox.Unchecked += CheckboxUnchecked;
                            checkbox.Content = groupItem.Key;
                            checkbox.Foreground = new SolidColorBrush(Colors.White);
                            checkbox.VerticalAlignment = VerticalAlignment.Center;
                            checkbox.Style = CurrentCountRegisteredFleet.Resources["LegengCheckBoxStyle"] as Style;
                            panel.Children.Add(checkbox);
                            stackpanel.Children.Add(panel);
                        }
                    }
                }
            }

            //控制趋势图的滚动条
            int datetimecount = FleetRegisteredTrendMonthCollection.Select(p => p.DateTime).Distinct().Count();
            if (FleetRegisteredTrendMonthCollection != null && datetimecount >= 12)
            {
                CurrentCountRegisteredFleet.MonthCategoricalAxis.MajorTickInterval = datetimecount / 6;
            }
            else
            {
                CurrentCountRegisteredFleet.MonthCategoricalAxis.MajorTickInterval = 1;
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
                var grid = ((ScrollViewer)((StackPanel)((StackPanel)checkbox.Parent).Parent).Parent).Parent as Grid;
                if (grid != null && grid.Name.Equals("MonthGrid", StringComparison.OrdinalIgnoreCase))
                {
                    var chart = _monthGrid.Children[0] as RadCartesianChart;
                    if (chart != null)
                    {
                        var firstOrDefault = chart.Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                        if (firstOrDefault != null)
                            firstOrDefault.Visibility = Visibility.Visible;

                        Size size = chart.Zoom;
                        chart.Zoom = new Size(size.Width + 0.01, size.Height);
                        chart.Zoom = size;
                    }
                }
                else if (grid != null && grid.Name.Equals("YearGrid", StringComparison.OrdinalIgnoreCase))
                {
                    var chart = _yearGrid.Children[0] as RadCartesianChart;
                    if (chart != null)
                    {
                        var firstOrDefault = chart.Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                        if (firstOrDefault != null)
                            firstOrDefault.Visibility = Visibility.Visible;

                        Size size = chart.Zoom;
                        chart.Zoom = new Size(size.Width + 0.01, size.Height);
                        chart.Zoom = size;
                    }
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
                var grid = ((ScrollViewer)((StackPanel)((StackPanel)checkbox.Parent).Parent).Parent).Parent as Grid;
                if (grid != null && grid.Name.Equals("MonthGrid", StringComparison.OrdinalIgnoreCase))
                {
                    var firstOrDefault = ((RadCartesianChart)_monthGrid.Children[0]).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                    if (
                        firstOrDefault != null)
                        firstOrDefault.Visibility = Visibility.Collapsed;
                }
                else if (grid != null && grid.Name.Equals("YearGrid", StringComparison.OrdinalIgnoreCase))
                {
                    CartesianSeries first = ((RadCartesianChart)_yearGrid.Children[0]).Series.FirstOrDefault(p => p.DisplayName.Equals(checkbox.Content.ToString(), StringComparison.OrdinalIgnoreCase));
                    if (first != null) first.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// 控制右键的打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (menu.Name.Equals("MonthGridData", StringComparison.OrdinalIgnoreCase))
                {
                    _i = 1;
                    _monthExportRadgridview.ElementExporting -= ElementExporting;
                    _monthExportRadgridview.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _monthExportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (menu.Name.Equals("YearGridData", StringComparison.OrdinalIgnoreCase))
                {
                    _i = 1;
                    _yearExportRadgridview.ElementExporting -= ElementExporting;
                    _yearExportRadgridview.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _yearExportRadgridview.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
            }
            else if (menu != null && menu.Header.ToString().Equals("导出图片", StringComparison.OrdinalIgnoreCase))
            {
                if (menu.Name.Equals("MonthGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_monthGrid != null)
                    {
                        CommonMethod.ExportToImage(_monthGrid);
                    }
                }
                else if (menu.Name.Equals("YearGridImage", StringComparison.OrdinalIgnoreCase))
                {
                    //导出图片
                    if (_yearGrid != null)
                    {
                        CommonMethod.ExportToImage(_monthGrid);
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
                if (_i % 4 == 3 && _i >= 7)
                {
                    var a = sender as RadGridView;
                    if (a != null && a.Name.Equals("FleetTrendPnrMonth", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                    }
                }
            }
            _i++;
        }

        /// <summary>
        /// 右键数据导出是否可使用
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        bool CanExport(object sender)
        {
            return CanExportData;
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
                CreatFleetRegisteredTrendMonthCollection();
                CreatFleetRegisteredTrendYearCollection();
            }
        }

        /// <summary>
        /// 获取月平均在册飞机数趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreatFleetRegisteredTrendMonthCollection()
        {
            var fleetRegisteredTrendMonthList = new List<FleetRegisteredTrend>();//月在册的机型飞机数集合
            var allFleetRegisteredTrendMonthCollection = new List<FleetRegisteredTrend>();

            #region 飞机运力XML文件的读写
            var xmlconfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("在册分析", StringComparison.OrdinalIgnoreCase));

            string aircraftColor = string.Empty;
            var colorconfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p =>
            {
                var xAttributeTypeName = p.Attribute("TypeName");
                return xAttributeTypeName != null && xAttributeTypeName.Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase);
            }))
            {
                var firstOrDefault = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (
                    firstOrDefault != null)
                {
                    var orDefault = firstOrDefault
                        .Descendants("Item").FirstOrDefault(p =>
                                                            {
                                                                var xAttributeName = p.Attribute("Name");
                                                                return xAttributeName != null && xAttributeName.Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase);
                                                            });
                    if (orDefault != null)
                        aircraftColor = orDefault.Attribute("Color").Value;
                }
            }

            XElement typecolor = null;
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typecolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
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

                        XElement typexelement = datetime.Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
                        if (typexelement != null)
                        {
                            //平均在册总飞机数的柱状数据
                            var amounttrendmnth = new FleetRegisteredTrend
                                                  {
                                                      DateTime = currentTime,
                                                      RegisteredCount =
                                                          Convert.ToDouble(
                                                              typexelement.Attribute("Amount")
                                                          .Value),
                                                      AircraftType = "所有机型"
                                                  };//月平均在册的总飞机数对象
                            if (!string.IsNullOrEmpty(aircraftColor))
                            {
                                amounttrendmnth.Color = aircraftColor;
                            }
                            fleetRegisteredTrendMonthList.Add(amounttrendmnth);

                            foreach (XElement item in typexelement.Descendants("Item"))
                            {
                                var fleetregisteredtrenmonth = new FleetRegisteredTrend
                                                               {
                                                                   DateTime = currentTime,
                                                                   AircraftType =
                                                                       item.Attribute("Name")
                                                                       .Value,
                                                                   RegisteredCount =
                                                                       Convert.ToDouble(item.Value)
                                                               };//月平均在册的机型飞机数对象
                                if (typecolor != null)
                                {
                                    var firstOrDefault = typecolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetregisteredtrenmonth.AircraftType, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault !=
                                        null)
                                        fleetregisteredtrenmonth.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                fleetRegisteredTrendMonthList.Add(fleetregisteredtrenmonth);
                            }
                        }
                    }
                }
            }
            #endregion

            FleetRegisteredTrendMonthCollection = fleetRegisteredTrendMonthList;
            allFleetRegisteredTrendMonthCollection.AddRange(FleetRegisteredTrendMonthCollection);
            //创建RadGridView
            var columnsList = new Dictionary<string, string>
                {
                    {"DateTime", "时间点"},
                    {"AircraftType", "机型"},
                    {"RegisteredCount", "月平均在册飞机数"}
                };
            CreateDataGridView(columnsList, allFleetRegisteredTrendMonthCollection, "FleetTrendPnrMonth");
        }

        /// <summary>
        /// 获取年平均在册飞机数趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreatFleetRegisteredTrendYearCollection()
        {
            var fleetRegisteredTrendMonthList = new List<FleetRegisteredTrend>();//月平均在册的机型飞机数集合

            #region 飞机运力XML文件的读写
            var xmlconfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("在册分析", StringComparison.OrdinalIgnoreCase));

            string aircraftColor = string.Empty;
            var colorconfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                var firstOrDefault = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (
                    firstOrDefault != null)
                {
                    var orDefault = firstOrDefault.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                    if (orDefault != null)
                        aircraftColor = orDefault.Attribute("Color").Value;
                }
            }

            XElement typecolor = null;
            if (colorconfig != null && XElement.Parse(colorconfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typecolor = XElement.Parse(colorconfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
            }

            if (xmlconfig != null)
            {
                XElement xelement = XElement.Parse(xmlconfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (XElement datetime in xelement.Descendants("DateTime"))
                    {
                        string currentTime = Convert.ToDateTime(datetime.Attribute("EndOfMonth").Value).ToString("yyyy/M");

                        XElement typexelement = datetime.Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
                        if (typexelement != null)
                        {
                            //平均在册总飞机数的柱状数据
                            var amounttrendmnth = new FleetRegisteredTrend
                                                  {
                                                      DateTime = currentTime,
                                                      RegisteredCount =
                                                          Convert.ToDouble(
                                                              typexelement.Attribute("Amount")
                                                          .Value),
                                                      AircraftType = "所有机型"
                                                  };//月平均在册的总飞机数对象
                            if (!string.IsNullOrEmpty(aircraftColor))
                            {
                                amounttrendmnth.Color = aircraftColor;
                            }
                            fleetRegisteredTrendMonthList.Add(amounttrendmnth);

                            foreach (XElement item in typexelement.Descendants("Item"))
                            {
                                var fleetregisteredtrenmonth = new FleetRegisteredTrend
                                                               {
                                                                   DateTime = currentTime,
                                                                   AircraftType =
                                                                       item.Attribute("Name")
                                                                       .Value,
                                                                   RegisteredCount =
                                                                       Convert.ToDouble(item.Value)
                                                               };//月平均在册的机型飞机数对象
                                if (typecolor != null)
                                {
                                    var firstOrDefault = typecolor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetregisteredtrenmonth.AircraftType, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault !=
                                        null)
                                        fleetregisteredtrenmonth.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                fleetRegisteredTrendMonthList.Add(fleetregisteredtrenmonth);
                            }
                        }
                    }
                }
            }
            #endregion

            var fleetregisteredtrendyearlist = new List<FleetRegisteredTrend>();//年平均在册的机型飞机数集合
            var allFleetRegisteredtrendYeatList = new List<FleetRegisteredTrend>();
            if (fleetRegisteredTrendMonthList.Any())
            {
                //获取按机型分类的年平均在册飞机数的集合
                //原有的linq语句转为普通语句--ToList方法少了ForEach方法
                var fleetregisteredtrendmonth = fleetRegisteredTrendMonthList.GroupBy(p => p.AircraftType).ToList();
                foreach (var variablemonth in fleetregisteredtrendmonth)
                {
                    var vaiableYear = variablemonth.GroupBy(pp => Convert.ToDateTime(pp.DateTime).Year).ToList();
                    foreach (var variableyear in vaiableYear)
                    {
                        var fleetRegisterYear = new FleetRegisteredTrend();
                        var registeredTrend = variableyear.FirstOrDefault();
                        if (registeredTrend != null)
                            fleetRegisterYear.DateTime = registeredTrend.DateTime;
                        var firstOrDefault = variablemonth.FirstOrDefault();
                        if (firstOrDefault != null)
                            fleetRegisterYear.AircraftType = firstOrDefault.AircraftType;
                        fleetRegisterYear.RegisteredCount = Math.Round(variableyear.Sum(a => a.RegisteredCount * Convert.ToDateTime(a.DateTime).AddMonths(1).AddDays(-1).Day) / (new DateTime(variableyear.Key + 1, 1, 1) - new DateTime(variableyear.Key, 1, 1)).TotalDays, 4);
                        var fleetRegisteredTrend = variablemonth.FirstOrDefault();
                        if (fleetRegisteredTrend != null)
                            fleetRegisterYear.Color = fleetRegisteredTrend.Color;
                        fleetregisteredtrendyearlist.Add(fleetRegisterYear);
                    }
                }

                //fleetRegisteredTrendMonthList.GroupBy(p => p.AircraftType).ToList().ForEach(p =>
                //    fleetregisteredtrendyearlist.AddRange(p.GroupBy(pp => Convert.ToDateTime(pp.DateTime).Year).Select(o => new FleetRegisteredTrend
                //    {
                //        DateTime = o.Key.ToString(),
                //        AircraftType = p.Key,
                //        RegisteredCount = Math.Round(o.Sum(a => a.RegisteredCount * Convert.ToDateTime(a.DateTime).AddMonths(1).AddDays(-1).Day) / (new DateTime(o.Key + 1, 1, 1) - new DateTime(o.Key, 1, 1)).TotalDays, 4),
                //        Color = p.FirstOrDefault().Color
                //    })
                //    )
                //);
            }

            FleetRegisteredTrendYearCollection = fleetregisteredtrendyearlist;
            allFleetRegisteredtrendYeatList.AddRange(FleetRegisteredTrendYearCollection);

            //创建RadGridView
            var columnsList2 = new Dictionary<string, string>
                {
                    {"DateTime", "年份"},
                    {"AircraftType", "机型"},
                    {"RegisteredCount", "年平均在册飞机数"}
                };
            CreateDataGridView(columnsList2, allFleetRegisteredtrendYeatList, "FleetTrendPnrYear");
        }

        #endregion
        #endregion

        #region Class

        /// <summary>
        /// 趋势图的对象
        /// </summary>
        public class FleetRegisteredTrend
        {
            public FleetRegisteredTrend()
            {
                Color = CommonMethod.GetRandomColor();
            }
            public string AircraftType { get; set; }//机型名称
            public double RegisteredCount { get; set; }//机型的平均在册飞机数
            public string DateTime { get; set; }//时间点
            public string Color { get; set; }//颜色
        }

        #endregion
    }
}

