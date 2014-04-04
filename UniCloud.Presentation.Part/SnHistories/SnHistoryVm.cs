#region NameSpace

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.SnHistories
{
    [Export(typeof (SnHistoryVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SnHistoryVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;

        [ImportingConstructor]
        public SnHistoryVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVm();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            ApuEngineSnRegWorks = _service.CreateCollection(_context.ApuEngineSnRegs,p=>p.SnHistories);
            _service.RegisterCollectionView(ApuEngineSnRegWorks); //注册查询集合

            Aircrafts = _service.CreateCollection(_context.Aircrafts); //创建飞机集合
            _service.RegisterCollectionView(Aircrafts);
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
            if (!ApuEngineSnRegWorks.AutoLoad)
                ApuEngineSnRegWorks.AutoLoad = true;
            else
                ApuEngineSnRegWorks.Load(true);

            Aircrafts.AutoLoad = true;
        }

        #region Apu、Engine的SnReg

        private ApuEngineSnRegDTO _apuEngineSnRegWork;
        private SnHistoryDTO _selSnHistory;

        /// <summary>
        ///     Apu、Engine,SnReg集合
        /// </summary>
        public QueryableDataServiceCollectionView<ApuEngineSnRegDTO> ApuEngineSnRegWorks { get; set; }

        /// <summary>
        ///     选中的Apu、Engine,SnReg
        /// </summary>
        public ApuEngineSnRegDTO ApuEngineSnRegWork
        {
            get { return _apuEngineSnRegWork; }
            set
            {
                if (value != null && _apuEngineSnRegWork != value)
                {
                    _apuEngineSnRegWork = value;
                    RaisePropertyChanged(() => ApuEngineSnRegWork);
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     选中的SnHistory
        /// </summary>
        public SnHistoryDTO SelSnHistory
        {
            get { return _selSnHistory; }
            set
            {
                _selSnHistory = value;
                RaisePropertyChanged(() => ApuEngineSnRegWork);
                RefreshCommandState();
            }
        }

        #endregion

        #region 飞机

        /// <summary>
        ///     飞机
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

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

        #region 添加拆装历史

        /// <summary>
        ///     添加拆装历史
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            if (ApuEngineSnRegWork == null)
            {
                MessageAlert("提示", "Sn不能为空");
            }
            var snHistory = new SnHistoryDTO
            {
                Id = RandomHelper.Next(),
                InstallDate = DateTime.Now,
                SnRegId = ApuEngineSnRegWork.Id,
            };
            //新增拆装历史
            ApuEngineSnRegWork.SnHistories.Add(snHistory);
        }

        private bool CanNew(object obj)
        {
            return ApuEngineSnRegWork != null;
        }

        #endregion

        #region 删除拆装历史

        /// <summary>
        ///     删除拆装历史
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (ApuEngineSnRegWork != null && SelSnHistory != null)
            {
                ApuEngineSnRegWork.SnHistories.Remove(SelSnHistory);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selSnHistory != null;
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
        }

        #endregion

        #endregion
    }
}