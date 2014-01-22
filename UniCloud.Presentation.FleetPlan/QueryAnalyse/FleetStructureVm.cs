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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
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
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Export;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof (FleetStructureVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetStructureVm : ViewModelBase
    {
        #region 声明、初始化

        private static readonly CommonMethod Commonmethod = new CommonMethod();
        private readonly FleetPlanData _fleetPlanContext;
        private readonly RadWindow _regionalWindow = new RadWindow(); //用于单击座级饼状图的用户提示
        private readonly RadWindow _typeWindow = new RadWindow(); //用于单击机型饼状图的用户提示
        private RadGridView _aircraftDetail; //初始化RadGridView
        private RadGridView _exportRadgridview; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _lineGrid; //趋势折线图区域，趋势柱状图区域， 座级饼图区域，机型饼图区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;
        private Grid _regionalPieGrid; //趋势折线图区域，趋势柱状图区域， 座级饼图区域，机型饼图区域
        private Grid _typePieGrid; //趋势折线图区域，趋势柱状图区域， 座级饼图区域，机型饼图区域

        [ImportingConstructor]
        public FleetStructureVm(IFleetPlanService service) : base(service)
        {
            _fleetPlanContext = service.Context;
            ViewModelInitializer();
            InitalizerRadWindows(_regionalWindow, "Regional", 220);
            InitalizerRadWindows(_typeWindow, "Type", 240);
            AddRadMenu(_regionalWindow);
            AddRadMenu(_typeWindow);
            InitializeVm();
        }

        public FleetStructure CurrrentFleetStructure
        {
            get { return ServiceLocator.Current.GetInstance<FleetStructure>(); }
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
            ExportCommand = new DelegateCommand<object>(OnExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView);
            ToggleButtonCommand = new DelegateCommand<object>(ToggleButtonCheck);
            _lineGrid = CurrrentFleetStructure.LineGrid;
            _regionalPieGrid = CurrrentFleetStructure.RegionalPieGrid;
            _typePieGrid = CurrrentFleetStructure.TypePieGrid;
            _aircraftDetail = CurrrentFleetStructure.AircraftDetail;
        }

        #endregion

        #region 数据

        #region 公共数据

        private ObservableCollection<FleetData> _fleetDatas;
        private ObservableCollection<FleetAircraftRegionalTrend> _regionals;
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

        public ObservableCollection<FleetAircraftRegionalTrend> Regionals
        {
            get { return _regionals; }
            set
            {
                _regionals = value;
                RaisePropertyChanged("Regionals");
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
        ///     座级趋势图的数据源集合
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
        ///     柱状趋势图的数据源集合
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
        ///     座级饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftRegionalCollection
        {
            get { return _fleetAircraftRegionalCollection; }
            set
            {
                if (!Equals(_fleetAircraftRegionalCollection, value))
                {
                    _fleetAircraftRegionalCollection = value;
                    RaisePropertyChanged(() => FleetAircraftRegionalCollection);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftTypeCollection--机型饼图的数据集合（指定时间点）

        private IEnumerable<FleetAircraftTypeComposition> _fleetAircraftTypeCollection;

        /// <summary>
        ///     机型饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftTypeCollection
        {
            get { return _fleetAircraftTypeCollection; }
            set
            {
                if (Equals(_fleetAircraftTypeCollection, value)) return;
                _fleetAircraftTypeCollection = value;
                RaisePropertyChanged(() => FleetAircraftTypeCollection);
            }
        }

        #endregion

        #region ViewModel 属性 AircraftDataObjectList --座级（机型)饼图所对应的所有飞机数据（指定时间点）

        private List<AircraftDTO> _aircraftCollection;

        /// <summary>
        ///     座级（机型)饼图所对应的所有飞机数据（指定时间点）
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

        #region ViewModel 属性 SelectedTimeRegional --座级饼图的标识提示

        private string _selectedTimeRegional = "所选时间的座级分布图";

        /// <summary>
        ///     座级饼图的标识提示
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
        ///     机型饼图的标识提示
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
                    CreateFleetAircraftTypeTrendCollection();
                    RaisePropertyChanged(() => SelectedIndex);
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
                    CreateFleetAircraftTypeTrendCollection();
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
                    CreateFleetAircraftTypeTrendCollection();
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

        #region 操作

        #region Method

        public DelegateCommand<object> ToggleButtonCommand { get; set; }

        /// <summary>
        ///     控制趋势图滚动条
        /// </summary>
        /// <param name="aircraftAmountCollection"></param>
        public void ControlTrendScroll(List<FleetAircraftRegionalTrend> aircraftAmountCollection)
        {
            if (aircraftAmountCollection != null && aircraftAmountCollection.Count() >= 12)
            {
                CurrrentFleetStructure.LineCategoricalAxis.MajorTickInterval = aircraftAmountCollection.Count()/6;
                CurrrentFleetStructure.BarCategoricalAxis.MajorTickInterval = aircraftAmountCollection.Count()/6;
            }
            else
            {
                CurrrentFleetStructure.LineCategoricalAxis.MajorTickInterval = 1;
                CurrrentFleetStructure.BarCategoricalAxis.MajorTickInterval = 1;
            }
        }

        /// <summary>
        ///     座级数据源绑定界面
        /// </summary>
        /// <param name="fleetAircraftTypeTrendCollection"></param>
        public void AircraftTypeTrendInit(List<FleetAircraftRegionalTrend> fleetAircraftTypeTrendCollection)
        {
            FleetDatas = new ObservableCollection<FleetData>();
            StaticFleetDatas = new ObservableCollection<FleetData>();
            var result = new ObservableCollection<FleetData>();
            var regionaleResult = new ObservableCollection<FleetAircraftRegionalTrend>();

            if (fleetAircraftTypeTrendCollection != null)
            {
                foreach (var groupItem in fleetAircraftTypeTrendCollection.GroupBy(p => p.AircraftRegional).ToList())
                {
                    var fleetAircraftRegionalTrend = groupItem.FirstOrDefault();
                    if (fleetAircraftRegionalTrend != null)
                    {
                        var tempData = new FleetData
                        {
                            AircraftTypeName = groupItem.Key,
                            Data = new ObservableCollection<FleetAircraftRegionalTrend>()
                        };
                        groupItem.ToList().ForEach(tempData.Data.Add);
                        result.Add(tempData);
                        regionaleResult.Add(fleetAircraftRegionalTrend);
                    }
                }
            }

            result.ToList().ForEach(FleetDatas.Add);
            result.ToList().ForEach(StaticFleetDatas.Add);
            Regionals = regionaleResult;
        }

        /// <summary>
        ///     关闭座级机型子窗口
        /// </summary>
        public void CloseRegionAndTypeWindows()
        {
            _regionalWindow.Close();
            _typeWindow.Close();
        }

        /// <summary>
        ///     初始化时初始化滚动条
        /// </summary>
        public void ZoomInit()
        {
            Zoom = new Size(1, 1);
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
            radwindow.Height = 300;
            radwindow.Width = 600;
            radwindow.ResizeMode = ResizeMode.CanResize;
            radwindow.Content = Commonmethod.CreatOperationGridView();
            radwindow.Closed += RadwindowClosed;
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
            if (radWindow != null && radWindow.Name.Equals("Regional", StringComparison.OrdinalIgnoreCase))
            {
                grid = _regionalPieGrid;
            }
            else if (radWindow != null && radWindow.Name.Equals("Type", StringComparison.OrdinalIgnoreCase))
            {
                grid = _typePieGrid;
            }

            //更改对应饼图的突出显示
            foreach (var item in ((RadPieChart) grid.Children[0]).Series[0].DataPoints)
            {
                item.IsSelected = false;
            }
            //更改对应饼图的标签大小
            ((RadLegend) grid.Children[1]).Items.ToList().ForEach(p => p.IsHovered = false);
        }

        /// <summary>
        ///     趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            DataPoint selectedPoint =
                ((ChartSelectionBehavior) sender).Chart.SelectedPoints.FirstOrDefault(
                    p => ((CategoricalSeries) p.Presenter).Visibility == Visibility.Visible);
            if (selectedPoint != null)
            {
                var fleetAircraftRegionalTrend = selectedPoint.DataItem as FleetAircraftRegionalTrend;
                if (fleetAircraftRegionalTrend != null && SelectedTime != fleetAircraftRegionalTrend.DateTime)
                {
                    //选中时间点
                    SelectedTime = fleetAircraftRegionalTrend.DateTime;

                    DateTime time = Convert.ToDateTime(fleetAircraftRegionalTrend.DateTime).AddMonths(1).AddDays(-1);
                    ChartSelectionBehaviorSelection(time);
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
            RadChartBase radChartBase = ((ChartSelectionBehavior) sender).Chart;
            var selectedPoint = radChartBase.SelectedPoints.FirstOrDefault() as PieDataPoint;
            var items = new LegendItemCollection();
            if (radChartBase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
            {
                items = ((RadLegend) _regionalPieGrid.Children[1]).Items;
            }
            else if (radChartBase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
            {
                items = ((RadLegend) _typePieGrid.Children[1]).Items;
            }

            if (selectedPoint != null)
            {
                items.ToList().ForEach(p => p.IsHovered = false);
                foreach (var item in items)
                {
                    if (item.Title.Equals(((FleetAircraftTypeComposition) selectedPoint.DataItem).AircraftRegional,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        item.IsHovered = true;
                        break;
                    }
                }
                if (radChartBase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                {
                    GetGridViewDataSourse(selectedPoint, _regionalWindow, "座级");
                }
                else if (radChartBase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                {
                    GetGridViewDataSourse(selectedPoint, _typeWindow, "机型");
                }
            }
            else
            {
                if (radChartBase.EmptyContent.ToString().Equals("座级分布", StringComparison.OrdinalIgnoreCase))
                {
                    _regionalWindow.Close();
                }
                else if (radChartBase.EmptyContent.ToString().Equals("机型分布", StringComparison.OrdinalIgnoreCase))
                {
                    _typeWindow.Close();
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
                            p => p.AircraftTypeName.Equals((string) button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (temp != null &&
                        !FleetDatas.Any(
                            p => p.AircraftTypeName.Equals(temp.AircraftTypeName, StringComparison.OrdinalIgnoreCase)))
                    {
                        FleetDatas.Add(temp);
                    }
                }
                else
                {
                    for (int i = FleetDatas.Count - 1; i > -1; i--)
                    {
                        var temp = FleetDatas[i];
                        if (temp.AircraftTypeName.Equals((string) button.Tag, StringComparison.OrdinalIgnoreCase))
                        {
                            FleetDatas.Remove(temp);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     控制趋势图中折线（饼状）的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    var columnsList = new Dictionary<string, string>
                    {
                        {"DateTime", "时间点"},
                        {"AircraftType", "机型"},
                        {"AirNum", "飞机数"},
                        {"Amount", "期末飞机数"}
                    };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetAircraftTypeTrendCollection, "FleetStructure");

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
                else if (menu.Name.Equals("RegionalPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftRegionalCollection == null || !FleetAircraftRegionalCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> {{"AircraftType", "座级"}, {"AirNum", "飞机数"}};
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetAircraftRegionalCollection, "RegionalPieStructure");

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
                else if (menu.Name.Equals("TypePieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftTypeCollection == null || !FleetAircraftTypeCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> {{"AircraftType", "机型"}, {"AirNum", "飞机数(架)"}};
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetAircraftTypeCollection, "TypePieStructure");

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
                if (_i%5 == 3 && _i >= 8 &&
                    ((RadGridView) sender).Name.Equals("FleetStructure", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }

        #endregion

        #region ViewModel 命令 --导出数据AircraftDetail

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

        #region Methods

        /// <summary>
        ///     根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem">选中点</param>
        /// <param name="radWindow">弹出窗体</param>
        /// <param name="header">窗体标示</param>
        protected void GetGridViewDataSourse(PieDataPoint selectedItem, RadWindow radWindow, string header)
        {
            if (selectedItem != null && radWindow != null)
            {
                var fleetAircraftTypeComposition = selectedItem.DataItem as FleetAircraftTypeComposition;
                if (fleetAircraftTypeComposition != null)
                {
                    DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
                    var aircraft = Aircrafts.Where(o => o.OperationHistories.Any(
                        a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))
                                                        &&
                                                        o.AircraftBusinesses.Any(
                                                            a =>
                                                                a.StartDate <= time &&
                                                                !(a.EndDate != null && a.EndDate < time)));
                    var airlineAircrafts = new List<AircraftDTO>();
                    if (header.Equals("座级", StringComparison.OrdinalIgnoreCase))
                    {
                        airlineAircrafts = aircraft.Where(p =>
                        {
                            var aircraftBusinessDto = p.AircraftBusinesses.FirstOrDefault(pp => pp.StartDate <= time
                                                                                                &&
                                                                                                !(pp.EndDate != null &&
                                                                                                  pp.EndDate < time));
                            return aircraftBusinessDto != null &&
                                   aircraftBusinessDto.Regional.Equals(fleetAircraftTypeComposition.AircraftRegional,
                                       StringComparison.OrdinalIgnoreCase);
                        }).ToList();
                    }
                    else if (header.Equals("机型", StringComparison.OrdinalIgnoreCase))
                    {
                        airlineAircrafts = aircraft.Where(p =>
                        {
                            var aircraftBusinessDto = p.AircraftBusinesses.FirstOrDefault(pp => pp.StartDate <= time
                                                                                                &&
                                                                                                !(pp.EndDate != null &&
                                                                                                  pp.EndDate < time));
                            return aircraftBusinessDto != null &&
                                   aircraftBusinessDto.AircraftTypeName.Equals(
                                       fleetAircraftTypeComposition.AircraftRegional, StringComparison.OrdinalIgnoreCase);
                        }).ToList();
                    }
                    //找到子窗体的RadGridView，并为其赋值
                    var rgv = radWindow.Content as RadGridView;
                    if (rgv != null) rgv.ItemsSource = Commonmethod.GetAircraftByTime(airlineAircrafts, time);
                    radWindow.Header = header + " " + fleetAircraftTypeComposition.AircraftRegional + "：" +
                                       fleetAircraftTypeComposition.AirTt;
                    if (!radWindow.IsOpen)
                    {
                        Commonmethod.ShowRadWindow(radWindow);
                    }
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
                IsBusy = false;
                _loadXmlSetting = false;
                _loadXmlConfig = false;
                CreateFleetAircraftTypeTrendCollection();
            }
        }

        /// <summary>
        ///     获取座级趋势图的数据源集合
        /// </summary>
        /// <returns></returns>
        private void CreateFleetAircraftTypeTrendCollection()
        {
            var regionalCollection = new List<FleetAircraftRegionalTrend>();
            var amountCollection = new List<FleetAircraftRegionalTrend>();

            #region 座级机型XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("座级机型", StringComparison.OrdinalIgnoreCase));

            XElement aircraftColor = null;
            XmlSettingDTO colorConfig =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                aircraftColor =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
            }

            if (aircraftColor != null)
            {
                var firstOrDefault =
                    aircraftColor.Descendants("Item")
                        .FirstOrDefault(p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                    AircraftColor = firstOrDefault.Attribute("Color").Value;
            }

            XElement regionalColor = null;
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase)))
            {
                regionalColor =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (XElement dateTime in xelement.Descendants("DateTime"))
                    {
                        string currentTime =
                            Convert.ToDateTime(dateTime.Attribute("EndOfMonth").Value).ToString("yyyy/M");

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


                        foreach (XElement type in dateTime.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                            {
                                string currentAmount = type.Attribute("Amount").Value;

                                //飞机总数柱状集合
                                var aircraftAmount = new FleetAircraftRegionalTrend
                                {
                                    DateTime = currentTime,
                                    Amount = Convert.ToInt32(currentAmount),
                                    Color = AircraftColor
                                };
                                amountCollection.Add(aircraftAmount);

                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    //座级折线集合
                                    var fleetAircraftRegionalTrend = new FleetAircraftRegionalTrend
                                    {
                                        AircraftRegional = item.Attribute("Name").Value,
                                        DateTime = currentTime,
                                        AirNum = Convert.ToInt32(item.Value),
                                        Amount = Convert.ToInt32(currentAmount)
                                    };
                                    if (regionalColor != null)
                                    {
                                        var firstOrDefault =
                                            regionalColor.Descendants("Item")
                                                .FirstOrDefault(
                                                    p =>
                                                        p.Attribute("Name")
                                                            .Value.Equals(fleetAircraftRegionalTrend.AircraftRegional,
                                                                StringComparison.OrdinalIgnoreCase));
                                        if (firstOrDefault != null)
                                            fleetAircraftRegionalTrend.Color = firstOrDefault.Attribute("Color").Value;
                                    }
                                    regionalCollection.Add(fleetAircraftRegionalTrend);
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
        ///     趋势图的选择点事件数据源
        /// </summary>
        public void ChartSelectionBehaviorSelection(DateTime time)
        {
            var aircraft =
                Aircrafts.Where(
                    o => o.OperationHistories.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time))
                         &&
                         o.AircraftBusinesses.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)));

            #region 座级机型XML文件的读写

            var xmlConfig =
                XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("座级机型", StringComparison.OrdinalIgnoreCase));

            XElement regionalColor = null;
            var colorConfig =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase)))
            {
                regionalColor = XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase));
            }
            XElement typeColor = null;
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typeColor = XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .FirstOrDefault(
                        p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
            }
            if (xmlConfig != null)
            {
                var aircraftRegionalList = new List<FleetAircraftTypeComposition>(); //座级饼图集合
                var aircraftTypeList = new List<FleetAircraftTypeComposition>(); //机型饼图集合

                XElement xelement = XElement.Parse(xmlConfig.ConfigContent)
                    .Descendants("DateTime")
                    .FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                if (xelement != null)
                {
                    foreach (XElement type in xelement.Descendants("Type"))
                    {
                        if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XElement item in type.Descendants("Item"))
                            {
                                var fleetAircraftTypeComposition = new FleetAircraftTypeComposition
                                {
                                    AircraftRegional = item.Attribute("Name").Value,
                                    AirNum = Convert.ToInt32(item.Value),
                                    AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                };
                                if (regionalColor != null)
                                {
                                    var firstOrDefault = regionalColor.Descendants("Item")
                                        .FirstOrDefault(p =>
                                            p.Attribute("Name")
                                                .Value.Equals(fleetAircraftTypeComposition.AircraftRegional,
                                                    StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetAircraftTypeComposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetAircraftTypeComposition.AirNum > 0)
                                {
                                    aircraftRegionalList.Add(fleetAircraftTypeComposition);
                                }
                            }
                        }
                        else if (type.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (XElement item in type.Descendants("Item"))
                            {
                                var fleetAircraftTypeComposition = new FleetAircraftTypeComposition
                                {
                                    AircraftRegional = item.Attribute("Name").Value,
                                    AirNum = Convert.ToInt32(item.Value),
                                    AirTt = item.Value + " 架,占 " + item.Attribute("Percent").Value
                                };
                                if (typeColor != null)
                                {
                                    var firstOrDefault = typeColor.Descendants("Item")
                                        .FirstOrDefault(p =>
                                            p.Attribute("Name")
                                                .Value.Equals(fleetAircraftTypeComposition.AircraftRegional,
                                                    StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        fleetAircraftTypeComposition.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                if (fleetAircraftTypeComposition.AirNum > 0)
                                {
                                    aircraftTypeList.Add(fleetAircraftTypeComposition);
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
            AircraftCollection = Commonmethod.GetAircraftByTime(aircraft.ToList(), time);
        }

        #endregion

        #endregion

        #region Class

        /// <summary>
        ///     座级趋势对象
        /// </summary>
        public class FleetAircraftRegionalTrend
        {
            public FleetAircraftRegionalTrend()
            {
                Color = Commonmethod.GetRandomColor();
            }

            public string AircraftRegional { get; set; } //座级名称
            public int AirNum { get; set; } //座级的飞机数
            public string DateTime { get; set; } //时间点
            public int Amount { get; set; } //飞机数
            public string Color { get; set; } //颜色
        }

        /// <summary>
        ///     饼图的分布对象
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

        public class FleetData
        {
            public string AircraftTypeName { get; set; }
            public ObservableCollection<FleetAircraftRegionalTrend> Data { get; set; }
        }

        #endregion
    }
}