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

            PnRegs = _service.CreateCollection(_context.PnRegs);
            _service.RegisterCollectionView(PnRegs);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);
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

            if (!Items.AutoLoad)
                Items.AutoLoad = true;
            Items.Load(true);

            if (!ItemMaintainCtrls.AutoLoad)
                ItemMaintainCtrls.AutoLoad = true;
            ItemMaintainCtrls.Load(true);

            if (!PnRegs.AutoLoad)
                PnRegs.AutoLoad = true;
            PnRegs.Load(true);
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
                    if (value != null)
                    {
                        ViewPnRegs = PnRegs.SourceCollection.Cast<PnRegDTO>().Where(p => p.ItemId == value.Id);
                        EditViewPnRegs =
                            PnRegs.SourceCollection.Cast<PnRegDTO>()
                                .Where(p => p.IsLife == value.IsLife && p.ItemId == null);
                        CurItemMaintainCtrl = ItemMaintainCtrls.SingleOrDefault(p => p.ItemId == value.Id);
                    }
                    RaisePropertyChanged(() => SelItem);
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
            get { return _curItemMaintainCtrl; }
            private set
            {
                if (_curItemMaintainCtrl != value)
                    _curItemMaintainCtrl = value;
                RaisePropertyChanged(() => CurItemMaintainCtrl);
            }
        }

        #endregion

        #region 附件集合

        private IEnumerable<PnRegDTO> _viewPnRegs;
        private IEnumerable<PnRegDTO> _editViewPnRegs;
        private PnRegDTO _selPnReg;

        /// <summary>
        ///     附件集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        /// <summary>
        /// 选中的附件项下可互换的件的集合
        /// </summary>
        public IEnumerable<PnRegDTO> ViewPnRegs
        {
            get { return _viewPnRegs; }
            private set
            {
                if (_viewPnRegs != value)
                    _viewPnRegs = value;
                RaisePropertyChanged(() => ViewPnRegs);
            }
        }

        /// <summary>
        /// 界面中选中的附件
        /// </summary>
        public PnRegDTO SelPnReg
        {
            get { return _selPnReg; }
            private set
            {
                if (_selPnReg != value)
                    _selPnReg = value;
                RaisePropertyChanged(() => SelPnReg);
                RefreshCommandState();
            }
        }

        /// <summary>
        /// 子窗体中用于选择的件号
        /// </summary>
        public IEnumerable<PnRegDTO> EditViewPnRegs
        {
            get { return _editViewPnRegs; }
            private set
            {
                if (_editViewPnRegs != value)
                    _editViewPnRegs = value;
                RaisePropertyChanged(() => EditViewPnRegs);
            }
        }

        #endregion

        #region 选择的维修控制明细

        private MaintainCtrlLineDTO _selCtrlLine;

        /// <summary>
        /// 选择的维修控制明细
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
            if (SelItem != null && SelItem.IsLife == false && CurItemMaintainCtrl!=null)
            {
                var content = "将此项设置改为非寿控项，将移除已维护的维修控制组，是否继续？";
                MessageConfirm("确认修改为非寿控项", content, (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        if (SelItem!=null && CurItemMaintainCtrl!=null)
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
            else PnRegsChildView.ShowDialog();
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
            if (SelPnReg != null)
                PnRegs.First(p => p == SelPnReg).ItemId = null;
            ViewPnRegs = PnRegs.Where(p => p.ItemId == SelItem.Id);
            EditViewPnRegs = PnRegs.Where(p => p.ItemId == null && p.IsLife == SelItem.IsLife);
            RaisePropertyChanged(() => ViewPnRegs);
        }

        private bool CanRemoveEntity(object obj)
        {
            if (SelPnReg != null)
                return true;
            else return false;
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
            if (SelItem != null)
            {
                radGridView.SelectedItems.ToList().ForEach(p => (p as PnRegDTO).ItemId = SelItem.Id);
                ViewPnRegs = PnRegs.Where(p => p.ItemId == SelItem.Id);
                EditViewPnRegs = PnRegs.Where(p => p.ItemId == null && p.IsLife == SelItem.IsLife);
                RaisePropertyChanged(() => ViewPnRegs);
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