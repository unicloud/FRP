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

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AddMaintainCtrlCommand = new DelegateCommand<object>(OnAddCtrl, CanAddCtrl);
            RemoveMaintainCtrlCommand = new DelegateCommand<object>(OnRemoveCtrl, CanRemoveCtrl);
            AddCtrlLineCommand = new DelegateCommand<object>(OnAddCtrlLine, CanAddCtrlLine);
            RemoveCtrlLineCommand = new DelegateCommand<object>(OnRemoveCtrlLine, CanRemoveCtrlLine);
            PnIsLifeChanged = new DelegateCommand<object>(OnChanged);
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
        ///     维修控制策略
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

            if (!PnRegs.AutoLoad)
                PnRegs.AutoLoad = true;
            PnRegs.Load(true);

            if (!PnMaintainCtrls.AutoLoad)
                PnMaintainCtrls.AutoLoad = true;
            PnMaintainCtrls.Load(true);

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
                    RaisePropertyChanged(() => SelPnReg);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 附件维修控制组

        private ObservableCollection<PnMaintainCtrlDTO> _viewPnMaintainCtrls = new ObservableCollection<PnMaintainCtrlDTO>();
        private PnMaintainCtrlDTO _selPnMaintainCtrl;

        public QueryableDataServiceCollectionView<PnMaintainCtrlDTO> PnMaintainCtrls { get; set; }

        /// <summary>
        ///      选中的PnReg的维修控制组集合
        /// </summary>
        public ObservableCollection<PnMaintainCtrlDTO> ViewPnMaintainCtrls
        {
            get { return _viewPnMaintainCtrls; }
            private set
            {
                if (_viewPnMaintainCtrls != value)
                {
                    _viewPnMaintainCtrls = value;
                    SelPnMaintainCtrl = ViewPnMaintainCtrls.FirstOrDefault();
                    RaisePropertyChanged(() => ViewPnMaintainCtrls);
                }
            }
        }

        /// <summary>
        /// 选中的附件的维修控制组
        /// </summary>
        public PnMaintainCtrlDTO SelPnMaintainCtrl
        {
            get { return _selPnMaintainCtrl; }
            private set
            {
                if (_selPnMaintainCtrl != value)
                {
                    _selPnMaintainCtrl = value;
                    RaisePropertyChanged(() => SelPnMaintainCtrl);
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
            RemoveMaintainCtrlCommand.RaiseCanExecuteChanged();
            AddCtrlLineCommand.RaiseCanExecuteChanged();
            RemoveCtrlLineCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 附件项是否寿控属性变化

        /// <summary>
        ///     附件项是否寿控属性变化
        /// </summary>
        public DelegateCommand<object> PnIsLifeChanged { get; private set; }

        private void OnChanged(object obj)
        {
            var gridView = obj as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "IsLife"))
                {
                    if (SelPnReg != null && SelPnReg.IsLife && ViewPnMaintainCtrls.Count != 0)
                    {
                        RefreshCommandState();
                    }
                    else if (SelPnReg != null && !SelPnReg.IsLife && ViewPnMaintainCtrls.Count != 0)
                    {
                        const string content = "将此附件设置改为非寿控件，将移除已维护的维修控制组，是否继续？";
                        MessageConfirm("确认修改为非寿控件", content, (o, e) =>
                        {
                            if (e.DialogResult == true)
                            {
                                if (SelPnReg != null && ViewPnMaintainCtrls.Count != 0)
                                {
                                    ViewPnMaintainCtrls.ToList().ForEach(p =>
                                    {
                                        ViewPnMaintainCtrls.Remove(p);
                                        PnMaintainCtrls.Remove(p);
                                    });
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
                var newMaintainCtrl = new PnMaintainCtrlDTO
                {
                    Id = RandomHelper.Next(),
                    Pn = SelPnReg.Pn,
                    PnRegId = SelPnReg.Id,
                    CtrlStrategy = 1,
                };
                PnMaintainCtrls.AddNew(newMaintainCtrl);
                RefreshCommandState();
            }
        }

        private bool CanAddCtrl(object obj)
        {
            if (SelPnReg != null && SelPnReg.IsLife)
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
            if (SelPnMaintainCtrl != null)
            {
                ViewPnMaintainCtrls.Remove(SelPnMaintainCtrl);
                PnMaintainCtrls.Remove(SelPnMaintainCtrl);
            }
        }

        private bool CanRemoveCtrl(object obj)
        {
            if (SelPnMaintainCtrl != null)
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
            if (SelPnMaintainCtrl != null)
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
        }

        private bool CanRemoveCtrlLine(object obj)
        {
            return false;
        }

        #endregion
        #endregion
    }
}
