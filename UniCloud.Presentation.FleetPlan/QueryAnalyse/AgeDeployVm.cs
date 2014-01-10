#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/31 9:49:42
// 文件名：AgeDeployVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/31 9:49:42
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof (AgeDeployVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AgeDeployVm : ViewModelBase
    {
        #region 声明、初始化

        private static readonly CommonMethod CommonMethod = new CommonMethod();
        private readonly FleetPlanData _fleetPlanContext;
        private RadGridView _ageDeployGridView; //机龄配置列表
        private bool _loadXmlConfig;
        private bool _loadXmlSetting;

        [ImportingConstructor]
        public AgeDeployVm(IFleetPlanService service) : base(service)
        {
            _fleetPlanContext = service.Context;
            ViewModelInitializer();
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AbortCommand = new DelegateCommand<object>(OnAbort, CanAbort);
            InitializeVm();
        }

        public AgeDeploy CurrentAgeDeploy
        {
            get { return ServiceLocator.Current.GetInstance<AgeDeploy>(); }
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
                CreatAgeDeployCollection(); //将机龄配置XML转换成机龄配置对象的集合
            };
            XmlSettings = new QueryableDataServiceCollectionView<XmlSettingDTO>(_fleetPlanContext,
                _fleetPlanContext.XmlSettings);
            XmlSettings.LoadedData += (o, e) =>
            {
                _loadXmlSetting = true;
                CreatAgeDeployCollection(); //将机龄配置XML转换成机龄配置对象的集合
            };
        }

        #endregion

        #region 数据

        #region 公共属性

        public QueryableDataServiceCollectionView<XmlConfigDTO> XmlConfigs { get; set; } //XmlConfig集合
        public QueryableDataServiceCollectionView<XmlSettingDTO> XmlSettings { get; set; }

        #region ViewModel 属性  IsChanged

        private bool _isChanged; //判断数据是否更改

        /// <summary>
        ///     判断数据是否更改(用于控制界面的按钮）
        /// </summary>
        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                if (_isChanged != value)
                {
                    _isChanged = value;
                    RaisePropertyChanged(() => IsChanged);
                    _canSave = value;
                    _canAbort = value;
                    CommandRaiseCanExecute();
                }
            }
        }

        #endregion

        #region ViewModel 属性 AgeDeployCollection --机龄配置的集合

        private List<AgeDeployClass> _ageDeployCollection = new List<AgeDeployClass>();

        /// <summary>
        ///     机龄配置的集合
        /// </summary>
        public List<AgeDeployClass> AgeDeployCollection
        {
            get { return _ageDeployCollection; }
            set
            {
                _ageDeployCollection = value;
                RaisePropertyChanged(() => AgeDeployCollection);
            }
        }

        #endregion

        //XmlSetting集合

        #endregion

        #region 加载数据

        public override void LoadData()
        {
            IsBusy = true;
            XmlConfigs.Load(true);
            XmlSettings.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        /// <summary>
        ///     以View的实例初始化ViewModel相关字段、属性
        /// </summary>
        private void ViewModelInitializer()
        {
            _ageDeployGridView = CurrentAgeDeploy.AgeDeployGridView;
            //定义数据编辑的事件（控制保存、放弃修改按钮的可用性）
            _ageDeployGridView.CellEditEnded += AgeDeployGridViewCellEditEnded;
        }

        /// <summary>
        ///     更改时命令可用
        /// </summary>
        public void CommandRaiseCanExecute()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     控制保存、放弃修改按钮的可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgeDeployGridViewCellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if (!e.Cell.Column.Header.ToString().Equals("对应颜色", StringComparison.OrdinalIgnoreCase) &&
                Convert.ToUInt32(e.NewData) != Convert.ToUInt32(e.OldData))
            {
                IsChanged = true;
            }
            else if (e.Cell.Column.Header.ToString().Equals("对应颜色", StringComparison.OrdinalIgnoreCase))
            {
                IsChanged = true;
            }
        }

        #region Command

        #region ViewModel 命令 -- 创建新区间

        private bool _canAdd = true; //判断增加按钮可用
        public DelegateCommand<object> AddCommand { get; set; }

        public bool CanAddBtn
        {
            get { return _canAdd; }
            set
            {
                _canAdd = value;
                RaisePropertyChanged(() => CanAddBtn);
            }
        }

        private void OnAdd(object sender)
        {
            var ageDeploy = new AgeDeployClass {Color = "#FFEFF8F8"};
            var collection = new List<AgeDeployClass>();
            collection.AddRange(AgeDeployCollection);
            collection.Add(ageDeploy);
            AgeDeployCollection = collection;
            _ageDeployGridView.SelectedItem = ageDeploy;
            //设置保存、放弃修改按钮的可用性
            IsChanged = true;
        }

        /// <summary>
        ///     添加按钮的可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private bool CanAdd(object sender)
        {
            return CanAddBtn;
        }

        #endregion

        #region ViewModel 命令 -- 移除区间

        private bool _canRemove = true; //判断删除按钮可用
        public DelegateCommand<object> RemoveCommand { get; set; }

        public bool CanRemoveBtn
        {
            get { return _canRemove; }
            set
            {
                _canRemove = value;
                RaisePropertyChanged(() => CanRemoveBtn);
            }
        }

        private void OnRemove(object sender)
        {
            var selectedAgeDeploy = _ageDeployGridView.SelectedItem as AgeDeployClass;
            if (selectedAgeDeploy != null)
            {
                int selectIndex = AgeDeployCollection.IndexOf(selectedAgeDeploy);
                var collection = new List<AgeDeployClass>();
                collection.AddRange(AgeDeployCollection);
                collection.Remove(selectedAgeDeploy);
                AgeDeployCollection = collection;
                //设置选中行
                if (AgeDeployCollection.Any() && selectIndex > 0)
                {
                    _ageDeployGridView.SelectedItem = AgeDeployCollection.Count() > selectIndex
                        ? AgeDeployCollection[selectIndex]
                        : AgeDeployCollection[AgeDeployCollection.Count() - 1];
                }
                //设置保存、放弃修改按钮的可用性
                IsChanged = true;
            }
        }

        /// <summary>
        ///     删除按钮可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private bool CanRemove(object sender)
        {
            return CanRemoveBtn;
        }

        #endregion

        #region ViewModel 命令 -- 保存

        private bool _canSave; //判断保持按钮的可用性
        public DelegateCommand<object> SaveCommand { get; set; }

        public bool CanSaveBtn
        {
            get { return _canSave; }
            set
            {
                _canSave = value;
                RaisePropertyChanged(() => CanSaveBtn);
            }
        }

        private void OnSave(object sender)
        {
            SaveAgeDeploXml();
            //设置保存、放弃修改按钮的可用性
            IsChanged = false;
            CurrentAgeDeploy.Tag = true;
        }

        /// <summary>
        ///     保持按钮的可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private bool CanSave(object sender)
        {
            return CanSaveBtn;
        }

        #endregion

        #region ViewModel 命令 -- 放弃更改

        private bool _canAbort; //判断按钮是否可撤销
        public DelegateCommand<object> AbortCommand { get; set; }

        /// <summary>
        ///     撤销按钮可用性
        /// </summary>
        public bool CanAbortBtn
        {
            get { return _canAbort; }
            set
            {
                _canAbort = value;
                RaisePropertyChanged(() => CanAbortBtn);
            }
        }

        private void OnAbort(object sender)
        {
            CreatAgeDeployCollection();
            //设置保存、放弃修改按钮的可用性
            IsChanged = false;
        }

        /// <summary>
        ///     撤销按钮可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool CanAbort(object sender)
        {
            return CanAbortBtn;
        }

        #endregion

        #endregion

        #region 将机龄配置XML转换成机龄配置对象的集合

        /// <summary>
        ///     将机龄配置XML转换成机龄配置对象的集合
        /// </summary>
        /// <returns></returns>
        protected void CreatAgeDeployCollection()
        {
            if (_loadXmlConfig && _loadXmlSetting)
            {
                _loadXmlConfig = false;
                _loadXmlSetting = false;
                IsBusy = false;
                var collection = new List<AgeDeployClass>(); //将机龄配置XML转换成机龄配置对象的集合
                if (XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄配置", StringComparison.OrdinalIgnoreCase)) !=
                    null)
                {
                    var xmlConfig =
                        XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄配置", StringComparison.OrdinalIgnoreCase));
                    if (xmlConfig != null)
                    {
                        XElement xelement = XElement.Parse(xmlConfig.ConfigContent);

                        XElement ageColor = null;
                        var colorConfig =
                            XmlSettings.FirstOrDefault(
                                p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
                        if (colorConfig != null && XElement.Parse(colorConfig.SettingContent).Descendants("Type")
                            .Any(p => p.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase)))
                        {
                            ageColor = XElement.Parse(colorConfig.SettingContent)
                                .Descendants("Type")
                                .FirstOrDefault(
                                    p => p.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase));
                        }
                        if (xelement != null)
                        {
                            foreach (var item in xelement.Descendants("Item"))
                            {
                                var ageDeploy = new AgeDeployClass
                                {
                                    Name = item.Value,
                                    StartYear = Convert.ToUInt32(item.Attribute("Start").Value),
                                    EndYear = Convert.ToUInt32(item.Attribute("End").Value)
                                };
                                if (ageColor != null)
                                {
                                    var firstOrDefault =
                                        ageColor.Descendants("Item")
                                            .FirstOrDefault(
                                                p =>
                                                    p.Attribute("Name")
                                                        .Value.Equals(ageDeploy.Name, StringComparison.OrdinalIgnoreCase));
                                    if (firstOrDefault != null)
                                        ageDeploy.Color = firstOrDefault.Attribute("Color").Value;
                                }
                                collection.Add(ageDeploy);
                            }
                        }
                    }
                }
                AgeDeployCollection = collection;
            }
        }

        #endregion

        #region 将机龄配置对象的集合转换成机龄配置XML

        /// <summary>
        ///     将机龄配置对象的集合转换成机龄配置XML
        /// </summary>
        /// <returns></returns>
        protected void SaveAgeDeploXml()
        {
            //将机龄配置对象的集合转换成机龄配置XML
            var ageDeploy = new XElement("AgeDeploy");
            //机龄范围的颜色节点
            var colorNode = new XElement("Type", new XAttribute("TypeName", "机龄"));
            foreach (AgeDeployClass agedeploy in AgeDeployCollection)
            {
                string name = agedeploy.StartYear + "至" + agedeploy.EndYear + "年之间";
                //供应商节点
                var itemNode = new XElement("Item", new XAttribute("Start", agedeploy.StartYear),
                    new XAttribute("End", agedeploy.EndYear), name);
                ageDeploy.Add(itemNode);

                //颜色节点
                var childNode = new XElement("Item", new XAttribute("Name", name),
                    new XAttribute("Color", agedeploy.Color));
                colorNode.Add(childNode);
            }
            if (XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄配置", StringComparison.OrdinalIgnoreCase)) != null)
            {
                var xmlConfigDataObject =
                    XmlConfigs.FirstOrDefault(p => p.ConfigType.Equals("机龄配置", StringComparison.OrdinalIgnoreCase));
                if (xmlConfigDataObject != null)
                    xmlConfigDataObject.ConfigContent = ageDeploy.ToString();
            }
            else
            {
                var xmlConfig = new XmlConfigDTO
                {
                    XmlConfigId = Guid.NewGuid(),
                    ConfigType = "机龄配置",
                    VersionNumber = 1,
                    ConfigContent = (string) ageDeploy
                };
                XmlConfigs.AddNew(xmlConfig);
            }
            //XElement colorxelement = XElement.Parse(this._xmlSettingDataObjectList.FirstOrDefault(p => p.SettingType == "颜色配置").SettingContent);
            var xmlSetting =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (xmlSetting != null)
            {
                var firstOrDefault =
                    XElement.Parse(xmlSetting.SettingContent)
                        .Descendants("Type")
                        .FirstOrDefault(
                            p => p.Attribute("TypeName").Value.Equals("机龄", StringComparison.OrdinalIgnoreCase));
                if (firstOrDefault != null)
                    firstOrDefault.Remove();
            }
            var settingDataObject =
                XmlSettings.FirstOrDefault(p => p.SettingType.Equals("颜色配置", StringComparison.OrdinalIgnoreCase));
            if (settingDataObject != null)
                XElement.Parse(settingDataObject.SettingContent).Add(colorNode);
            //this._xmlSettingDataObjectList.FirstOrDefault(p => p.SettingType == "颜色配置").SettingContent = colorxelement;
            //缺少SubmitChanges方法
            //this._service.SubmitChanges(CAFMStrings.SaveSuccess, CAFMStrings.SaveFail, sm => { }, null);
        }

        #endregion

        #endregion

        #region Classes

        /// <summary>
        ///     机龄配置的对象
        /// </summary>
        public class AgeDeployClass
        {
            public AgeDeployClass()
            {
                Color = CommonMethod.GetRandomColor();
            }

            public string Name { get; set; } //机龄区间的名称
            public uint StartYear { get; set; } //开始年份
            public uint EndYear { get; set; } //结束年份
            public string Color { get; set; } //颜色
        }

        #endregion
    }
}