#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:52:15
// 文件名：ManageSystemConfigVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:52:15
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using System.Xml.Linq;
#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof(ManageSystemConfigVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageSystemConfigVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageSystemConfigVm(IRegionManager regionManager, IBaseManagementService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            // 创建并注册CollectionView
            AircraftCabinTypes = _service.CreateCollection(_context.AircraftCabinTypes);
            _service.RegisterCollectionView(AircraftCabinTypes);
            AircraftCabinTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectAircraftCabinType = !AircraftCabinTypes.HasChanges;
                }
            };

            var fleetService = ServiceLocator.Current.GetInstance<IFleetPlanService>();
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(fleetService.Context,
              fleetService.Context.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                var xmlconfig = XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                if (xmlconfig != null)
                {
                    XElement xelement = XElement.Parse(xmlconfig.SettingContent);
                    if (xelement != null)
                    {
                        foreach (XElement type in xelement.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    //if (ValidName(item.Attribute("Name").Value)) //判断是否为当前航空公司或其子公司
                                    {
                                        var xmlItem = new XmlItem
                                                      {
                                                          Name = item.Attribute("Name").Value,
                                                          Color = item.Attribute("Color").Value
                                                      };
                                        _airlinelist.Add(xmlItem);
                                    }
                                    //else
                                    //{
                                    //    var xmlItem = new XmlItem
                                    //                  {
                                    //                      Name = item.Attribute("Name").Value,
                                    //                      Color = item.Attribute("Color").Value
                                    //                  };
                                    //    _restairlinelist.Add(xmlItem);
                                    //}
                                }
                                AirLineList = _airlinelist;
                                RestAirLineList = _restairlinelist;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                                  {
                                                      Name = item.Attribute("Name").Value,
                                                      Color = item.Attribute("Color").Value
                                                  };
                                    _regionallist.Add(xmlItem);
                                }
                                RegionalList = _regionallist;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                                  {
                                                      Name = item.Attribute("Name").Value,
                                                      Color = item.Attribute("Color").Value
                                                  };
                                    _aircrafttypelist.Add(xmlItem);
                                }
                                AircraftTypeList = _aircrafttypelist;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                                  {
                                                      Name = item.Attribute("Name").Value,
                                                      Color = item.Attribute("Color").Value
                                                  };
                                    _importtypelist.Add(xmlItem);
                                }
                                ImportTypeList = _importtypelist;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                                  {
                                                      Name = item.Attribute("Name").Value,
                                                      Color = item.Attribute("Color").Value
                                                  };
                                    _aircraftagelist.Add(xmlItem);
                                }
                                AircraftAgeList = _aircraftagelist;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XElement item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                                  {
                                                      Name = item.Attribute("Name").Value,
                                                      Color = item.Attribute("Color").Value
                                                  };
                                    _aircraftrendlist.Add(xmlItem);
                                }
                                AircraftTrendList = _aircraftrendlist;
                            }
                        }
                    }
                }
            };
        }

        #endregion

        #region 数据

        #region 公共属性

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            // 将CollectionView的AutoLoad属性设为True
            if (!AircraftCabinTypes.AutoLoad)
                AircraftCabinTypes.AutoLoad = true;
            AircraftCabinTypes.Load(true);
            XmlSettings.Load(true);
        }

        #region 飞机舱位类型
        /// <summary>
        ///     飞机舱位类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCabinTypeDTO> AircraftCabinTypes { get; set; }

        /// <summary>
        ///     选中的飞机舱位类型
        /// </summary>
        private AircraftCabinTypeDTO _aircraftCabinType;
        public AircraftCabinTypeDTO AircraftCabinType
        {
            get { return _aircraftCabinType; }
            set
            {
                if (_aircraftCabinType != value)
                {
                    _aircraftCabinType = value;
                    RaisePropertyChanged(() => AircraftCabinType);
                }
            }
        }

        //用户能否选择
        private bool _canSelectAircraftCabinType = true;
        public bool CanSelectAircraftCabinType
        {
            get { return _canSelectAircraftCabinType; }
            set
            {
                if (_canSelectAircraftCabinType != value)
                {
                    _canSelectAircraftCabinType = value;
                    RaisePropertyChanged(() => CanSelectAircraftCabinType);
                }
            }
        }

        #endregion

        #region 颜色配置
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; } //XmlSetting集合

        private readonly List<XmlItem> _airlinelist = new List<XmlItem>();
        private readonly List<XmlItem> _restairlinelist = new List<XmlItem>();//用来保存展示没被用到的航空公司
        private readonly List<XmlItem> _regionallist = new List<XmlItem>();
        private readonly List<XmlItem> _aircrafttypelist = new List<XmlItem>();
        private readonly List<XmlItem> _importtypelist = new List<XmlItem>();
        private readonly List<XmlItem> _aircraftagelist = new List<XmlItem>();
        private readonly List<XmlItem> _aircraftrendlist = new List<XmlItem>();

        private List<XmlItem> _airLineList;
        /// <summary>
        /// 航空公司颜色配置集合
        /// </summary>
        public List<XmlItem> AirLineList
        {
            get { return _airLineList; }
            set
            {
                if (_airLineList != value)
                {
                    _airLineList = value;
                    RaisePropertyChanged(() => AirLineList);
                }
            }
        }

        private List<XmlItem> _restAirLineList;
        /// <summary>
        /// 暂时用不上的航空公司颜色配置集合
        /// </summary>
        public List<XmlItem> RestAirLineList
        {
            get { return _restAirLineList; }
            set
            {
                _restAirLineList = value;
                RaisePropertyChanged(() => RestAirLineList);
            }
        }

        private List<XmlItem> _regionalList;
        /// <summary>
        /// 座级颜色配置集合
        /// </summary>
        public List<XmlItem> RegionalList
        {
            get { return _regionalList; }
            set
            {

                _regionalList = value;
                RaisePropertyChanged(() => RegionalList);
            }
        }

        private List<XmlItem> _aircraftTypeList;
        /// <summary>
        /// 机型颜色配置集合
        /// </summary>
        public List<XmlItem> AircraftTypeList
        {
            get { return _aircraftTypeList; }
            set
            {
                _aircraftTypeList = value;
                RaisePropertyChanged(() => AircraftTypeList);
            }
        }

        private List<XmlItem> _importTypeList;
        /// <summary>
        /// 引进方式颜色配置集合
        /// </summary>
        public List<XmlItem> ImportTypeList
        {
            get { return _importTypeList; }
            set
            {
                _importTypeList = value;
                RaisePropertyChanged(() => ImportTypeList);
            }
        }

        private List<XmlItem> _aircraftAgeList;
        /// <summary>
        /// 机龄颜色配置集合
        /// </summary>
        public List<XmlItem> AircraftAgeList
        {
            get { return _aircraftAgeList; }
            set
            {
                _aircraftAgeList = value;
                RaisePropertyChanged(() => AircraftAgeList);
            }
        }

        private List<XmlItem> _aircraftTrendList;
        /// <summary>
        /// 运力字段颜色配置集合
        /// </summary>
        public List<XmlItem> AircraftTrendList
        {
            get { return _aircraftTrendList; }
            set
            {
                _aircraftTrendList = value;
                RaisePropertyChanged(() => AircraftTrendList);
            }
        }
        #endregion
        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #endregion
    }

    /// <summary>
    ///XML读取的项
    /// </summary>
    public class XmlItem
    {
        public string Name { get; set; }
        public string Color { get; set; }
        // public Color ChangeColor { get; set; }
    }
}
