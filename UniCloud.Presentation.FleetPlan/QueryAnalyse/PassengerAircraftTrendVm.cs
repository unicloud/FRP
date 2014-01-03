#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/31 17:08:52
// 文件名：PassengerAircraftTrendVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/31 17:08:52
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
    [Export(typeof(PassengerAircraftTrendVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PassengerAircraftTrendVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _fleetPlanContext;
        public PassengerAircraftTrend CurrentPassengerAircraftTrend
        {
            get { return ServiceLocator.Current.GetInstance<PassengerAircraftTrend>(); }
        }
        private readonly RadWindow _aircraftWindow = new RadWindow(); //用于单击飞机数饼状图的用户提示
        private readonly CommonMethod _commonMethod = new CommonMethod();
        private readonly RadWindow _loadWindow = new RadWindow(); //用于单击商载量饼状图的用户提示
        private readonly RadWindow _seatWindow = new RadWindow(); //用于单击座位数饼状图的用户提示
        private readonly IFleetPlanService _service;
        private RadGridView _aircraftDetail; //初始化RadGridView
        private Grid _aircraftPieGrid; //飞机数饼图区域， 座位数饼图区域， 商载量区域
        private Grid _barGrid; //飞机数饼图区域， 座位数饼图区域， 商载量区域
        private RadGridView _exportRadGridView; //初始化RadGridView
        private int _i; //导出数据源格式判断
        private Grid _lineGrid; //飞机数饼图区域， 座位数饼图区域， 商载量区域
        private Grid _loadPieGrid; //飞机数饼图区域， 座位数饼图区域， 商载量区域
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;
        private Grid _seatPieGrid; //飞机数饼图区域， 座位数饼图区域， 商载量区域

        [ImportingConstructor]
        public PassengerAircraftTrendVm(IFleetPlanService service)
        {
            _service = service;
            _fleetPlanContext = _service.Context;

            ViewModelInitializer();

            InitalizerRadWindows(_aircraftWindow, "Aircraft", 200);
            InitalizerRadWindows(_seatWindow, "Seat", 220);
            InitalizerRadWindows(_loadWindow, "Load", 240);
            AddRadMenu(_aircraftWindow);
            AddRadMenu(_seatWindow);
            AddRadMenu(_loadWindow);
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
        public void ViewModelInitializer()
        {
            ExportCommand = new DelegateCommand<object>(OnExport); //导出图表源数据（Source data）
            ExportGridViewCommand = new DelegateCommand<object>(OnExportGridView);
            ToggleButtonCommand = new DelegateCommand<object>(ToggleButtonCheck);

            _lineGrid = CurrentPassengerAircraftTrend.LineGrid;
            _barGrid = CurrentPassengerAircraftTrend.BarGrid;
            _aircraftPieGrid = CurrentPassengerAircraftTrend.AircraftPieGrid;
            _seatPieGrid = CurrentPassengerAircraftTrend.SeatPieGrid;
            _loadPieGrid = CurrentPassengerAircraftTrend.LoadPieGrid;
            _aircraftDetail = CurrentPassengerAircraftTrend.AircraftDetail;
        }

        /// <summary>
        ///     初始化ViewModel控件属性，飞机数据以及相应图表
        /// </summary>
        public void InitializeData()
        {
            if (_loadXmlConfig && _loadXmlSetting)
            {
                _loadXmlConfig = false;
                _loadXmlSetting = false;
                IsBusy = false;
                CreatFleetAircraftTrendCollection();
                SetRadCartesianChartColor();
            }
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
                    _seatWindow.Close();
                    _loadWindow.Close();
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeAircraft = "所选时间的飞机分布图";
                        SelectedTimeSeat = "所选时间的座位分布图";
                        SelectedTimeLoad = "所选时间的商载分布图";
                    }
                    else
                    {
                        SelectedTimeAircraft = SelectedTime + "末的飞机分布图";
                        SelectedTimeSeat = SelectedTime + "末的座位分布图";
                        SelectedTimeLoad = SelectedTime + "末的商载分布图";
                    }
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
                _isHidden = value;
                CurrentPassengerAircraftTrend.GridViewPane.IsHidden = !value;
                RaisePropertyChanged("IsHidden");
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
                    RaisePropertyChanged("AircraftCollection");
                    if (SelectedTime.Equals("所选时间", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedTimeAircraftList = "所选时间的飞机详细";
                    }
                    else
                    {
                        if (AircraftCollection == null || AircraftCollection.Count == 0)
                        {
                            SelectedTimeAircraftList = SelectedTime + "末的飞机详细（0架）";
                        }
                        else
                        {
                            SelectedTimeAircraftList = SelectedTime + "末的飞机详细（" + AircraftCollection.Count + "架）";
                        }
                    }
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
                    RaisePropertyChanged("EndDate");
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
                    _startDate = value;
                    RaisePropertyChanged("StartDate");
                    CreatFleetAircraftTrendCollection();
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
                    RaisePropertyChanged("Visibility");
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
                        CurrentPassengerAircraftTrend.LineCategoricalAxis.MajorTickInterval =
                            FleetAircraftTrendLineCollection.Count() / 6;
                        CurrentPassengerAircraftTrend.BarCategoricalAxis.MajorTickInterval =
                            FleetAircraftTrendLineCollection.Count() / 6;
                    }
                    else
                    {
                        CurrentPassengerAircraftTrend.LineCategoricalAxis.MajorTickInterval = 1;
                        CurrentPassengerAircraftTrend.BarCategoricalAxis.MajorTickInterval = 1;
                    }
                    RaisePropertyChanged("FleetAircraftTrendLineCollection");
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
                    RaisePropertyChanged("FleetAircraftTrendBarCollection");
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
                    RaisePropertyChanged("FleetAircraftCollection");
                    SetPieMark(FleetAircraftCollection, _aircraftPieGrid);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetSeatCollection --选中时间点的座位数分布集合

        private IEnumerable<FleetAircraft> _fleetSeatCollection;

        /// <summary>
        ///     选中时间点的座位数分布集合
        /// </summary>
        public IEnumerable<FleetAircraft> FleetSeatCollection
        {
            get { return _fleetSeatCollection; }
            set
            {
                if (!Equals(FleetSeatCollection, value))
                {
                    _fleetSeatCollection = value;
                    RaisePropertyChanged("FleetSeatCollection");
                    SetPieMark(FleetSeatCollection, _seatPieGrid);
                }
            }
        }

        #endregion

        #region ViewModel 属性 FleetAircraftCollection --选中时间点的商载量分布集合

        private IEnumerable<FleetAircraft> _fleetLoadCollection;

        /// <summary>
        ///     选中时间点的商载量分布集合
        /// </summary>
        public IEnumerable<FleetAircraft> FleetLoadCollection
        {
            get { return _fleetLoadCollection; }
            set
            {
                if (!Equals(FleetLoadCollection, value))
                {
                    _fleetLoadCollection = value;
                    RaisePropertyChanged("FleetLoadCollection");
                    SetPieMark(FleetLoadCollection, _loadPieGrid);
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
                    RaisePropertyChanged("Zoom");
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
                    RaisePropertyChanged("PanOffset");
                }
            }
        }

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
                    RaisePropertyChanged("SelectedTimeAircraft");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeSeat--座位数饼图的标识提示

        private string _selectedTimeSeat = "所选时间的座位分布图";

        /// <summary>
        ///     座位数饼图的标识提示
        /// </summary>
        public string SelectedTimeSeat
        {
            get { return _selectedTimeSeat; }
            set
            {
                if (SelectedTimeSeat != value)
                {
                    _selectedTimeSeat = value;
                    RaisePropertyChanged("SelectedTimeSeat");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeLoad --商载量饼图的标识提示

        private string _selectedTimeLoad = "所选时间的商载分布图";

        /// <summary>
        ///     商载量饼图的标识提示
        /// </summary>
        public string SelectedTimeLoad
        {
            get { return _selectedTimeLoad; }
            set
            {
                if (SelectedTimeLoad != value)
                {
                    _selectedTimeLoad = value;
                    RaisePropertyChanged("SelectedTimeLoad");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedTimeAircraft --飞机列表的标识提示

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
                    RaisePropertyChanged("SelectedTimeAircraftList");
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
                    RaisePropertyChanged("SelectedIndex");
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftMaxValue --柱状图中飞机数轴的最大值

        private double _aircraftMaxValue;

        /// <summary>
        ///     柱状图中飞机数轴的最大值
        /// </summary>
        public double AircraftMaxValue
        {
            get { return _aircraftMaxValue; }
            set
            {
                if (!Equals(AircraftMaxValue, value))
                {
                    _aircraftMaxValue = value;
                    RaisePropertyChanged("AircraftMaxValue");
                }
            }
        }

        #endregion

        #region ViewModel 属性 AircraftMinValue --柱状图中飞机数轴的最小值

        private double _aircraftMinValue;

        /// <summary>
        ///     柱状图中飞机数轴的最小值
        /// </summary>
        public double AircraftMinValue
        {
            get { return _aircraftMinValue; }
            set
            {
                if (!Equals(AircraftMinValue, value))
                {
                    _aircraftMinValue = value;
                    RaisePropertyChanged("AircraftMinValue");
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
                    RaisePropertyChanged("AircraftStep");
                }
            }
        }

        #endregion

        #region ViewModel 属性 LoadMaxValue --柱状图中商载量轴的最大值

        private double _loadMaxValue;

        /// <summary>
        ///     柱状图中商载量轴的最大值
        /// </summary>
        public double LoadMaxValue
        {
            get { return _loadMaxValue; }
            set
            {
                if (!Equals(LoadMaxValue, value))
                {
                    _loadMaxValue = value;
                    RaisePropertyChanged("LoadMaxValue");
                }
            }
        }

        #endregion

        #region ViewModel 属性 LoadMinValue --柱状图中商载量轴的最小值

        private double _loadMinValue;

        /// <summary>
        ///     柱状图中商载量轴的最小值
        /// </summary>
        public double LoadMinValue
        {
            get { return _loadMinValue; }
            set
            {
                if (!Equals(LoadMinValue, value))
                {
                    _loadMinValue = value;
                    RaisePropertyChanged("LoadMinValue");
                }
            }
        }

        #endregion

        #region ViewModel 属性 LoadStep --柱状图中商载量轴的节点距离

        private int _loadStep = 200;

        /// <summary>
        ///     柱状图中商载量轴的节点距离
        /// </summary>
        public int LoadStep
        {
            get { return _loadStep; }
            set
            {
                if (LoadStep != value)
                {
                    _loadStep = value;
                    RaisePropertyChanged("LoadStep");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SeatMaxValue --柱状图中座位数轴的最大值

        private double _seatMaxValue;

        /// <summary>
        ///     柱状图中座位数轴的最大值
        /// </summary>
        public double SeatMaxValue
        {
            get { return _seatMaxValue; }
            set
            {
                if (!Equals(SeatMaxValue, value))
                {
                    _seatMaxValue = value;
                    RaisePropertyChanged("SeatMaxValue");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SeatMinValue --柱状图中座位数轴的最小值

        private double _seatMinValue;

        /// <summary>
        ///     柱状图中座位数轴的最小值
        /// </summary>
        public double SeatMinValue
        {
            get { return _seatMinValue; }
            set
            {
                if (!Equals(SeatMinValue, value))
                {
                    _seatMinValue = value;
                    RaisePropertyChanged("SeatMinValue");
                }
            }
        }

        #endregion

        #region ViewModel 属性 SeatStep --柱状图中座位数轴的节点距离

        private int _seatStep = 200;

        /// <summary>
        ///     柱状图中座位数轴的节点距离
        /// </summary>
        public int SeatStep
        {
            get { return _seatStep; }
            set
            {
                if (SeatStep != value)
                {
                    _seatStep = value;
                    RaisePropertyChanged("SeatStep");
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
                    RaisePropertyChanged("IsContextMenuOpen");
                }
            }
        }

        #endregion

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
        }

        #endregion

        #endregion

        #region 操作

        /// <summary>
        ///     获取趋势图的颜色配置
        /// </summary>
        public Dictionary<string, string> GetColorDictionary()
        {
            var colorDictionary = new Dictionary<string, string>();
            XmlSettingDTO colorConfig =
                XmlSettings.FirstOrDefault(
                    config => config.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null &&
                XElement.Parse(colorConfig.SettingContent)
                    .Descendants("Type")
                    .Any(type => type.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                XElement capacityColor =
                    XElement.Parse(colorConfig.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            type => type.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
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
        public void CreatFleetAircraftTrendCollection()
        {
            var fleetAircraftTrendLineList = new List<FleetAircraftTrend>(); //折线图统计总数的集合
            var fleetAircraftTrendBarList = new List<FleetAircraftTrend>(); //柱状图统计净增的集合

            #region 客机运力XML解析

            var xmlConfig =
                XmlConfigs.FirstOrDefault(config => config.ConfigType.Equals("客机运力", StringComparison.OrdinalIgnoreCase));
            Dictionary<string, string> colorDictionary = GetColorDictionary();
            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    //记录上一个时间点的总数，便于统计净增数据
                    int lastAircraftAmount = 0;
                    int lastSeatAmount = 0;
                    int lastLoadAmount = 0;

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
                        var fleetAircraftTrendLine = new FleetAircraftTrend { DateTime = currentTime }; //折线图的总数对象
                        var fleetAircraftTrendBar = new FleetAircraftTrend { DateTime = currentTime }; //柱状图的净增数对象
                        foreach (XElement type in datetime.Descendants("Type"))
                        {
                            switch (type.Attribute("TypeName").Value)
                            {
                                case "飞机数":
                                    fleetAircraftTrendLine.AircraftAmount = Convert.ToInt32(type.Attribute("Amount").Value);
                                    fleetAircraftTrendBar.AircraftAmount = fleetAircraftTrendLine.AircraftAmount - lastAircraftAmount; //飞机净增数
                                    fleetAircraftTrendLine.AircraftColor = fleetAircraftTrendBar.AircraftColor = colorDictionary["飞机数"];
                                    break;
                                case "座位数":
                                    fleetAircraftTrendLine.SeatAmount = Convert.ToInt32(type.Attribute("Amount").Value);
                                    fleetAircraftTrendBar.SeatAmount = fleetAircraftTrendLine.SeatAmount - lastSeatAmount; //座位净增数
                                    fleetAircraftTrendLine.SeatColor = fleetAircraftTrendBar.SeatColor = colorDictionary["座位数"];
                                    break;
                                case "商载量":
                                    fleetAircraftTrendLine.LoadAmount = Convert.ToInt32(type.Attribute("Amount").Value);
                                    fleetAircraftTrendBar.LoadAmount = fleetAircraftTrendLine.LoadAmount - lastLoadAmount; //商载净增数
                                    fleetAircraftTrendLine.LoadColor = fleetAircraftTrendBar.LoadColor = colorDictionary["商载量"];
                                    break;
                            }
                        }

                        //将当前总数赋值做为下一次计算净增量。
                        lastAircraftAmount = fleetAircraftTrendLine.AircraftAmount;
                        lastSeatAmount = fleetAircraftTrendLine.SeatAmount;
                        lastLoadAmount = fleetAircraftTrendLine.LoadAmount;

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
                        fleetAircraftTrendLineList.Add(fleetAircraftTrendLine);
                        fleetAircraftTrendBarList.Add(fleetAircraftTrendBar);
                    }

                    if (fleetAircraftTrendBarList.Count > 0)
                    {
                        AircraftStep = Convert.ToInt32(AircraftMaxValue / 2);
                        SeatStep = Convert.ToInt32(SeatMaxValue / 2);
                        LoadStep = Convert.ToInt32(LoadMaxValue / 2);
                    }
                }
            }

            #endregion

            FleetAircraftTrendLineCollection = fleetAircraftTrendLineList;
            FleetAircraftTrendBarCollection = fleetAircraftTrendBarList;

            SelectedTime = "所选时间";
            FleetAircraftCollection = null;
            FleetSeatCollection = null;
            FleetLoadCollection = null;
            AircraftCollection = null;
            Zoom = new Size(1, 1);
        }

        /// <summary>
        ///     控制趋势图的Y轴和折线及标签颜色
        /// </summary>
        public void SetRadCartesianChartColor()
        {
            Dictionary<string, string> colorDictionary = GetColorDictionary();

            #region 控制折线趋势图的Y轴颜色

            var radCartesianChart = _lineGrid.Children[0] as RadCartesianChart;
            if (radCartesianChart != null)
            {
                var axisCollection = radCartesianChart.Resources["additionalVerticalAxis"] as AxisCollection;
                if (axisCollection != null)
                    foreach (var item in axisCollection)
                    {
                        var linearAxis = item as LinearAxis;
                        if (linearAxis != null)
                            switch (linearAxis.Title.ToString())
                            {
                                case "飞机数（架）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                                    break;
                                case "座位数（座）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["座位数"]));
                                    break;
                                case "商载量（吨）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["商载量"]));
                                    break;
                            }
                    }
            }

            #endregion

            #region 控制折线趋势图的线条颜色

            var cartesianChart = _lineGrid.Children[0] as RadCartesianChart;
            if (cartesianChart != null)
                foreach (var item in (cartesianChart.Series))
                {
                    var lineSeries = item as LineSeries;
                    if (lineSeries != null)
                        switch (lineSeries.DisplayName)
                        {
                            case "期末飞机数":
                                lineSeries.Stroke = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                                break;
                            case "期末座位数":
                                lineSeries.Stroke = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["座位数"]));
                                break;
                            case "期末商载量":
                                lineSeries.Stroke = new SolidColorBrush(_commonMethod.GetColor(colorDictionary["商载量"]));
                                break;
                        }
                }

            #endregion

            #region 控制柱状趋势图的Y轴颜色

            var chart = _barGrid.Children[0] as RadCartesianChart;
            if (chart != null)
            {
                var collection = chart.Resources["additionalVerticalAxis"] as AxisCollection;
                if (collection != null)
                    foreach (var item in collection)
                    {
                        var linearAxis = item as LinearAxis;
                        if (linearAxis != null)
                            switch (linearAxis.Title.ToString())
                            {
                                case "飞机净增（架）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["飞机数"]));
                                    break;
                                case "座位净增（座）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["座位数"]));
                                    break;
                                case "商载净增（吨）":
                                    linearAxis.ElementBrush =
                                        new SolidColorBrush(_commonMethod.GetColor(colorDictionary["商载量"]));
                                    break;
                            }
                    }
            }

            #endregion
        }

        #region Command

        #region ViewModel 命令 --导出图表
        public DelegateCommand<object> ExportCommand { get; set; }

        private void OnExport(object sender)
        {
            var radMenuItem = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (radMenuItem != null && radMenuItem.Header.ToString().Equals("导出源数据", StringComparison.OrdinalIgnoreCase))
            {
                if (radMenuItem.Name.Equals("LineGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //if (CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType == 1))
                    //{
                    //    //当包含子公司时
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount", "期末客机数(子)"},
                    //                          {"AircraftAmount1", "期末客机数"},
                    //                          {"SeatAmount", "期末座位数(子)"},
                    //                          {"SeatAmount1", "期末座位数"},
                    //                          {"LoadAmount", "期末商载量(子)"},
                    //                          {"LoadAmount1", "期末商载量"}
                    //                      };
                    //    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendLineCollection, "SubFleetTrendPnr");
                    //}
                    //else
                    //{
                    //    //创建RadGridView
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount1", "期末客机数"},
                    //                          {"SeatAmount1", "期末座位数"},
                    //                          {"LoadAmount1", "期末商载量"}
                    //                      };
                    //    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendLineCollection, "FleetTrendPnr");
                    //}
                    //_i = 1;
                    //_exportRadGridView.ElementExporting -= ElementExporting;
                    //_exportRadGridView.ElementExporting += ElementExporting;
                    //using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    //{
                    //    if (stream != null)
                    //    {
                    //        _exportRadGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    //    }
                    //}
                }
                else if (radMenuItem.Name.Equals("BarGridData", StringComparison.OrdinalIgnoreCase))
                {
                    //if (CurrentAirlines.SubAirlines != null && CurrentAirlines.SubAirlines.Any(p => p.SubType == 1))
                    //{
                    //    //当包含子公司时
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount", "客机净增数(子)"},
                    //                          {"AircraftAmount1", "客机净增数(子)"},
                    //                          {"SeatAmount", "座位净增数(子)"},
                    //                          {"SeatAmount1", "座位净增数"},
                    //                          {"LoadAmount", "商载净增量(子)"},
                    //                          {"LoadAmount1", "商载净增量"}
                    //                      };
                    //    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendBarCollection, "SubFleetTrendPnr");
                    //}
                    //else
                    //{
                    //    //创建RadGridView
                    //    var columnsList = new Dictionary<string, string>
                    //                      {
                    //                          {"DateTime", "时间点"},
                    //                          {"AircraftAmount1", "客机净增数"},
                    //                          {"SeatAmount1", "座位净增数"},
                    //                          {"LoadAmount1", "商载净增量"}
                    //                      };
                    //    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftTrendBarCollection, "FleetTrendPnr");
                    //}
                    _i = 1;
                    _exportRadGridView.ElementExporting -= ElementExporting;
                    _exportRadGridView.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _exportRadGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (radMenuItem.Name.Equals("AircraftPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetAircraftCollection == null || !FleetAircraftCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "Aircraft", "航空公司" }, { "Amount", "飞机数（架）" } };
                    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetAircraftCollection, "PieFleetTrend");

                    _i = 1;
                    _exportRadGridView.ElementExporting -= ElementExporting;
                    _exportRadGridView.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.xlsx)|*.xlsx|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _exportRadGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (radMenuItem.Name.Equals("SeatPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetSeatCollection == null || !FleetSeatCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "Aircraft", "航空公司" }, { "Amount", "座位数" } };
                    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetSeatCollection, "PieFleetTrend");

                    _i = 1;
                    _exportRadGridView.ElementExporting -= ElementExporting;
                    _exportRadGridView.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _exportRadGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
                else if (radMenuItem.Name.Equals("LoadPieGridData", StringComparison.OrdinalIgnoreCase))
                {
                    if (FleetLoadCollection == null || !FleetLoadCollection.Any())
                    {
                        return;
                    }

                    //创建RadGridView
                    var columnsList = new Dictionary<string, string> { { "Aircraft", "航空公司" }, { "Amount", "商载量（吨）" } };
                    _exportRadGridView = ImageAndGridOperation.CreatDataGridView(columnsList, FleetLoadCollection, "PieFleetTrend");

                    _i = 1;
                    _exportRadGridView.ElementExporting -= ElementExporting;
                    _exportRadGridView.ElementExporting += ElementExporting;
                    using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc"))
                    {
                        if (stream != null)
                        {
                            _exportRadGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                        }
                    }
                }
            }
            else if (radMenuItem != null && radMenuItem.Header.ToString().Equals("导出图片", StringComparison.OrdinalIgnoreCase))
            {
                switch (radMenuItem.Name)
                {
                    case "LineGridImage":
                    case "BarGridImage":
                        if (_lineGrid != null)
                        {
                            _commonMethod.ExportToImage(_lineGrid.Parent as Grid);
                        }
                        break;
                    case "AircraftPieGridImage":
                        if (_aircraftPieGrid != null)
                        {
                            _commonMethod.ExportToImage(_aircraftPieGrid);
                        }
                        break;
                    case "SeatPieGridImage":
                        if (_seatPieGrid != null)
                        {
                            _commonMethod.ExportToImage(_seatPieGrid);
                        }
                        break;
                    case "LoadPieGridImage":
                        if (_loadPieGrid != null)
                        {
                            _commonMethod.ExportToImage(_loadPieGrid);
                        }
                        break;
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
                var radGridView = sender as RadGridView;
                if (radGridView != null &&
                    (_i % 5 == 3 && _i >= 8 &&
                     radGridView.Name.Equals("FleetTrendPnr", StringComparison.OrdinalIgnoreCase)))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
                else if (radGridView != null && _i % 8 == 3 && _i >= 11 &&
                         radGridView.Name.Equals("SubFleetTrendPnr", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).AddMonths(1).AddDays(-1).ToString("yyyy/M/d");
                }
            }
            _i++;
        }
        #endregion

        #region  增加子窗体的右键导出功能

        public void AddRadMenu(RadWindow rwindow)
        {
            var radContextMenu = new RadContextMenu(); //新建右键菜单
            radContextMenu.Opened += Radcm_Opened;
            var radMenuItem = new RadMenuItem { Header = "导出表格" }; //新建右键菜单项
            radMenuItem.Click += MenuItemClick; //为菜单项注册事件
            radMenuItem.DataContext = rwindow.Name;
            radContextMenu.Items.Add(radMenuItem);
            RadContextMenu.SetContextMenu(rwindow, radContextMenu); //为控件绑定右键菜单
        }

        private void Radcm_Opened(object sender, RoutedEventArgs e)
        {
            var radContextMenu = sender as RadContextMenu;
            if (radContextMenu != null) radContextMenu.StaysOpen = true;
        }

        public void MenuItemClick(object sender, RadRoutedEventArgs e)
        {
            var radMenuItem = sender as RadMenuItem;
            if (radMenuItem != null)
            {
                var radContextMenu = radMenuItem.Parent as RadContextMenu;
                if (radContextMenu != null) radContextMenu.StaysOpen = false;
            }
            RadGridView radGridView = null;
            if (radMenuItem != null)
                switch (radMenuItem.DataContext.ToString())
                {
                    case "Aircraft":
                        radGridView = _aircraftWindow.Content as RadGridView;
                        break;
                    case "Seat":
                        radGridView = _seatWindow.Content as RadGridView;
                        break;
                    case "Load":
                        radGridView = _loadWindow.Content as RadGridView;
                        break;
                }

            if (radGridView != null)
            {
                radGridView.ElementExporting -= ElementExporting;
                radGridView.ElementExporting += ElementExporting;
                using (Stream stream = ImageAndGridOperation.DowmLoadDialogStream("文档文件(*.xls)|*.xls|文档文件(*.doc)|*.doc")
                    )
                {
                    if (stream != null)
                    {
                        radGridView.Export(stream, ImageAndGridOperation.SetGridViewExportOptions());
                    }
                }
            }
        }

        #endregion

        #region ViewModel 命令 --导出数据aircraftDetail
        public DelegateCommand<object> ExportGridViewCommand { get; set; }

        private void OnExportGridView(object sender)
        {
            var radMenuItem = sender as RadMenuItem;
            IsContextMenuOpen = false;
            if (radMenuItem != null && radMenuItem.Header.ToString().Equals("导出数据", StringComparison.OrdinalIgnoreCase) &&
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

        #endregion

        #region Methods

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
                            var setter = new Setter { Property = Shape.FillProperty, Value = item.Color };
                            var style = new Style { TargetType = typeof(System.Windows.Shapes.Path) };
                            style.Setters.Add(setter);
                            radPieChart.Series[0].SliceStyles.Add(style);

                            var barPanel = new StackPanel();
                            barPanel.MouseLeftButtonDown += PiePanelMouseLeftButtonDown;
                            barPanel.Orientation = Orientation.Horizontal;
                            var rectangle = new Rectangle
                                            {
                                                Width = 15,
                                                Height = 15,
                                                Fill =
                                                    new SolidColorBrush(_commonMethod.GetColor(item.Color))
                                            };
                            var textBlock = new TextBlock
                                            {
                                                Text = item.Aircraft,
                                                Style = CurrentPassengerAircraftTrend.Resources.FirstOrDefault(
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
        ///     根据选中饼图的航空公司弹出相应的数据列表窗体
        /// </summary>
        /// <param name="selectedItem">选中点</param>
        /// <param name="radWindow">弹出窗体</param>
        /// <param name="header">窗体标示</param>
        private void GetGridViewDataSourse(PieDataPoint selectedItem, RadWindow radWindow, string header)
        {
            //if (selectedItem != null && radWindow != null)
            //{
            //    var fleetAircraft = selectedItem.DataItem as FleetAircraft;
            //    DateTime time = Convert.ToDateTime(SelectedTime).AddMonths(1).AddDays(-1);
            //    var aircraftData = Aircrafts.Where(aircraft => aircraft.OperationHistories.Any(operation =>
            //        (operation.Airlines.ShortName.Equals(_service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase) || operation.Airlines.SubType == 2)
            //        && operation.StartDate <= time && !(operation.EndDate != null && operation.EndDate < time))
            //        && aircraft.AircraftBusinesses.Any(aircraftBusiness =>
            //            aircraftBusiness.AircraftType.AircraftCategory.Category.Equals("客机", StringComparison.OrdinalIgnoreCase) && aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time)));

            //    var airlineAircrafts = new List<AircraftDTO>();
            //    if (fleetAircraft.Aircraft.Equals(_service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase))
            //    {
            //        //部分属性变化------------------
            //        //#region 筛选飞机数据
            //        //airlineAircrafts = aircraftData.Where(aircraft =>
            //        //{
            //        //    var aircraftbusiness = aircraft.AircraftBusinesses.FirstOrDefault(aircraftBusiness => aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time));
            //        //    if (aircraftbusiness == null) return false;
            //        //    if (!aircraftbusiness.AircraftType.AircraftCategory.Category.Equals("客机", StringComparison.OrdinalIgnoreCase)) return false;

            //        //    var operationHistory = aircraft.OperationHistories.FirstOrDefault(operation => operation.Airlines.ShortName.Equals(this._service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase)
            //        //        && operation.StartDate <= time && !(operation.EndDate != null && operation.EndDate < time));
            //        //    if (operationHistory == null)
            //        //    {
            //        //        return false;
            //        //    }
            //        //    else if (operationHistory.SubOperationCategorys.Count <= 0)
            //        //    {
            //        //        return true;
            //        //    }
            //        //    else
            //        //    {
            //        //        var subOperationCategory = operationHistory.SubOperationCategorys.Where(subOperation => subOperation.StartDate <= time && !(subOperation.EndDate != null && subOperation.EndDate < time));
            //        //        if (subOperationCategory == null || subOperationCategory.Count() == 0) return true;
            //        //        return subOperationCategory.Any(subOperation => subOperation.Airlines.ShortName.Equals(this._service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase));
            //        //    }

            //        //}).ToList();
            //        //#endregion
            //    }
            //    else
            //    {
            //        #region 分子公司的筛选
            //        var aircraftSubCompany = aircraftData.Where(aircraft =>
            //        {
            //            var aircraftbusiness = aircraft.AircraftBusinesses.FirstOrDefault(aircraftBusiness => aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time));
            //            if (aircraftbusiness == null) return false;
            //            if (aircraftbusiness.AircraftType.AircraftCategory.Category != "客机") return false;
            //            return aircraft.OperationHistories.Any(operation =>
            //                                                operation.Airlines.ShortName.Equals(fleetAircraft.Aircraft, StringComparison.OrdinalIgnoreCase) &&
            //                                                operation.Airlines.SubType == 2 && operation.StartDate <= time &&
            //                                       !(operation.EndDate != null && operation.EndDate < time));
            //        }).ToList();
            //        #endregion

            //        //分公司部分属性有变化-----------------
            //        //#region 分公司的筛选
            //        //var aircraftFiliale = aircraftData.Where(aircraft =>
            //        //{
            //        //    var aircraftbusiness = aircraft.AircraftBusinesses.FirstOrDefault(aircraftBusiness => aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time));
            //        //    if (aircraftbusiness == null) return false;
            //        //    if (aircraftbusiness.AircraftType.AircraftCategory.Category != "客机") return false;

            //        //    var operationHistory = aircraft.OperationHistories.FirstOrDefault(operation => operation.Airlines.ShortName.Equals(this._service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase) && operation.StartDate <= time && !(operation.EndDate != null && operation.EndDate < time));
            //        //    if (operationHistory == null || operationHistor.SubOperationCategorys.Count <= 0)
            //        //    {
            //        //        return false;
            //        //    }
            //        //    else
            //        //    {
            //        //        return
            //        //            operationHistory.SubOperationCategorys.Any(subOperation =>
            //        //                subOperation.Airlines.ShortName.Equals(fleetAircraft.Aircraft, StringComparison.OrdinalIgnoreCase) &&
            //        //                subOperation.StartDate <= time && !(subOperation.EndDate != null && subOperation.EndDate < time));
            //        //    }
            //        //}).ToList();
            //        //#endregion
            //        //airlineAircrafts = aircraftSubCompany.Union(aircraftFiliale).ToList();
            //    };
            //    //找到子窗体的RadGridView，并为其赋值
            //    var radGridView = radWindow.Content as RadGridView;
            //    if (radGridView != null)
            //        radGridView.ItemsSource = _commonMethod.GetAircraftByTime(airlineAircrafts, time);
            //    radWindow.Header = fleetAircraft.Aircraft + header + "：" + fleetAircraft.ToolTip;
            //    if (!radWindow.IsOpen)
            //    {
            //        _commonMethod.ShowRadWindow(radWindow);
            //    }
            //}
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
            if (radWindow != null)
                switch (radWindow.Name)
                {
                    case "Aircraft":
                        grid = _aircraftPieGrid;
                        break;
                    case "Seat":
                        grid = _seatPieGrid;
                        break;
                    case "Load":
                        grid = _loadPieGrid;
                        break;
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
                            var rectangle = panel.Children[0] as Rectangle;
                            rectangle.Width = 15;
                            rectangle.Height = 15;
                        }
                    }
            }
        }

        /// <summary>
        ///     初始化提示窗体
        /// </summary>
        public void InitalizerRadWindows(RadWindow radWindow, string windowsName, int length)
        {
            //运营计划子窗体的设置
            radWindow.Name = windowsName;
            radWindow.Top = length;
            radWindow.Left = length;
            radWindow.Height = 250;
            radWindow.Width = 500;
            radWindow.ResizeMode = ResizeMode.CanResize;
            radWindow.Content = _commonMethod.CreatOperationGridView();
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
                DataPoint selectedPoint = chartSelectionBehavior.Chart.SelectedPoints.FirstOrDefault(point =>
                                                                                                     {
                                                                                                         var categoricalSeries = point.Presenter as CategoricalSeries;
                                                                                                         return categoricalSeries != null && categoricalSeries.Visibility == Visibility.Visible;
                                                                                                     });
                if (selectedPoint != null)
                {
                    var fleetAircraftTrend = selectedPoint.DataItem as FleetAircraftTrend;
                    if (fleetAircraftTrend != null && SelectedTime != fleetAircraftTrend.DateTime)
                    {
                        #region 根据选中时间点过滤飞机数据

                        SelectedTime = fleetAircraftTrend.DateTime;

                        DateTime time = Convert.ToDateTime(fleetAircraftTrend.DateTime).AddMonths(1).AddDays(-1);
                        //var aircraftlist = Aircrafts.Where(aircraft => aircraft.OperationHistories.Any(operation => (operation.Airlines.ShortName.Equals(_service.CurrentAirlines.ShortName, StringComparison.OrdinalIgnoreCase) || operation.Airlines.SubType == 2)
                        //                                                                                       && operation.StartDate <= time && !(operation.EndDate != null && operation.EndDate < time))
                        //                                          && aircraft.AircraftBusinesses.Any(aircraftBusiness => aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time)))
                        //                       .Where(aircraft =>
                        //                       {
                        //                           var aircraftBusinessDataObject = aircraft.AircraftBusinesses.FirstOrDefault(aircraftBusiness => aircraftBusiness.StartDate <= time && !(aircraftBusiness.EndDate != null && aircraftBusiness.EndDate < time));
                        //                           return aircraftBusinessDataObject != null && aircraftBusinessDataObject
                        //                                                                            .AircraftType.AircraftCategory.Category.Equals("客机", StringComparison.OrdinalIgnoreCase);
                        //                       }).ToList();

                        //AircraftCollection = _commonMethod.GetAircraftByTime(aircraftlist, time);

                        #endregion

                        #region 客机运力XML解析

                        var xmlConfig =
                            XmlConfigs.FirstOrDefault(
                                config => config.ConfigType.Equals("客机运力", StringComparison.OrdinalIgnoreCase));

                        XElement airlineColor = null;
                        XmlSettingDTO colorConfig =
                            XmlSettings.FirstOrDefault(
                                config => config.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                        if (colorConfig != null &&
                            XElement.Parse(colorConfig.SettingContent)
                                .Descendants("Type")
                                .Any(
                                    type =>
                                        type.Attribute("TypeName")
                                            .Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase)))
                        {
                            var xmlSettingDataObject =
                                XmlSettings.FirstOrDefault(
                                    config => config.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                            if (
                                xmlSettingDataObject !=
                                null)
                                airlineColor =
                                    XElement.Parse(xmlSettingDataObject.SettingContent)
                                        .Descendants("Type")
                                        .FirstOrDefault(
                                            type =>
                                                type.Attribute("TypeName")
                                                    .Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase));
                        }
                        if (xmlConfig != null)
                        {
                            var aircraftList = new List<FleetAircraft>(); //飞机数饼图集合
                            var seatList = new List<FleetAircraft>(); //座位数饼图集合
                            var loadList = new List<FleetAircraft>(); //商载量饼图集合

                            XElement xelement =
                                XElement.Parse(xmlConfig.ConfigContent)
                                    .Descendants("DateTime")
                                    .FirstOrDefault(
                                        dateTime => Convert.ToDateTime(dateTime.Attribute("EndOfMonth").Value) == time);
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
                                                var firstOrDefault =
                                                    airlineColor.Descendants("Item")
                                                        .FirstOrDefault(
                                                            aircraftItem =>
                                                                aircraftItem.Attribute("Name")
                                                                    .Value.Equals(fleetAircraft.Aircraft,
                                                                        StringComparison.OrdinalIgnoreCase));
                                                if (firstOrDefault !=
                                                    null)
                                                    fleetAircraft.Color = firstOrDefault.Attribute("Color").Value;
                                            }
                                            if (fleetAircraft.Amount > 0)
                                            {
                                                aircraftList.Add(fleetAircraft);
                                            }
                                        }
                                    }
                                    else if (type.Attribute("TypeName")
                                        .Value.Equals("座位数", StringComparison.OrdinalIgnoreCase))
                                    {
                                        foreach (XElement item in type.Descendants("Item"))
                                        {
                                            var fleetAircraft = new FleetAircraft
                                                                {
                                                                    Aircraft = item.Attribute("Name").Value,
                                                                    Amount = Convert.ToDecimal(item.Value),
                                                                    ToolTip = item.Value + " 座,占 " + item.Attribute("Percent").Value
                                                                };
                                            if (airlineColor != null)
                                            {
                                                var firstOrDefault =
                                                    airlineColor.Descendants("Item")
                                                        .FirstOrDefault(
                                                            aircraftItem =>
                                                                aircraftItem.Attribute("Name")
                                                                    .Value.Equals(fleetAircraft.Aircraft,
                                                                        StringComparison.OrdinalIgnoreCase));
                                                if (firstOrDefault !=
                                                    null)
                                                    fleetAircraft.Color = firstOrDefault.Attribute("Color").Value;
                                            }
                                            if (fleetAircraft.Amount > 0)
                                            {
                                                seatList.Add(fleetAircraft);
                                            }
                                        }
                                    }
                                    else if (type.Attribute("TypeName")
                                        .Value.Equals("商载量", StringComparison.OrdinalIgnoreCase))
                                    {
                                        foreach (XElement item in type.Descendants("Item"))
                                        {
                                            var fleetAircraft = new FleetAircraft
                                                                {
                                                                    Aircraft = item.Attribute("Name").Value,
                                                                    Amount = Convert.ToDecimal(item.Value),
                                                                    ToolTip = item.Value + " 吨,占 " + item.Attribute("Percent").Value
                                                                };
                                            if (airlineColor != null)
                                            {
                                                var firstOrDefault =
                                                    airlineColor.Descendants("Item")
                                                        .FirstOrDefault(
                                                            aircraftItem =>
                                                                aircraftItem.Attribute("Name")
                                                                    .Value.Equals(fleetAircraft.Aircraft,
                                                                        StringComparison.OrdinalIgnoreCase));
                                                if (firstOrDefault !=
                                                    null)
                                                    fleetAircraft.Color =
                                                        firstOrDefault.Attribute("Color").Value;
                                            }
                                            if (fleetAircraft.Amount > 0)
                                            {
                                                loadList.Add(fleetAircraft);
                                            }
                                        }
                                    }
                                }
                            }
                            FleetAircraftCollection = aircraftList;
                            FleetSeatCollection = seatList;
                            FleetLoadCollection = loadList;
                        }

                        #endregion
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
                RadChartBase radChartBase = chartSelectionBehavior.Chart;
                var selectedPoint = radChartBase.SelectedPoints.FirstOrDefault() as PieDataPoint;

                var stackPanel = new StackPanel();
                switch (radChartBase.EmptyContent.ToString())
                {
                    case "飞机数分布":
                        stackPanel = (_aircraftPieGrid.Children[1] as ScrollViewer).Content as StackPanel;
                        break;
                    case "座位数分布":
                        stackPanel = (_seatPieGrid.Children[1] as ScrollViewer).Content as StackPanel;
                        break;
                    case "商载量分布":
                        stackPanel = (_loadPieGrid.Children[1] as ScrollViewer).Content as StackPanel;
                        break;
                }

                if (stackPanel != null)
                {
                    foreach (var item in stackPanel.Children)
                    {
                        var panel = item as StackPanel;
                        if (panel != null)
                        {
                            var itemRectangle = panel.Children[0] as Rectangle;
                            if (itemRectangle != null)
                            {
                                itemRectangle.Width = 15;
                                itemRectangle.Height = 15;
                            }
                        }
                    }

                    if (selectedPoint != null)
                    {
                        var childStackPanel = stackPanel.Children
                            .FirstOrDefault(
                                panel =>
                                    ((panel as StackPanel).Children[1] as TextBlock).Text.Equals(
                                        (selectedPoint.DataItem as FleetAircraft).Aircraft,
                                        StringComparison.OrdinalIgnoreCase)) as StackPanel;
                        if (childStackPanel != null)
                        {
                            var rectangle = childStackPanel.Children[0] as Rectangle;
                            if (rectangle != null)
                            {
                                rectangle.Width = 12;
                                rectangle.Height = 12;
                            }
                        }

                        switch (radChartBase.EmptyContent.ToString())
                        {
                            case "飞机数分布":
                                GetGridViewDataSourse(selectedPoint, _aircraftWindow, "飞机数");
                                break;
                            case "座位数分布":
                                GetGridViewDataSourse(selectedPoint, _seatWindow, "座位数");
                                break;
                            case "商载量分布":
                                GetGridViewDataSourse(selectedPoint, _loadWindow, "商载量");
                                break;
                        }
                    }
                    else
                    {
                        switch (radChartBase.EmptyContent.ToString())
                        {
                            case "飞机数分布":
                                _aircraftWindow.Close();
                                break;
                            case "座位数分布":
                                _seatWindow.Close();
                                break;
                            case "商载量分布":
                                _loadWindow.Close();
                                break;
                        }
                    }
                }
            }
        }


        /// <summary>
        ///     饼状图标签的选择事件
        /// </summary>
        /// <param name="sender"></param>
        public void PiePanelMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            //选中航空公司的名称
            var stackPanel = sender as StackPanel;
            if (stackPanel != null)
            {
                var block = stackPanel.Children[1] as TextBlock;
                if (block != null)
                {
                    string shortName = block.Text;

                    #region 修改饼图标签中的突出显示

                    var panel = stackPanel.Parent as StackPanel;
                    if (panel != null)
                        foreach (var item in panel.Children)
                        {
                            var childStackPanel = item as StackPanel;
                            if (childStackPanel != null)
                            {
                                var itemRectangle = childStackPanel.Children[0] as Rectangle;
                                var textBlock = childStackPanel.Children[1] as TextBlock;
                                if (textBlock != null)
                                {
                                    string itemShortName = textBlock.Text;
                                    if (itemShortName.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (itemRectangle != null && itemRectangle.Width == 12)
                                        {
                                            itemRectangle.Width = 15;
                                            itemRectangle.Height = 15;
                                        }
                                        else if (itemRectangle != null)
                                        {
                                            itemRectangle.Width = 12;
                                            itemRectangle.Height = 12;
                                        }
                                    }
                                    else if (itemRectangle != null)
                                    {
                                        itemRectangle.Width = 15;
                                        itemRectangle.Height = 15;
                                    }
                                }
                            }
                        }

                    #endregion

                    #region 修改对应饼图块状的突出显示

                    var stackPanel1 = stackPanel.Parent as StackPanel;
                    if (stackPanel1 != null)
                    {
                        var scrollViewer = stackPanel1.Parent as ScrollViewer;
                        if (scrollViewer != null)
                        {
                            var grid = scrollViewer.Parent as Grid;
                            if (grid != null)
                            {
                                var radPieChart = grid.Children[0] as RadPieChart;
                                if (radPieChart != null)
                                    foreach (var item in radPieChart.Series[0].DataPoints)
                                    {
                                        PieDataPoint pieDataPoint = item;
                                        var fleetAircraft = pieDataPoint.DataItem as FleetAircraft;
                                        if (fleetAircraft != null &&
                                            fleetAircraft.Aircraft.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                                        {
                                            pieDataPoint.IsSelected = !pieDataPoint.IsSelected;
                                            if (pieDataPoint.IsSelected)
                                            {
                                                switch (radPieChart.EmptyContent.ToString())
                                                {
                                                    case "飞机数分布":
                                                        GetGridViewDataSourse(pieDataPoint, _aircraftWindow, "飞机数");
                                                        break;
                                                    case "座位数分布":
                                                        GetGridViewDataSourse(pieDataPoint, _seatWindow, "座位数");
                                                        break;
                                                    case "商载量分布":
                                                        GetGridViewDataSourse(pieDataPoint, _loadWindow, "商载量");
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                switch (radPieChart.EmptyContent.ToString())
                                                {
                                                    case "飞机数分布":
                                                        _aircraftWindow.Close();
                                                        break;
                                                    case "座位数分布":
                                                        _seatWindow.Close();
                                                        break;
                                                    case "商载量分布":
                                                        _loadWindow.Close();
                                                        break;
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
                }
            }

                    #endregion
        }

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
                    var firstOrDefault = CurrentPassengerAircraftTrend.LineSeries.Series.FirstOrDefault(p => p.Name.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (firstOrDefault != null)
                        firstOrDefault.Visibility = Visibility.Visible;
                    var cartesianSeries = CurrentPassengerAircraftTrend.BarSerires.Series.FirstOrDefault(p => p.Name.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (cartesianSeries != null)
                        cartesianSeries.Visibility = Visibility.Visible;
                }
                else
                {
                    var firstOrDefault = CurrentPassengerAircraftTrend.LineSeries.Series.FirstOrDefault(p => p.Name.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (firstOrDefault != null)
                        firstOrDefault.Visibility = Visibility.Collapsed;
                    var cartesianSeries = CurrentPassengerAircraftTrend.BarSerires.Series.FirstOrDefault(p => p.Name.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                    if (cartesianSeries != null)
                        cartesianSeries.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            IsContextMenuOpen = true;
        }

        #endregion

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

            public string Aircraft { get; set; } //航空公司的名称
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
            public int SeatAmount { get; set; } //座位数的总数
            public int LoadAmount { get; set; } //商载量的总数
            public string AircraftColor { get; set; } //飞机数的颜色
            public string SeatColor { get; set; } //座位数的颜色
            public string LoadColor { get; set; } //商载量的颜色
        }

        #endregion
    }
}
