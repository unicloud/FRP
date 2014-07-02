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
using System.Xml.Linq;
using Telerik.Windows.Controls.ColorEditor;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof (ManageSystemConfigVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageSystemConfigVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageSystemConfigVm(IBaseManagementService service)
            : base(service)
        {
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
            AircraftCabinTypes.PageSize = 18;
            _service.RegisterCollectionView(AircraftCabinTypes);
            AircraftCabinTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectAircraftCabinType = !AircraftCabinTypes.HasChanges;
                }
            };

            XmlSettings = _service.CreateCollection(_context.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                _airlines.Clear();
                _regionals.Clear();
                _aircraftTypes.Clear();
                _importTypes.Clear();
                _aircraftAges.Clear();
                _aircrafTrends.Clear();
                XmlSetting =
                    XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                if (XmlSetting != null)
                {
                    var xelement = XElement.Parse(XmlSetting.SettingContent);
                    if (xelement != null)
                    {
                        foreach (var type in xelement.Descendants("Type"))
                        {
                            if (type.Attribute("TypeName").Value.Equals("航空公司", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _airlines.Add(xmlItem);
                                }
                                AirLineList = _airlines;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("座级", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _regionals.Add(xmlItem);
                                }
                                RegionalList = _regionals;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("机型", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _aircraftTypes.Add(xmlItem);
                                }
                                AircraftTypeList = _aircraftTypes;
                            }
                            else if (type.Attribute("TypeName").Value.Equals("引进方式", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _importTypes.Add(xmlItem);
                                }
                                ImportTypeList = _importTypes;
                            }
                            else if (type.Attribute("TypeName")
                                .Value.Equals("机龄", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _aircraftAges.Add(xmlItem);
                                }
                                AircraftAgeList = _aircraftAges;
                            }
                            else if (type.Attribute("TypeName")
                                .Value.Equals("运力变化", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var item in type.Descendants("Item"))
                                {
                                    var xmlItem = new XmlItem
                                    {
                                        Name = item.Attribute("Name").Value,
                                        Color = item.Attribute("Color").Value
                                    };
                                    _aircrafTrends.Add(xmlItem);
                                }
                                AircraftTrendList = _aircrafTrends;
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
        ///     选中的飞机舱位类型
        /// </summary>
        private AircraftCabinTypeDTO _aircraftCabinType;

        //用户能否选择
        private bool _canSelectAircraftCabinType = true;

        /// <summary>
        ///     飞机舱位类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCabinTypeDTO> AircraftCabinTypes { get; set; }

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

        private readonly List<XmlItem> _aircrafTrends = new List<XmlItem>();
        private readonly List<XmlItem> _aircraftAges = new List<XmlItem>();
        private readonly List<XmlItem> _aircraftTypes = new List<XmlItem>();
        private readonly List<XmlItem> _airlines = new List<XmlItem>();
        private readonly List<XmlItem> _importTypes = new List<XmlItem>();
        private readonly List<XmlItem> _regionals = new List<XmlItem>();
        private List<XmlItem> _aircraftAgeList;
        private List<XmlItem> _aircraftTrendList;
        private List<XmlItem> _aircraftTypeList;

        private List<XmlItem> _airlineList;
        private List<XmlItem> _importTypeList;

        private List<XmlItem> _regionalList;
        private XmlSettingDTO _xmlSetting;
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; }

        public XmlSettingDTO XmlSetting
        {
            get { return _xmlSetting; }
            set
            {
                _xmlSetting = value;
                RaisePropertyChanged(() => XmlSetting);
            }
        }

        /// <summary>
        ///     航空公司颜色配置集合
        /// </summary>
        public List<XmlItem> AirLineList
        {
            get { return _airlineList; }
            set
            {
                if (_airlineList != value)
                {
                    _airlineList = value;
                    RaisePropertyChanged(() => AirLineList);
                }
            }
        }

        /// <summary>
        ///     座级颜色配置集合
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

        /// <summary>
        ///     机型颜色配置集合
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

        /// <summary>
        ///     引进方式颜色配置集合
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

        /// <summary>
        ///     机龄颜色配置集合
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

        /// <summary>
        ///     运力字段颜色配置集合
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

        public void ColorEditorSelectedColorChanged(object sender, ColorChangeEventArgs e)
        {
            //将颜色配置对象的集合转换成颜色配置XML
            var fleetColorSet = new XElement("FleetColorSet");
            var colorSet = new XElement("ColorSet");
            fleetColorSet.Add(colorSet);
            //航空公司节点
            var airLineNode = new XElement("Type", new XAttribute("TypeName", "航空公司"));
            colorSet.Add(airLineNode);
            foreach (var xmlItem in AirLineList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                airLineNode.Add(itemNode);
            }
            //座级节点
            var aircraftRegionalNode = new XElement("Type", new XAttribute("TypeName", "座级"));
            colorSet.Add(aircraftRegionalNode);
            foreach (var xmlItem in RegionalList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                aircraftRegionalNode.Add(itemNode);
            }
            //机型节点
            var aircraftTypeNode = new XElement("Type", new XAttribute("TypeName", "机型"));
            colorSet.Add(aircraftTypeNode);
            foreach (var xmlItem in AircraftTypeList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                aircraftTypeNode.Add(itemNode);
            }
            //引进类型节点
            var importTypeNode = new XElement("Type", new XAttribute("TypeName", "引进方式"));
            colorSet.Add(importTypeNode);
            foreach (var xmlItem in ImportTypeList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                importTypeNode.Add(itemNode);
            }
            //机龄节点
            var aircraftAgeNode = new XElement("Type", new XAttribute("TypeName", "机龄"));
            colorSet.Add(aircraftAgeNode);
            foreach (var xmlItem in AircraftAgeList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                aircraftAgeNode.Add(itemNode);
            }
            //运力变化节点
            var aircraftTrendNode = new XElement("Type", new XAttribute("TypeName", "运力变化"));
            colorSet.Add(aircraftTrendNode);
            foreach (var xmlItem in AircraftTrendList)
            {
                var itemNode = new XElement("Item", new XAttribute("Name", xmlItem.Name),
                    new XAttribute("Color", xmlItem.Color));
                aircraftTrendNode.Add(itemNode);
            }

            XmlSetting.SettingContent = fleetColorSet.ToString();
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
    ///     XML读取的项
    /// </summary>
    public class XmlItem
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}