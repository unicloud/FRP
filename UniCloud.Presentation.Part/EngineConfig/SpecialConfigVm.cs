#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 17:47:31
// 文件名：SpecialConfigVm
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
using System.Linq;
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
    [Export(typeof(SpecialConfigVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SpecialConfigVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private PartData _context;

        [ImportingConstructor]
        public SpecialConfigVm(IRegionManager regionManager, IPartService service)
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
            SpecialConfigs = _service.CreateCollection(_context.SpecialConfigs);
            _service.RegisterCollectionView(SpecialConfigs);//注册查询集合

            BasicConfigGroups = new QueryableDataServiceCollectionView<BasicConfigGroupDTO>(_context, _context.BasicConfigGroups);

            TechnicalSolutions = new QueryableDataServiceCollectionView<TechnicalSolutionDTO>(_context, _context.TechnicalSolutions);

            ContractAircrafts = new QueryableDataServiceCollectionView<ContractAircraftDTO>(_context, _context.ContractAircrafts);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
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
            if (!SpecialConfigs.AutoLoad)
                SpecialConfigs.AutoLoad = true;
            else
                SpecialConfigs.Load(true);

            BasicConfigGroups.AutoLoad = true;

            TechnicalSolutions.AutoLoad = true;

            ContractAircrafts.AutoLoad = true;
        }

        #region 业务

        #region 基本构型组集合

        /// <summary>
        ///     基本构型组集合
        /// </summary>
        public QueryableDataServiceCollectionView<BasicConfigGroupDTO> BasicConfigGroups { get; set; }

        #endregion

        #region 技术解决方案集合

        /// <summary>
        ///     技术解决方案集合
        /// </summary>
        public QueryableDataServiceCollectionView<TechnicalSolutionDTO> TechnicalSolutions { get; set; }

        #endregion

        #region 合同飞机的集合

        /// <summary>
        ///     合同飞机的集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        #endregion

        #region 特定选型集合

        /// <summary>
        ///     特定选型集合
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialConfigDTO> SpecialConfigs { get; set; }


        private ObservableCollection<SpecialConfigDTO> _viewSpecialConfigs = new ObservableCollection<SpecialConfigDTO>();

        /// <summary>
        /// 合同飞机关联的特定选型集合
        /// </summary>
        public ObservableCollection<SpecialConfigDTO> ViewSpecialConfigs
        {
            get { return this._viewSpecialConfigs; }
            private set
            {
                if (this._viewSpecialConfigs != value)
                {
                    _viewSpecialConfigs = value;
                    this.RaisePropertyChanged(() => this.ViewSpecialConfigs);
                }
            }
        }
        #endregion

        #region 选择的合同飞机

        private ContractAircraftDTO _selContractAircraft;

        /// <summary>
        ///     选择的合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return _selContractAircraft; }
            private set
            {
                if (_selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    ViewSpecialConfigs.Clear();
                    foreach (var specialConfig in SpecialConfigs.SourceCollection.Cast<SpecialConfigDTO>().ToList())
                    {
                        if (specialConfig.ContractAircraftId == value.Id)
                            ViewSpecialConfigs.Add(specialConfig);
                    }
                    RaisePropertyChanged(() => SelContractAircraft);
                    RaisePropertyChanged(() => ViewSpecialConfigs);

                    // 刷新按钮状态
                    RefreshCommandState();
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

        #endregion

        #endregion

        #endregion

        #region 操作
        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 添加特定选型

        /// <summary>
        ///     添加特定选型
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var specialConfig = new SpecialConfigDTO()
            {
                Id = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                ContractAircraftId = SelContractAircraft.Id,
                StartDate = DateTime.Now,
                IsValid = true,
            };
            ViewSpecialConfigs.Add(specialConfig);
            SpecialConfigs.AddNew(specialConfig);
        }

        private bool CanNew(object obj)
        {
            return _selContractAircraft != null;
        }

        #endregion

        #region 删除特定选型

        /// <summary>
        ///     删除特定选型
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selSpecialConfig != null)
            {
                SpecialConfigs.Remove(_selSpecialConfig);
                ViewSpecialConfigs.Remove(_selSpecialConfig);
            }
        }

        private bool CanRemove(object obj)
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
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "TsNumber"))
                {
                    var value = SelSpecialConfig.TsId;
                    var ts =
                        TechnicalSolutions.FirstOrDefault(p => p.Id == value);
                    if (ts != null)
                    {
                        SelSpecialConfig.TsId = ts.Id;
                        SelSpecialConfig.FiNumber = ts.FiNumber;
                        SelSpecialConfig.TsNumber = ts.TsNumber;
                    }
                }
            }
        }

        #endregion
        #endregion
    }
}