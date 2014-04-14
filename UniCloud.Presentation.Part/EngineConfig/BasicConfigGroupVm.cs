#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 17:46:48
// 文件名：BasicConfigGroupVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export(typeof(BasicConfigGroupVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BasicConfigGroupVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly PartData _context;

        [ImportingConstructor]
        public BasicConfigGroupVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            BasicConfigGroups = _service.CreateCollection(_context.BasicConfigGroups);//TODO按机型分组
            _service.RegisterCollectionView(BasicConfigGroups);//注册查询集合

            BasicConfigs = _service.CreateCollection(_context.BasicConfigs);
            BasicConfigs.LoadedData += (o, e) =>
            {
                BasicConfigs.ToList().ForEach(p=>p.SubBasicConfigs.Clear());
                BasicConfigs.ToList().ForEach(GenerateBasicConfigStructure);
                if(SelBasicConfigGroup!=null)
                {
                    ViewBasicConfigs.Clear();
                    List<BasicConfigDTO> bcs =
                        BasicConfigs.SourceCollection.Cast<BasicConfigDTO>()
                            .Where(p => p.BasicConfigGroupId == SelBasicConfigGroup.Id && p.ParentId == null).ToList();
                    foreach (var bc in bcs)
                    {
                        ViewBasicConfigs.Add(bc);
                    }
                    ViewBasicConfigs.Distinct();
                }
            };
            _service.RegisterCollectionView(BasicConfigs);//注册查询集合

            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_context, _context.AircraftTypes);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
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
            if (!BasicConfigGroups.AutoLoad)
                BasicConfigGroups.AutoLoad = true;
            else
                BasicConfigGroups.Load(true);

            if (!BasicConfigs.AutoLoad)
                BasicConfigs.AutoLoad = true;
            else
                BasicConfigs.Load(true);

            AircraftTypes.AutoLoad = true;
        }

        #region 业务

        #region 基本构型组集合

        /// <summary>
        ///     基本构型组集合
        /// </summary>
        public QueryableDataServiceCollectionView<BasicConfigGroupDTO> BasicConfigGroups { get; set; }

        #endregion

        #region 基本构型集合

        /// <summary>
        ///     基本构型集合
        /// </summary>
        public QueryableDataServiceCollectionView<BasicConfigDTO> BasicConfigs { get; set; }

        private ObservableCollection<BasicConfigDTO> _viewBasicConfigs = new ObservableCollection<BasicConfigDTO>();

        /// <summary>
        /// 基本构型集合
        /// </summary>
        public ObservableCollection<BasicConfigDTO> ViewBasicConfigs
        {
            get { return this._viewBasicConfigs; }
            private set
            {
                if (this._viewBasicConfigs != value)
                {
                    _viewBasicConfigs = value;
                    this.RaisePropertyChanged(() => this.ViewBasicConfigs);
                }
            }
        }

        #endregion

        #region 机型集合

        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        #endregion

        #region 选择的基本构型组

        private BasicConfigGroupDTO _selbBasicConfigGroup;

        /// <summary>
        ///     选择的基本构型组
        /// </summary>
        public BasicConfigGroupDTO SelBasicConfigGroup
        {
            get { return _selbBasicConfigGroup; }
            private set
            {
                if (_selbBasicConfigGroup != value)
                {
                    _selbBasicConfigGroup = value;
                    ViewItems = new ObservableCollection<ItemDTO>();
                    if (value != null)
                    {
                        ViewBasicConfigs.Clear();
                        List<BasicConfigDTO> bcs =
                            BasicConfigs.SourceCollection.Cast<BasicConfigDTO>()
                                .Where(p => p.BasicConfigGroupId == value.Id && p.ParentId == null).ToList();
                        foreach (var bc in bcs)
                        {
                            ViewBasicConfigs.Add(bc);
                        }
                        if (value.AircraftTypeId != Guid.Empty)
                        {
                            var path = CreateItemQueryPath(value.AircraftTypeId.ToString());
                            OnLoadItems(path);
                        }
                    }
                    RaisePropertyChanged(() => SelBasicConfigGroup);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的基本构型

        private BasicConfigDTO _selBasicConfig;

        /// <summary>
        ///     选择的基本构型
        /// </summary>
        public BasicConfigDTO SelBasicConfig
        {
            get { return _selBasicConfig; }
            private set
            {
                if (_selBasicConfig != value)
                {
                    _selBasicConfig = value;
                    RaisePropertyChanged(() => SelBasicConfig);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 查询机型对应的项的集合

        private ObservableCollection<ItemDTO> _viewItems = new ObservableCollection<ItemDTO>();

        /// <summary>
        /// 机型对应的项的集合
        /// </summary>
        public ObservableCollection<ItemDTO> ViewItems
        {
            get { return this._viewItems; }
            private set
            {
                if (!this._viewItems.Equals(value))
                {
                    _viewItems = value;
                    this.RaisePropertyChanged(() => this.ViewItems);
                }
            }
        }

        private ItemDTO _selItem;

        /// <summary>
        /// 选择的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return this._selItem; }
            private set
            {
                if (this._selItem != value)
                {
                    _selItem = value;
                    this.RaisePropertyChanged(() => this.SelItem);
                }
            }
        }

        /// <summary>
        ///     创建查询路径
        /// </summary>
        /// <param name="aircraftTypeId"></param>
        /// <returns></returns>
        private Uri CreateItemQueryPath(string aircraftTypeId)
        {
            return new Uri(string.Format("GetItemsByAircraftType?aircraftTypeId='{0}'", aircraftTypeId),
                UriKind.Relative);
        }

        /// <summary>
        ///     加载的附件项集合
        /// </summary>
        public void OnLoadItems(Uri path)
        {
            //查询
            _context.BeginExecute<ItemDTO>(path,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PartData;
                    try
                    {
                        if (context != null)
                        {
                            ViewItems = new ObservableCollection<ItemDTO>();
                            foreach (var item in context.EndExecute<ItemDTO>(result))
                            {
                                ViewItems.Add(item);
                            }
                            ViewItems.OrderByDescending(p => p.Name);
                            RaisePropertyChanged(() => ViewItems);
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        QueryOperationResponse response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重组成有层次结构的构型

        public void GenerateBasicConfigStructure(BasicConfigDTO basicConfig)
        {
            var temp = BasicConfigs.Where(p => p.ParentId == basicConfig.Id).ToList().OrderBy(p => p.Position);
            basicConfig.SubBasicConfigs.Load(temp);
            foreach (var subItem in basicConfig.SubBasicConfigs)
            {
                GenerateBasicConfigStructure(subItem);
            }
        }
        #endregion

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            AddAttachCommand.RaiseCanExecuteChanged();
            NewCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
            CellEditEndCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建基本构型组

        /// <summary>
        ///     创建基本构型组
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var basicConfigGroup = new BasicConfigGroupDTO()
            {
                Id = RandomHelper.Next(),
            };
            BasicConfigGroups.AddNew(basicConfigGroup);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除基本构型组

        /// <summary>
        ///     删除基本构型组
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selbBasicConfigGroup != null)
            {
                var bcs = BasicConfigs.Where(p => p.BasicConfigGroupId == _selbBasicConfigGroup.Id).ToList();
                foreach (var bc in bcs)
                {
                    BasicConfigs.Remove(bc);
                }
                BasicConfigGroups.Remove(_selbBasicConfigGroup);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selbBasicConfigGroup != null;
        }

        #endregion

        #region 移除基本构型

        /// <summary>
        ///     移除基本构型
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selBasicConfig != null)
            {
                RemoveBasicConifgs(_selBasicConfig);
                ViewBasicConfigs.Clear();
                BasicConfigs.ToList().ForEach(p => p.SubBasicConfigs.Clear());
                BasicConfigs.ToList().ForEach(GenerateBasicConfigStructure);
                List<BasicConfigDTO> bcs =
                    BasicConfigs.Where(p => p.BasicConfigGroupId == SelBasicConfigGroup.Id && p.ParentId == null).ToList();
                foreach (var bc in bcs)
                {
                    ViewBasicConfigs.Add(bc);
                }
                RaisePropertyChanged(()=>ViewBasicConfigs);
            }
        }

        public void RemoveBasicConifgs(BasicConfigDTO basicConfig)
        {
            if (basicConfig != null && basicConfig.SubBasicConfigs.Count != 0)
            {
                basicConfig.SubBasicConfigs.ToList().ForEach(RemoveBasicConifgs);
                basicConfig.SubBasicConfigs.Clear();
                BasicConfigs.Remove(basicConfig);
            }
            else if (basicConfig != null && basicConfig.SubBasicConfigs.Count == 0)
                BasicConfigs.Remove(basicConfig);
        }


        private bool CanRemoveEntity(object obj)
        {
            return _selBasicConfig != null;
        }

        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "AircraftType"))
                {
                    var value = SelBasicConfigGroup.AircraftTypeId;
                    var aircraftType =
                        AircraftTypes.FirstOrDefault(p => p.Id == value);
                    if (aircraftType != null)
                    {
                        var path = CreateItemQueryPath(value.ToString());
                        OnLoadItems(path);
                        SelBasicConfigGroup.AircraftTypeName = aircraftType.Name;
                    }
                }
            }
        }

        #endregion
        #endregion
    }
}