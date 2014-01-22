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

        private readonly RadWindow _aircraftWindow = new RadWindow(); //用于单击飞机数饼状图的用户提示
        private readonly CommonMethod _commonMethod = new CommonMethod();
        private readonly FleetPlanData _fleetPlanContext;
        private readonly IFleetPlanService _service;
        private RadGridView _exportRadgridview; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _lineGrid; //折线趋势图区域，柱状趋势图区域， 飞机数饼图区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;
        private RadGridView _planDetailGridview; //初始化RadGridView

        [ImportingConstructor]
        public FleetTrendVm(IFleetPlanService service) : base(service)
        {
            _service = service;
            _service.GetAirlineses(() => { });
            _fleetPlanContext = _service.Context;
            ViewModelInitializer();
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
            ExportCommand = new DelegateCommand<object>(OnExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView); //导出数据表数据
            _lineGrid = CurrentFleetTrend.LineGrid;
            _planDetailGridview = CurrentFleetTrend.PlanDetailGridview;
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
                    _endDate = value;
                    RaisePropertyChanged(() => EndDate);
                    CreatFleetAircraftTrendCollection();
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
                    CreatFleetAircraftTrendCollection();
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

        #endregion

        #endregion

        #region 操作

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
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string>
                    {
                        {"DateTime", "时间点"},
                        {"AircraftAmount", "期末飞机数"}
                    };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetAircraftTrendLineCollection, "FleetTrendAll");
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
                    //创建RadGridView
                    var columnsList = new Dictionary<string, string>
                    {
                        {"DateTime", "时间点"},
                        {"AircraftAmount", "飞机净增数"}
                    };
                    _exportRadgridview = ImageAndGridOperation.CreatDataGridView(columnsList,
                        FleetAircraftTrendBarCollection, "FleetTrendAll");
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
                var temp = (sender as RadGridView);
                if (temp != null)
                {
                    if (_i%3 == 0 && _i >= 6 &&
                        temp.Name.Equals("FleetTrendAll", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                    }
                    else if (_i%4 == 3 && _i >= 7 &&
                             temp.Name.Equals("SubFleetTrendAll", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                    }
                }
            }
            _i++;
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
            RadGridView rgview = null;
            if (rmi != null)
            {
                var radcm = rmi.Parent as RadContextMenu;
                if (radcm != null) radcm.StaysOpen = false;
                if (rmi.DataContext.ToString().Equals("Aircraft", StringComparison.OrdinalIgnoreCase))
                {
                    rgview = _aircraftWindow.Content as RadGridView;
                }
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
            if (colorConfig != null && XElement.Parse(colorConfig.XmlContent)
                .Descendants("Type")
                .Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                XElement capacityColor = XElement.Parse(colorConfig.XmlContent)
                    .Descendants("Type")
                    .FirstOrDefault(
                        p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (capacityColor != null)
                    foreach (var item in capacityColor.Descendants("Item"))
                    {
                        colorDictionary.Add(item.Attribute("Name").Value, item.Attribute("Color").Value);
                    }
            }
            else
            {
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
                            if (type.Attribute("TypeName").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase))
                            {
                                fleetAircraftTrenLine.AircraftAmount = Convert.ToInt32(type.Attribute("Amount").Value);
                                //飞机净增数
                                fleetAircraftTrenBar.AircraftAmount = fleetAircraftTrenLine.AircraftAmount -
                                                                      lastAircraftAmount;
                                fleetAircraftTrenLine.AircraftColor =
                                    fleetAircraftTrenBar.AircraftColor = colordictionary["飞机数"];
                            }
                        }

                        //将当前总数赋值做为下一次计算净增量。
                        lastAircraftAmount = fleetAircraftTrenLine.AircraftAmount;

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
            CurrentAirlines = _service.CurrentAirlines();
            var chartSelectionBehavior = sender as ChartSelectionBehavior;
            if (chartSelectionBehavior != null)
            {
                DataPoint selectedPoint = chartSelectionBehavior.Chart.SelectedPoints.FirstOrDefault(
                    p =>
                    {
                        var categoricalSeries = p.Presenter as CategoricalSeries;
                        return categoricalSeries != null && categoricalSeries.Visibility == Visibility.Visible;
                    });
                if (selectedPoint != null)
                {
                    var fleetAircraftTrend = selectedPoint.DataItem as FleetAircraftTrend;
                    if (fleetAircraftTrend != null && SelectedTime != fleetAircraftTrend.DateTime)
                    {
                        //选中时间点
                        SelectedTime = fleetAircraftTrend.DateTime;

                        DateTime time = Convert.ToDateTime(fleetAircraftTrend.DateTime).AddMonths(1).AddDays(-1);
                        var aircraftListRoot =
                            Aircrafts.Where(
                                o =>
                                    o.OperationHistories.Any(
                                        a =>
                                            (a.AirlinesName.Equals(CurrentAirlines.CnName,
                                                StringComparison.OrdinalIgnoreCase))
                                            && a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))
                                    &&
                                    o.AircraftBusinesses.Any(
                                        a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time))).ToList();
                        AircraftCollection = _commonMethod.GetAircraftByTime(aircraftListRoot, time);

                        #region 飞机运力XML文件的读写

                        var xmlConfig =
                            XmlConfigs.FirstOrDefault(
                                p => p.ConfigType.Equals("飞机运力", StringComparison.OrdinalIgnoreCase));

                        XElement airlineColor = null;
                        XmlConfigDTO colorConfig =
                            XmlConfigs.FirstOrDefault(
                                p => p.ConfigType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                        if (colorConfig != null && XElement.Parse(colorConfig.XmlContent)
                            .Descendants("Type")
                            .Any(p => p.Attribute("TypeName").Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase)))
                        {
                            var firstOrDefault =
                                XmlConfigs.FirstOrDefault(
                                    p => p.ConfigType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                            if (firstOrDefault != null)
                                airlineColor = XElement.Parse(firstOrDefault.XmlContent)
                                    .Descendants("Type")
                                    .FirstOrDefault(
                                        p => p.Attribute("TypeName")
                                            .Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase));
                        }
                        if (xmlConfig != null)
                        {
                            var aircraftList = new List<FleetAircraft>(); //飞机数饼图集合

                            XElement xelement = XElement.Parse(xmlConfig.XmlContent)
                                .Descendants("DateTime")
                                .FirstOrDefault(p => Convert.ToDateTime(p.Attribute("EndOfMonth").Value) == time);
                            if (xelement != null)
                            {
                                foreach (XElement type in xelement.Descendants("Type"))
                                {
                                    if (type.Attribute("TypeName")
                                        .Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase))
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
                                                    .FirstOrDefault(p =>
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
            public int AircraftAmount { get; set; } //飞机数的总数
            public string AircraftColor { get; set; } //飞机数的颜色
        }

        #endregion
    }
}