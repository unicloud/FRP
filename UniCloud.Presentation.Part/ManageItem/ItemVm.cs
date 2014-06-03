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
using Telerik.Windows.Controls.GridView;
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
        private FilterDescriptor _pnRegFilter;

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
            Items.PageSize = 20;
            Items.LoadedData += (o, e) =>
                                {
                                    if (SelItem == null)
                                        SelItem = Items.FirstOrDefault();
                                };
            _service.RegisterCollectionView(Items);

            ItemMaintainCtrls = _service.CreateCollection(_context.ItemMaintainCtrls);
            _service.RegisterCollectionView(ItemMaintainCtrls);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);

            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);
            _pnRegFilter = new FilterDescriptor("ItemId", FilterOperator.IsEqualTo, -1);
            PnRegs.FilterDescriptors.Add(_pnRegFilter);
            PnRegs.PageSize = 5;//只是用于判断是否能删除附件项，因此不用把项下所有的附件都取出。
            PnRegs.LoadedData += (s, e) => RefreshCommandState();
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddMaintainCtrlCommand = new DelegateCommand<object>(OnAddCtrl, CanAddCtrl);
            RemoveMaintainCtrlCommand=new DelegateCommand<object>(OnRemoveCtrl,CanRemoveCtrl);
            AddCtrlLineCommand = new DelegateCommand<object>(OnAddCtrlLine, CanAddCtrlLine);
            RemoveCtrlLineCommand = new DelegateCommand<object>(OnRemoveCtrlLine, CanRemoveCtrlLine);
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
        ///     维修控制策略
        /// </summary>
        public Dictionary<int, ControlStrategy> ControlStrategies
        {
            get
            {
                return Enum.GetValues(typeof(ControlStrategy))
                    .Cast<object>()
                    .ToDictionary(value => (int)value, value => (ControlStrategy)value);
            }
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

            if (!Items.AutoLoad)
                Items.AutoLoad = true;
            Items.Load(true);

            if (!ItemMaintainCtrls.AutoLoad)
                ItemMaintainCtrls.AutoLoad = true;
            ItemMaintainCtrls.Load(true);
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
            get { return _selItem; }
            private set
            {
                if (_selItem != value)
                {
                    _selItem = value;
                    ViewItemMaintainCtrls = new ObservableCollection<ItemMaintainCtrlDTO>();
                    if (value != null)
                    {
                        _pnRegFilter.Value = value.Id;
                        PnRegs.Load(true);
                        foreach (var maintainCtrl in ItemMaintainCtrls.SourceCollection.Cast<ItemMaintainCtrlDTO>())
                        {
                            if (maintainCtrl.ItemId == value.Id)
                                ViewItemMaintainCtrls.Add(maintainCtrl);
                        }
                    }
                    RaisePropertyChanged(() => SelItem);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 项维修控制组

        private ObservableCollection<ItemMaintainCtrlDTO> _viewItemMaintainCtrls = new ObservableCollection<ItemMaintainCtrlDTO>();
        private ItemMaintainCtrlDTO _selItemMaintaiinCtrl;

        /// <summary>
        ///     项维修控制组
        /// </summary>
        public QueryableDataServiceCollectionView<ItemMaintainCtrlDTO> ItemMaintainCtrls { get; set; }

        /// <summary>
        ///     选中的Item的维修控制组集合
        /// </summary>
        public ObservableCollection<ItemMaintainCtrlDTO> ViewItemMaintainCtrls
        {
            get { return _viewItemMaintainCtrls; }
            private set
            {
                if (_viewItemMaintainCtrls != value)
                {
                    _viewItemMaintainCtrls = value;
                    SelItemMaintainCtrl = ViewItemMaintainCtrls.FirstOrDefault();
                    RaisePropertyChanged(() => ViewItemMaintainCtrls);
                }
            }
        }

        /// <summary>
        ///     选中的Item的维修控制组集合
        /// </summary>
        public ItemMaintainCtrlDTO SelItemMaintainCtrl
        {
            get { return _selItemMaintaiinCtrl; }
            private set
            {
                if (_selItemMaintaiinCtrl != value)
                {
                    _selItemMaintaiinCtrl = value;
                    RaisePropertyChanged(() => SelItemMaintainCtrl);
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
            AddMaintainCtrlCommand.RaiseCanExecuteChanged();
            RemoveMaintainCtrlCommand.RaiseCanExecuteChanged();
            AddCtrlLineCommand.RaiseCanExecuteChanged();
            RemoveCtrlLineCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建附件项

        /// <summary>
        ///     创建附件项
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            SelItem = new ItemDTO
            {
                Id = RandomHelper.Next(),
            };
            Items.AddNew(SelItem);
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
            var gridView = obj as RadGridView;
            if (gridView != null)
            {
                GridViewCell cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "IsLife"))
                {
                    if (SelItem != null && SelItem.IsLife && ViewItemMaintainCtrls.Count != 0)
                    {
                        RefreshCommandState();
                    }
                    else if (SelItem != null && !SelItem.IsLife && ViewItemMaintainCtrls.Count != 0)
                    {
                        const string content = "将此项设置改为非寿控项，将移除已维护的维修控制组，是否继续？";
                        MessageConfirm("确认修改为非寿控项", content, (o, e) =>
                        {
                            if (e.DialogResult == true)
                            {
                                if (SelItem != null && ViewItemMaintainCtrls.Count != 0)
                                {
                                    ViewItemMaintainCtrls.ToList().ForEach(p =>
                                    {
                                        ViewItemMaintainCtrls.Remove(p);
                                        ItemMaintainCtrls.Remove(p);
                                    });
                                    RefreshCommandState();
                                }
                            }
                            else
                            {
                                SelItem.IsLife = true;
                            }
                        });
                    }
                    RefreshCommandState();
                }
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
            if (SelItem == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                Items.Remove(SelItem);
                                                SelItem = Items.FirstOrDefault();
                                                RefreshCommandState();
                                            });
        }

        private bool CanRemove(object obj)
        {
            if (SelItem == null)
                return false;
            if (!PnRegs.SourceCollection.Cast<PnRegDTO>().Any())
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
                var newMaintainCtrl = new ItemMaintainCtrlDTO
                {
                    Id = RandomHelper.Next(),
                    ItemId = SelItem.Id,
                    ItemNo = SelItem.ItemNo,
                };
                ViewItemMaintainCtrls.Add(newMaintainCtrl);
                ItemMaintainCtrls.AddNew(newMaintainCtrl);
            }
        }

        private bool CanAddCtrl(object obj)
        {
            if (SelItem != null && SelItem.IsLife)
                return true;
            return false;
        }

        #endregion

        #region 移除维修控制组

        /// <summary>
        ///     移除维修控制组
        /// </summary>
        public DelegateCommand<object> RemoveMaintainCtrlCommand { get; private set; }

        private void OnRemoveCtrl(object obj)
        {
            if (SelItemMaintainCtrl != null)
            {
                ViewItemMaintainCtrls.Remove(SelItemMaintainCtrl);
                ItemMaintainCtrls.Remove(SelItemMaintainCtrl);
            }
        }

        private bool CanRemoveCtrl(object obj)
        {
            if (SelItemMaintainCtrl != null)
                return true;
            return false;
        }

        #endregion

        #region 增加维修控制明细

        /// <summary>
        ///     增加维修控制明细
        /// </summary>
        public DelegateCommand<object> AddCtrlLineCommand { get; private set; }

        private void OnAddCtrlLine(object obj)
        {

        }

        private bool CanAddCtrlLine(object obj)
        {
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
        }

        private bool CanRemoveCtrlLine(object obj)
        {
            return false;
        }

        #endregion

        #endregion
    }
}