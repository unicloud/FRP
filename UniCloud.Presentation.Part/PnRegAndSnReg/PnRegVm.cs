#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/8 14:52:17
// 文件名：PnRegVm
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
using Microsoft.Practices.Prism;
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

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export(typeof(PnRegVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PnRegVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;

        [ImportingConstructor]
        public PnRegVm(IRegionManager regionManager, IPartService service)
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
            PnRegs = _service.CreateCollection(_context.PnRegs);
            PnRegs.PageSize = 20;
            PnRegs.LoadedData += (o, e) =>
                                 {
                                     if (SelPnReg == null)
                                         SelPnReg = PnRegs.FirstOrDefault();
                                 };
            _service.RegisterCollectionView(PnRegs);

            PnMaintainCtrls = _service.CreateCollection(_context.PnMaintainCtrls);
            _service.RegisterCollectionView(PnMaintainCtrls);

            ItemMaintainCtrls = new QueryableDataServiceCollectionView<ItemMaintainCtrlDTO>(_context, _context.ItemMaintainCtrls);
            ItemMaintainCtrls.LoadedData += (s, e) =>
            {
                if (!PnMaintainCtrls.AutoLoad)
                    PnMaintainCtrls.AutoLoad = true;
                PnMaintainCtrls.Load(true);

                if (!PnRegs.AutoLoad)
                    PnRegs.AutoLoad = true;
                PnRegs.Load(true);
            };
            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);
            Items = new QueryableDataServiceCollectionView<ItemDTO>(_context, _context.Items);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AddMaintainCtrlCommand = new DelegateCommand<object>(OnAddCtrl, CanAddCtrl);
            AddCtrlLineCommand = new DelegateCommand<object>(OnAddCtrlLine, CanAddCtrlLine);
            RemoveCtrlLineCommand = new DelegateCommand<object>(OnRemoveCtrlLine, CanRemoveCtrlLine);
            ItemIsLifeChanged = new DelegateCommand<object>(OnChanged);
        }

        #endregion

        #region 数据

        #region 公共属性
        private ObservableCollection<CtrlUnitDTO> _viewCtrlUnits = new ObservableCollection<CtrlUnitDTO>();
        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        /// <summary>
        /// 选中的件号对应的项号的维修控制组中已维护了的那些控制单位
        /// </summary>
        public ObservableCollection<CtrlUnitDTO> ViewCtrlUnits
        {
            get { return _viewCtrlUnits; }
            set
            {
                if (!_viewCtrlUnits.Equals(value))
                    _viewCtrlUnits = value;
                RaisePropertyChanged(() => ViewCtrlUnits);
            }
        }

        /// <summary>
        ///     维修工作集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainWorkDTO> MaintainWorks { get; set; }

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        /// <summary>
        ///     维修控制策略
        /// </summary>
        public Dictionary<int, ControlStrategy> ControlStrategies
        {
            get { return Enum.GetValues(typeof(ControlStrategy)).Cast<object>().ToDictionary(value => (int)value, value => (ControlStrategy)value); }
        }

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
                {
                    _curItemMaintainCtrl = value;
                    RaisePropertyChanged(() => CurItemMaintainCtrl);
                }
            }
        }
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
            CtrlUnits.Load(true);
            MaintainWorks.Load(true);
            ItemMaintainCtrls.Load(true);
            Items.Load(true);
        }

        #region 业务

        #region 附件

        private PnRegDTO _selPnReg;

        /// <summary>
        ///     附件集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        /// <summary>
        ///     选择的附件
        /// </summary>
        public PnRegDTO SelPnReg
        {
            get { return _selPnReg; }
            private set
            {
                if (_selPnReg != value)
                {
                    _selPnReg = value;
                    ViewCtrlUnits.Clear();
                    CurPnMaintainCtrl = PnMaintainCtrls.SingleOrDefault(p => p.PnRegId == value.Id);
                    if (value != null)
                    {
                        CurItemMaintainCtrl = ItemMaintainCtrls.SourceCollection.Cast<ItemMaintainCtrlDTO>().FirstOrDefault(q => q.ItemId == value.ItemId);
                        if (CurItemMaintainCtrl != null && CurItemMaintainCtrl.MaintainCtrlLines != null && CtrlUnits.SourceCollection.Cast<CtrlUnitDTO>() != null)
                        {
                            ViewCtrlUnits.AddRange(CtrlUnits.SourceCollection.Cast<CtrlUnitDTO>().Where(
                                p => CurItemMaintainCtrl.MaintainCtrlLines.ToList().Any(q => q.CtrlUnitId == p.Id)));
                        }
                    }
                    RefreshCommandState();
                    RaisePropertyChanged(() => SelPnReg);
                }
            }
        }

        #endregion

        #region 附件维修控制组

        private PnMaintainCtrlDTO _curPnMaintainCtrl;

        public QueryableDataServiceCollectionView<PnMaintainCtrlDTO> PnMaintainCtrls { get; set; }

        /// <summary>
        /// 选中的附件的维修控制组
        /// </summary>
        public PnMaintainCtrlDTO CurPnMaintainCtrl
        {
            get { return _curPnMaintainCtrl; }
            private set
            {
                if (_curPnMaintainCtrl != value)
                {
                    _curPnMaintainCtrl = value;
                    RaisePropertyChanged(() => CurPnMaintainCtrl);
                }
            }
        }
        #endregion

        #region 选择的附件维修控制明细

        private MaintainCtrlLineDTO _selCtrlLine;

        /// <summary>
        /// 选择的附件维修控制明细
        /// </summary>
        public MaintainCtrlLineDTO SelCtrlLine
        {
            get { return _selCtrlLine; }
            private set
            {
                if (_selCtrlLine != value)
                {
                    _selCtrlLine = value;
                    RaisePropertyChanged(() => SelCtrlLine);
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
            AddMaintainCtrlCommand.RaiseCanExecuteChanged();
            AddCtrlLineCommand.RaiseCanExecuteChanged();
            RemoveCtrlLineCommand.RaiseCanExecuteChanged();
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
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "IsLife"))
                {
                    if (SelPnReg != null && SelPnReg.IsLife && CurPnMaintainCtrl == null)
                    {
                        RefreshCommandState();
                    }
                    else if (SelPnReg != null && !SelPnReg.IsLife && CurItemMaintainCtrl != null)
                    {
                        const string content = "将此附件设置改为非寿控件，将移除已维护的维修控制组，是否继续？";
                        MessageConfirm("确认修改为非寿控件", content, (o, e) =>
                        {
                            if (e.DialogResult == true)
                            {
                                if (SelPnReg != null && CurPnMaintainCtrl != null)
                                {
                                    PnMaintainCtrls.Remove(CurPnMaintainCtrl);
                                    CurPnMaintainCtrl = null;
                                    RefreshCommandState();
                                }
                            }
                            else
                            {
                                SelPnReg.IsLife = true;
                            }
                        });
                    }
                }
            }
        }

        #endregion

        #region 添加维修控制组

        /// <summary>
        ///     添加维修控制组
        /// </summary>
        public DelegateCommand<object> AddMaintainCtrlCommand { get; private set; }

        private void OnAddCtrl(object obj)
        {
            if (SelPnReg != null)
            {
                CurPnMaintainCtrl = new PnMaintainCtrlDTO
                {
                    Id = RandomHelper.Next(),
                    Pn = SelPnReg.Pn,
                    PnRegId = SelPnReg.Id,
                    CtrlStrategy = 1,
                };
                PnMaintainCtrls.AddNew(CurPnMaintainCtrl);
                RefreshCommandState();
            }
        }

        private bool CanAddCtrl(object obj)
        {
            if (SelPnReg != null && SelPnReg.IsLife && ViewCtrlUnits.Any() && PnMaintainCtrls.SourceCollection.Cast<PnMaintainCtrlDTO>().All(p => p.PnRegId != SelPnReg.Id))
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
            if (CurPnMaintainCtrl != null)
            {
                SelCtrlLine = new MaintainCtrlLineDTO
                {
                    Id = RandomHelper.Next(),
                    MaintainCtrlId = CurPnMaintainCtrl.Id,
                };
                CurPnMaintainCtrl.MaintainCtrlLines.Add(SelCtrlLine);
            }
        }

        private bool CanAddCtrlLine(object obj)
        {
            if (CurPnMaintainCtrl != null)
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
            if (SelCtrlLine == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                CurPnMaintainCtrl.MaintainCtrlLines.Remove(SelCtrlLine);
                                                SelCtrlLine = CurPnMaintainCtrl.MaintainCtrlLines.FirstOrDefault();
                                            });
        }

        private bool CanRemoveCtrlLine(object obj)
        {
            if (SelCtrlLine != null)
                return true;
            else return false;
        }

        #endregion
        #endregion
    }
}
