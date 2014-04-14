#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/11 23:21:49
// 文件名：AircraftConfigVm
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
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export(typeof(AircraftConfigVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftConfigVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private FilterDescriptor _bcGroupDescriptor;

        [ImportingConstructor]
        public AircraftConfigVm(IRegionManager regionManager, IPartService service)
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
            ContractAircrafts = new QueryableDataServiceCollectionView<ContractAircraftDTO>(_context, _context.ContractAircrafts);
            var sort = new SortDescriptor { Member = "CSCNumber", SortDirection = ListSortDirection.Ascending };
            ContractAircrafts.SortDescriptors.Add(sort);

            BasicConfigGroups = new QueryableDataServiceCollectionView<BasicConfigGroupDTO>(_context, _context.BasicConfigGroups);
            _bcGroupDescriptor = new FilterDescriptor("AircraftTypeId", FilterOperator.IsEqualTo, Guid.Empty);
            BasicConfigGroups.FilterDescriptors.Add(_bcGroupDescriptor);

            BasicConfigHistories = _service.CreateCollection(_context.BasicConfigHistories);
            _service.RegisterCollectionView(BasicConfigHistories);

            SpecialConfigs = _service.CreateCollection(_context.SpecialConfigs);
            SpecialConfigs.LoadedData += (o, e) =>
            {
                SpecialConfigs.ToList().ForEach(p=>p.SubSpecialConfigs.Clear());
                SpecialConfigs.ToList().ForEach(GenerateSpecialConfigStructure);
                if (SelContractAircraft != null)
                {
                    ViewSpecialConfigs.Clear();
                    List<SpecialConfigDTO> bcs =
                        SpecialConfigs.SourceCollection.Cast<SpecialConfigDTO>()
                            .Where(p => p.ContractAircraftId == SelContractAircraft.Id && p.ParentId == null).ToList();
                    foreach (SpecialConfigDTO bc in bcs)
                    {
                        ViewSpecialConfigs.Add(bc);
                    }
                }
            };
            _service.RegisterCollectionView(SpecialConfigs); //注册查询集合
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

        #region 合同飞机集合

        private ContractAircraftDTO _selContractAircraft;

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        /// <summary>
        /// 选择的合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return this._selContractAircraft; }
            private set
            {
                if (_selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    ViewSpecialConfigs = new ObservableCollection<SpecialConfigDTO>();
                    ViewBasicConfigHistories = new ObservableCollection<BasicConfigHistoryDTO>();
                    if (value != null && value.AircraftTypeId != Guid.Empty)
                    {
                        //加载合同飞机所属机型的基本构型组集合
                        _bcGroupDescriptor.Value = value.AircraftTypeId;
                        BasicConfigGroups.Load(true);

                        //加载机型下可用的附件项
                        var path = CreateItemQueryPath(value.AircraftTypeId.ToString());
                        OnLoadItems(path);

                        //加载基本构型历史
                        var bchs = BasicConfigHistories.Where(p => p.ContractAircraftId == value.Id).ToList();
                        if (bchs.Count() != 0)
                        {
                            bchs.ToList().ForEach(p => ViewBasicConfigHistories.Add(p));
                        }

                        //加载特定选型集合
                        List<SpecialConfigDTO> bcs =
                            SpecialConfigs.SourceCollection.Cast<SpecialConfigDTO>()
                                .Where(p => p.ContractAircraftId == SelContractAircraft.Id && p.ParentId == null).ToList();
                        foreach (SpecialConfigDTO bc in bcs)
                        {
                            ViewSpecialConfigs.Add(bc);
                        }
                    }
                    RaisePropertyChanged(() => SelContractAircraft);
                }
                RefreshCommandState();
            }
        }

        #endregion

        #region 基本构型组集合

        /// <summary>
        ///     基本构型组集合
        /// </summary>
        public QueryableDataServiceCollectionView<BasicConfigGroupDTO> BasicConfigGroups { get; set; }

        #endregion

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
            ContractAircrafts.Load(true);

            if (!BasicConfigHistories.AutoLoad)
                BasicConfigHistories.AutoLoad = true;
            else
                BasicConfigHistories.Load(true);

            if (!SpecialConfigs.AutoLoad)
                SpecialConfigs.AutoLoad = true;
            else
                SpecialConfigs.Load(true);
        }

        #region 业务

        #region 基本构型历史集合

        private BasicConfigHistoryDTO _selBasicConfigHistory;
        private ObservableCollection<BasicConfigHistoryDTO> _viewBasicConfigHistories = new ObservableCollection<BasicConfigHistoryDTO>();

        /// <summary>
        ///     基本构型历史集合
        /// </summary>
        public QueryableDataServiceCollectionView<BasicConfigHistoryDTO> BasicConfigHistories { get; set; }


        /// <summary>
        ///     选中合同飞机对应的基本构型历史集合
        /// </summary>
        public ObservableCollection<BasicConfigHistoryDTO> ViewBasicConfigHistories
        {
            get { return _viewBasicConfigHistories; }
            private set
            {
                if (_viewBasicConfigHistories != value)
                {
                    _viewBasicConfigHistories = value;
                    RaisePropertyChanged(() => ViewBasicConfigHistories);
                }
            }
        }

        /// <summary>
        /// 选择的基本构型历史
        /// </summary>
        public BasicConfigHistoryDTO SelBasicConfigHistory
        {
            get { return this._selBasicConfigHistory; }
            private set
            {
                if (_selBasicConfigHistory != value)
                {
                    _selBasicConfigHistory = value;
                    RaisePropertyChanged(() => SelBasicConfigHistory);
                }
            }
        }
        #endregion

        #region 特定选型集合

        private ObservableCollection<SpecialConfigDTO> _viewSpecialConfigs = new ObservableCollection<SpecialConfigDTO>();

        /// <summary>
        ///     特定选型集合
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialConfigDTO> SpecialConfigs { get; set; }

        /// <summary>
        ///     特定选型集合
        /// </summary>
        public ObservableCollection<SpecialConfigDTO> ViewSpecialConfigs
        {
            get { return _viewSpecialConfigs; }
            private set
            {
                if (_viewSpecialConfigs != value)
                {
                    _viewSpecialConfigs = value;
                    RaisePropertyChanged(() => ViewSpecialConfigs);
                }
            }
        }

        #endregion

        #region 选择的特定选型

        private SpecialConfigDTO _selSpecialConfig;

        /// <summary>
        ///     选择的特定选型
        /// </summary>
        public SpecialConfigDTO SelSpecialConfig
        {
            get { return _selSpecialConfig; }
            private set
            {
                if (_selSpecialConfig != value)
                {
                    _selSpecialConfig = value;
                    RaisePropertyChanged(() => SelSpecialConfig);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 查询机型对应的项的集合

        private ItemDTO _selItem;

        private ObservableCollection<ItemDTO> _viewItems = new ObservableCollection<ItemDTO>();

        /// <summary>
        ///     机型对应的项的集合
        /// </summary>
        public ObservableCollection<ItemDTO> ViewItems
        {
            get { return _viewItems; }
            private set
            {
                if (!_viewItems.Equals(value))
                {
                    _viewItems = value;
                    RaisePropertyChanged(() => ViewItems);
                }
            }
        }

        /// <summary>
        ///     选择的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return _selItem; }
            private set
            {
                if (_selItem != value)
                {
                    _selItem = value;
                    RaisePropertyChanged(() => SelItem);
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
                            foreach (ItemDTO item in context.EndExecute<ItemDTO>(result))
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

        public void GenerateSpecialConfigStructure(SpecialConfigDTO specialConfig)
        {
            IOrderedEnumerable<SpecialConfigDTO> temp =
                SpecialConfigs.Where(p => p.ParentId == specialConfig.Id).ToList().OrderBy(p => p.Position);
            specialConfig.SubSpecialConfigs.Load(temp);
            foreach (SpecialConfigDTO subItem in specialConfig.SubSpecialConfigs)
            {
                GenerateSpecialConfigStructure(subItem);
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

        #region 创建基本构型历史

        private DateTime? _startDisplayDate;
        /// <summary>
        /// 创建基本构型历史开始时间 
        /// </summary>
        public DateTime? StartDisplayDate
        {
            get { return _startDisplayDate; }
            set
            {
                if (_startDisplayDate != value)
                {
                    _startDisplayDate = value;
                    RaisePropertyChanged(() => StartDisplayDate);
                }
            }
        }

        /// <summary>
        ///     创建基本构型历史
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            StartDisplayDate =
            ViewBasicConfigHistories.Select(p => p.StartDate).OrderBy(p => p).LastOrDefault();

            var newBasicConfigHistory = new BasicConfigHistoryDTO
            {
                Id = RandomHelper.Next(),
                ContractAircraftId = SelContractAircraft.Id,
                StartDate = (StartDisplayDate == null || StartDisplayDate.Value == DateTime.MinValue) ? DateTime.Now : StartDisplayDate.Value.AddDays(1),
            };
            ViewBasicConfigHistories.Add(newBasicConfigHistory);
            BasicConfigHistories.AddNew(newBasicConfigHistory);
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return _selContractAircraft != null;
        }

        #endregion

        #region 删除基本构型历史

        /// <summary>
        ///     删除基本构型历史
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selBasicConfigHistory != null)
            {
                ViewBasicConfigHistories.Remove(_selBasicConfigHistory);
                BasicConfigHistories.Remove(_selBasicConfigHistory);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selBasicConfigHistory != null && _selBasicConfigHistory.EndDate == null;
        }

        #endregion

        #region 移除特定选型

        /// <summary>
        ///     移除特定选型
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selSpecialConfig != null)
            {
                RemoveBasicConifgs(_selSpecialConfig);
                ViewSpecialConfigs.Clear();
                SpecialConfigs.ToList().ForEach(p => p.SubSpecialConfigs.Clear());
                SpecialConfigs.ToList().ForEach(GenerateSpecialConfigStructure);
                List<SpecialConfigDTO> bcs =
                    SpecialConfigs.Where(p => p.ContractAircraftId == SelContractAircraft.Id && p.ParentId == null).ToList();
                foreach (var bc in bcs)
                {
                    ViewSpecialConfigs.Add(bc);
                }
                RaisePropertyChanged(() => ViewSpecialConfigs);
            }
        }

        public void RemoveBasicConifgs(SpecialConfigDTO basicConfig)
        {
            if (basicConfig != null && basicConfig.SubSpecialConfigs.Count != 0)
            {
                basicConfig.SubSpecialConfigs.ToList().ForEach(RemoveBasicConifgs);
                basicConfig.SubSpecialConfigs.Clear();
                SpecialConfigs.Remove(basicConfig);
            }
            else if (basicConfig != null && basicConfig.SubSpecialConfigs.Count == 0)
                SpecialConfigs.Remove(basicConfig);
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selSpecialConfig != null;
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
                GridViewCell cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "BasicConfigGroup"))
                {
                    int value = SelBasicConfigHistory.BasicConfigGroupId;
                    BasicConfigGroupDTO basicConfigGroup =
                        BasicConfigGroups.FirstOrDefault(p => p.Id == value);
                    if (basicConfigGroup != null)
                    {
                        Uri path = CreateItemQueryPath(basicConfigGroup.AircraftTypeId.ToString());
                        OnLoadItems(path);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}