#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/3 14:52:41
// 文件名：ItemVm
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
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof(ItemVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ItemVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private bool _addToInstallController=false;
        private bool _addToDependency=false;

        [ImportingConstructor]
        public ItemVm(IRegionManager regionManager, IPartService service)
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
            Items = _service.CreateCollection(_context.Items);
            _service.RegisterCollectionView(Items);

            ItemMaintainCtrls = _service.CreateCollection(_context.ItemMaintainCtrls, o => o.MaintainCtrlLines);
            _service.RegisterCollectionView(ItemMaintainCtrls);

            InstallControllers = _service.CreateCollection(_context.InstallControllers, o => o.Dependencies);
            _service.RegisterCollectionView(InstallControllers);

            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);
            AircraftTypes=new QueryableDataServiceCollectionView<AircraftTypeDTO>(_context,_context.AircraftTypes);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            AddDependencyCommand=new DelegateCommand<object>(OnAddDependency,CanAddDependency);
            RemoveDependencyCommand=new DelegateCommand<object>(OnRemoveDependency,CanRemoveDependency);
            AddMaintainCtrlCommand = new DelegateCommand<object>(OnAddCtrl, CanAddCtrl);
            AddCtrlLineCommand = new DelegateCommand<object>(OnAddCtrlLine, CanAddCtrlLine);
            RemoveCtrlLineCommand = new DelegateCommand<object>(OnRemoveCtrlLine, CanRemoveCtrlLine);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            ItemIsLifeChanged = new DelegateCommand<object>(OnChanged);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        /// <summary>
        ///     维修工作集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainWorkDTO> MaintainWorks { get; set; }

        /// <summary>
        ///     附件集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        /// <summary>
        ///     维修发票维修项
        /// </summary>
        public Dictionary<int, ControlStrategy> ControlStrategies
        {
            get { return Enum.GetValues(typeof(ControlStrategy)).Cast<object>().ToDictionary(value => (int)value, value => (ControlStrategy)value); }
        }
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
            CtrlUnits.Load(true);
            MaintainWorks.Load(true);
            PnRegs.Load(true);
            AircraftTypes.Load(true);

            if (!Items.AutoLoad)
                Items.AutoLoad = true;
            Items.Load(true);

            if (!ItemMaintainCtrls.AutoLoad)
                ItemMaintainCtrls.AutoLoad = true;
            ItemMaintainCtrls.Load(true);

            if (!InstallControllers.AutoLoad)
                InstallControllers.AutoLoad = true;
            InstallControllers.Load(true);
        }

        #region 业务

        #region 附件项

        private ItemDTO _selItem;

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        /// <summary>
        ///     选择的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return this._selItem; }
            private set
            {
                if (this._selItem != value)
                {
                    this._selItem = value;
                    ViewInstallControllers=new Collection<InstallControllerDTO>();
                    if (value != null)
                    {
                        ViewInstallControllers = InstallControllers.Where(p => p.ItemId == value.Id);
                        CurItemMaintainCtrl = ItemMaintainCtrls.SingleOrDefault(p => p.ItemId == value.Id);
                    }
                    RaisePropertyChanged(() => this.SelItem);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 项维修控制组

        private ItemMaintainCtrlDTO _curItemMaintainCtrl;

        /// <summary>
        ///     项维修控制组
        /// </summary>
        public QueryableDataServiceCollectionView<ItemMaintainCtrlDTO> ItemMaintainCtrls { get; set; }

        /// <summary>
        /// 选中的Item的维修控制组
        /// </summary>
        public ItemMaintainCtrlDTO CurItemMaintainCtrl
        {
            get { return this._curItemMaintainCtrl; }
            private set
            {
                if (this._curItemMaintainCtrl != value)
                {
                    _curItemMaintainCtrl = value;
                    this.RaisePropertyChanged(() => this.CurItemMaintainCtrl);
                }
            }
        }
        #endregion

        #region 选择的项维修控制明细

        private MaintainCtrlLineDTO _selCtrlLine;

        /// <summary>
        /// 选择的项维修控制明细
        /// </summary>
        public MaintainCtrlLineDTO SelCtrlLine
        {
            get { return this._selCtrlLine; }
            private set
            {
                if (this._selCtrlLine != value)
                {
                    _selCtrlLine = value;
                    this.RaisePropertyChanged(() => this.SelCtrlLine);
                }
            }
        }

        #endregion

        #region 装机控制集合

        private IEnumerable<InstallControllerDTO> _viewInstallControllers=new Collection<InstallControllerDTO>(); 

        /// <summary>
        ///     装机控制集合
        /// </summary>
        public QueryableDataServiceCollectionView<InstallControllerDTO> InstallControllers { get; set; }

        /// <summary>
        /// 选中项对应的装机控制集合
        /// </summary>
        public IEnumerable<InstallControllerDTO> ViewInstallControllers
        {
            get { return this._viewInstallControllers; }
            private set
            {
                if (!this._viewInstallControllers.Equals(value))
                    _viewInstallControllers = value;
                RaisePropertyChanged(()=>ViewInstallControllers);
            }
        }

        #endregion

        #region 选择的装机控制

        private InstallControllerDTO _selInstallController;

        /// <summary>
        /// 选择的装机控制
        /// </summary>
        public InstallControllerDTO SelInstallController
        {
            get { return this._selInstallController; }
            private set
            {
                if (this._selInstallController != value)
                {
                    _selInstallController = value;
                    this.RaisePropertyChanged(() => this.SelInstallController);
                }
                RefreshCommandState();
            }
        }

        #endregion

        #region 选择的依赖项

        private DependencyDTO _selDependency;

        /// <summary>
        /// 选择的依赖项
        /// </summary>
        public DependencyDTO SelDependency
        {
            get { return this._selDependency; }
            private set
            {
                if (this._selDependency != value)
                {
                    _selDependency = value;
                    this.RaisePropertyChanged(() => this.SelDependency);
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
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
            AddMaintainCtrlCommand.RaiseCanExecuteChanged();
            AddCtrlLineCommand.RaiseCanExecuteChanged();
            RemoveCtrlLineCommand.RaiseCanExecuteChanged();
            AddDependencyCommand.RaiseCanExecuteChanged();
            RemoveDependencyCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建附件项

        /// <summary>
        ///     创建附件项
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var item = new ItemDTO
            {
                Id = RandomHelper.Next(),
            };
            Items.AddNew(item);
            SelItem = item;
            //TODO:同时添加一个控制组
            var itemMaintainCtrl = new ItemMaintainCtrlDTO
            {
                Id = RandomHelper.Next(),
                ItemId = SelItem.Id,
                CtrlStrategy = 1,
            };
            ItemMaintainCtrls.AddNew(itemMaintainCtrl);
            CurItemMaintainCtrl = itemMaintainCtrl;
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 附件项是否寿控属性变化

        /// <summary>
        ///     附件项是否寿控属性变化
        /// </summary>
        public DelegateCommand<object> ItemIsLifeChanged { get; private set; }

        private void OnChanged(object obj)
        {
            if (SelItem != null && SelItem.IsLife == false && CurItemMaintainCtrl != null)
            {
                var content = "将此项设置改为非寿控项，将移除已维护的维修控制组，是否继续？";
                MessageConfirm("确认修改为非寿控项", content, (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        if (SelItem != null && CurItemMaintainCtrl != null)
                        {
                            ItemMaintainCtrls.Remove(CurItemMaintainCtrl);
                            CurItemMaintainCtrl = null;
                            RefreshCommandState();
                        }
                    }
                });
            }
        }

        #endregion

        #region 删除附件项

        /// <summary>
        ///     删除附件项
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (SelItem != null)
            {
                PnRegs.Where(p => p.ItemId == SelItem.Id).ToList().ForEach(p => p.ItemId = null);
                Items.Remove(SelItem);
            }
            RefreshCommandState();
        }

        private bool CanRemove(object obj)
        {
            if (SelItem != null)
                return true;
            else return false;
        }

        #endregion

        #region 增加互换件

        /// <summary>
        ///     增加互换件
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            if (SelItem == null)
                MessageAlert("提示", "请先选中附件项！");
            else
            {
                _addToInstallController = true;
                _addToDependency = false;
                PnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanAddEntity(object obj)
        {
            if (SelItem == null)
                return false;
            return true;
        }

        #endregion

        #region 移除互换件

        /// <summary>
        ///     移除互换件
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            InstallControllers.Remove(SelInstallController);
            ViewInstallControllers = InstallControllers.Where(p => p.ItemId == SelItem.Id);
            RefreshCommandState();
        }

        private bool CanRemoveEntity(object obj)
        {
            if (SelItem!=null && SelInstallController != null)
                return true;
            return false;
        }

        #endregion

        #region 增加依赖项

        /// <summary>
        ///     增加依赖项
        /// </summary>
        public DelegateCommand<object> AddDependencyCommand { get; private set; }

        private void OnAddDependency(object obj)
        {
            if (SelInstallController == null)
                MessageAlert("提示", "请先选中装机控制项！");
            else
            {
                _addToDependency = true;
                _addToInstallController = false;
                PnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanAddDependency(object obj)
        {
            if (SelInstallController == null)
                return false;
            return true;
        }

        #endregion

        #region 移除依赖项

        /// <summary>
        ///     移除依赖项
        /// </summary>
        public DelegateCommand<object> RemoveDependencyCommand { get; private set; }

        private void OnRemoveDependency(object obj)
        {
            SelInstallController.Dependencies.Remove(SelDependency);
            RefreshCommandState();
        }

        private bool CanRemoveDependency(object obj)
        {
            if (SelInstallController != null && SelDependency != null)
                return true;
            return false;
        }

        #endregion

        #region 添加维修控制组

        /// <summary>
        ///     添加维修控制组
        /// </summary>
        public DelegateCommand<object> AddMaintainCtrlCommand { get; private set; }

        private void OnAddCtrl(object obj)
        {
            if (SelItem != null)
            {
                CurItemMaintainCtrl = new ItemMaintainCtrlDTO
                {
                    Id = RandomHelper.Next(),
                    ItemId = SelItem.Id,
                    ItemNo = SelItem.ItemNo,
                };
                ItemMaintainCtrls.AddNew(CurItemMaintainCtrl);
            }
        }

        private bool CanAddCtrl(object obj)
        {
            if (SelItem == null)
                return false;
            if (!SelItem.IsLife)
                return false;
            if (ItemMaintainCtrls.Any(p => p.ItemId == SelItem.Id))
                return false;
            return true;
        }

        #endregion

        #region 增加维修控制明细

        /// <summary>
        ///     增加维修控制明细
        /// </summary>
        public DelegateCommand<object> AddCtrlLineCommand { get; private set; }

        private void OnAddCtrlLine(object obj)
        {
            if (CurItemMaintainCtrl != null)
            {
                var maintainCtrlLine = new MaintainCtrlLineDTO
                {
                    Id = RandomHelper.Next(),
                    MaintainCtrlId = CurItemMaintainCtrl.Id,
                };
                CurItemMaintainCtrl.MaintainCtrlLines.Add(maintainCtrlLine);
            }
        }

        private bool CanAddCtrlLine(object obj)
        {
            if (CurItemMaintainCtrl != null)
                return true;
            return false;
        }

        #endregion

        #region 移除维修控制明细

        /// <summary>
        ///     移除维修控制明细
        /// </summary>
        public DelegateCommand<object> RemoveCtrlLineCommand { get; private set; }

        private void OnRemoveCtrlLine(object obj)
        {
            if (SelCtrlLine != null)
                CurItemMaintainCtrl.MaintainCtrlLines.Remove(SelCtrlLine);
        }

        private bool CanRemoveCtrlLine(object obj)
        {
            if (SelCtrlLine != null)
                return true;
            else return false;
        }

        #endregion
        #endregion

        #region 子窗体相关

        [Import]
        public PnRegsChildView PnRegsChildView; //初始化子窗体


        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            _addToInstallController = false;
            _addToDependency = false;
            PnRegsChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            var radGridView = sender as RadGridView;
            if (radGridView == null) return;
            if (SelItem != null && _addToInstallController)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var pnRegDTO = p as PnRegDTO;
                    if (pnRegDTO != null)
                    {
                        var installController = new InstallControllerDTO
                        {
                            Id = RandomHelper.Next(),
                            Pn = pnRegDTO.Pn,
                            PnRegId = pnRegDTO.Id,
                            ItemId = SelItem.Id,
                            ItemNo = SelItem.ItemNo,
                            Description = pnRegDTO.Description,
                            StartDate = DateTime.Now,
                        };
                        InstallControllers.AddNew(installController);
                    }
                });
                ViewInstallControllers = InstallControllers.Where(p => p.ItemId == SelItem.Id);
                RaisePropertyChanged(() => ViewInstallControllers);
                _addToInstallController = false;
            }
            else if (_selInstallController != null && _addToDependency)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var pnRegDTO = p as PnRegDTO;
                    if (pnRegDTO != null)
                    {
                        var dependency = new DependencyDTO()
                        {
                            Id = RandomHelper.Next(),
                            Pn = pnRegDTO.Pn,
                            DependencyPnId = pnRegDTO.Id,
                            InstallControllerId = SelInstallController.Id,
                        };
                        SelInstallController.Dependencies.Add(dependency);
                    }
                });
                _addToDependency = false;
            }
            PnRegsChildView.Close();
        }

        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion
        #endregion
        #endregion
    }
}