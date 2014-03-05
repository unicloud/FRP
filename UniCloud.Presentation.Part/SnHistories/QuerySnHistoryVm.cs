#region NameSpace

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.SnHistories
{
    [Export(typeof (QuerySnHistoryVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QuerySnHistoryVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;

        [ImportingConstructor]
        public QuerySnHistoryVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            ApuEngineSnRegWorks = _service.CreateCollection(_context.ApuEngineSnRegs, p => p.SnHistories);
            _service.RegisterCollectionView(ApuEngineSnRegWorks); //注册查询集合
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

        #endregion

        #endregion
    }
}