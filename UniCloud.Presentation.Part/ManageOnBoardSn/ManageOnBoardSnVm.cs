#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/19 14:08:23
// 文件名：ManageOnBoardSnVm
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/19 14:08:23
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageOnBoardSn
{
    [Export(typeof (ManageOnBoardSnVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageOnBoardSnVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public ManageOnBoardSnVm(IPartService service)
            : base(service)
        {
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
            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);

            SnRegs = _service.CreateCollection(_context.SnRegs);
            var cfd = new CompositeFilterDescriptor {LogicalOperator = FilterCompositionLogicalOperator.Or};
            cfd.FilterDescriptors.Add(new FilterDescriptor("Status", FilterOperator.IsEqualTo, (int) SnStatus.在库));
            cfd.FilterDescriptors.Add(new FilterDescriptor("Status", FilterOperator.IsEqualTo, (int) SnStatus.在修));
            cfd.FilterDescriptors.Add(new FilterDescriptor("Status", FilterOperator.IsEqualTo, (int) SnStatus.出租));
            SnRegs.FilterDescriptors.Add(cfd);
            SnRegs.PageSize = 20;
            _service.RegisterCollectionView(SnRegs);

            SnHistories = _service.CreateCollection(_context.SnHistories);
            _service.RegisterCollectionView(SnHistories);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
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

            if (!SnRegs.AutoLoad)
                SnRegs.AutoLoad = true;
            SnRegs.Load(true);
        }

        #region 业务

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        #region 选择的序号件

        private SnRegDTO _selSnReg;

        /// <summary>
        ///     选择的序号件
        /// </summary>
        public SnRegDTO SelSnReg
        {
            get { return _selSnReg; }
            set
            {
                if (_selSnReg != value)
                {
                    _selSnReg = value;
                    ViewSnHistories.Clear();
                    if (value != null)
                    {
                        var shs = SnHistories.Where(p => p.SnRegId == value.Id).ToList();
                        foreach (var sh in shs)
                        {
                            ViewSnHistories.Add(sh);
                        }
                    }
                    RaisePropertyChanged(() => ViewSnHistories);
                    RaisePropertyChanged(() => SelSnReg);
                }
            }
        }

        #endregion

        #endregion

        #region 装机历史集合

        /// <summary>
        ///     所有的装机历史集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnHistoryDTO> SnHistories { get; set; }

        #region 选择的序号的装机历史记录

        private ObservableCollection<SnHistoryDTO> _viewSnHistories = new ObservableCollection<SnHistoryDTO>();

        /// <summary>
        ///     选择的序号的装机历史记录
        /// </summary>
        public ObservableCollection<SnHistoryDTO> ViewSnHistories
        {
            get { return _viewSnHistories; }
            set
            {
                if (_viewSnHistories != value)
                {
                    _viewSnHistories = value;
                    RaisePropertyChanged(() => ViewSnHistories);
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
            set
            {
                if (_selSnHistory != value)
                {
                    _selSnHistory = value;
                    RaisePropertyChanged(() => SelSnHistory);
                }
            }
        }

        #endregion

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

        #region 创建附件项

        /// <summary>
        ///     创建附件项
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除附件项

        /// <summary>
        ///     删除附件项
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
        }

        private bool CanRemove(object obj)
        {
            return false;
        }

        #endregion

        #endregion
    }
}