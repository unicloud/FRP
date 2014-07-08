#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.BaseConfigurations
{
    /// <summary>
    ///     基础配置，包括维修工作单元、控制单位的维护
    /// </summary>
    [Export(typeof (BaseConfigurationVm))]
    public class BaseConfigurationVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public BaseConfigurationVm(IPartService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            //创建并注册CollectionView
            MaintainWorks = _service.CreateCollection(_context.MaintainWorks);
            _service.RegisterCollectionView(MaintainWorks);
            MaintainWorks.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = MaintainWorks.CurrentAddItem as MaintainWorkDTO;
                    if (newItem != null)
                    {
                        newItem.Id = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectMaintainWork = !MaintainWorks.HasChanges;
                }
            };

            //创建并注册CollectionView
            CtrlUnits = _service.CreateCollection(_context.CtrlUnits);
            _service.RegisterCollectionView(CtrlUnits);
            CtrlUnits.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = CtrlUnits.CurrentAddItem as CtrlUnitDTO;
                    if (newItem != null)
                    {
                        newItem.Id = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectCtrlUnit = !CtrlUnits.HasChanges;
                }
            };

            //创建并注册CollectionView
            Thresholds = _service.CreateCollection(_context.Thresholds);
            _service.RegisterCollectionView(Thresholds);

            Items = new QueryableDataServiceCollectionView<ItemDTO>(_context, _context.Items);

            //初始化按钮
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 附件项集合

        private ItemDTO _selItem;

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        /// <summary>
        ///     选中的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return _selItem; }
            set
            {
                if (_selItem != value)
                {
                    _selItem = value;
                    if (_selItem != null)
                    {
                        var path = CreatePnRegQueryPath(value.Id);
                        LoadPnRegs(path);
                    }
                    RaisePropertyChanged(() => SelItem);
                }
            }
        }

        #endregion

        #region 附件集合

        private List<PnRegDTO> _pnRegs = new List<PnRegDTO>();
        private PnRegDTO _selPnReg;

        /// <summary>
        ///     附件集合
        /// </summary>
        public List<PnRegDTO> PnRegs
        {
            get { return _pnRegs; }
            set
            {
                if (_pnRegs != value)
                {
                    _pnRegs = value;
                    RaisePropertyChanged(() => PnRegs);
                }
            }
        }

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
                    if (value != null)
                    {
                        var threshold = Thresholds.FirstOrDefault(p => p.PnRegId == value.Id);
                        SelThreshold = threshold;
                    }
                    RaisePropertyChanged(() => SelPnReg);
                    RefreshCommandState();
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!MaintainWorks.AutoLoad)
                MaintainWorks.AutoLoad = true;
            MaintainWorks.Load(true);

            //// 将CollectionView的AutoLoad属性设为True
            if (!CtrlUnits.AutoLoad)
                CtrlUnits.AutoLoad = true;
            CtrlUnits.Load(true);

            //// 将CollectionView的AutoLoad属性设为True
            if (!Thresholds.AutoLoad)
                Thresholds.AutoLoad = true;
            Thresholds.Load(true);

            Items.Load(true);
        }

        #region 工作代码

        private bool _canSelectMaintainWork = true;
        private MaintainWorkDTO _maintainWork;

        /// <summary>
        ///     工作代码集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainWorkDTO> MaintainWorks { get; set; }

        /// <summary>
        ///     选中的工作代码
        /// </summary>
        public MaintainWorkDTO MaintainWork
        {
            get { return _maintainWork; }
            set
            {
                if (value != null && _maintainWork != value)
                {
                    _maintainWork = value;
                    RaisePropertyChanged(() => MaintainWork);
                }
            }
        }

        //用户能否选择
        public bool CanSelectMaintainWork
        {
            get { return _canSelectMaintainWork; }
            set
            {
                if (_canSelectMaintainWork != value)
                {
                    _canSelectMaintainWork = value;
                    RaisePropertyChanged(() => CanSelectMaintainWork);
                }
            }
        }

        #endregion

        #region 控制单位

        private bool _canSelectCtrlUnit = true;
        private CtrlUnitDTO _ctrlUnit;

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        /// <summary>
        ///     选中的控制单位
        /// </summary>
        public CtrlUnitDTO CtrlUnit
        {
            get { return _ctrlUnit; }
            set
            {
                if (value != null && _ctrlUnit != value)
                {
                    _ctrlUnit = value;
                    RaisePropertyChanged(() => CtrlUnit);
                }
            }
        }

        //用户能否选择
        public bool CanSelectCtrlUnit
        {
            get { return _canSelectCtrlUnit; }
            set
            {
                if (_canSelectCtrlUnit != value)
                {
                    _canSelectCtrlUnit = value;
                    RaisePropertyChanged(() => CanSelectCtrlUnit);
                }
            }
        }

        #endregion

        #region 阀值

        private ThresholdDTO _selThreshold;

        /// <summary>
        ///     阀值集合
        /// </summary>
        public QueryableDataServiceCollectionView<ThresholdDTO> Thresholds { get; set; }

        /// <summary>
        ///     选中的阀值
        /// </summary>
        public ThresholdDTO SelThreshold
        {
            get { return _selThreshold; }
            set
            {
                if (_selThreshold != value)
                {
                    _selThreshold = value;
                    RaisePropertyChanged(() => SelThreshold);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #region 创建查询路径

        /// <summary>
        ///     创建查询路径
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private Uri CreatePnRegQueryPath(int itemId)
        {
            return new Uri(string.Format("GetPnRegsByItem?itemId={0}", itemId),
                UriKind.Relative);
        }

        private void LoadPnRegs(Uri path)
        {
            //查询
            _context.BeginExecute<PnRegDTO>(path,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PartData;
                    try
                    {
                        if (context != null)
                        {
                            _pnRegs = new List<PnRegDTO>();
                            _pnRegs = context.EndExecute<PnRegDTO>(result).ToList();
                            SelPnReg = PnRegs.FirstOrDefault();
                            RaisePropertyChanged(() => PnRegs);
                            RefreshCommandState();
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        var response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }

        #endregion

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建阀值配置

        /// <summary>
        ///     创建阀值配置
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var newThreshold = new ThresholdDTO
            {
                Id = RandomHelper.Next(),
                PnRegId = SelPnReg.Id,
                Pn = SelPnReg.Pn,
            };
            Thresholds.AddNew(newThreshold);
            SelThreshold = newThreshold;
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            if (SelPnReg != null)
            {
                var threshold = Thresholds.FirstOrDefault(p => p.PnRegId == SelPnReg.Id);
                return PnRegs.Count != 0 && threshold == null;
            }
            return false;
        }

        #endregion

        #region 删除规划

        /// <summary>
        ///     删除规划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (SelThreshold == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                Thresholds.Remove(SelThreshold);
                SelThreshold = Thresholds.FirstOrDefault();
            });
        }

        private bool CanRemove(object obj)
        {
            return _selThreshold != null;
        }

        #endregion

        #endregion
    }
}