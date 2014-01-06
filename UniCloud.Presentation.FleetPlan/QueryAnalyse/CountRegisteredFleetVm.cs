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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Export;
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
        private readonly FleetPlanData _fleetPlanContext;
        public CountRegisteredFleet CurrentCountRegisteredFleet
        {
            get { return ServiceLocator.Current.GetInstance<CountRegisteredFleet>(); }
        }
        private static readonly CommonMethod CommonMethod = new CommonMethod();

        private int _i; //导出数据源格式判断
        private Grid _monthGrid, _yearGrid;
        private RadGridView _monthExportRadgridview, _yearExportRadgridview; //初始化RadGridView

        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        [ImportingConstructor]
        public CountRegisteredFleetVm(IFleetPlanService service)
        {
            _fleetPlanContext = service.Context;

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
            XmlConfigs = new QueryableDataServiceCollectionView<XmlConfigDTO>(_fleetPlanContext, _fleetPlanContext.XmlConfigs);
            XmlConfigs.LoadedData += (o, e) =>
            {
                _loadXmlConfig = true;
                InitializeData();
            };
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(_fleetPlanContext, _fleetPlanContext.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                _loadXmlSetting = true;
                InitializeData();
            };
        }

        /// <summary>
        /// 以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            ExportCommand = new DelegateCommand<object>(OnExport);//导出图表源数据（Source data）
            ToggleButtonCommand = new DelegateCommand<object>(ToggleButtonCheck);
            _monthGrid = CurrentCountRegisteredFleet.MonthGrid;
            _yearGrid = CurrentCountRegisteredFleet.YearGrid;
        }
        #endregion

        #region 数据
        #region 公共数据
        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; }//XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合

        private ObservableCollection<FleetData> _monthFleetDatas;
        public ObservableCollection<FleetData> MonthFleetDatas
        {
            get { return _monthFleetDatas; }
            set
            {
                _monthFleetDatas = value;
                RaisePropertyChanged("MonthFleetDatas");
            }
        }
        private ObservableCollection<FleetData> StaticMonthFleetDatas { get; set; }
        private ObservableCollection<FleetRegisteredTrend> _monthAircraftTypes;
        public ObservableCollection<FleetRegisteredTrend> MonthAircraftTypes
        {
            get { return _monthAircraftTypes; }
            set
            {
                _monthAircraftTypes = value;
                RaisePropertyChanged("MonthAircraftTypes");
            }
        }

        private ObservableCollection<FleetData> _yearFleetDatas;
        public ObservableCollection<FleetData> YearFleetDatas
        {
            get { return _yearFleetDatas; }
            set
            {
                _yearFleetDatas = value;
                RaisePropertyChanged("YearFleetDatas");
            }
        }
        private ObservableCollection<FleetData> StaticYearFleetDatas { get; set; }

        private ObservableCollection<FleetRegisteredTrend> _yearAircraftTypes;
        public ObservableCollection<FleetRegisteredTrend> YearAircraftTypes
        {
            get { return _yearAircraftTypes; }
            set
            {
                _yearAircraftTypes = value;
                RaisePropertyChanged("YearAircraftTypes");
            }
        }


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
                    _endDate = value;
                    RaisePropertyChanged(() => EndDate);
                    CreatFleetRegisteredTrendMonthCollection();
                }
            }
        }
        #endregion

        #region ViewModel 属性 StartDate --开始时间
        private DateTime? _startDate = new DateTime(2000, 1, 1);
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

        public override void LoadData()
        {
            IsBusy = true;
            XmlConfigs.AutoLoad = true;
            XmlSettings.AutoLoad = true;
        }

        #endregion
        #endregion

        #region 操作

        /// <summary>
        /// 创建GradDataView
        /// </summary>
        /// <param name="structs"></param>
        /// <param name="itemsSource"></param>
        /// <param name="headerName"></param>
        public void CreateDataGridView(Dictionary<string, string> structs, List<FleetRegisteredTrend> itemsSource, string headerName)
        {
            if (headerName.Equals("FleetTrendPnrMonth", StringComparison.OrdinalIgnoreCase))
            {
                _monthExportRadgridview = ImageAndGridOperation.CreatDataGridView(structs, itemsSource, headerName);
            }
            else
            {
                _yearExportRadgridview = ImageAndGridOperation.CreatDataGridView(structs, itemsSource, headerName);
            }
        }


        /// <summary>
        /// 年平均在册的机型飞机数集合变化时
        /// </summary>
        /// <param name="fleetRegisteredTrendYearCollection"></param>
        public void FleetRegisteredTrendYearCollectionChange(List<FleetRegisteredTrend> fleetRegisteredTrendYearCollection)
        {
            YearFleetDatas = new ObservableCollection<FleetData>();
            StaticYearFleetDatas = new ObservableCollection<FleetData>();
            var result = new ObservableCollection<FleetData>();
            var aircraftTypeResult = new ObservableCollection<FleetRegisteredTrend>();

            if (FleetRegisteredTrendYearCollection != null)
            {
                foreach (var groupItem in FleetRegisteredTrendYearCollection.GroupBy(p => p.AircraftType).ToList())
                {
                    var fleetRegisteredTrend = groupItem.FirstOrDefault();
                    if (fleetRegisteredTrend != null)
                    {
                        var tempData = new FleetData
                        {
                            AircraftTypeName = groupItem.Key,
                            Data = new ObservableCollection<FleetRegisteredTrend>()
                        };
                        groupItem.ToList().ForEach(tempData.Data.Add);
                        result.Add(tempData);
                        aircraftTypeResult.Add(fleetRegisteredTrend);
                    }
                }
            }
            YearFleetDatas.Add(result.FirstOrDefault(p => p.AircraftTypeName.Equals("所有机型", StringComparison.OrdinalIgnoreCase)));
            result.ToList().ForEach(StaticYearFleetDatas.Add);
            YearAircraftTypes = aircraftTypeResult;
            //控制趋势图的滚动条
            int dateTimeCount = FleetRegisteredTrendYearCollection.Select(p => p.DateTime).Distinct().Count();
            if (FleetRegisteredTrendYearCollection != null && dateTimeCount >= 12)
            {
                CurrentCountRegisteredFleet.YearCategoricalAxis.MajorTickInterval = dateTimeCount / 6;
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
            MonthFleetDatas = new ObservableCollection<FleetData>();
            StaticMonthFleetDatas = new ObservableCollection<FleetData>();
            var result = new ObservableCollection<FleetData>();
            var aircraftTypeResult = new ObservableCollection<FleetRegisteredTrend>();

            if (FleetRegisteredTrendMonthCollection != null)
            {
                foreach (var groupItem in FleetRegisteredTrendMonthCollection.GroupBy(p => p.AircraftType).ToList())
                {
                    var fleetRegisteredTrend = groupItem.FirstOrDefault();
                    if (fleetRegisteredTrend != null)
                    {
                        var tempData = new FleetData
                        {
                            AircraftTypeName = groupItem.Key,
                            Data = new ObservableCollection<FleetRegisteredTrend>()
                        };
                        groupItem.ToList().ForEach(tempData.Data.Add);
                        result.Add(tempData);
                        aircraftTypeResult.Add(fleetRegisteredTrend);
                    }
                }
            }
            MonthFleetDatas.Add(result.FirstOrDefault(p => p.AircraftTypeName.Equals("所有机型", StringComparison.OrdinalIgnoreCase)));
            result.ToList().ForEach(StaticMonthFleetDatas.Add);
            MonthAircraftTypes = aircraftTypeResult;
            //控制趋势图的滚动条
            int dateTimeCount = FleetRegisteredTrendMonthCollection.Select(p => p.DateTime).Distinct().Count();
            if (FleetRegisteredTrendMonthCollection != null && dateTimeCount >= 12)
            {
                CurrentCountRegisteredFleet.MonthCategoricalAxis.MajorTickInterval = dateTimeCount / 6;
            }
            else
            {
                CurrentCountRegisteredFleet.MonthCategoricalAxis.MajorTickInterval = 1;
            }
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
                if (button.IsChecked != null && (bool)button.IsChecked)
                {
                    if ("YearToggleButton".Equals(button.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        var temp = StaticYearFleetDatas.FirstOrDefault(p => p.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                        if (temp != null && !YearFleetDatas.Any(p => p.AircraftTypeName.Equals(temp.AircraftTypeName, StringComparison.OrdinalIgnoreCase)))
                        {
                            YearFleetDatas.Add(temp);
                        }
                    }
                    else
                    {
                        var temp = StaticMonthFleetDatas.FirstOrDefault(p => p.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase));
                        if (temp != null && !MonthFleetDatas.Any(p => p.AircraftTypeName.Equals(temp.AircraftTypeName, StringComparison.OrdinalIgnoreCase)))
                        {
                            MonthFleetDatas.Add(temp);
                        }
                    }
                }
                else
                {
                    if ("YearToggleButton".Equals(button.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        for (int i = YearFleetDatas.Count - 1; i > -1; i--)
                        {
                            var temp = YearFleetDatas[i];
                            if (temp.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase))
                            {
                                YearFleetDatas.Remove(temp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = MonthFleetDatas.Count - 1; i > -1; i--)
                        {
                            var temp = MonthFleetDatas[i];
                            if (temp.AircraftTypeName.Equals((string)button.Tag, StringComparison.OrdinalIgnoreCase))
                            {
                                MonthFleetDatas.Remove(temp);
                                break;
                            }
                        }
                    }
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
            // ReSharper disable once CSharpWarnings::CS0618
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
            var xmlConfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("在册分析", StringComparison.OrdinalIgnoreCase));

            string aircraftColor = string.Empty;
            var colorConfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type").Any(p =>
            {
                var xAttributeTypeName = p.Attribute("TypeName");
                return xAttributeTypeName != null && xAttributeTypeName.Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase);
            }))
            {
                var firstOrDefault = XElement.Parse(colorConfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                {
                    var orDefault = firstOrDefault.Descendants("Item").FirstOrDefault(p =>
                                                            {
                                                                var xAttributeName = p.Attribute("Name");
                                                                return xAttributeName != null && xAttributeName.Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase);
                                                            });
                    if (orDefault != null)
                        aircraftColor = orDefault.Attribute("Color").Value;
                }
            }

            XElement typeColor = null;
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typeColor = XElement.Parse(colorConfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
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
                        XElement typeElement = datetime.Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
                        if (typeElement != null)
                        {
                            //平均在册总飞机数的柱状数据
                            var amountTrendMonth = new FleetRegisteredTrend
                                                  {
                                                      DateTime = currentTime,
                                                      RegisteredCount = Convert.ToDouble(typeElement.Attribute("Amount").Value),
                                                      AircraftType = "所有机型"
                                                  };//月平均在册的总飞机数对象
                            if (!string.IsNullOrEmpty(aircraftColor))
                            {
                                amountTrendMonth.Color = aircraftColor;
                            }
                            fleetRegisteredTrendMonthList.Add(amountTrendMonth);

                            foreach (XElement item in typeElement.Descendants("Item"))
                            {
                                var fleetRegisteredTrenMonth = new FleetRegisteredTrend
                                                               {
                                                                   DateTime = currentTime,
                                                                   AircraftType = item.Attribute("Name").Value,
                                                                   RegisteredCount = Convert.ToDouble(item.Value)
                                                               };//月平均在册的机型飞机数对象
                                if (typeColor != null)
                                {
                                    var firstOrDefault = typeColor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetRegisteredTrenMonth.AircraftType, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault !=
                                        null)
                                        fleetRegisteredTrenMonth.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                fleetRegisteredTrendMonthList.Add(fleetRegisteredTrenMonth);
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
            var xmlConfig = XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("在册分析", StringComparison.OrdinalIgnoreCase));

            string aircraftColor = string.Empty;
            var colorConfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase)))
            {
                var firstOrDefault = XElement.Parse(colorConfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                {
                    var orDefault = firstOrDefault.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals("飞机数", StringComparison.OrdinalIgnoreCase));
                    if (orDefault != null)
                        aircraftColor = orDefault.Attribute("Color").Value;
                }
            }

            XElement typeColor = null;
            if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type").Any(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase)))
            {
                typeColor = XElement.Parse(colorConfig.SettingContent).Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
            }

            if (xmlConfig != null)
            {
                XElement xelement = XElement.Parse(xmlConfig.ConfigContent);
                if (xelement != null)
                {
                    foreach (XElement datetime in xelement.Descendants("DateTime"))
                    {
                        string currentTime = Convert.ToDateTime(datetime.Attribute("EndOfMonth").Value).ToString("yyyy/M");

                        XElement typeElement = datetime.Descendants("Type").FirstOrDefault(p => p.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase));
                        if (typeElement != null)
                        {
                            //平均在册总飞机数的柱状数据
                            var amountTrendMonth = new FleetRegisteredTrend
                                                  {
                                                      DateTime = currentTime,
                                                      RegisteredCount = Convert.ToDouble(typeElement.Attribute("Amount").Value),
                                                      AircraftType = "所有机型"
                                                  };//月平均在册的总飞机数对象
                            if (!string.IsNullOrEmpty(aircraftColor))
                            {
                                amountTrendMonth.Color = aircraftColor;
                            }
                            fleetRegisteredTrendMonthList.Add(amountTrendMonth);

                            foreach (XElement item in typeElement.Descendants("Item"))
                            {
                                var fleetRegisteredTrendMonth = new FleetRegisteredTrend
                                                               {
                                                                   DateTime = currentTime,
                                                                   AircraftType = item.Attribute("Name").Value,
                                                                   RegisteredCount = Convert.ToDouble(item.Value)
                                                               };//月平均在册的机型飞机数对象
                                if (typeColor != null)
                                {
                                    var firstOrDefault = typeColor.Descendants("Item").FirstOrDefault(p => p.Attribute("Name").Value.Equals(fleetRegisteredTrendMonth.AircraftType, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault !=
                                        null)
                                        fleetRegisteredTrendMonth.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                fleetRegisteredTrendMonthList.Add(fleetRegisteredTrendMonth);
                            }
                        }
                    }
                }
            }
            #endregion

            var fleetRegisteredTrendYearList = new List<FleetRegisteredTrend>();//年平均在册的机型飞机数集合
            var allFleetRegisteredTrendYeatList = new List<FleetRegisteredTrend>();
            if (fleetRegisteredTrendMonthList.Any())
            {
                //获取按机型分类的年平均在册飞机数的集合
                //原有的linq语句转为普通语句--ToList方法少了ForEach方法
                var fleetRegisteredTrendMonth = fleetRegisteredTrendMonthList.GroupBy(p => p.AircraftType).ToList();
                foreach (var variableMonth in fleetRegisteredTrendMonth)
                {
                    var vaiableYear = variableMonth.GroupBy(pp => Convert.ToDateTime(pp.DateTime).Year).ToList();
                    foreach (var variableYear in vaiableYear)
                    {
                        var fleetRegisterYear = new FleetRegisteredTrend();
                        var registeredTrend = variableYear.FirstOrDefault();
                        if (registeredTrend != null)
                            fleetRegisterYear.DateTime = registeredTrend.DateTime;
                        var firstOrDefault = variableMonth.FirstOrDefault();
                        if (firstOrDefault != null)
                            fleetRegisterYear.AircraftType = firstOrDefault.AircraftType;
                        fleetRegisterYear.RegisteredCount = Math.Round(variableYear.Sum(a => a.RegisteredCount * Convert.ToDateTime(a.DateTime).AddMonths(1).AddDays(-1).Day) / (new DateTime(variableYear.Key + 1, 1, 1) - new DateTime(variableYear.Key, 1, 1)).TotalDays, 4);
                        var fleetRegisteredTrend = variableMonth.FirstOrDefault();
                        if (fleetRegisteredTrend != null)
                            fleetRegisterYear.Color = fleetRegisteredTrend.Color;
                        fleetRegisteredTrendYearList.Add(fleetRegisterYear);
                    }
                }

                fleetRegisteredTrendMonthList.GroupBy(p => p.AircraftType).ToList().ForEach(p =>
                    fleetRegisteredTrendYearList.AddRange(p.GroupBy(pp => Convert.ToDateTime(pp.DateTime).Year).Select(o =>
                                                                                                                       {
                                                                                                                           var orDefault = p.FirstOrDefault();
                                                                                                                           return orDefault != null ? new FleetRegisteredTrend
                                                                                                                                                           {
                                                                                                                                                               DateTime = o.Key.ToString(CultureInfo.InvariantCulture),
                                                                                                                                                               AircraftType = p.Key,
                                                                                                                                                               RegisteredCount = Math.Round(o.Sum(a => a.RegisteredCount * Convert.ToDateTime(a.DateTime).AddMonths(1).AddDays(-1).Day) / (new DateTime(o.Key + 1, 1, 1) - new DateTime(o.Key, 1, 1)).TotalDays, 4),
                                                                                                                                                               Color = orDefault.Color
                                                                                                                                                           } : null;
                                                                                                                       }))
                );
            }

            FleetRegisteredTrendYearCollection = fleetRegisteredTrendYearList;
            allFleetRegisteredTrendYeatList.AddRange(FleetRegisteredTrendYearCollection);

            //创建RadGridView
            var columnsList2 = new Dictionary<string, string>
                {
                    {"DateTime", "年份"},
                    {"AircraftType", "机型"},
                    {"RegisteredCount", "年平均在册飞机数"}
                };
            CreateDataGridView(columnsList2, allFleetRegisteredTrendYeatList, "FleetTrendPnrYear");
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

        public class FleetData
        {
            public string AircraftTypeName { get; set; }
            public ObservableCollection<FleetRegisteredTrend> Data { get; set; }
        }
        #endregion
    }
}

