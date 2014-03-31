#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 17:44:18
// 文件名：ManagePnAndSnVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export(typeof(ManagePnAndSnVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagePnAndSnVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly CompositeFilterDescriptor _cfd = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.Or };
        private FilterDescriptor _pnFilterDescriptor;
        private FilterDescriptor _snFilterDescriptor;
        private PartData _context;

        [ImportingConstructor]
        public ManagePnAndSnVm(IRegionManager regionManager, IPartService service)
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
            SnRegs = _service.CreateCollection(_context.SnRegs, o => o.SnHistories, o => o.LiftMonitors);
            _service.RegisterCollectionView(SnRegs);//注册查询集合

            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AddShCommand = new DelegateCommand<object>(OnAddSh, CanAddSh);
            RemoveShCommand = new DelegateCommand<object>(OnRemoveSh, CanRemoveSh);
            AddLmCommand = new DelegateCommand<object>(OnAddLm, CanAddLm);
            RemoveLmCommand = new DelegateCommand<object>(OnRemoveLm, CanRemoveLm);

        }

        #endregion

        #region 数据

        #region 公共属性

        #region A~G选中

        private bool _isAgChecked;

        /// <summary>
        /// A~G选中
        /// </summary>
        public bool IsAgChecked
        {
            get { return this._isAgChecked; }
            private set
            {
                if (this._isAgChecked != value)
                {
                    _isAgChecked = value;
                    if (_isAgChecked)
                    {
                        _cfd.FilterDescriptors.Clear();
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "A", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "B", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "C", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "D", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "E", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "F", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "G", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        PnRegs.FilterDescriptors.Add(_cfd);
                        PnRegs.Load(true);
                    }
                    this.RaisePropertyChanged(() => this.IsAgChecked);
                }
            }
        }

        #endregion

        #region H~N选中

        private bool _isHnChecked;

        /// <summary>
        /// H~N选中
        /// </summary>
        public bool IsHnChecked
        {
            get { return this._isHnChecked; }
            private set
            {
                if (this._isHnChecked != value)
                {
                    _isHnChecked = value;
                    if (_isHnChecked)
                    {
                        _cfd.FilterDescriptors.Clear();
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "H", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "I", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "J", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "K", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "L", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "M", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "N", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        PnRegs.FilterDescriptors.Add(_cfd);
                        PnRegs.Load(true);
                    }
                    this.RaisePropertyChanged(() => this.IsHnChecked);
                }
            }
        }

        #endregion

        #region O~T选中

        private bool _isOtChecked;

        /// <summary>
        ///O~T选中
        /// </summary>
        public bool IsOtChecked
        {
            get { return this._isOtChecked; }
            private set
            {
                if (this._isOtChecked != value)
                {
                    _isOtChecked = value;
                    _isHnChecked = value;
                    if (_isHnChecked)
                    {
                        _cfd.FilterDescriptors.Clear();
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "O", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "P", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "Q", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "R", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "S", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "T", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        PnRegs.FilterDescriptors.Add(_cfd);
                        PnRegs.Load(true);
                    }
                    this.RaisePropertyChanged(() => this.IsOtChecked);
                }
            }
        }

        #endregion

        #region U~Z选中

        private bool _isUzChecked;

        /// <summary>
        /// U~Z选中
        /// </summary>
        public bool IsUzChecked
        {
            get { return this._isUzChecked; }
            private set
            {
                if (this._isUzChecked != value)
                {
                    _isUzChecked = value;
                    if (_isUzChecked)
                    {
                        _cfd.FilterDescriptors.Clear();
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "U", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "V", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "W", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "X", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "Y", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "Z", false);
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        PnRegs.FilterDescriptors.Add(_cfd);
                        PnRegs.Load(true);
                    }
                    this.RaisePropertyChanged(() => this.IsUzChecked);
                }
            }
        }

        #endregion

        #region 0~9选中

        private bool _isNumChecked;

        /// <summary>
        /// 0~9选中
        /// </summary>
        public bool IsNumChecked
        {
            get { return this._isNumChecked; }
            private set
            {
                if (this._isNumChecked != value)
                {
                    _isNumChecked = value;
                    if (_isNumChecked)
                    {
                        _cfd.FilterDescriptors.Clear();
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "0");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "1");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "2");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "3");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "4");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "5");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "6");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "7");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "8");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        _pnFilterDescriptor = new FilterDescriptor("Pn", FilterOperator.StartsWith, "9");
                        _cfd.FilterDescriptors.Add(_pnFilterDescriptor);
                        PnRegs.FilterDescriptors.Add(_cfd);
                        PnRegs.Load(true);
                    }
                    this.RaisePropertyChanged(() => this.IsNumChecked);
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
            CtrlUnits.AutoLoad = true;
        }

        #region 业务

        #region 件号集合

        /// <summary>
        ///     件号集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        #endregion

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        #endregion

        #region 控制单位集合

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        #endregion

        #region 选择的件号

        private PnRegDTO _selPnReg;

        /// <summary>
        ///     选择的件号
        /// </summary>
        public PnRegDTO SelPnReg
        {
            get { return _selPnReg; }
            private set
            {
                if (_selPnReg != value)
                {
                    _selPnReg = value;
                    if (value != null)
                    {
                        _snFilterDescriptor = new FilterDescriptor("PnRegId", FilterOperator.IsEqualTo, value.Id);
                        SnRegs.FilterDescriptors.Clear();
                        SnRegs.FilterDescriptors.Add(_snFilterDescriptor);
                        SnRegs.Load(true);
                    }
                    RaisePropertyChanged(() => SelPnReg);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的序号

        private SnRegDTO _selSnReg;

        /// <summary>
        ///     选择的序号
        /// </summary>
        public SnRegDTO SelSnReg
        {
            get { return _selSnReg; }
            private set
            {
                if (_selSnReg != value)
                {
                    _selSnReg = value;
                    RaisePropertyChanged(() => SelSnReg);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的装机历史

        private SnHistoryDTO _selSnHistory;

        /// <summary>
        ///     选择的装机历史
        /// </summary>
        public SnHistoryDTO SelSnHistory
        {
            get { return _selSnHistory; }
            private set
            {
                if (_selSnHistory != value)
                {
                    _selSnHistory = value;
                    RaisePropertyChanged(() => SelSnHistory);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的到寿监控

        private LifeMonitorDTO _selLifeMonitor;

        /// <summary>
        ///     选择的到寿监控
        /// </summary>
        public LifeMonitorDTO SelLifeMonitor
        {
            get { return _selLifeMonitor; }
            private set
            {
                if (_selLifeMonitor != value)
                {
                    _selLifeMonitor = value;
                    RaisePropertyChanged(() => SelLifeMonitor);
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
            AddShCommand.RaiseCanExecuteChanged();
            RemoveShCommand.RaiseCanExecuteChanged();
            AddLmCommand.RaiseCanExecuteChanged();
            RemoveLmCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 增加装机历史

        /// <summary>
        ///     增加装机历史
        /// </summary>
        public DelegateCommand<object> AddShCommand { get; private set; }

        private void OnAddSh(object obj)
        {
            var newSnHistory = new SnHistoryDTO()
            {
                Id = RandomHelper.Next(),
            };
            SelSnReg.SnHistories.Add(newSnHistory);
        }

        private bool CanAddSh(object obj)
        {
            return _selSnReg != null;
        }

        #endregion

        #region 移除装机历史

        /// <summary>
        ///     移除装机历史
        /// </summary>
        public DelegateCommand<object> RemoveShCommand { get; private set; }

        private void OnRemoveSh(object obj)
        {
            if (_selSnHistory != null)
            {
                SelSnReg.SnHistories.Remove(_selSnHistory);
            }
        }

        private bool CanRemoveSh(object obj)
        {
            return _selSnHistory != null;
        }

        #endregion

        #region 增加到寿监控

        /// <summary>
        ///     增加到寿监控
        /// </summary>
        public DelegateCommand<object> AddLmCommand { get; private set; }

        private void OnAddLm(object obj)
        {
            var newLifeMonitor = new LifeMonitorDTO()
            {
                Id = RandomHelper.Next(),
            };
            SelSnReg.LiftMonitors.Add(newLifeMonitor);
        }

        private bool CanAddLm(object obj)
        {
            return _selSnReg != null;
        }

        #endregion

        #region 移除到寿监控

        /// <summary>
        ///     移除到寿监控
        /// </summary>
        public DelegateCommand<object> RemoveLmCommand { get; private set; }

        private void OnRemoveLm(object obj)
        {
            if (_selLifeMonitor != null)
            {
                SelSnReg.LiftMonitors.Remove(_selLifeMonitor);
            }
        }

        private bool CanRemoveLm(object obj)
        {
            return _selLifeMonitor != null;
        }

        #endregion
        #endregion
    }
}